using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using SmtpServeurXmlProvider;
using MemberInfoData;

public partial class Member_Email : System.Web.UI.Page
{
    //static string strPath = Server.MapPath( "~\\Files" ) + "\\";
    static string FilesDirectory = "~\\Files" + "\\";

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !Page.IsPostBack )
        {
            chkRecipients.DataSource = Roles.GetAllRoles();
            chkRecipients.DataBind();
        }

    }

    protected void btnSend_Click( object sender, System.EventArgs e )
    {
        bool recipientChecked = false;
        foreach ( ListItem item in chkRecipients.Items )
        {
            if ( item.Selected )
            {
                recipientChecked = true;
                break; // TODO: might not be correct. Was : Exit For
            }
        }

        if ( recipientChecked == false )
        {
            if ( chkAllMembers.Checked == false )
            {
                lblError.Text = "No recipients selected.";
                lblError.Visible = true;
                return;
            }
        }
        else
        {
            lblError.Text = "";
            lblError.Visible = false;
        }

        if ( string.IsNullOrEmpty( AttachmentFile.FileName ) || AttachmentFile.PostedFile == null )
        {
        }
        else
        {
            fileUpload_Click( this, System.EventArgs.Empty );
        }

        bool haveAttachment = false;
        string[] strFiles = null;

        if ( txtAttachments.Text.Length > 0 )
        {
            strFiles = txtAttachments.Text.Split( ';' );
            haveAttachment = true;
        }

        MailAddressCollection ToAddresses = GetRecipients();

        MembershipUser mem = Membership.GetUser();
        Guid gui = new Guid( mem.ProviderUserKey.ToString() );
        string name = GetName( gui );
        MailAddress msgFrom = new MailAddress( mem.Email.ToString(), name.ToString() );
        MailMessage msgMail = new MailMessage( msgFrom, msgFrom );

        foreach ( MailAddress msgTo in ToAddresses )
        {
            msgMail.To.Add( msgTo );
        }

        msgMail.Subject = txtSubject.Text;

        msgMail.IsBodyHtml = true;
        msgMail.Body = FCKeditor1.Value;

        if ( haveAttachment )
        {
            foreach ( string str in strFiles )
            {
                if ( str == "" ) continue;
                msgMail.Attachments.Add( new Attachment( Server.MapPath( FilesDirectory ) + str ) );
            }
        }
        SmtpServeurXml serveur = new SmtpServeurXml();

        SmtpClient smtp = new SmtpClient( serveur.ServerName );
        System.Net.NetworkCredential a = new System.Net.NetworkCredential( serveur.UserEmail, serveur.UserPassWord );
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = a;
        smtp.Send( msgMail );
        Response.Redirect( "~/default.aspx" );
    }

    protected void fileUpload_Click( object sender, System.EventArgs e )
    {
        if ( string.IsNullOrEmpty( AttachmentFile.FileName ) || AttachmentFile.PostedFile == null )
        {
            return;
        }
        else
        {
            string strFileName = AttachmentFile.FileName;
            string c = System.IO.Path.GetFileName( strFileName );

            try
            {

                txtAttachments.Visible = true;
                AttachmentFile.PostedFile.SaveAs( Server.MapPath( FilesDirectory ) + c );
                //txtAttachments.Text;  // ERROR: Unknown assignment operator ConcatString
            }
            catch ( Exception Exp )
            {
                throw Exp;
            }
        }
    }

    private string GetName( Guid userid )
    {
        MemberInfo membre = MemberInfo.GetMemberInfo( userid );
        return membre.Prenom + " " + membre.Nom;
        //return ClubStarterKit.Web.Members.FirstAndLastName( userid );
    }

    protected MailAddressCollection GetRecipients()
    {
        MailAddressCollection retval = new MailAddressCollection();
        if ( chkAllMembers.Checked )
        {
            foreach ( MembershipUser mem in Membership.GetAllUsers() )
            {
                Guid gui = new Guid( mem.ProviderUserKey.ToString() );
                string name = GetName( gui );
                if ( name == "" )
                {
                    name = "-";
                }
                MailAddress address = new MailAddress( mem.Email, name );
                MembershipUser user = Membership.GetUser( gui );

                MemberInfo membre = MemberInfo.GetMemberInfo( gui );
                //if (MemInfo.IsLoaded == true && (bool)MemInfo.Newsletter == true)
                //{
                retval.Add( membre.Adresse );
                //}
            }
        }
        else
        {
            List<string> selectedRoles = new List<string>();
            foreach ( ListItem item in chkRecipients.Items )
            {
                if ( item.Selected )
                {
                    selectedRoles.Add( item.Text );
                }
            }
            ArrayList usernames = new ArrayList();

            string[] strUsernames;
            foreach ( string selRole in selectedRoles )
            {
                strUsernames = Roles.GetUsersInRole( selRole );
                foreach ( string Str in strUsernames )
                {
                    usernames.Add( Str );
                }
            }
            MembershipUser mem;

            foreach ( string str in usernames )
            {
                mem = Membership.GetUser( str );
                Guid gui = new Guid( mem.ProviderUserKey.ToString() );
                string name = GetName( gui );
                MailAddress address = new MailAddress( mem.Email, name );
                MembershipUser user = Membership.GetUser( gui );

                //MemberInfo meminfo = new MemberInfo(MemberInfo.Columns.Memberid, user.ProviderUserKey);

                MemberInfo membre = MemberInfo.GetMemberInfo( gui );
                //if (MemInfo.IsLoaded == true && (bool)MemInfo.Newsletter == true)
                //{
                retval.Add( membre.Adresse );
                //}
            }
        }
        return retval;
    }
}


