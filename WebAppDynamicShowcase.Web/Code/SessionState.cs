using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Collections.Generic;
using WebContentData;
using MemberInfoData;

/// <summary>
/// Conserver l'etat des CheckBox pendant toute la Session
/// </summary>
public class CheckBoxSessionState
{
    // Le constructeur est obligatoire ici sinon SessionState.CheckBox est vide
    public CheckBoxSessionState()
    {
    }

    new public bool this[ string key ]
    {
        get
        {
            if ( HttpContext.Current.Session[ "CheckBoxSessionState" ] == null )
            {
                Dictionary<string, bool> _cbss = new Dictionary<string, bool>();
                _cbss.Add( "CheckBoxInstruction", true );
                _cbss.Add( "CheckBoxMessage", true );
                _cbss.Add( "CheckBoxTous", false );
                _cbss.Add( "CheckBoxDate", false );
                _cbss.Add( "CheckBoxSociete", false );
                _cbss.Add( "CheckBoxDescription", false );
                _cbss.Add( "CheckBoxAfficherReponseTextuelle", true );
                _cbss.Add( "CheckBoxAfficherDateVote", false );
                HttpContext.Current.Session[ "CheckBoxSessionState" ] = _cbss;
            }
            return ( ( Dictionary<string, bool> )HttpContext.Current.Session[ "CheckBoxSessionState" ] )[ key ];
        }
        set 
        {
            ( ( Dictionary<string, bool> )HttpContext.Current.Session[ "CheckBoxSessionState" ] )[ key ] = value; 
        }
    }
}

/// <summary>
/// Gerer les variables de sessions d'une maniere structuree
/// </summary>
public class SessionState 
{
    public static CheckBoxSessionState CheckBox = new CheckBoxSessionState();

    // L'etat du bouton d'affichage des commentaires
    public static bool ButtonCommentaires
    {
        get
        {
            if ( HttpContext.Current.Session[ "ButtonCommentaires" ] != null )
                return ( bool )( HttpContext.Current.Session[ "ButtonCommentaires" ] );

            return false;
        }

        set
        {
            HttpContext.Current.Session[ "ButtonCommentaires" ] = value;
        }
    }

    public static string ValidationMessage
    {
        get
        {
            if ( HttpContext.Current.Session[ "ValidationMessage" ] != null )
                return ( string )( HttpContext.Current.Session[ "ValidationMessage" ] );

            return null;
        }

        set
        {
            HttpContext.Current.Session[ "ValidationMessage" ] = value;
        }
    }

    /// <summary>
    /// Informations sur un Membre authentifie
    /// </summary>
    public static MemberInfo MemberInfo
    {
        get 
        {
            if ( HttpContext.Current.Session[ "MemberInfo" ] == null )
            {
                if ( HttpContext.Current.User != null )
                {
                    if ( HttpContext.Current.User.Identity.IsAuthenticated )
                    {
                        MembershipUser user = Membership.GetUser();
                        MemberInfo member = MemberInfo.GetMemberInfo( ( Guid )user.ProviderUserKey );
                        HttpContext.Current.Session[ "MemberInfo" ] = member;
                    }
                }
                else // La session de l'utilisateur a expiree
                {
                    HttpContext.Current.Response.Redirect( "~/Default.aspx" );
                }
            }

            return ( MemberInfo )HttpContext.Current.Session[ "MemberInfo" ];
        }
    }

    /// <summary>
    /// Contenu Web d'une Section
    /// </summary>
    public static WebContent WebContent
    {
        get
        {
            if ( HttpContext.Current.Session[ "WebContent" ] != null )
                return ( WebContent )( HttpContext.Current.Session[ "WebContent" ] );

            return null;
        }

        set
        {
            HttpContext.Current.Session[ "WebContent" ] = value;
        }
    }
}
