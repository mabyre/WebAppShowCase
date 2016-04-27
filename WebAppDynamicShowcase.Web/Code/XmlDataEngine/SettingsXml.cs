//
// Les tags XML sont en minuscules du a SaveXmlData()
//

#region using

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

#endregion

namespace SettingXmlProvider
{
    public class SettingXml
    {
        public static StringDictionary Dictionary = DataProviderXml.LoadXmlData( "Settings.xml" );

        public SettingXml()
        {
            StringDictionary dic = DataProviderXml.LoadXmlData( "Settings.xml" );

            avatar = dic[ "avatar" ];

            textlabeldefaut = dic[ "textlabeldefaut" ];
            editeurhauteur = dic[ "editeurhauteur" ];
            editeurelargir = dic[ "editeurelargir" ];

            adresseWebmaster = dic[ "adressewebmaster" ];
            sujetcourrielmaj = dic[ "sujetcourrielmaj" ];
            envoyerMiseAjour = dic[ "envoyermiseajour" ] == "vrai" ? true : false;

            labelBoutonQuestion = dic[ "labelboutonquestion" ];
            logUser = dic[ "loguser" ] == "vrai" ? true : false;
            membreverification = dic[ "membreverification" ] == "vrai" ? true : false;
            membrePrevenir = dic[ "membreprevenir" ] == "vrai" ? true : false;
            membreConnexionPrevenir = dic[ "membreprevenirconnexion" ] == "vrai" ? true : false;

            colonnedroitevisible = dic[ "colonnedroitevisible" ] == "vrai" ? true : false;
            enpiedsitevisible = dic[ "enpiedsitevisible" ] == "vrai" ? true : false;

            virtualPath = dic[ "virtualpath" ] == null ? virtualPath : dic[ "virtualpath" ];

            contactsParPageMin = dic[ "contactsparpagemin" ] == null ? contactsParPageMin : dic[ "contactsparpagemin" ];
            contactsParPageMax = dic[ "contactsparpagemax" ] == null ? contactsParPageMax : dic[ "contactsparpagemax" ]; ;
            contactsParPageCourant = dic[ "contactsparpagecourant" ] == null ? contactsParPageCourant : dic[ "contactsparpagecourant" ]; ;

            rangpagesauteurmin = dic[ "rangpagesauteurmin" ] == null ? rangpagesauteurmin : dic[ "rangpagesauteurmin" ];
            rangpagesauteurmax = dic[ "rangpagesauteurmax" ] == null ? rangpagesauteurmax : dic[ "rangpagesauteurmax" ];

            htmlheader = dic[ "htmlheader" ];
        }

        public SettingXml Reload()
        {
            SettingXml sxml = new SettingXml();
            return sxml;
        }

        public void Save( SettingXml sxml )
        {
            StringDictionary dic = DataProviderXml.LoadXmlData( "Settings.xml" );

            dic[ "avatar" ] = avatar;

            dic[ "editeurhauteur" ] = editeurhauteur;
            dic[ "editeurelargir" ] = editeurelargir;
            dic[ "virtualpath" ] = textlabeldefaut;
            dic[ "virtualpath" ] = virtualPath;
            dic[ "adressewebmaster" ] = adresseWebmaster;
            dic[ "sujetcourrielmaj" ] = sujetcourrielmaj;
            dic[ "envoyermiseajour" ] = envoyerMiseAjour == true ? "vrai" : "faux";
            dic[ "labelboutonquestion" ] = labelBoutonQuestion;
            dic[ "loguser" ] = logUser == true ? "vrai" : "faux";
            dic[ "membreverification" ] = membreverification == true ? "vrai" : "faux";
            dic[ "membreprevenir" ] = membrePrevenir == true ? "vrai" : "faux";
            dic[ "membreprevenirconnexion" ] = membreConnexionPrevenir == true ? "vrai" : "faux";
            dic[ "contactsparpagemin" ] = contactsParPageMin;
            dic[ "contactsparpagemax" ] = contactsParPageMax;
            dic[ "contactsparpagecourant" ] = contactsParPageCourant;
            dic[ "colonnedroitevisible" ] = ColonneDroiteVisible == true ? "vrai" : "faux";
            dic[ "enpiedsitevisible" ] = enpiedsitevisible == true ? "vrai" : "faux";

            dic[ "htmlheader" ] = htmlheader;

            dic[ "rangpagesauteurmin" ] = rangpagesauteurmin; 
            dic[ "rangpagesauteurmax" ] = rangpagesauteurmax;

            XmlDataProvider.DataProviderXml.SaveXmlData( dic, "Settings.xml" );
        }

