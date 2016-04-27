using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using Fichiers;

public partial class Admin_StyleSheet : PageBase
{
    static string DirectoryCss = "~/App_Themes/Sodevlog/";
    static string DirectoryImagesCss = "~/App_Themes/Sodevlog/Images/";

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( Page.IsPostBack == false )
        {
            LabelErreurMessage.Text = "";
            ButtonDir.ToolTip = "Lister le répertoire : " + DirectoryImagesCss;
        }

        BindFichiers();
        TailleDesFichiers();
    }

    // Calculer la taille de tous les fichiers dans DirectoryCss
    protected void TailleDesFichiers()
    {
        string directory = Server.MapPath( VirtualPathUtility.ToAbsolute( DirectoryCss ) );
        long size = RepertoireInfo.GetTaille( directory );
        LabelTailleUserFiles.Text = "Taille des fichiers : " + Strings.FileSizeFormat( size, "N" );
    }

    protected void BindFichiers()
    {
        string dirName = Server.MapPath( DirectoryCss );

        if ( Directory.Exists( dirName ) )
        {
            FichierCollection fichiers = FichierCollection.GetAll( dirName );
            foreach ( Fichier f in fichiers )
            {
                string fichier = "";
                FileStream fs = new FileStream( f.Nom, FileMode.Open, FileAccess.Read );
                StreamReader sr = new StreamReader( fs );
                fichier = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                string nom = Strings.GetFileName( f.Nom );

                Button button = new Button();
                button.CssClass = "ButtonStyle";
                button.Text = nom;
                PlaceHolderCss.Controls.Add( button );
                button.Click += new EventHandler( ButtonSauver_Click );
                button.ToolTip = "Sauver le fichier .css";

                Label br = new Label();
                br.CssClass = "LabelStyle";
                br.Text = "<br />";
                PlaceHolderCss.Controls.Add( br );

                TextBox textBox = new TextBox();
                textBox.CssClass = "TextBoxLogStyle";
                int lignes = Strings.Lignes( fichier, "\r\n" );
                textBox.TextMode = TextBoxMode.MultiLine;
                textBox.Rows = lignes < 50 ? lignes : 50;
                textBox.Width = Unit.Pixel( 880 );
                textBox.Text = fichier;
                string[] _nom = nom.Split( '.' );
                textBox.ID = "TextBox" + _nom[ 0 ];
                PlaceHolderCss.Controls.Add( textBox );

                br = new Label();
                br.CssClass = "LabelStyle";
                br.Text = "<br />";
                PlaceHolderCss.Controls.Add( br );
            }
        }
    }

    public void ButtonUploadImage_Click( object sender, EventArgs e )
    {
        string folder = Server.MapPath( DirectoryImagesCss );
        FileUploadImage.PostedFile.SaveAs( folder + FileUploadImage.FileName );
    }

    protected void ButtonSauver_Click( object sender, EventArgs e )
    {
        string dirName = Server.MapPath( DirectoryCss );

        Button button = ( Button )sender;
        string file = button.Text.Trim();
        file = dirName + file;
        if ( File.Exists( file ) == false )
        {
            LabelErreurMessage.Visible = true;
            LabelErreurMessage.CssClass = "LabelValidationMessageErrorStyle";
            LabelErreurMessage.Text = "Le fichier n'existe pas.";
            return;
        }

        Control ctrl = Utils.FindControlRecursively( "PlaceHolderCss", Page.Controls );
        PlaceHolder phld = ( PlaceHolder )ctrl;
        string[] _nom = button.Text.Trim().Split( '.' );
        string textBoxID = "TextBox" + _nom[ 0 ];
        TextBox textBox = ( TextBox )phld.FindControl( textBoxID );

        try
        {
            FileStream fs = new FileStream( file, FileMode.Truncate, FileAccess.Write );
            StreamWriter sw = new StreamWriter( fs );
            sw.Write( textBox.Text );
            sw.Close();
            fs.Close();
        }
        catch ( Exception ex )
        {
            LabelErreurMessage.Visible = true;
            LabelErreurMessage.CssClass = "LabelValidationMessageErrorStyle";
            LabelErreurMessage.Text = ex.Message;
            BindFichiers();
            TailleDesFichiers();
            return;
        }

        LabelErreurMessage.Visible = true;
        LabelErreurMessage.CssClass = "LabelValidationMessageStyle";
        LabelErreurMessage.Text = "Fichier sauvé avec succès.";

        PlaceHolderCss.Controls.Clear(); 
        BindFichiers();
        TailleDesFichiers();
    }

    protected void ButtonDir_Click( object sender, EventArgs e )
    {
        LabelMessage.Text = "";
        string dirName = Server.MapPath( DirectoryImagesCss );
        if ( Directory.Exists( dirName ) )
        {
            FichierCollection fichiers = FichierCollection.GetAll( dirName );
            if ( fichiers.Count == 0 )
            {
                LabelMessage.Text += "Pas de fichier.<br />";
            }
            foreach ( Fichier f in fichiers )
            {
                LabelMessage.Text += Strings.GetFileName( f.Nom ) + "<br />";
            }
        }
        LabelMessage.Visible = true;
        LabelMessage.CssClass = "LabelValidationMessageStyle";
    }

    protected void ButtonSupprimer_Click( object sender, EventArgs e )
    {
        string dirName = Server.MapPath( DirectoryImagesCss );

        if ( TextBoxFichier.Text.Trim() == "" )
        {
            LabelErreurMessage.Visible = true;
            LabelErreurMessage.CssClass = "LabelValidationMessageErrorStyle";
            LabelErreurMessage.Text = "Donner un fichier à supprimer.";
            return;
        }

        string file = TextBoxFichier.Text.Trim();
        file = dirName + file;
        if ( File.Exists( file ) == false  )
        {
            LabelErreurMessage.Visible = true;
            LabelErreurMessage.CssClass = "LabelValidationMessageErrorStyle";
            LabelErreurMessage.Text = "Le fichier n'existe pas.";
            return;
        }

        File.Delete( file );
        LabelErreurMessage.Visible = true;
        LabelErreurMessage.CssClass = "LabelValidationMessageStyle";
        LabelErreurMessage.Text = "Fichier supprimé avec succès.";

        BindFichiers();
        TailleDesFichiers();
    }
}
