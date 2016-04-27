//
// Edit.aspx est reserve a l'admin
// ici l'utilisateur peut modifier les caracteristiques de son compte
// le supprimer
// j'ai trouve le moyen de mettre a jour le mot de passe dans MemberInfo
// c'est pas mal
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
using MemberInfoData;

public partial class Page_MemberDetails : PageBase
{
    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( IsPostBack == false )
        {
            if ( User.Identity.IsAuthenticated == false )
            {
                Response.Write( "Pour visualiser les détails de votre compte, vous devez être authentifié.<br/><a href='login.aspx'>Register</a>" );
                Response.End();
            }

            MembershipUser user = Membership.GetUser();
            MemberInfo member = MemberInfo.GetMemberInfo( user.UserName );
            LabelUserName.Text = user.UserName;
            TextBoxEmail.Text = user.Email;
            TextBoxNom.Text = member.Nom;
            TextBoxPrenom.Text = member.Prenom;
            TextBoxAdresse.Text = member.Adresse;
            TextBoxTelephone.Text = member.Telephone;
            TextBoxSociete.Text = member.Societe;

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

        MembershipUser user = Membership.GetUser();
        MemberInfo member = MemberInfo.GetMemberInfo( user.UserName );

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

        ValidationMessage.Visible = true;
    }

    protected void ButtonCancel_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/Default.aspx" );
    }

    protected void ButtonSupprimerCompte_Click( object sender, System.EventArgs e )
    {
        MembershipUser user = Membership.GetUser();
        MemberInfo member = MemberInfo.GetMemberInfo( ( Guid )user.ProviderUserKey );
        member.Delete();
        Membership.DeleteUser( user.UserName );

        FormsAuthentication.SignOut();
        Response.Redirect( "~/Default.aspx" );
    }

    protected void ChangePassword1_ChangingPassword( object sender, LoginCancelEventArgs e )
    {
        // Mettre a jour le mot de passe dans les MemberInfos
        ChangePassword chgpw = ( ChangePassword )sender;
        string username = chgpw.UserName;
        MemberInfo member = MemberInfo.GetMemberInfo( username );
        member.MotDePasse = chgpw.NewPassword;
        member.Update();
    }
}