        private string avatar = "none";
        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }

        private string htmlheader = "";
        public string HtmlHeader
        {
            get { return htmlheader; }
            set { htmlheader = value; }
        }

        private bool colonnedroitevisible = false;
        public bool ColonneDroiteVisible
        {
            get { return colonnedroitevisible; }
            set { colonnedroitevisible = value; }
        }

        private bool enpiedsitevisible = false;
        public bool EnpiedSiteVisible
        {
            get { return enpiedsitevisible; }
            set { enpiedsitevisible = value; }
        }

        private string textlabeldefaut = "Bonjour et bienvenu(e) sur ce site";
        public string TextLabelDefaut
        {
            get { return textlabeldefaut; }
            set { textlabeldefaut = value; }
        }
        
        private string contactsParPageMin = "5";
        public string ContactsParPageMin
        {
            get { return contactsParPageMin; }
            set { contactsParPageMin = value; }
        }

        private string contactsParPageMax = "500";
        public string ContactsParPageMax
        {
            get { return contactsParPageMax; }
            set { contactsParPageMax = value; }
        }

        private string rangpagesauteurmin = "5";
        public string RangPagesAuteurMin
        {
            get { return rangpagesauteurmin; }
            set { rangpagesauteurmin = value; }
        }

        private string rangpagesauteurmax = "50";
        public string RangPagesAuteurMax
        {
            get { return rangpagesauteurmax; }
            set { rangpagesauteurmax = value; }
        }

        private string contactsParPageCourant = "50";
        public string ContactsParPageCourant
        {
            get { return contactsParPageCourant; }
            set { contactsParPageCourant = value; }
        }

        private string reponseTextuelleLigneMax = "500";
        public string ReponseTextuelleLigneMax
        {
            get { return reponseTextuelleLigneMax; }
            set { reponseTextuelleLigneMax = value; }
        }

        private string virtualPath = "~/";
        public string VirtualPath
        {
            get { return virtualPath; }
            set { virtualPath = value; }
        }

        private bool logUser = false;
        public bool LogUser
        {
            get { return logUser; }
            set { logUser = value; }
        }

        private bool membreverification = true;
        public bool MembreVerification
        {
            get { return membreverification; }
            set { membreverification = value; }
        }

        private bool membrePrevenir = true;
        public bool MembrePrevenir
        {
            get { return membrePrevenir; }
            set { membrePrevenir = value; }
        }

        private bool membreConnexionPrevenir = true;
        public bool MembreConnexionPrevenir
        {
            get { return membreConnexionPrevenir; }
            set { membreConnexionPrevenir = value; }
        }

        private string editeurhauteur = "200";
        public string EditeurHauteur
        {
            get { return editeurhauteur; }
            set { editeurhauteur = value; }
        }

        private string editeurelargir = "80";
        public string EditeurElargir
        {
            get { return editeurelargir; }
            set { editeurelargir = value; }
        }

        private string adresseWebmaster = "webmaster@monsite.com";
        public string AdresseWebmaster
        {
            get { return adresseWebmaster; }
            set { adresseWebmaster = value; }
        }

        private string sujetcourrielmaj = "Mise à jour";
        public string SujetCourrielMaj
        {
            get { return sujetcourrielmaj; }
            set { sujetcourrielmaj = value; }
        }

        private string labelBoutonQuestion = "Répondre";
        public string LabelBoutonQuestion
        {
            get { return labelBoutonQuestion; }
            set { labelBoutonQuestion = value; }
        }

        private bool envoyerMiseAjour = true;
        public bool EnvoyerMiseAjour
        {
            get { return envoyerMiseAjour; }
            set { envoyerMiseAjour = value; }
        }
    }
}