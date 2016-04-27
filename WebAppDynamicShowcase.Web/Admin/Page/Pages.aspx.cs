//
// Attention on doit s'ecarter de BlogEngine.NET pour ne pas utiliser MetaWebLog
// Les images et les fichiers concernant les pages sont dans le rep /Files/
// Et quand on supprime les pages que deviennent ces fichiers ? Bonne question !
// Ni ici, ni dans BlogEngine ces objets ne sont supprimes !
//


using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using PageEngine;

public partial class Admin_Pages : System.Web.UI.Page
{
    private static string fileDirectory = VirtualPathUtility.ToAbsolute( "~/UserFiles/" );

    protected void Page_Load( object sender, EventArgs e )
    {
        base.MaintainScrollPositionOnPostBack = true;

        if ( IsPostBack == false && IsCallback == false )
        {
            if ( Page.User.IsInRole( "Administrateur" ) )
            {
                PanelAdministrateur.Visible = true;
            }

            BindDropDownListAuteur();

            cbIsFrontPage.Text = "Est la page d'accueil"; //Global.LabelsXml.IsFrontPage;
            int editeurHauteur = int.Parse( Global.SettingsXml.EditeurHauteur );
            FCKeditor2.Height = Unit.Pixel( editeurHauteur );
            TextBoxRang.Text = "1";
            TextBoxJoursCommentaires.Text = "0";

            if ( !String.IsNullOrEmpty( Request.QueryString[ "id" ] ) && Request.QueryString[ "id" ].Length == 36 )
            {
                Guid id = new Guid( Request.QueryString[ "id" ] );
                BindPage( id );
                BindParents( id );
            }
            else
            {
                BindParents( Guid.Empty );
                ButtonAnnuler.Enabled = false;
            }

            BindPageList();
        }

        btnUploadFile.Click += new EventHandler( btnUploadFile_Click );
        btnUploadImage.Click += new EventHandler( btnUploadImage_Click );
        Page.Title = "Application Web Administration"; // Resources.labels.pages;
        Page.Form.DefaultButton = ButtonSauver.UniqueID; // Pour donner le focus
    }

    private void BindDropDownListAuteur()
    {
        foreach ( MembershipUser user in Membership.GetAllUsers() )
        {
            DropDownListAuteur.Items.Add( user.UserName );
        }
        DropDownListAuteur.SelectedValue = User.Identity.Name;
    }

    protected void ButtonEditorAugmenter_Click( object sender, EventArgs e )
    {
        int height = int.Parse( Global.SettingsXml.EditeurElargir );
        int pix = ( int )( FCKeditor2.Height.Value );
        FCKeditor2.Height = Unit.Pixel( pix + height ) ;
    }
    
    private void btnUploadImage_Click( object sender, EventArgs e )
    {
        Upload( fileDirectory, txtUploadImage );
        string filePath = fileDirectory + Server.UrlEncode( txtUploadImage.FileName );
        string imgage = string.Format( "<img src=\"{0}\" alt=\"{0}\" />", filePath );

        FCKeditor2.Value += imgage;
    }

    private void btnUploadFile_Click( object sender, EventArgs e )
    {
        Upload( fileDirectory, txtUploadFile );
        string filePath = fileDirectory + Server.UrlEncode( txtUploadFile.FileName );
        string text = txtUploadFile.FileName + " (" + Strings.FileSizeFormat( txtUploadFile.FileBytes.Length, "N" ) + ")";
        string aref = string.Format( "<p><a href=\"{0}\" >{1}</a></p>", filePath, text );

        FCKeditor2.Value += aref;
    }

    private void Upload( string virtualFolder, FileUpload control )
    {
        string folder = Server.MapPath( virtualFolder );
        control.PostedFile.SaveAs( folder + control.FileName );
    }


    #region Event handlers

    protected void ButtonSauver_Click( object sender, EventArgs e )
    {
        if ( !Page.IsValid )
            throw new InvalidOperationException( "Page invalide." );

        PagePost page;
        if ( Request.QueryString[ "id" ] != null )
            page = PagePost.GetPage( new Guid( Request.QueryString[ "id" ] ) );
        else
            page = new PagePost();

        if ( string.IsNullOrEmpty( FCKeditor2.Value ) )
            FCKeditor2.Value = "[Pas de texte]";

        bool rangIsChanged = false;
        try 
        { 
            int rang = int.Parse( TextBoxRang.Text );
            if ( page.Rang != rang )
            {
                // l'Administrateur met les pages ou il veut
                if ( Page.User.IsInRole( "Administrateur" ) )
                {
                    page.Rang = rang;
                }
                else
                {
                    int max = int.Parse( Global.SettingsXml.RangPagesAuteurMax );
                    int min = int.Parse( Global.SettingsXml.RangPagesAuteurMin );
                    if ( rang < min )
                    {
                        rang = min;
                    }
                    if ( rang > max )
                    {
                        rang = max;
                    }

                    page.Rang = rang;
                }
                rangIsChanged = true;
            }
        }
        catch{}

        try
        {
            page.DaysCommentsAreEnable = int.Parse( TextBoxJoursCommentaires.Text );
        }
        catch { }

        page.Author = DropDownListAuteur.SelectedValue;
        page.Menu = TextBoxMenu.Text;
        page.Title = TextBoxTitle.Text.Trim() != string.Empty ? TextBoxTitle.Text.Trim() : TextBoxMenu.Text;
        page.Content = FCKeditor2.Value;
        page.Description = txtDescription.Text;

        if ( page.Keywords != txtKeyword.Text )
        {
            page.Keywords = txtKeyword.Text;
            page.RemakeTags( ref page );
        }

        // Si cette page est choisie comme FrontPage alors desactiver les autres
        if ( cbIsFrontPage.Checked )
        {
            foreach ( PagePost otherPage in PagePost.Pages )
            {
                if ( otherPage.IsFrontPage )
                {
                    otherPage.IsFrontPage = false;
                    otherPage.Save();
                }
            }
        }

        page.IsFrontPage = cbIsFrontPage.Checked;
        page.ShowInList = cbShowInList.Checked;
        page.IsPublished = cbIsPublished.Checked;
        page.IsCommentsEnabled = CheckBoxAutoriserCommentaire.Checked;
        page.IsCommentsVisible = CheckBoxIsCommentVisible.Checked;
        page.IsTitleVisible = CheckBoxTitleIsVisible.Checked;
        page.IsReserved = CheckBoxIsReserved.Checked;

        if ( DropDownListParent.SelectedIndex != 0 )
            page.Parent = new Guid( DropDownListParent.SelectedValue );
        else
            page.Parent = Guid.Empty;

        page.Save();
        if ( rangIsChanged )
        {
            PagePost.Sort();
        }

        if ( ( ( Button )sender ).ID == "ButtonHaut" )
        {
            Response.Redirect( "~/Admin/Page/Pages.aspx?id=" + page.Id.ToString() );
        }
        else
        {
            Response.Redirect( page.RelativeLink.ToString() );
        }
    }

