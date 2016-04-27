#region Using

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using SettingXmlProvider;

#endregion

public partial class Admin_Pages_Settings : PageBase
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            BindSettings();

            // Informations en lecture uniquement
            string directory = Server.MapPath( VirtualPathUtility.ToAbsolute( "~/UserFiles" ) );
            long size = RepertoireInfo.GetTaille( directory );
            LabelTailleUserFiles.Text = Strings.FileSizeFormat( size, "N" );

            LabelUtilisateursConnecte.Text = Application[ "ActiveUsers" ].ToString();
        }

        Page.MaintainScrollPositionOnPostBack = true;
    }


    private void BindSettings()
    {
        // Parametres
        TextBoxVirtualPath.Text = Global.SettingsXml.VirtualPath;
        CheckBoxLogUser.Checked = Global.SettingsXml.LogUser;
        TextBoxSujetMiseAJour.Text = Global.SettingsXml.SujetCourrielMaj;

        // Parametres Membre
        CheckBoxMembreVerification.Checked = Global.SettingsXml.MembreVerification;
        CheckBoxNouveauMembrePrevenir.Checked = Global.SettingsXml.MembrePrevenir;
        CheckBoxConnexionMembrePrevenir.Checked = Global.SettingsXml.MembreConnexionPrevenir;

        // Editeur HTML
        TextBoxEditeurHauteur.Text = Global.SettingsXml.EditeurHauteur;
        TextBoxEditeurElargir.Text = Global.SettingsXml.EditeurElargir;

        RadioButtonListAvatar.SelectedValue = Global.SettingsXml.Avatar;

        // Parametres Graphiques
        CheckBoxColonneDroiteVisible.Checked = Global.SettingsXml.ColonneDroiteVisible;
        CheckBoxEnpiedSiteVisible.Checked = Global.SettingsXml.EnpiedSiteVisible;

        // Entete Html
        TextBoxHeadHtml.Text = Global.SettingsXml.HtmlHeader;

        // Maitriser le rang des pages d'auteurs
        TextBoxPagesAuteurMin.Text = Global.SettingsXml.RangPagesAuteurMin;
        TextBoxPagesAuteurMax.Text = Global.SettingsXml.RangPagesAuteurMax;
    }

    public void ButtonSave_OnClick( object sender, EventArgs e )
    {
        SettingXml sxml = new SettingXml();

        sxml.EditeurHauteur = TextBoxEditeurHauteur.Text;
        sxml.EditeurElargir = TextBoxEditeurElargir.Text;

        sxml.VirtualPath = TextBoxVirtualPath.Text;
        sxml.SujetCourrielMaj = TextBoxSujetMiseAJour.Text;
        sxml.LogUser = CheckBoxLogUser.Checked;

        sxml.MembreVerification = CheckBoxMembreVerification.Checked;
        sxml.MembrePrevenir = CheckBoxNouveauMembrePrevenir.Checked;
        sxml.MembreConnexionPrevenir = CheckBoxConnexionMembrePrevenir.Checked;

        sxml.Avatar = RadioButtonListAvatar.SelectedValue;

        sxml.ColonneDroiteVisible = CheckBoxColonneDroiteVisible.Checked;
        sxml.EnpiedSiteVisible = CheckBoxEnpiedSiteVisible.Checked;

        sxml.HtmlHeader = TextBoxHeadHtml.Text;

        sxml.RangPagesAuteurMax = TextBoxPagesAuteurMax.Text;
        sxml.RangPagesAuteurMin = TextBoxPagesAuteurMin.Text;

        sxml.Save( sxml );

        Global.SettingsXml = sxml.Reload();
        Response.Redirect( Request.RawUrl, true );
    }
}