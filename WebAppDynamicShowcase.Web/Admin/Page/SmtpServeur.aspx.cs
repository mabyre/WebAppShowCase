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
using SmtpServeurXmlProvider;

#endregion

public partial class Admin_Pages_SettingsSmtp : PageBase
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( IsPostBack == false )
        {
            SmtpServeurXml serveur = new SmtpServeurXml();
            if ( serveur != null )
            {
                TextBoxEmailSujet.Text = serveur.SujetEmail;
                TextBoxEmailAdmin.Text = serveur.AdminEmail;
                TextBoxUserName.Text = serveur.UserName;
                TextBoxPassWord.Text = serveur.UserPassWord;
                TextBoxServerName.Text = serveur.ServerName;
                TextBoxServerPort.Text = serveur.ServerPort.ToString();
                TextBoxEmail.Text = serveur.UserEmail;
                CheckBoxEnableSSL.Checked = serveur.EnableSSL;
            }
            else
            {
                TextBoxServerPort.Text = "25";
            }
        }

        Page.MaintainScrollPositionOnPostBack = true;
    }

    protected void ButtonTestSmtp_Click( object sender, EventArgs e )
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress( TextBoxEmail.Text );
            mail.To.Add( new MailAddress( TextBoxEmailAdmin.Text ) );
            mail.Subject = TextBoxEmailSujet.Text;
            mail.Body = "Message de test"; 
            mail.IsBodyHtml = true;
            
            SmtpClient smtp = new SmtpClient( TextBoxServerName.Text );
            smtp.Credentials = new System.Net.NetworkCredential( TextBoxUserName.Text, TextBoxPassWord.Text );
            smtp.EnableSsl = CheckBoxEnableSSL.Checked;
            smtp.Port = int.Parse( TextBoxServerPort.Text );
            smtp.Send( mail );
            LabelSmtpStatus.Text = "Test réussi.";
            LabelSmtpStatus.Style.Add( HtmlTextWriterStyle.Color, "green" );
        }
        catch
        {
            LabelSmtpStatus.Text = "Connexion impossible";
            LabelSmtpStatus.Style.Add( HtmlTextWriterStyle.Color, "red" );
        }
    }

    public void ButtonSave_OnClick( object sender, EventArgs e )
    {
        SmtpServeurXml serveur = new SmtpServeurXml();

        serveur.SujetEmail = TextBoxEmailSujet.Text;
        serveur.AdminEmail = TextBoxEmailAdmin.Text;
        serveur.UserName = TextBoxUserName.Text;
        serveur.UserPassWord = TextBoxPassWord.Text;
        serveur.ServerName = TextBoxServerName.Text;
        serveur.ServerPort = int.Parse( TextBoxServerPort.Text );
        serveur.UserEmail = TextBoxEmail.Text;
        serveur.EnableSSL = CheckBoxEnableSSL.Checked;
        serveur.Save();

        Response.Redirect( Request.RawUrl, true );
    }
}