    protected void ButtonAnnuler_Click( object sender, EventArgs e )
    {
        PagePost page = PagePost.GetPage( new Guid( Request.QueryString[ "id" ] ) );
        Response.Redirect( page.RelativeLink.ToString() );
    }

    protected void ButtonNew_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/Admin/Page/Pages.aspx", true );
    }

    #endregion

    #region Data binding

    private void BindPage( Guid pageId )
    {
        PagePost page = PagePost.GetPage( pageId );

        TextBoxMenu.Text = page.Menu;
        TextBoxTitle.Text = page.Title;
        TextBoxRang.Text = page.Rang.ToString();
        TextBoxJoursCommentaires.Text = page.DaysCommentsAreEnable.ToString();
        FCKeditor2.Value = page.Content;
        txtDescription.Text = page.Description;
        txtKeyword.Text = page.Keywords;
        cbIsFrontPage.Checked = page.IsFrontPage;
        cbShowInList.Checked = page.ShowInList;
        cbIsPublished.Checked = page.IsPublished;
        CheckBoxAutoriserCommentaire.Checked = page.IsCommentsEnabled;
        CheckBoxIsCommentVisible.Checked = page.IsCommentsVisible;
        CheckBoxTitleIsVisible.Checked = page.IsTitleVisible;
        CheckBoxIsReserved.Checked = page.IsReserved;

        DropDownListAuteur.SelectedValue = page.Author;
    }

    private void BindParents( Guid pageId )
    {
        foreach ( PagePost page in PagePost.Pages )
        {
            if ( Page.User.IsInRole( "Administrateur" ) || Page.User.Identity.Name == page.Author )
            {
                if ( pageId != page.Id )
                    DropDownListParent.Items.Add( new ListItem( page.Menu, page.Id.ToString() ) );
            }
        }

        DropDownListParent.Items.Insert( 0, "-- Pas de parent --" );
        if ( pageId != Guid.Empty )
        {
            PagePost parent = PagePost.GetPage( pageId );
            if ( parent != null )
                DropDownListParent.SelectedValue = parent.Parent.ToString();
        }
    }

    // Les pages des auteurs apres les page de l'admin
    private int GetAdminPageRangMax()
    {
        int rangMax = 0;
        foreach ( PagePost page in PagePost.Pages )
        {
            if ( page.Author == "admin" )
            {
                rangMax = rangMax < page.Rang ? page.Rang : rangMax;
            }
        }
        return rangMax;
    }

    private void BindPageList()
    {
        int pageAuteur = 0;
        foreach ( PagePost page in PagePost.Pages )
        {
            if ( Page.User.IsInRole( "Administrateur" ) || Page.User.Identity.Name == page.Author )
            {
                HtmlGenericControl li = new HtmlGenericControl( "li" );
                HtmlAnchor a = new HtmlAnchor();
                a.HRef = "?id=" + page.Id.ToString();
                a.InnerHtml = page.Menu;

                string show = page.ShowInList == true ? "1" : "0";
                string published = page.IsPublished == true ? "1" : "0";
                string reserved = page.IsReserved == true ? "1" : "0";
                string parent = page.Parent != Guid.Empty ? PagePost.GetPage( page.Parent ).Menu : "pas de parent";
                string literal = " (" + page.DateCreated.ToString( "yyyy-dd-MM HH:mm" ) + ")" + " reserved : " + reserved + " menu : " + show + " published : " + published + " rang : " + page.Rang.ToString() + " parent : " + parent;
                if ( User.IsInRole( "Administrateur" ) )
                {
                    literal = " auteur : " + page.Author + literal;
                }
                System.Web.UI.LiteralControl text = new System.Web.UI.LiteralControl( literal );

                li.Controls.Add( a );
                li.Controls.Add( text );
                ulPages.Controls.Add( li );

                pageAuteur += 1;
            }
        }

        divPages.Visible = true;
        aPages.InnerHtml = pageAuteur.ToString() + " Pages"; // +PagePost.Pages.Count + " " + "Pages" /*Resources.labels.pages*/;
    }

    #endregion

}
