#region Using

using System;
using System.Web;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Specialized;

#endregion

namespace PageEngine
{
    /// <summary>
    /// Moteur de donnes qui utilise des fichiers XML.
    /// <remarks>
    /// To build another provider, you can just copy and modify
    /// this one. Then add it to the web.config's BlogEngine section.
    /// </remarks>
    /// </summary>
    public partial class XmlPagePostProvider //: BlogProvider
    {
        private static string _pageFolder = System.Web.HttpContext.Current.Server.MapPath( "~/App_Data/Pages/" /*BlogSettings.Instance.StorageLocation*/ );

        #region Pages

        /// <summary>
        /// Retrieves a Page from the data store.
        /// </summary>
        public static /*override*/ PagePost SelectPage( Guid id )
        {
            string fileName = _pageFolder + id.ToString() + ".xml";
            XmlDocument doc = new XmlDocument();
            doc.Load( fileName );

            PagePost page = new PagePost();

            page.Author = doc.SelectSingleNode( "page/author" ).InnerText;
            page.Menu = doc.SelectSingleNode( "page/menu" ).InnerText;
            page.Title = doc.SelectSingleNode( "page/title" ).InnerText;
            page.Description = doc.SelectSingleNode( "page/description" ).InnerText;
            page.Content = doc.SelectSingleNode( "page/content" ).InnerText;
            page.Keywords = doc.SelectSingleNode( "page/keywords" ).InnerText;

            if ( doc.SelectSingleNode( "page/parent" ) != null )
                page.Parent = new Guid( doc.SelectSingleNode( "page/parent" ).InnerText );

            if ( doc.SelectSingleNode( "page/isfrontpage" ) != null )
                page.IsFrontPage = bool.Parse( doc.SelectSingleNode( "page/isfrontpage" ).InnerText );

            if ( doc.SelectSingleNode( "page/showinlist" ) != null )
                page.ShowInList = bool.Parse( doc.SelectSingleNode( "page/showinlist" ).InnerText );

            if ( doc.SelectSingleNode( "page/ispublished" ) != null )
                page.IsPublished = bool.Parse( doc.SelectSingleNode( "page/ispublished" ).InnerText );

            if ( doc.SelectSingleNode( "page/isreserved" ) != null )
                page.IsReserved = bool.Parse( doc.SelectSingleNode( "page/isreserved" ).InnerText );

            if ( doc.SelectSingleNode( "page/rang" ) != null )
                page.Rang = int.Parse( doc.SelectSingleNode( "page/rang" ).InnerText );

            if ( doc.SelectSingleNode( "page/dayscommentsareenable" ) != null )
                page.DaysCommentsAreEnable = int.Parse( doc.SelectSingleNode( "page/dayscommentsareenable" ).InnerText );

            if ( doc.SelectSingleNode( "page/iscommentsenabled" ) != null )
                page.IsCommentsEnabled = bool.Parse( doc.SelectSingleNode( "page/iscommentsenabled" ).InnerText );

            if ( doc.SelectSingleNode( "page/iscommentvisible" ) != null )
                page.IsCommentsVisible = bool.Parse( doc.SelectSingleNode( "page/iscommentvisible" ).InnerText );

            if ( doc.SelectSingleNode( "page/istitlevisible" ) != null )
                page.IsTitleVisible = bool.Parse( doc.SelectSingleNode( "page/istitlevisible" ).InnerText );

            page.DateCreated = DateTime.Parse( doc.SelectSingleNode( "page/datecreated" ).InnerText, CultureInfo.InvariantCulture );
            page.DateModified = DateTime.Parse( doc.SelectSingleNode( "page/datemodified" ).InnerText, CultureInfo.InvariantCulture );

            // Comments
            foreach ( XmlNode node in doc.SelectNodes( "page/comments/comment" ) )
            {
                Comment comment = new Comment();
                comment.Id = new Guid( node.Attributes[ "id" ].InnerText );
                comment.Author = node.SelectSingleNode( "author" ).InnerText;
                comment.Email = node.SelectSingleNode( "email" ).InnerText;
                comment.Parent = page;

                if ( node.SelectSingleNode( "country" ) != null )
                    comment.Country = node.SelectSingleNode( "country" ).InnerText;

                if ( node.SelectSingleNode( "ip" ) != null )
                    comment.IP = node.SelectSingleNode( "ip" ).InnerText;

                if ( node.SelectSingleNode( "website" ) != null )
                {
                    Uri website;
                    if ( Uri.TryCreate( node.SelectSingleNode( "website" ).InnerText, UriKind.Absolute, out website ) )
                        comment.Website = website;
                }

                if ( node.Attributes[ "approved" ] != null )
                    comment.IsApproved = bool.Parse( node.Attributes[ "approved" ].InnerText );
                else
                    comment.IsApproved = true;

                comment.Content = node.SelectSingleNode( "content" ).InnerText;
                comment.DateCreated = DateTime.Parse( node.SelectSingleNode( "date" ).InnerText, CultureInfo.InvariantCulture );
                page.Comments.Add( comment );
            }

            page.Comments.Sort();

            return page;
        }

