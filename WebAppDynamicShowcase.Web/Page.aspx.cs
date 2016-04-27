#region Using

using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using PageEngine;

#endregion

public partial class Default_Page : PageBase //System.Web.UI.Page //: BlogEngine.Core.Web.Controls.BlogBasePage
{
    public bool AdminLinksIsVisible()
    {
        return Page.User.IsInRole( "Administrateur" ) || this.PagePost.Author == System.Threading.Thread.CurrentPrincipal.Identity.Name;
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( Request.QueryString[ "deletepage" ] != null && Request.QueryString[ "deletepage" ].Length == 36 )
        {
            DeletePage( new Guid( Request.QueryString[ "deletepage" ] ) );
        }

        if ( Request.QueryString[ "id" ] != null && Request.QueryString[ "id" ].Length == 36 )
        {
            ServePage( new Guid( Request.QueryString[ "id" ] ) );
        }
        else
        {
            Response.Redirect( "~/" );
        }

        if ( PagePost.IsCommentsVisible == false && User.IsInRole( "Administrateur" ) == false )
        {
            PanelCommentairesVisibles.Visible = false;
        }

        CommentView1.Post = PagePost;
        PanelCommentaires.Visible = SessionState.ButtonCommentaires;
        if ( CommentView1.Post.Comments.Count >= 0 )
        {
            ButtonCommentaires.Text = "Commentaires (" + CommentView1.Post.Comments.Count.ToString() + ")";
        }
    }

    /// <summary>
    /// Serves the page to the containing DIV tag on the page.
    /// </summary>
    /// <param name="id">The id of the page to serve.</param>
    private void ServePage( Guid id )
    {
        this.PagePost = PagePost.GetPage( id );

        if ( this.PagePost == null )
            Response.Redirect( Tools.PageErreurPath + "Page de page", true );

        if ( this.PagePost.IsReserved == true && User.Identity.IsAuthenticated == false )
            Response.Redirect( Tools.PageErreurPath + "Cette page est réservée aux membres enregistrés", true );

        if ( this.PagePost.IsPublished == false )
            Response.Redirect( Tools.PageErreurPath + "La page :\"" + this.PagePost.Title + "\" n'est pas encore publiée", true );

        // Ajouter le Titre a la page
        if ( this.PagePost.IsTitleVisible )
            h1Title.InnerHtml = this.PagePost.Title;

        ServingEventArgs arg = new ServingEventArgs( this.PagePost.Content, ServingLocation.SinglePage );
        PagePost.OnServing( this.PagePost, arg );

        if ( arg.Cancel )
            Response.Redirect( Tools.PageErreurPath + "arg.Cancel", true );

        if ( arg.Body.ToLowerInvariant().Contains( "[usercontrol" ) ) // zarbi !
        {
            InjectUserControls( arg.Body );
        }
        else
        {
            divText.InnerHtml = arg.Body;
        }

        AddMetaTags();
    }

    /// <summary>
    /// Adds the meta tags and title to the HTML header.
    /// </summary>
    private void AddMetaTags()
    {
        if ( this.Page != null )
        {
            base.Title = Server.HtmlEncode( this.PagePost.Title );
            base.AddMetaTag( "description", this.PagePost.Description );
            base.AddMetaTag( "keywords", this.PagePost.Keywords );
        }
    }

    /// <summary>
    /// Deletes the page.
    /// </summary>
    private void DeletePage( Guid id )
    {
        if ( System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated )
        {
            PagePost page = PagePost.GetPage( id );
            page.Delete();
            page.Save();
            Response.Redirect( "~/", true );
        }
    }

    private static readonly Regex _BodyRegex = new Regex( @"\[UserControl:(.*?)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase );

    /// <summary>
    /// Injects any user controls if one is referenced in the text.
    /// </summary>
    private void InjectUserControls( string content )
    {
        int currentPosition = 0;
        MatchCollection myMatches = _BodyRegex.Matches( content );

        foreach ( Match myMatch in myMatches )
        {
            if ( myMatch.Index > currentPosition )
            {
                divText.Controls.Add( new LiteralControl( content.Substring( currentPosition, myMatch.Index - currentPosition ) ) );
            }

            try
            {
                divText.Controls.Add( LoadControl( myMatch.Groups[ 1 ].Value ) );
            }
            catch ( Exception )
            {
                divText.Controls.Add( new LiteralControl( "ERROR - UNABLE TO LOAD CONTROL : " + myMatch.Groups[ 1 ].Value ) );
            }

            currentPosition = myMatch.Index + myMatch.Groups[ 0 ].Length;
        }

        // Finally we add any trailing static text.
        divText.Controls.Add( new LiteralControl( content.Substring( currentPosition, content.Length - currentPosition ) ) );
    }


    /// <summary>
    /// The Page instance to render on the page.
    /// </summary>
    public PagePost PagePost;

    /// <summary>
    /// Gets the admin links to edit and delete a page.
    /// </summary>
    /// <value>The admin links.</value>
    public string AdminLinks
    {
        get
        {
            if ( System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated && AdminLinksIsVisible() )
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine( "<div id=\"admin\">" );
                sb.AppendFormat( "<a href=\"{0}admin/page/pages.aspx?id={1}\" class=\"ButtonStyle\">{2}</a> | ", Utils.RelativeWebRoot, this.PagePost.Id.ToString(), "Editer" /*Resources.labels.edit*/ );
                sb.AppendFormat( "<a href=\"?deletepage={0}\" class=\"ButtonStyle\" onclick=\"return confirm('Vous êtes sûr de vouloir suppimer cette page ?')\">{1}</a>", this.PagePost.Id.ToString(), "Supprimer" /*Resources.labels.delete*/ );
                sb.AppendLine( "</div>" );
                return sb.ToString();
            }

            return string.Empty;
        }
    }

    protected void ButtonCommentaires_Click( object sender, EventArgs e )
    {
        SessionState.ButtonCommentaires = SessionState.ButtonCommentaires == false;
        PanelCommentaires.Visible = SessionState.ButtonCommentaires;
    }
}