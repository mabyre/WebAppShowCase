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
using System.Reflection;
using System.Security.Permissions;

public class Strings
{
    /// <summary>
    /// str == null || str == string.Empty
    /// </summary>
    static public bool IsNullOrEmpty( string str )
    {
        if ( str == null || str == string.Empty )
            return true;
        else
            return false;
    }

	static public string TexteToHTML( string str )
	{
		string r = str.Replace( "\r\n", "<br/>" );
		r = r.Replace( "\n", "<br/>" );
		r = r.Replace( "\r", "<br/>" );
		return r;
	}

	static public string HTMLToTexte( string str )
	{
		string r = str.Replace( "<br/>", "\r\n" );
		return r;
	}

    static public string ByteArrayToString( byte[] ba )
	{
        string s = "";
        for ( int i = 0 ; i < ba.Length ; i ++ )
            s += ba[ i ].ToString();

        return s;
	}
    
    // Sous entendu le tableau byte est un tableau de char
    static public string ByteArrayInString( byte[] ba )
	{
        string s = "";
        for ( int i = 0 ; i < ba.Length ; i ++ )
            s += (char)ba[ i ];

        return s;
	}

    static public string CharArrayToString( char[] ca )
	{
        string s = "";
        for ( int i = 0 ; i < ca.Length ; i ++ )
            s += ca[ i ].ToString();

        return s;
	}

    static public void CharArrayToByteArray( ref byte[] ba, char[] ca )
	{
        for ( int i = 0 ; i < ca.Length ; i ++ )
            ba[ i ] = (byte)ca[ i ];
	}

	// Rechercher "mot" dans la chaine "str"
	static public bool SearchWord( string str, string mot )
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

    // Rechercher le "mot" dans la string "str" indiquer la position "pos" du mot
	static public bool SearchWord( string str, string mot, ref int pos )
	{
		bool trouve = false;

	    pos = 0;            
		for ( int i = 0; i < str.Length; i++ )
		{
			if ( String.Compare( mot, 0, str, i, mot.Length ) == 0 )
			{
				trouve = true;
			}
			if ( trouve == true ) 
            {
                pos = i;
                break;
            }
		}

		return trouve;
	}

    // Indiquer si le "car" est present dans la string "str" 
    static public bool SearchCar( char car , string str )
    {
        bool trouve = false;
        char[] strInChar = str.ToCharArray();

        for ( int i = 0; i < str.Length; i++ )
        {
            if ( car == strInChar[ i ] )
            {
                trouve = true;
                break;
            }
        }
        return trouve;
    }

	// Remplacer les caracteres par le code Ascii
	// pour trouver un code copier le caractere et passer en mode hexa
	// ya moyen de faire autrement mais faut trouver un truc
	// dans le bordel de l'aide en ligne sur la string ...
	static public string FiltrerASCII( string stringEntree )
	{
		string[,] change = new string[,] 
		{ 
			{"\r\n","%0D%0A"},
			{"ç","%E7"},
			{"\"","%22"},
			{"@","%40"},
			{"à","%E0"}, {"â","%E2"}, {"ä","%E4"}, 
			{"é","%E9"}, {"è","%E8"}, {"ê","%EA"}, {"ê","%EA"}, {"ë","%EB"}, 
			{"ï","%EF"}, {"î","%EE"},
			{"ô","%F4"}, {"ö","%F6"}, 
			{"ù","%F9"}, {"û","%F9"}, {"ü","%FB"}                        
		};

		if ( stringEntree == null )
			return "";
            
		string filtre = stringEntree;
		for ( int i = 0; i < change.Length/2; i++ )
		{
			filtre = filtre.Replace( change[ i, 0 ], change[ i, 1 ] );
		}

		return filtre;
	}

    // Supprimer ce qui n'est pas le nom du fichier dans le path
	static public string GetFileName( string filePath )
	{
        string[] name = filePath.Split( '\\' );
        return name[ name.Length - 1 ];
	}

    // retourne le nombre de ligne dans str
    static public int Lignes( string str, string separator )
    {
        string[] _separator = new string[ 1 ];
        _separator[ 0 ] = separator;
        string[] ligne = str.Split( _separator, StringSplitOptions.None );
        return ligne.Length;
    }

    // Format la taille d'un fichier
    public static string FileSizeFormat( long size, string formatString )
    {
        if ( size < 1024 )
            return size.ToString( formatString ) + " octets";

        if ( size < Math.Pow( 1024, 2 ) )
            return ( size / 1024 ).ToString( formatString ) + " Ko";

        if ( size < Math.Pow( 1024, 3 ) )
            return ( size / Math.Pow( 1024, 2 ) ).ToString( formatString ) + " Mo";

        if ( size < Math.Pow( 1024, 4 ) )
            return ( size / Math.Pow( 1024, 3 ) ).ToString( formatString ) + " Go";

        return size.ToString( formatString );
    }
}