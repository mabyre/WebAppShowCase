using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

public sealed class Utils
{
    /// <summary>
    /// L'url du site web
    /// ne me demandez pas comment ca marche
    /// </summary>
    public static string WebSiteUri
    {
        get
        {
            return HttpContext.Current.Request.Url.AbsoluteUri.Substring( 0, HttpContext.Current.Request.Url.AbsoluteUri.Length - HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.Substring( 1 ).Length );
        }
    }

    public static Control FindControlRecursively( string controlID, ControlCollection controls )
    {
        if ( controlID == null || controls == null )
            return null;

        foreach ( Control c in controls )
        {
            if ( c.ID == controlID )
                return c;

            if ( c.HasControls() )
            {
                Control inner = FindControlRecursively( controlID, c.Controls );
                if ( inner != null )
                    return inner;
            }
        }
        return null;
    }

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
        text = text.Replace( "'", string.Empty );
        text = text.Replace( " ", "-" );
        text = RemoveDiacritics( text );
        text = RemoveExtraHyphen( text );

        return HttpUtility.UrlEncode( text ).Replace( "%", string.Empty );
    }

    private static string RemoveExtraHyphen( string text )
    {
        if ( text.Contains( "--" ) )
        {
            text = text.Replace( "--", "-" );
            return RemoveExtraHyphen( text );
        }

        return text;
    }

    private static String RemoveDiacritics( string text )
    {
        String normalized = text.Normalize( NormalizationForm.FormD );
        StringBuilder sb = new StringBuilder();

        for ( int i = 0;i < normalized.Length;i++ )
        {
            Char c = normalized[ i ];
            if ( CharUnicodeInfo.GetUnicodeCategory( c ) != UnicodeCategory.NonSpacingMark )
                sb.Append( c );
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the relative root of the website.
    /// </summary>
    /// <value>A string that ends with a '/'.</value>
    public static string RelativeWebRoot
    {
        get { return VirtualPathUtility.ToAbsolute( Global.SettingsXml.VirtualPath ); }
    }

    private static Uri _AbsoluteWebRoot;

    /// <summary>
    /// Gets the absolute root of the website.
    /// </summary>
    /// <value>A string that ends with a '/'.</value>
    public static Uri AbsoluteWebRoot
    {
        get
        {
            if ( _AbsoluteWebRoot == null )
            {
                HttpContext context = HttpContext.Current;
                if ( context == null )
                    throw new System.Net.WebException( "The current HttpContext is null" );

                _AbsoluteWebRoot = new Uri( context.Request.Url.Scheme + "://" + context.Request.Url.Authority + RelativeWebRoot );
            }
            return _AbsoluteWebRoot;
        }
    }
    /// <summary>
    /// Converts a relative URL to an absolute one.
    /// </summary>
    public static Uri ConvertToAbsolute( Uri relativeUri )
    {
        return ConvertToAbsolute( relativeUri.ToString() ); ;
    }

    /// <summary>
    /// Converts a relative URL to an absolute one.
    /// </summary>
    public static Uri ConvertToAbsolute( string relativeUri )
    {
        if ( String.IsNullOrEmpty( relativeUri ) )
            throw new ArgumentNullException( "relativeUri" );

        string absolute = AbsoluteWebRoot.ToString();
        int index = absolute.LastIndexOf( RelativeWebRoot.ToString() );

        return new Uri( absolute.Substring( 0, index ) + relativeUri );
    }
}
