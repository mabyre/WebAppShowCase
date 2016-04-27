using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MemberInfoData;

public partial class Contact_MemberDelete : PageBase
{
    static Guid MembreGUID = Guid.Empty;

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !IsPostBack )
        {
            if ( Request.QueryString[ "MembreGUID" ] == null )
            {
                Response.Redirect( "~/Member/Manage.aspx" );
            }
            else
            {
                MembreGUID = new Guid( Request.QueryString[ "MembreGUID" ] );
                MembershipUser user = Membership.GetUser( MembreGUID );
                MemberInfo member = MemberInfo.GetMemberInfo( MembreGUID );
                ValidationMessage.Text += "Suppression du Membre : " + member.Nom + " " + member.Prenom + " " + user.Email + "<br />";
                ValidationMessage.Text += "-- Nom d'utilisateur : " + user.UserName + "<br /><br />";
                ValidationMessage.Visible = true;
            }
        }
    }

    protected void ButtonCancel_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/Member/Manage.aspx" );
    }

    protected void ButtonSupprimer_Click( object sender, EventArgs e )
    {
        if ( MembreGUID == Guid.Empty )
        {
            ValidationMessage.Text += "<br/>Choisir un membre à supprimer.<br/>";
            ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
            ValidationMessage.Visible = true;
        }
        else
        {
            int status = 0;
            int statusGlobal = 0;

            ValidationMessage.Text += "<br />-----------------------------------------------------<br />";
            ValidationMessage.Text += " Début de la Suppression du Membre <br />";
            ValidationMessage.Text += "-----------------------------------------------------<br />";

            MemberInfo member = MemberInfo.GetMemberInfo( MembreGUID );
            MembershipUser user = Membership.GetUser( MembreGUID );

            ValidationMessage.Text += "Suppression du Membre : " + member.Nom + " " + member.Prenom + " " + user.Email + " " + user.UserName + "<br />";
            status = member.Delete();
            statusGlobal = statusGlobal + status;
            ValidationMessage.Text += "status : " + status.ToString() + "<br />";
            ValidationMessage.Text += "Suppression de l'Utilisateur : " + user.UserName + "<br />";

            bool ok = Membership.DeleteUser( user.UserName, true );
            if ( ok )
                status = 0;
            else
                status = 1;
            ValidationMessage.Text += "status : " + status.ToString() + "<br />";
            ValidationMessage.Text += "<br />status global : " + statusGlobal.ToString() + "<br />";

            ValidationMessage.Visible = true;
            MembreGUID = Guid.Empty;
        }
    }
}
