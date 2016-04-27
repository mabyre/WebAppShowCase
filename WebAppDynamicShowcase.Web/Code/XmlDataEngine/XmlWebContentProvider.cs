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
/// XML Data Layer for WebContent
/// </summary>

namespace WebContentData
{
    public class XmlWebContentProvider
    {
        private static string _xmlFile = "WebContent.xml";
        private static string _xsdFile = "WebContent.xsd";

        #region CreateUpdateDelete

        public static int Create( WebContent o )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            DataRow row = dataTable.NewRow();

            row[ "WebContentID" ] = Guid.NewGuid();
            row[ "Section" ] = o.Section;
            row[ "SectionContent" ] = o.SectionContent;
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

        public static int Update( WebContent o )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            foreach ( DataRow row in dataTable.Rows )
            {
                if ( o.WebContentID == new Guid( row[ "WebContentID" ].ToString() ) )
                {
                    row[ "SectionContent" ] = o.SectionContent;
                    row[ "CreationDate" ] = DateTime.Now.ToString();
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

        public static int Delete( WebContent o )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            foreach ( DataRow row in dataTable.Rows )
            {
                if ( o.WebContentID == ( Guid )row[ "WebContentID" ] )
                {
                    row.Delete();
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

        public static WebContentCollection GetAll()
        {
            WebContentCollection list = new WebContentCollection();
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );

            DataTable dataTable = dataSet.Tables[ 0 ];
            foreach ( DataRow r in dataTable.Rows )
            {
                WebContent current = WebContent.Fill( r );
                list.Add( current );
            }
            return list;
        }

        public static WebContent GetWebContent( string section )
        {
            DataSet dataSet = XmlUtil.ReadAndValidateXml( _xmlFile, _xsdFile );
            DataTable dataTable = dataSet.Tables[ 0 ];
            foreach ( DataRow r in dataTable.Rows )
            {
                if ( section == ( string )r[ "Section" ] ) // match found
                {
                    WebContent a = WebContent.Fill( r );
                    return a;
                }
            }
            return null;
        }
    }
}
