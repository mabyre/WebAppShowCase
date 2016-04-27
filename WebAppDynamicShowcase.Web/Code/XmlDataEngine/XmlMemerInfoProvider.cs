using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// XML Data Layer for MemberInfo
/// </summary>

namespace MemberInfoData
{
    public class XmlMemberInfoProvider
    {
        private static string _xmlFile = "MemberInfo.xml";
        private static string _xsdFile = "MemberInfo.xsd";

        #region CreateUpdateDelete

        public static int Create( MemberInfo o )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            DataRow row = dataTable.NewRow();

            row[ "MemberGUID" ] = o.MemberGUID;
            row[ "NomUtilisateur" ] = o.NomUtilisateur;
            row[ "MotDePasse" ] = o.MotDePasse;
            row[ "Nom" ] = o.Nom;
            row[ "Prenom" ] = o.Prenom;
            row[ "Adresse" ] = o.Adresse;
            row[ "Telephone" ] = o.Telephone;
            row[ "Societe" ] = o.Societe;
            row[ "CreationDate" ] = DateTime.Now.ToString();

            dataTable.Rows.Add( row );

            try
            {
                XmlUtil.DataSetWriteXml( ref dataSet, _xmlFile );
            }
            catch
            {
                return 0;
            }

            return 1;
        }

        public static int Update( MemberInfo o )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            
            foreach ( DataRow row in dataTable.Rows )
            {
                if ( o.MemberGUID == new Guid( row[ "MemberGUID" ].ToString() ) )
                {
                    row[ "NomUtilisateur" ] = o.NomUtilisateur;
                    row[ "MotDePasse" ] = o.MotDePasse;
                    row[ "Nom" ] = o.Nom;
                    row[ "Prenom" ] = o.Prenom;
                    row[ "Adresse" ] = o.Adresse;
                    row[ "Telephone" ] = o.Telephone;
                    row[ "Societe" ] = o.Societe;
                    break;
                }
            }

            try
            {
                XmlUtil.DataSetWriteXml( ref dataSet, _xmlFile );
            }
            catch
            {
                return 0;
            }

            return 1;
        }

        public static int Delete( MemberInfo o )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            
            foreach ( DataRow row in dataTable.Rows )
            {
                if ( o.MemberGUID == new Guid( row[ "MemberGUID" ].ToString() ) )
                {
                    row.Delete();
                    break;
                }
            }

            try
            {
                XmlUtil.DataSetWriteXml( ref dataSet, _xmlFile );
            }
            catch
            {
                return 0;
            }

            return 1;
        }

        #endregion

        public static MemberInfoCollection GetAll()
        {
            MemberInfoCollection list = new MemberInfoCollection();
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );

            DataTable dataTable = dataSet.Tables[ 0 ];
            foreach ( DataRow r in dataTable.Rows )
            {
                MemberInfo current = new MemberInfo();

                current.MemberGUID = new Guid( r[ "MemberGUID" ].ToString() );
                current.NomUtilisateur = ( string )r[ "NomUtilisateur" ];
                current.MotDePasse = ( string )r[ "MotDePasse" ];
                current.Nom = ( string )r[ "Nom" ];
                current.Prenom = ( string )r[ "Prenom" ];
                current.Adresse = ( string )r[ "Adresse" ];
                current.Telephone = ( string )r[ "Telephone" ];
                current.Societe = ( string )r[ "Societe" ];
                current.CreationDate = DateTime.Parse( ( string )r[ "CreationDate" ] );

                list.Add( current );
            }
            return list;
        }

        public static MemberInfo GetMemberInfo( string nomUtilisateur )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];

            foreach ( DataRow r in dataTable.Rows )
            {
                if ( nomUtilisateur == ( string )r[ "NomUtilisateur" ] ) // match found
                {
                    MemberInfo current = new MemberInfo();

                    current.MemberGUID = new Guid( r[ "MemberGUID" ].ToString() );
                    current.NomUtilisateur = ( string )r[ "NomUtilisateur" ];
                    current.MotDePasse = ( string )r[ "MotDePasse" ];
                    current.Nom = ( string )r[ "Nom" ];
                    current.Prenom = ( string )r[ "Prenom" ];
                    current.Adresse = ( string )r[ "Adresse" ];
                    current.Telephone = ( string )r[ "Telephone" ];
                    current.Societe = ( string )r[ "Societe" ];
                    current.CreationDate = DateTime.Parse( ( string )r[ "CreationDate" ] );

                    return current;
                }
            }
            return null;
        }
    }
}
