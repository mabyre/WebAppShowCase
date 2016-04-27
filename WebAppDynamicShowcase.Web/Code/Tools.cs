using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Web.Caching;
using System.Text.RegularExpressions;

public class Tools 
{
    /// <summary>
    /// Strips all illegal characters from the specified title.
    /// </summary>
    public static string RemoveIllegalCharacters( string text )
    {
        if ( string.IsNullOrEmpty( text ) )
            return text;

        text = text.Replace( ":", string.Empty );
        text = text.Replace( "/", string.Empty );
        text = text.Replace( "?", string.Empty );
        text = text.Replace( "#", string.Empty );
        text = text.Replace( "[", string.Empty );
        text = text.Replace( "]", string.Empty );
        text = text.Replace( "@", string.Empty );
        text = text.Replace( ".", string.Empty );
        text = text.Replace( "\"", string.Empty );
        text = text.Replace( "&", string.Empty );
        text = text.Replace( " ", "-" );

        return HttpUtility.UrlEncode( text ).Replace( "%", string.Empty );
    }

    public static string PageErreurPath
    {
        get
        {
            // BRY20022016
            //return HttpContext.Current.Request.ApplicationPath + "/PageErreur.Aspx?msg=";
            return "~/PageErreur.Aspx?msg=";
        }
    }

	public static string DateTimeFormat
	{
		get
		{
			return SafeConfigString("appSettings", "defaultDateFormat", string.Empty) + " "
				+  SafeConfigString("appSettings", "defaultTimeFormat", string.Empty);
		}
	}

	public static string DateFormat
	{
		get
		{
			return SafeConfigString("appSettings", "defaultDateFormat", string.Empty);
		}
	}

	public static string TimeFormat
	{
		get
		{
			return SafeConfigString("appSettings", "defaultTimeFormat", string.Empty);
		}
	}

	public static DateTime DateInit
	{
		get 
		{
			return new DateTime(2000, 1, 1, 0, 0, 0);
		}
	}
	
	public static DateTime DateTimeNull
	{
		get 
		{
			return new DateTime( 1, 1, 1 );
		}
	}

    static public String DatabaseConnectionString
    {
        get
        {
            string connexion = System.Configuration.ConfigurationManager.ConnectionStrings[ "QuestionnaireDB" ].ConnectionString;
            return connexion;
        }
    }
    
    public static string SafeConfigString( string configSection, string configKey, string defaultValue ) 
	{
		NameValueCollection configSettings = ConfigurationSettings.GetConfig( configSection ) as NameValueCollection;
		if ( configSettings != null ) 
		{
			string configValue = configSettings[configKey] as string;
			if ( configValue != null ) 
			{
				return configValue;
			}
		}

		return defaultValue;
	}

	public static String GetFileNameUnique(String s)
	{
		String file = Path.GetDirectoryName(s) + "\\" + Path.GetFileNameWithoutExtension(s);
		String ext = Path.GetExtension(s);
		int i = 2;
		while (File.Exists(s))
		{
			s = file + "-" + i.ToString() + ext;
			i++;
		}
		return s;
	}

	public enum ObjectCreateStatus 
	{ 
		UnknownFailure, 
		Created, 
		DuplicateName, 
	}

	static public bool FileToString( string filename, ref string loadText )
	{
		loadText = "";

		FileStream fsIn;

		try
		{
			fsIn = new FileStream( filename, FileMode.Open );
		}
		catch (Exception)
		{
			return false;
		}

		try
		{
			if (fsIn.CanRead == false)
			{
				fsIn.Close();
				return false;
			}
		}
		catch (Exception)
		{
			fsIn.Close();
			return false;
		}

		StreamReader sr;

		try
		{
			sr = new StreamReader(fsIn, System.Text.Encoding.UTF8);
		}
		catch (Exception)
		{
			fsIn.Close();
			return false;
		}

		try
		{
			loadText = sr.ReadToEnd();
		}
		catch (Exception)
		{
			sr.Close();
			fsIn.Close();
			return false;
		}

		sr.Close();
		fsIn.Close();

		return true;
	}