        /// <summary>
        /// Inserts a new Page to the data store.
        /// </summary>
        public static /*override*/ void InsertPage( PagePost page )
        {
            if ( !Directory.Exists( _pageFolder ) )
                Directory.CreateDirectory( _pageFolder );

            string fileName = _pageFolder + page.Id.ToString() + ".xml";
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using ( XmlWriter writer = XmlWriter.Create( fileName, settings ) )
            {
                writer.WriteStartDocument( true );
                writer.WriteStartElement( "page" );

                writer.WriteElementString( "author", page.Author );
                writer.WriteElementString( "menu", page.Menu );
                writer.WriteElementString( "title", page.Title );
                writer.WriteElementString( "description", page.Description );
                writer.WriteElementString( "content", page.Content );
                writer.WriteElementString( "keywords", page.Keywords );
                writer.WriteElementString( "parent", page.Parent.ToString() );
                writer.WriteElementString( "isfrontpage", page.IsFrontPage.ToString() );
                writer.WriteElementString( "showinlist", page.ShowInList.ToString() );
                writer.WriteElementString( "ispublished", page.IsPublished.ToString() );
                writer.WriteElementString( "isreserved", page.IsReserved.ToString() );
                writer.WriteElementString( "datecreated", page.DateCreated.AddHours( 0 /*-BlogSettings.Instance.Timezone*/ ).ToString( "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture ) );
                writer.WriteElementString( "datemodified", page.DateModified.AddHours( 0 /*-BlogSettings.Instance.Timezone*/ ).ToString( "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture ) );
                writer.WriteElementString( "rang", page.Rang.ToString() );
                writer.WriteElementString( "dayscommentsareenable", page.DaysCommentsAreEnable.ToString() );
                writer.WriteElementString( "iscommentsenabled", page.IsCommentsEnabled.ToString() );
                writer.WriteElementString( "iscommentvisible", page.IsCommentsVisible.ToString() );
                writer.WriteElementString( "istitlevisible", page.IsTitleVisible.ToString() );

                // Comments
                writer.WriteStartElement( "comments" );
                foreach ( Comment comment in page.Comments )
                {
                    writer.WriteStartElement( "comment" );
                    writer.WriteAttributeString( "id", comment.Id.ToString() );
                    writer.WriteAttributeString( "approved", comment.IsApproved.ToString() );
                    writer.WriteElementString( "date", comment.DateCreated.AddHours( 0 /*-BlogSettings.Instance.Timezone*/ ).ToString( "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture ) );
                    writer.WriteElementString( "author", comment.Author );
                    writer.WriteElementString( "email", comment.Email );
                    writer.WriteElementString( "country", comment.Country );
                    writer.WriteElementString( "ip", comment.IP );
                    if ( comment.Website != null )
                        writer.WriteElementString( "website", comment.Website.ToString() );
                    writer.WriteElementString( "content", comment.Content );
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Updates a Page.
        /// </summary>
        public static /*override*/ void UpdatePage( PagePost page )
        {
            InsertPage( page );
        }

        /// <summary>
        /// Deletes a page from the data store.
        /// </summary>
        public static /*override*/ void DeletePage( PagePost page )
        {
            string fileName = _pageFolder + page.Id.ToString() + ".xml";
            if ( File.Exists( fileName ) )
                File.Delete( fileName );

            if ( PagePost.Pages.Contains( page ) )
                PagePost.Pages.Remove( page );
        }

        /// <summary>
        /// Retrieves all pages from the data store
        /// </summary>
        /// <returns>List of Pages</returns>
        public static /*override*/ List<PagePost> FillPages()
        {
            List<PagePost> pages = new List<PagePost>();
            foreach ( string file in Directory.GetFiles( _pageFolder, "*.xml", SearchOption.TopDirectoryOnly ) )
            {
                FileInfo info = new FileInfo( file );
                string id = info.Name.Replace( ".xml", string.Empty );
                PagePost page = PagePost.Load( new Guid( id ) );
                
                // Traitement des tags
                if ( page.Keywords.Trim() != "" )
                {
                    string[] tags = page.Keywords.Split( ',' );
                    for ( int i = 0;i < tags.Length;i++ )
                    {
                        page.Tags.Add( tags[ i ].Trim() );
                    }
                }

                pages.Add( page );
            }
            return pages;
        }

        #endregion
    }
}
