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
using MemberInfoData;

public partial class Page_MemberEdit : PageBase
{
    static Guid MembreGUID = Guid.Empty;

    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( IsPostBack == false )
        {
            if ( User.Identity.IsAuthenticated == false )
            {
                Response.Write( "Pour visualiser les détails de votre compte, vous devez être authentifié.<br/><a href='login.aspx'>Register</a>" );
                Response.End();
            }

            MemberInfo member = new MemberInfo();
            MembershipUser user = null;
            if ( User.IsInRole( "Administrateur" ) )
            {
                if ( Request.QueryString[ "MembreGUID" ] != null )
                {
                    MembreGUID = new Guid( Request.QueryString[ "MembreGUID" ] );
                    member = MemberInfo.GetMemberInfo( MembreGUID );
                    user = Membership.GetUser( MembreGUID );
                }
                else if ( Request.QueryString[ "nom" ] != null )
                {
                    string nomUtilisateur = Request.QueryString[ "nom" ].ToString();
                    member = MemberInfo.GetMemberInfo( nomUtilisateur );
                    MembreGUID = member.MemberGUID;
                    user = Membership.GetUser( MembreGUID );
                }
            }

            TextBoxUserName.Text = user.UserName;
            TextBoxUserName.Enabled = false; // On ne peut pas changer de UserName !
            TextBoxEmail.Text = user.Email;

            TextBoxMotDePasse.Text = member.MotDePasse;
            TextBoxMotDePasse.Enabled = false; // Ici on ne change pas le mot de passe
            TextBoxNom.Text = member.Nom;
            TextBoxPrenom.Text = member.Prenom;
            TextBoxAdresse.Text = member.Adresse;
            TextBoxTelephone.Text = member.Telephone;
            TextBoxSociete.Text = member.Societe;

            LabelOnline.Text = user.IsOnline.ToString();
            if ( user.IsOnline )
            {
                LabelOnline.CssClass = "LabelGreenStyle";
            }
            CheckBoxUserIsApproved.Checked = user.IsApproved;
            CheckBoxUserIsLocked.Checked = user.IsLockedOut;
            LabelIsApproved.Text = user.IsApproved.ToString();
            if ( user.IsApproved == false )
            {
                LabelIsApproved.CssClass = "LabelRedStyle";
            }

            LabelIsLockedOut.Text = user.IsLockedOut.ToString();
            if ( user.IsLockedOut )
            {
                LabelIsLockedOut.CssClass = "LabelRedStyle";
            }

            LabelCreationDate.Text = user.CreationDate.ToString();
            LabelLastLoginDate.Text = user.LastLoginDate.ToString();
            LastLockoutDate.Text = user.LastLockoutDate.ToString();
            LabelActivityDate.Text = user.LastActivityDate.ToString();
            LabelLastPasswordChangedDate.Text = user.LastPasswordChangedDate.ToString();

            ValidationMessage.Text = "";
        }
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

    protected void CheckBoxUserIsApproved_CheckedChanged( object sender, EventArgs e )
    {
        MembershipUser user = Membership.GetUser( MembreGUID );
        if ( CheckBoxUserIsApproved.Checked == true )
        {
            user.IsApproved = true;
        }
        else
        {
            user.IsApproved = false;
        }
        Membership.UpdateUser( user );
        Response.Redirect( Request.RawUrl );
    }

    protected void CheckBoxUserIsLocked_CheckedChanged( object sender, EventArgs e )
    {
        MembershipUser user = Membership.GetUser( MembreGUID );
        if ( CheckBoxUserIsLocked.Checked == false )
        {
            user.UnlockUser();
        }
        Membership.UpdateUser( user );
        Response.Redirect( Request.RawUrl );
    }

    protected void ButtonSave_Click( object sender, EventArgs e )
    {
        ValidationMessage.Text = "";
        ValidationMessage.CssClass = "LabelValidationMessageStyle";
        if ( TextBoxNom.Text.Trim().Length == 0 )
        {
            ValidationMessage.Text += "Entrer un Nom<br/>";
            ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
        }
        if ( TextBoxPrenom.Text.Trim().Length == 0 )
        {
            ValidationMessage.Text += "Entrer un Prénom<br/>";
            ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
        }
        if ( TextBoxSociete.Text.Trim().Length == 0 )
        {
            ValidationMessage.Text += "Entrer une Société<br/>";
            ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
        }
        if ( TextBoxEmail.Text.Trim().Length == 0 )
        {
            ValidationMessage.Text += "Entrer un E-mail<br/>";
            ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
        }

        if ( ValidationMessage.Text != "" )
        {
            ValidationMessage.Visible = true;
            return;
        }

        // C'est une mise a jour
        if ( MembreGUID != Guid.Empty )
        {
            MemberInfo member = MemberInfo.GetMemberInfo( MembreGUID );
            MembershipUser user = Membership.GetUser( MembreGUID );

            //user.UserName = TextBoxUserName.Text; IMPOSSIBLE
            user.Email = TextBoxEmail.Text;
            Membership.UpdateUser( user );
           
            member.Nom = TextBoxNom.Text;
            member.Prenom = TextBoxPrenom.Text;
            member.Adresse = TextBoxAdresse.Text;
            member.Telephone = TextBoxTelephone.Text;
            member.Societe = TextBoxSociete.Text;

            int retCode = member.Update();
            if ( retCode == 1 )
            {
                ValidationMessage.Text += "Membre mis à jour correctement.<br/>";
            }
            else
            {
                ValidationMessage.Text += "Erreur sur la mise à jour du Membre.<br/>";
                ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
            }
        }

        ValidationMessage.Visible = true;
    }

    protected void ButtonCancel_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/Member/Manage.aspx" );
    }

    protected void ButtonSupprimer_Click( object sender, EventArgs e )
    {
        if ( MembreGUID == Guid.Empty )
        {
            ValidationMessage.Text += "Choisir un membre à supprimer.<br/>";
            ValidationMessage.CssClass = "LabelValidationMessageErrorStyle";
            ValidationMessage.Visible = true;
        }
        else
        {
            Response.Redirect( "~/Member/Delete.aspx?MembreGUID=" + MembreGUID.ToString() );
        }
    }

    protected void ButtonSupprimerVotreCompte_Click( object sender, System.EventArgs e )
    {
        MembershipUser user = Membership.GetUser();
        MemberInfo member = MemberInfo.GetMemberInfo( ( Guid )user.ProviderUserKey );
        member.Delete();
        Membership.DeleteUser( user.UserName );

        FormsAuthentication.SignOut();
        Response.Redirect( "~/Default.aspx" );
    }
}
