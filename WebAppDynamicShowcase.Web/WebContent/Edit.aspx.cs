using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebContentData;

public partial class WebContent_Edit : PageBase
{
    private static string BaseFileDirectory = VirtualPathUtility.ToAbsolute( "~/UserFiles/" );

    private void Page_Load( object sender, System.EventArgs e )
    {
        if ( IsPostBack == false )
        {
            if ( WebContent.CanEdit() == false )
            {
                string msg = "Vous n'avez pas les droits pour éditer ce contenu.";
                Response.Redirect( Tools.PageErreurPath + msg );
            }

            if ( Request.QueryString[ "sectionname" ] != null )
            {
                string section = Request.QueryString[ "sectionname" ].ToString();
                SessionState.WebContent = WebContent.GetWebContent( section );
                if ( SessionState.WebContent == null )
                {
                    FCKeditor1.Value = "<p></p>";
                }
                else
                {
                    LableSection.Text = SessionState.WebContent.Section;
                    FCKeditor1.Value = SessionState.WebContent.SectionContent;
                }
            }
            
            if ( Request.QueryString[ "id" ] != null )
            {
                // Edition d'une section
                Guid webContentID = new Guid( Request.QueryString[ "id" ] );
                SessionState.WebContent = WebContent.GetWebContent( webContentID );
                if ( SessionState.WebContent == null )
                {
                    FCKeditor1.Value = "<p></p>";
                }
                else
                {
                    LableSection.Text = SessionState.WebContent.Section;
                    FCKeditor1.Value = SessionState.WebContent.SectionContent;
                }
            }
        }

        MessageValider();
    }

    void MessageValider()
    {
        if ( SessionState.ValidationMessage != null )
        {
            ValidationMessage.Text = SessionState.ValidationMessage;
            ValidationMessage.Visible = true;
            SessionState.ValidationMessage = null;
        }
        else
        {
            ValidationMessage.Visible = false;
        }
    }

    protected void ButtonSauver_Click( object sender, System.EventArgs e )
    {
        if ( WebContent.CanEdit() == false )
        {
            SessionState.ValidationMessage += "Vous n'avez pas les droits pour éditer cette section.<br/>";
        }

        if ( SessionState.ValidationMessage != null )
        {
            Response.Redirect( Request.RawUrl );
        }

        string newContenu = "";
        string oldContenu = "";
        if ( FCKeditor1.Value == "" )
        {
            newContenu = "<p></p>";
        }
        else
        {
            newContenu = FCKeditor1.Value;
        }

        oldContenu = SessionState.WebContent.SectionContent;
        SessionState.WebContent.SectionContent = newContenu;
        SessionState.WebContent.Update();

        // Update
        string adrIP = Request.ServerVariables[ "REMOTE_ADDR" ];
        if ( Global.SettingsXml.EnvoyerMiseAjour == true )
        {
            //Courriel.EnvoyerMiseAJour( oldContenu, newContenu, "Mise à jour", SessionState.WebContent.Section, adrIP );
        }

        if ( Request[ "ReturnURL" ] != null )
        {
            Response.Redirect( Request[ "ReturnURL" ].ToString() );
        }
        else
        {
            Response.Redirect( "~/WebContent/Manage.aspx" );
        }
    }

    protected void ButtonUploadImage_Click( object sender, EventArgs e )
    {
        if ( txtUploadImage.FileName == "" )
        {
            SessionState.ValidationMessage = "Choisir une image";
            Server.Transfer( Request.RawUrl );
        }
        else
        {
            string filePath = Upload( txtUploadImage );

            // Pour que l'image s'affiche dans un email envoye
            if ( SessionState.WebContent.Section == "CorpsEmail" )
            {
                filePath = "http://" + Request.Url.Authority + filePath;
            }

            string imgage = string.Format( "<img src=\"{0}\" alt=\"{1}\" />", filePath, txtUploadImage.FileName );

            FCKeditor1.Value += imgage;
        }
    }

    protected void ButtonUploadFile_Click( object sender, EventArgs e )
    {
        if ( txtUploadFile.FileName == "" )
        {
            SessionState.ValidationMessage = "Choisir un fichier";
            Server.Transfer( Request.RawUrl );
        }
        else
        {
            string filePath = Upload( txtUploadFile );
            string text = txtUploadFile.FileName + " (" + SizeFormat( txtUploadFile.FileBytes.Length, "N" ) + ")";
            string aref = string.Format( "<p><a href=\"{0}\" >{1}</a></p>", filePath, text );

            FCKeditor1.Value += aref;
        }
    }

    private string Upload( FileUpload control )
    {
        if ( SessionState.WebContent == null )
        {
            SessionState.ValidationMessage += "La Page n'est pas encore créée.<br/>";
            SessionState.ValidationMessage += "Vous devez d'abord créer la page avant de télécharger un fichier.<br/>";
            Server.Transfer( Request.RawUrl );
        }

        string dir = BaseFileDirectory + SessionState.WebContent.Section
            //+ "/" + SessionState.WebContent.Utilisateur
            //+ "/" + SessionState.WebContent.Visualisateur
            + "/";

        string directory = Server.MapPath( dir );
        if ( !Directory.Exists( directory ) )
        {
            try
            {
                Directory.CreateDirectory( directory );
            }
            catch ( Exception ex )
            {
                string msg = "Problème avec la création du répertoire dans WebContent/Edit.aspx.<br/>";
                msg += "Erreur : " + ex.Message;
                Response.Redirect( Tools.PageErreurPath + msg );
            }
        }

        control.PostedFile.SaveAs( directory + control.FileName );
        return dir + control.FileName;
    }

    private string SizeFormat( float size, string formatString )
    {
        if ( size < 1024 )
            return size.ToString( formatString ) + " bytes";

        if ( size < Math.Pow( 1024, 2 ) )
            return ( size / 1024 ).ToString( formatString ) + " kb";

        if ( size < Math.Pow( 1024, 3 ) )
            return ( size / Math.Pow( 1024, 2 ) ).ToString( formatString ) + " mb";

        if ( size < Math.Pow( 1024, 4 ) )
            return ( size / Math.Pow( 1024, 3 ) ).ToString( formatString ) + " gb";

        return size.ToString( formatString );
    }
}