    // J'ai decouvert le bit dans la BD SqlServer dont je me servais jamais
    // et pour cause ya qu'a voir le code si dessous ...
    // J'ajoute en plus les valeurs Vrai et Faux
    static public string StringToBoolean( string str )
    {
        if ( str.ToLower() != "true"
             && str.ToLower() != "false"
             && str.ToLower() != "0"
             && str.ToLower() != "1"
             && str.ToLower() != "vrai"
             && str.ToLower() != "faux" )
        {
            return null;
        }
        else
        {
            if ( str == "vrai" || str == "1" )
                return "True";
            if ( str == "faux" || str == "0" )
                return "False";
        }
        return str;
    }

    // Faire disparaitre c'est putains de valeurs de merde dans la BD
    static public string StringBooleanFalseToNull( string str )
    {
        if ( str.ToLower() == "false"
             || str.ToLower() == "0" )
        {
            return null;
        }

        return str;
    }

    static public void StringToFile( string s, ref string filename )
	{
		if (filename.Trim() == "")
			filename = System.IO.Path.GetTempFileName();
		FileStream oFs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
		StreamWriter oWriter = new StreamWriter(oFs);
		
		oWriter.Write(s);
		oWriter.Flush();
		oWriter.Close();

		oFs.Close();
	}

	public const String _appSettingsPrefix = "";

	public static string FormFieldErrorNotificationColor
	{
		get 
		{
			return "#F0F0EC";
		}
	}

	public static string LanguageCode
	{
		get 
		{
			return SafeConfigString("appSettings", "languageCode", string.Empty);
		}
	}

	public static string AppMessage(string messageKey)
	{
		return SafeConfigString("appSettings", messageKey, string.Empty);
	}

	public static StringCollection StringToList(string str)
	{
		StringCollection stringList = new StringCollection();
		if (str != null && str.Length > 0)
		{
			if (str[str.Length-1] != ';')
				str += ";";
		
			while(str.Length > 0)
			{
				int i = str.IndexOf(';');
				string str2 = str.Substring(0, i);

				stringList.Add(str2);
				str = str.Substring(i+1);
			}
		}
		return stringList;
	}

    // Attention str.Replace( "<br>", "\n" ); transforme \n en \r\n !!!!!!!!!!!
	static public string StringToUnix( string str )
	{
		string r = str.Replace( "\r\n", "\n" );
		return r;
	}

	static public string StringHTMLToUnix( string str )
	{
		string r = str.Replace( "<br>", "\n" );
		return r;
	}

    static public string StringTexteToHTML( string str )
	{
		string r = str.Replace( "\r\n", "<br>" );
		r = r.Replace( "\n", "<br>" );
		return r;
	}

	static public string StringHTMLToTexte( string str )
	{
		string r = str.Replace( "<br>", "\r\n" );
		return r;
	}

	static public bool StringSearchWord( string str, string mot )
	{
		bool trouve = false;
	                
		for ( int i = 0; i < str.Length; i++ )
		{
			if ( String.Compare( mot, 0, str, i, mot.Length ) == 0 )
			{
				trouve = true;
			}
			if ( trouve == true ) break;
		}
		return trouve;
	}

    // Attention elle ne parche pas bien, elle est la pour le framework 1.1
	static public string[] SplitString( string separator, string str )
	{
        char[] separ = separator.ToCharArray();
        string[] retour;
        ArrayList retourAL = new ArrayList();

		for ( int i = 0; i < str.Length; i++ )
		{
			if ( String.Compare( separator, 0, str, i, separator.Length ) == 0 )
			{ 
                int dc = i + separator.Length; // debut_chaine
                ArrayList sAL = new ArrayList();
                while ( str[ dc ] != separ[ 0 ] )
                {
                    sAL.Add( str[ dc ] );
                    dc += 1;
                    if ( dc >= str.Length )
                    {
                        i = dc;
                        break;
                    }
                }

                // Mort de rire ! Quel genre de connerie on est pas amene a ecrire !!!
                // putain j'en ai bave pour ecrire cette merde vive le 2.0
                IEnumerator e = sAL.GetEnumerator();
                string s = "";
    			while( e.MoveNext() ) 
                {
                    s += (char)e.Current;
                }
                    
                retourAL.Add( s );
			}
		}
        
        retour = ( string[] )retourAL.ToArray( typeof( string ) );
        return retour;
	}
}