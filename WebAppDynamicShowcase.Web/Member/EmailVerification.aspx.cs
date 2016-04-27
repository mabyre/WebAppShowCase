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

public partial class Member_EmailVerification : System.Web.UI.Page
{
    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( Request[ "username" ] != null )
        {
            MembershipUser member = Membership.GetUser( Request[ "username" ] );
            member.IsApproved = false;
            Membership.UpdateUser( member );
            LabelSatusTitre.Text = "Vérification de l'administrateur requise";
            LabelTexte.Text = "Votre inscription est en cours de vérification.<br/>Vous recevrez la confirmation de l'activation de votre compte par email.";

            MemberInfo membre = MemberInfo.GetMemberInfo( ( Guid )member.ProviderUserKey );
            MembershipUser user = Membership.GetUser( membre.MemberGUID ); 
            string body = "Demande d'approbation d'un nouveau membre :<br/>";
            body += "NomUtilisateur : " + membre.NomUtilisateur + "<br/>";
            body += "Nom : " + membre.Nom + "<br/>";
            body += "Prenom : " + membre.Prenom + "<br/>";
            body += "Societe : " + membre.Societe + "<br/>";
            body += "Email : " + user.Email + "<br/>";
            LabelEmailSatus.Text = Courriel.EnvoyerEmailAdministrateur( body );
        }
    }
}
