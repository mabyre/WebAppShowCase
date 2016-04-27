using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// Description résumée de MemberInfo
/// </summary>
namespace MemberInfoData
{
    public class MemberInfo
    {
        public MemberInfo()
        {
        }

        #region Proprietees

        // Liaison de donnees avec ASPNETDB 
        // et la table aspnet_Membership
        Guid _MemberGUID = new Guid();
        public Guid MemberGUID
        {
            get { return _MemberGUID; }
            set { _MemberGUID = value; }
        }

        string _NomUtilisateur = "";
        public string NomUtilisateur
        {
            get { return _NomUtilisateur; }
            set { _NomUtilisateur = value; }
        }

        string _MotDePasse = "";
        public string MotDePasse
        {
            get { return _MotDePasse; }
            set { _MotDePasse = value; }
        }

        string _Nom = "";
        public string Nom
        {
            get { return _Nom; }
            set { _Nom = value; }
        }

        string _Prenom = "";
        public string Prenom
        {
            get { return _Prenom; }
            set { _Prenom = value; }
        }

        string _Adresse = "";
        public string Adresse
        {
            get { return _Adresse; }
            set { _Adresse = value; }
        }

        string _Telephone = "";
        public string Telephone
        {
            get { return _Telephone; }
            set { _Telephone = value; }
        }

        string _Societe = "";
        public string Societe
        {
            get { return _Societe; }
            set { _Societe = value; }
        }

        DateTime _CreationDate = DateTime.Now;
        public DateTime CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }

        #endregion

        public static MemberInfo Fill( DataRow r )
        {
            MemberInfo o = new MemberInfo();

            o.MemberGUID = ( Guid )( r[ "MemberGUID" ] );
            o.NomUtilisateur = r[ "NomUtilisateur" ].ToString();
            o.MotDePasse = r[ "MotDePasse" ].ToString();
            o.Nom = r[ "Nom" ].ToString();
            o.Prenom = r[ "Prenom" ].ToString();
            o.Adresse = r[ "Adresse" ].ToString();
            o.Telephone = r[ "Telephone" ].ToString();
            o.Societe = r[ "Societe" ].ToString();

            return o;
        }

        #region CreateUpdateDeleteMethodes

        public int Create()
        {
            int status = XmlMemberInfoProvider.Create( this );
            return status;
        }

        public int Update()
        {
            int status = XmlMemberInfoProvider.Update( this );
            return status;
        }

        public int Delete()
        {
            int status = XmlMemberInfoProvider.Delete( this );
            return status;
        }

        #endregion


        public static MemberInfo GetMemberInfo( string nom, string prenom )
        {
            MemberInfoCollection oc = MemberInfoCollection.GetAll();
            foreach ( MemberInfo o in oc )
            {
                if ( o.Nom == nom && o.Prenom == prenom )
                {
                    return o;
                }
            }
            return null;
        }

        // MembershipUser.UserName
        public static MemberInfo GetMemberInfo( string nomUtilisateur )
        {
            MemberInfoCollection oc = MemberInfoCollection.GetAll();
            foreach ( MemberInfo o in oc )
            {
                if ( o.NomUtilisateur == nomUtilisateur )
                {
                    return o;
                }
            }
            return null;
        }

        public static MemberInfo GetMemberInfo( Guid membreGUID )
        {
            MemberInfoCollection oc = MemberInfoCollection.GetAll();
            foreach ( MemberInfo o in oc )
            {
                if ( o.MemberGUID == membreGUID )
                {
                    return o;
                }
            }
            return null;
        }
    }

    public class MemberInfoCollection : List<MemberInfo>
    {
        private List<MemberInfo> _collection = null;

        public MemberInfoCollection()
        {
            _collection = new List<MemberInfo>();
        }

        public static MemberInfoCollection GetAll()
        {
            return XmlMemberInfoProvider.GetAll();
        }
    }
}
