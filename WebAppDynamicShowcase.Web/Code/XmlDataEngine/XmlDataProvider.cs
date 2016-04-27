/*
** Utilisation :
** Le fichier XML se trouve dans le repertoire App_Data
** Le noeud principal du fichier xml doit etre nomme comme le fichier
**
** Remarque : 
** La fonction SaveXmlData utilise l'objet StringDictionary qui modifie les Keys pour les mettre 
** en non case sensitive et les reecrit en minuscule dans le fichier .xml. 
**
*/
#region Using

using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace XmlDataProvider
{
    public class DataProviderXml
    {
        public static StringDictionary LoadXmlData( string xmlFileName )
        {
            string fileName = HttpContext.Current.Server.MapPath( "~/App_Data/" + xmlFileName );
            if ( !File.Exists( fileName ) )
            {
                string message = string.Format( "Fichier pour le XmlDataProvider non trouvé : {0}", fileName );
                throw new FileNotFoundException( message );
            }

            StringDictionary dic = new StringDictionary();
            XmlDocument doc = new XmlDocument();
            doc.Load( fileName );

            string nodeName = xmlFileName.Substring( 0, xmlFileName.LastIndexOf( '.' ) );
            foreach ( XmlNode settingsNode in doc.SelectSingleNode( nodeName ).ChildNodes )
            {
                string name = settingsNode.Name;
                string value = settingsNode.InnerText;

                dic.Add( name, value );
            }

            return dic;
        }

        public static void SaveXmlData( StringDictionary dico, string xmlFileName )
        {
            if ( dico == null )
                throw new ArgumentNullException( "dico" );

            string filename = HttpContext.Current.Server.MapPath( "~/App_Data/" + xmlFileName );
            XmlWriterSettings writerSettings = new XmlWriterSettings(); ;
            writerSettings.Indent = true;

            using ( XmlWriter writer = XmlWriter.Create( filename, writerSettings ) )
            {
                string[] startElement = xmlFileName.Split( '.' );
                writer.WriteStartElement( startElement[ 0 ] );

                foreach ( string key in dico.Keys )
                {
                    writer.WriteElementString( key, dico[ key ] );
                }

                writer.WriteEndElement();
                writer.Close();
            }
        }
    }
}
