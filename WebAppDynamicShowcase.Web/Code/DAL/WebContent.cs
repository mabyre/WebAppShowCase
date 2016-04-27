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
/// Description résumée de WebContent
/// </summary>
namespace WebContentData
{
    public class WebContent
    {
        public WebContent( ) {}

        public static bool CanEdit()
        {
            if ( HttpContext.Current.User.IsInRole( "Administrateur" ) )
            {
                return true;
            }
            return false;
        }

        #region Proprietees

        Guid _WebContentID;
        public Guid WebContentID
        {
            get { return _WebContentID; }
            set { _WebContentID = value; }
        }

        string _Section;
        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }

        // une image de background ?
        // pour la section ?
        string _BackGround;
        public string BackGround
        {
            get { return _BackGround; }
            set { _BackGround = value; }
        }

        string _SectionContent;
        public string SectionContent
        {
            get { return _SectionContent; }
            set { _SectionContent = value; }
        }

        DateTime _CreationDate = DateTime.Now;
        public DateTime CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }

        #endregion

        public static WebContent Fill( DataRow r )
        {
            WebContent o = new WebContent();

            o.WebContentID = new Guid( r[ "WebContentID" ].ToString() );
            o.Section = r[ "Section" ].ToString();
            o.SectionContent = r[ "SectionContent" ].ToString();
            o.CreationDate = DateTime.Parse( r[ "CreationDate" ].ToString() );

            return o;
        }

        #region CreateUpdateDeleteMethodes

        public int Create()
        {
            int status = XmlWebContentProvider.Create( this );
            return status;
        }

        public int Update()
        {
            int status = XmlWebContentProvider.Update( this );
            return status;
        }

        public int Delete()
        {
            int status = XmlWebContentProvider.Delete( this );
            return status;
        }

        #endregion

        public static WebContent GetWebContent( Guid webContentID )
        {
            if ( webContentID == Guid.Empty )
            {
                return null;
            }

            WebContentCollection collection = WebContentCollection.GetAll();
            foreach ( WebContent o in collection )
            {
                if ( o.WebContentID == webContentID )
                {
                    return o;
                }
            }

            return null;
        }

        public static WebContent GetWebContent( string section )
        {
            if ( section == String.Empty )
            {
                return null;
            }

            WebContentCollection collection = WebContentCollection.GetAll();
            foreach ( WebContent o in collection )
            {
                if ( o.Section == section )
                {
                    return o;
                }
            }

            return null;
        }
    }

    public class WebContentCollection : List<WebContent>
    {
        private List<WebContent> _collection = null;

        public WebContentCollection()
        {
            _collection = new List<WebContent>();
        }

        public static WebContentCollection GetAll()
        {
            return XmlWebContentProvider.GetAll();
        }
    }
}