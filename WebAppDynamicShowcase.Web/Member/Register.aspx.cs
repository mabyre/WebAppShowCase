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
using System.Net.Mail;
using MemberInfoData;

public partial class Member_Register : PageBase
{
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( IsPostBack == false )
        {
            CustomValidatorPassWord.Text = "Minimum : " + Membership.MinRequiredPasswordLength;
        }

        ValiderMessage();
    }

    void ValiderMessage()
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


    protected void ButtonEnregistrer_Click( object sender, System.EventArgs e )
    {
        if ( TextBoxPassWord.Text == TextBoxConfirmPassword.Text )
        {
            MembershipCreateStatus memberCreateStatus = MembershipCreateStatus.InvalidAnswer;
            MemberInfo membre = new MemberInfo();
            try
            {
                MembershipCreateStatus mcs;
                Membership.CreateUser
                ( 
                    TextBoxNomUtilisateur.Text,
                    TextBoxPassWord.Text, 
                    TextBoxEmail.Text, 
                    TextBoxQuestion.Text, 
                    TextBoxAnswer.Text, 
                    true,
                    out memberCreateStatus 
                );

                switch ( memberCreateStatus )
                {
                    case MembershipCreateStatus.InvalidPassword :
                        SessionState.ValidationMessage = "Mot de passe invalide.<br/>";
                        break;
                    case MembershipCreateStatus.DuplicateUserName :
                        SessionState.ValidationMessage = "Erreur : Cet utilisateur existe déjà.<br/>";
                        break;
                    case MembershipCreateStatus.Success :
                        MembershipUser user = Membership.GetUser( TextBoxNomUtilisateur.Text );
                        SessionState.ValidationMessage += "Création de l'utilisateur avec succès.<br/>";

                        membre.MemberGUID = ( Guid )user.ProviderUserKey;
                        membre.NomUtilisateur = user.UserName;
                        membre.MotDePasse = TextBoxPassWord.Text;
                        membre.Nom = TextBoxLastName.Text;
                        membre.Prenom = TextBoxFisrtName.Text;
                        membre.Societe = TextBoxSociete.Text;
                        membre.Telephone = TextBoxTelephone.Text;
                        membre.Adresse = TextBoxAdresse.Text;

                        int status = membre.Create();
                        if ( status != 1 )
                        {
                            SessionState.ValidationMessage += "Erreur de création des informations utilisateurs.<br/>";
                            Membership.DeleteUser( user.UserName );
                            SessionState.ValidationMessage += "Suppression de l'utilisateur.<br/>";
                        }

                        if ( Global.SettingsXml.MembreVerification )
                        {
                            Response.Redirect( "~/Member/EmailVerification.aspx?username=" + TextBoxNomUtilisateur.Text );
                        }

                        //
                        // Envoyer l'email d'enregistrement a l'administrateur
                        //
                        if ( Global.SettingsXml.MembrePrevenir )
                        {
                            MailMessage mail = new MailMessage();
                            string sujetEmail2 = "Enregistrement d'un nouvel utilisateur sur le site : " + Utils.WebSiteUri;
                            string bodyEmail2 = "";

                            bodyEmail2 += "Nom d'utilisateur : " + membre.NomUtilisateur + "<br/>";
                            bodyEmail2 += "Mot de passe : " + membre.MotDePasse + "<br/>";
                            bodyEmail2 += "Nom : " + membre.Nom + "<br/>";
                            bodyEmail2 += "Prénom : " + membre.Prenom + "<br/>";
                            bodyEmail2 += "Société : " + membre.Societe + "<br/>";
                            bodyEmail2 += "Téléphone : " + membre.Telephone + "<br/>";
                            bodyEmail2 += "Adresse : " + membre.Adresse + "<br/>";
                            bodyEmail2 += "Email : " + TextBoxEmail.Text + "<br/>";

                            if ( Global.SettingsXml.MembreVerification )
                            {
                                bodyEmail2 += "<br/>Cet utilisateur est en attente d'approbation.<br/>";
                            }
                            else
                            {
                                bodyEmail2 += "<br/>Cet utilisateur est approuvé.<br/>";
                            }

                            bodyEmail2 += "<br/>Accès à l'application :<br/>" + string.Format( "<a href=\"{0}\" >{1}</a>", Utils.WebSiteUri, Utils.WebSiteUri ) + "<br/>";
                            Courriel.EnvoyerEmailToAdminAssynchrone( sujetEmail2, bodyEmail2 );
                        }

                        // Modifie l'utilisateur en cours et utilise le nouvel utilisateur cree
                        // si on ne veut que seulement l'admin cree les utilisatieurs
                        // il faut mettre ces lignes en commentaire
                        FormsAuthentication.SetAuthCookie( user.UserName, false );
                        Response.Redirect( "~/Default.aspx" );

                        break;
                    default :
                        SessionState.ValidationMessage = "Erreur : MembershipCreateStatus non traité.";
                        break;
                }
            }
            catch ( Exception ex )
            {
                SessionState.ValidationMessage += "Erreur exception : " + ex.Message;
            }
        }
        else
        {
            SessionState.ValidationMessage += "Erreur : Votre mot de Passe ne correspond pas!";
        }

        Response.Redirect( Request.RawUrl, true );
    }

    protected void ButtonRetour_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/Member/Manage.aspx" );
    }
}


