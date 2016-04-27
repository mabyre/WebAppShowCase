using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Data;
using System.Configuration;
using SmtpServeurXmlProvider;

/// <summary>
/// Description résumée de Courriel
/// </summary>
public class Courriel
{
	public Courriel()
	{
	}

    public static string EnvoyerEmailAdministrateur
    ( 
        string body
    )
    {
        string status = "";
        SmtpServeurXml smtpServer = new SmtpServeurXml();
        if ( smtpServer == null )
        {
            string msg = "Pas de serveur SMTP configuré pour cette application.";
            HttpContext.Current.Response.Redirect( Tools.PageErreurPath + msg );
        }

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress( smtpServer.UserEmail );
            mail.To.Add( smtpServer.AdminEmail );
            mail.Subject = smtpServer.SujetEmail;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient( smtpServer.ServerName );
            smtp.Credentials = new System.Net.NetworkCredential( smtpServer.UserName, smtpServer.UserPassWord );
            smtp.EnableSsl = smtpServer.EnableSSL;
            smtp.Port = smtpServer.ServerPort;
            smtp.Send( mail );
            status = "Message envoyé";
        }
        catch
        {
            status = "Connexion impossible";
        }

        return status;
    }

    public static string EnvoyerEmail
    (
        MailAddress mailTo,
        string body,
        string sujet
    )
    {
        string status = "";
        SmtpServeurXml smtpServer = new SmtpServeurXml();
        if ( smtpServer == null )
        {
            string msg = "Pas de serveur SMTP configuré pour cette application.";
            HttpContext.Current.Response.Redirect( Tools.PageErreurPath + msg );
        }

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress( smtpServer.UserEmail, smtpServer.UserName );
            mail.To.Add( mailTo );
            mail.Subject = sujet;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient( smtpServer.ServerName );
            smtp.Credentials = new System.Net.NetworkCredential( smtpServer.UserName, smtpServer.UserPassWord );
            smtp.EnableSsl = smtpServer.EnableSSL;
            smtp.Port = smtpServer.ServerPort;
            smtp.Send( mail );
            status = "Message envoyé";
        }
        catch
        {
            status = "Connexion impossible";
        }

        return status;
    }

    public static void EnvoyerEmailToAdminAssynchrone
    (
        string sujet,
        string body
    )
    {
        SmtpServeurXml smtpServer = new SmtpServeurXml();
        if ( smtpServer == null )
        {
            string msg = "Pas de serveur SMTP configuré pour cette application.";
            HttpContext.Current.Response.Redirect( Tools.PageErreurPath + msg );
        }

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress( smtpServer.UserEmail, smtpServer.UserName );
        mail.To.Add( smtpServer.AdminEmail );
        mail.Subject = sujet;
        mail.Body = body;
        mail.IsBodyHtml = true;

        SendMailMessageAsync( mail );
    }

    public static string EnvoyerMiseAJour( string original, string nouveau, string sujet, string objet, string IPAddress )
    {
        string status = "";
        SmtpServeurXml smtpServer = new SmtpServeurXml();
        if ( smtpServer == null )
        {
            string msg = "Pas de serveur SMTP configuré pour cette application.";
            HttpContext.Current.Response.Redirect( Tools.PageErreurPath + msg );
        }

        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress( smtpServer.UserEmail, smtpServer.UserName );
            mail.To.Add( Global.SettingsXml.AdresseWebmaster );
            mail.Subject = Global.SettingsXml.SujetCourrielMaj + " - " + sujet;

            mail.Body += "Date : " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            mail.Body += "<br />";
            mail.Body += "Section : " + objet;
            mail.Body += "<br />";
            mail.Body += "IP Address : " + IPAddress;
            mail.Body += "<br />";
            mail.Body += "Contenu original : " + original;
            mail.Body += "<br />";
            mail.Body += "Nouveau contenu : " + nouveau;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient( smtpServer.ServerName );
            smtp.Credentials = new System.Net.NetworkCredential( smtpServer.UserName, smtpServer.UserPassWord );
            smtp.EnableSsl = smtpServer.EnableSSL;
            smtp.Port = smtpServer.ServerPort;
            smtp.Send( mail );
            status = "Message envoyé";
        }
        catch
        {
            status = "Connexion impossible";
        }

        return status;
    }

    #region Send Email Assynchrones

    /// <summary>
    /// Sends a MailMessage object using the SMTP settings.
    /// </summary>
    ///  Si on utilise delegate pour lancer ca dans une thread HttpContext.Current n'existe pas !!!
    ///
    public static void SendMailMessage( MailMessage mail, SmtpServeurXml serveur )
    {
        if ( mail == null )
            throw new ArgumentNullException( "mail" );

        try
        {
            mail.BodyEncoding = Encoding.UTF8;

            SmtpClient smtp = new SmtpClient( serveur.ServerName );
            smtp.Credentials = new System.Net.NetworkCredential( serveur.UserName, serveur.UserPassWord );
            smtp.EnableSsl = serveur.EnableSSL;
            smtp.Port = serveur.ServerPort;
            smtp.Send( mail );

            OnEmailSent( mail );
        }
        catch ( SmtpException )
        {
            OnEmailFailed( mail );
        }
        finally
        {
            // Remove the pointer to the message object so the GC can close the thread.
            mail.Dispose();
            mail = null;
        }
    }

    /// <summary>
    /// Sends the mail message asynchronously in another thread.
    /// </summary>
    /// <param name="message">The message to send.</param>
    public static void SendMailMessageAsync( MailMessage message )
    {
        // Il faut instancier SmtpServeurXml avant de faire le delegate
        SmtpServeurXml serveur = new SmtpServeurXml();
        ThreadPool.QueueUserWorkItem( delegate { Courriel.SendMailMessage( message, serveur ); } );
    }

    /// <summary>
    /// Occurs after an e-mail has been sent. The sender is the MailMessage object.
    /// </summary>
    public static event EventHandler<EventArgs> EmailSent;
    private static void OnEmailSent( MailMessage message )
    {
        if ( EmailSent != null )
        {
            EmailSent( message, new EventArgs() );
        }
    }

    /// <summary>
    /// Occurs after an e-mail has been sent. The sender is the MailMessage object.
    /// </summary>
    public static event EventHandler<EventArgs> EmailFailed;
    private static void OnEmailFailed( MailMessage message )
    {
        if ( EmailFailed != null )
        {
            EmailFailed( message, new EventArgs() );
        }
    }

    #endregion
}
