///
/// Ici le probleme c'est User.Identity.Name se retrouve, dans la page suivante Default.aspx
/// avec le n'importe quoi que l'utilisateur à tapé sur son clavier majuscule ou pas, espace
/// ou pas, et il est authentifié quand même !
/// 
/// C'est vraiment déroutant pour moi qui joue plusieurs roles et a tendance a copier-coller 
/// avec des espaces apres le nom d'utilisateur.
/// 
/// Solution : Si l'utilisateur a choisi de mettre des majuscules tanpis pour lui il devra
/// les retaper pour se loguer ! putain c'est normal !
/// S'il met des espaces au moment de se logguer pareil et c'est normal
/// On ne va pas se taper d'ajouter Trim() derriere chaque User.Identity.Name 
/// d'autant plus que l'on ne sait pas bien ce que cela peut engendrer donc
/// s'il met des espaces il ne pourra pas nom plus se logguer mais on le previent.
/// 
/// Ainsi on aura bien : UserName.Text == User.Identity.Name
/// 

#region using
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
#endregion

public partial class Member_Login : System.Web.UI.Page
{
    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( Request[ "ReturnURL" ] == null )
        {
            Response.Redirect( "~/Member/Login.aspx?ReturnURL=~/default.aspx" );
        }

        if ( Request[ "err" ] == "name" )
        {
            Panel PanelMessageUtilisateur = ( Panel )LoginControl.FindControl( "PanelMessageUtilisateur" );
            Label LabelMessageUtilisateur = ( Label )LoginControl.FindControl( "LabelMessageUtilisateur" );
            PanelMessageUtilisateur.Visible = true;
            LabelMessageUtilisateur.Text = "Nom d'utilisateur incorrect.";
            LabelMessageUtilisateur.CssClass = "LabelRedStyle";
            LabelMessageUtilisateur.Visible = true;
        }

        Button LoginButton = ( Button )LoginControl.FindControl( "LoginButton" );
        Page.Form.DefaultButton = LoginButton.UniqueID;
    }

    protected void LoginButton_Click( object sender, System.EventArgs e )
    {
        TextBox UserName = ( TextBox )LoginControl.FindControl( "UserName" );
        TextBox Password = ( TextBox )LoginControl.FindControl( "Password" );
        Literal FailureText = ( Literal )LoginControl.FindControl( "FailureText" );
        Panel PanelMessageUtilisateur = ( Panel )LoginControl.FindControl( "PanelMessageUtilisateur" );
        Label LabelMessageUtilisateur = ( Label )LoginControl.FindControl( "LabelMessageUtilisateur" );

        bool userIsValide = Membership.ValidateUser( UserName.Text, Password.Text );

        MembershipUser user = Membership.GetUser( UserName.Text );
        if ( user != null && userIsValide )
        {
            // On va cherche le "vrai" nom d'utilisateur avec les majuscule et sans les espaces
            // ainsi seulement on peut comparer
            MembershipUser user2 = Membership.GetUser( user.ProviderUserKey );
            if ( user2.UserName != UserName.Text )
            {
                LabelMessageUtilisateur.CssClass = "LabelRedStyle";
                LabelMessageUtilisateur.Visible = true;
                if ( Request.RawUrl.Contains( "&err=name" ) == false )
                {
                    // Il ne faut jouter qu'une seule fois "&err=name"
                    // sinon le test Request[ "err" ] == "name" ne fonctionne plus
                    Response.Redirect( Request.RawUrl + "&err=name" );
                }
                else
                {
                    Response.Redirect( Request.RawUrl );
                }
            }
        }

        if ( user != null && user.IsApproved == false )
        {
            PanelMessageUtilisateur.Visible = true;
            LabelMessageUtilisateur.Text = "Votre compte n'est pas approuvé.";
            LabelMessageUtilisateur.CssClass = "LabelRedStyle";
            LabelMessageUtilisateur.Visible = true;
            FailureText.Visible = false; // On peut pas changer le texte mais ca ca marche !
        }

        if ( user != null && user.IsLockedOut )
        {
            PanelMessageUtilisateur.Visible = true;
            LabelMessageUtilisateur.Text = "Votre compte est vérouillé.";
            LabelMessageUtilisateur.CssClass = "LabelRedStyle";
            LabelMessageUtilisateur.Visible = true;
            //FailureText.Visible = false;
        }

        if ( user != null )
        {
            if ( Global.SettingsXml.MembreConnexionPrevenir )
            {
                string sujetEmail2 = "Connexion d'un utilisateur sur le site : " + Utils.WebSiteUri;
                string bodyEmail2 = "";
                bodyEmail2 += "Nom d'utilisateur : " + user.UserName + "<br/>";
                bodyEmail2 += "Email : " + user.Email + "<br/>";
                if ( user.IsApproved == false )
                {
                    bodyEmail2 += "<br>Cet utilisateur n'est pas approuvé.<br/>";
                }
                if ( user.IsLockedOut == true )
                {
                    bodyEmail2 += "<br>Cet utilisateur est vérouillé.<br/>";
                }
                bodyEmail2 += "<br/>Accès à l'application :<br/>" + string.Format( "<a href=\"{0}\" >{1}</a>", Utils.WebSiteUri, Utils.WebSiteUri ) + "<br/>";

                Courriel.EnvoyerEmailToAdminAssynchrone( sujetEmail2, bodyEmail2 );
            }
        }
    }
}
