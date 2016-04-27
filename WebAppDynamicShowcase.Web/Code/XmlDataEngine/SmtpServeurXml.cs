using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using XmlDataProvider;

namespace SmtpServeurXmlProvider
{
    public class SmtpServeurXml
    {
        public static StringDictionary Dictionary = DataProviderXml.LoadXmlData( "SmtpServeur.xml" );

        public SmtpServeurXml()
        {
            StringDictionary dic = DataProviderXml.LoadXmlData( "SmtpServeur.xml" );

            adminemail = dic[ "adminemail" ];
            sujetemail = dic[ "sujetemail" ];
            _UserEmail = dic[ "UserEmail" ];
            _UserName = dic[ "UserName" ];
            _UserPassWord = dic[ "UserPassWord" ];
            _ServerName = dic[ "ServerName" ];
            _ServerPort = int.Parse( dic[ "ServerPort" ] );
            _EnableSSL = dic[ "EnableSSL" ].ToLower() == "true" ? true : false;
        }

        public void Save()
        {
            StringDictionary dic = DataProviderXml.LoadXmlData( "SmtpServeur.xml" );

            dic[ "adminemail" ] = adminemail;
            dic[ "sujetemail" ] = sujetemail;
            dic[ "UserEmail" ] = UserEmail;
            dic[ "UserName" ] = UserName;
            dic[ "UserPassWord" ] = UserPassWord;
            dic[ "ServerName" ] = ServerName;
            dic[ "ServerPort" ] = ServerPort.ToString();
            dic[ "EnableSSL" ] = EnableSSL.ToString();

            XmlDataProvider.DataProviderXml.SaveXmlData( dic, "SmtpServeur.xml" );
        }

        private string sujetemail = "";
        public string SujetEmail
        {
            get { return sujetemail; }
            set { sujetemail = value; }
        }

        private string adminemail = "";
        public string AdminEmail
        {
            get { return adminemail; }
            set { adminemail = value; }
        }

        private string _UserEmail = "";
        public string UserEmail
        {
            get { return _UserEmail; }
            set { _UserEmail = value; }
        }

        private string _UserName = "";
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _UserPassWord = "";
        public string UserPassWord
        {
            get { return _UserPassWord; }
            set { _UserPassWord = value; }
        }

        private string _ServerName = "";
        public string ServerName
        {
            get { return _ServerName; }
            set { _ServerName = value; }
        }

        private int _ServerPort = 25;
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }

        private bool _EnableSSL = false;
        public bool EnableSSL
        {
            get { return _EnableSSL; }
            set { _EnableSSL = value; }
        }
    }
}