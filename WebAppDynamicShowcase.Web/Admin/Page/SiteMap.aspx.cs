//
// Entierement dynamique pour un seul fichier c'est un peu luxeux 
// mais cette page vient de StyleSheet.aspx
//
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

public partial class Admin_SiteMap : PageBase
{
    static string DirectorySiteMap = "~/";

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( Page.IsPostBack == false )
        {
            LabelErreurMessage.Text = "";
        }

        BindFichiers();
    }

    protected void BindFichiers()
    {
        string dirName = Server.MapPath( DirectorySiteMap );
        string fileName = dirName + "Web.sitemap";

        if ( Directory.Exists( dirName ) )
        {
            string fichier = "";
            FileStream fs = new FileStream( fileName, FileMode.Open, FileAccess.Read );
            StreamReader sr = new StreamReader( fs );
            fichier = sr.ReadToEnd();
            sr.Close();
            fs.Close();

            string nom = Strings.GetFileName( fileName );

            Button button = new Button();
            button.CssClass = "ButtonStyle";
            button.Text = nom;
            PlaceHolderSiteMap.Controls.Add( button );
            button.Click += new EventHandler( ButtonSauver_Click );
            button.ToolTip = "Sauver le fichier .css";

            Label br = new Label();
            br.CssClass = "LabelStyle";
            br.Text = "<br />";
            PlaceHolderSiteMap.Controls.Add( br );

            TextBox textBox = new TextBox();
            textBox.CssClass = "TextBoxLogStyle";
            int lignes = Strings.Lignes( fichier, "\r\n" );
            textBox.TextMode = TextBoxMode.MultiLine;
            textBox.Rows = lignes < 50 ? lignes : 50;
            textBox.Width = Unit.Percentage( 100 );
            textBox.Text = fichier;
            string[] _nom = nom.Split( '.' );
            textBox.ID = "TextBox" + _nom[ 0 ];
            PlaceHolderSiteMap.Controls.Add( textBox );

            br = new Label();
            br.CssClass = "LabelStyle";
            br.Text = "<br />";
            PlaceHolderSiteMap.Controls.Add( br );
        }
    }

    protected void ButtonSauver_Click( object sender, EventArgs e )
    {
        string dirName = Server.MapPath( DirectorySiteMap );

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

        Control ctrl = Utils.FindControlRecursively( "PlaceHolderSiteMap", Page.Controls );
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
            return;
        }

        LabelErreurMessage.Visible = true;
        LabelErreurMessage.CssClass = "LabelValidationMessageStyle";
        LabelErreurMessage.Text = "Fichier sauvé avec succès.";

        PlaceHolderSiteMap.Controls.Clear();
        BindFichiers();
    }
}
