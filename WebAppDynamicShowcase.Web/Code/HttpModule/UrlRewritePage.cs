#region Using

using System;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;

#endregion

namespace PageEngine.Web.HttpModules
{
    /// <summary>
    /// On intercept l'evement BeginRequest au niveau de l'application
    /// Handles pretty URL's and redirects them to the permalinks.
    /// </summary>
    public class UrlRewritePage : IHttpModule
    {
        #region IHttpModule Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public void Init( HttpApplication application )
        {
            application.BeginRequest += new EventHandler( Application_BeginRequest );
        }

        #endregion

        /// <summary>
        /// Handles the BeginRequest event of the context control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = ( ( HttpApplication )sender ).Context;
            if ( context.Request.RawUrl.ToLowerInvariant().Contains( ".aspx" ) )
            {
                if ( context.Request.RawUrl.ToLowerInvariant().Contains( "/page/" ) )
                {
                    RewritePage( context );
                }
            }
        }

        private static void RewritePage( HttpContext context )
        {
            string title = ExtractTitle( context );
            title = Tools.RemoveIllegalCharacters( title ).ToLowerInvariant();
            foreach ( PagePost page in PagePost.Pages )
            {
                string legalTitle = Tools.RemoveIllegalCharacters( page.Slug ).ToLowerInvariant();
                if ( title.Equals( legalTitle ) )
                {
                    context.RewritePath( "~/page.aspx?id=" + page.Id + GetQueryString( context ), false );
                    break;
                }
            }
        }

        /// <summary>
        /// Extracts the title from the requested URL.
        /// </summary>
        private static string ExtractTitle( HttpContext context )
        {
            int index = context.Request.RawUrl.ToLowerInvariant().LastIndexOf( "/" ) + 1;
            int stop = context.Request.RawUrl.ToLowerInvariant().LastIndexOf( ".aspx" /*BlogSettings.Instance.FileExtension*/ );
            string title = context.Request.RawUrl.Substring( index, stop - index ).Replace( ".aspx" /*BlogSettings.Instance.FileExtension*/, string.Empty );
            return context.Server.UrlEncode( title );
        }

        /// <summary>
        /// Gets the query string from the requested URL.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        private static string GetQueryString( HttpContext context )
        {
            string query = context.Request.QueryString.ToString();
            if ( !string.IsNullOrEmpty( query ) )
                return "&" + query;

            return string.Empty;
        }
    }
}