//
// Issu de BLogEngine.NET on supprime la couche provider en remplacant
// BlogService par XmlPagePostProvider, en supprimant override des methodes dont on n'herite plus
//
#region Using

using System;
using System.Web;
using System.Collections.Generic;
using System.Collections.ObjectModel; // pour faire les tags et le TagCloud
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Net.Mail;
using SmtpServeurXmlProvider;

#endregion

namespace PageEngine
{
    /// <summary>
    /// A page is much like a post, but is not part of the
    /// blog chronology and is more static in nature.
    /// <remarks>
    /// Pages can be used for "About" pages or other static
    /// information.
    /// </remarks>
    /// </summary>
    [Serializable]
    public class PagePost : BusinessObjectBase<PagePost, Guid>, IComparable<PagePost>, IPublishable
    {
        #region Constructor

        /// <summary>
        /// The contructor sets default values.
        /// </summary>
        public PagePost()
        {
            base.Id = Guid.NewGuid();
            _Rang = 0;
            DateCreated = DateTime.Now;
            _DaysCommentsAreEnable = 0;
            _Tags = new Collection<string>(); // les mots clefs
            _Comments = new List<Comment>();
            _IsPublished = true;
            _IsCommentsEnabled = true;
        }

        #endregion

        #region Properties

        private string _Menu;
        /// <summary>
        /// Afficher dans le menu
        /// </summary>
        public string Menu
        {
            get { return _Menu; }
            set
            {
                if ( _Menu != value ) MarkDirty( "Menu" );
                _Menu = value;
            }
        }

        private string _Title;
        /// <summary>
        /// Gets or sets the Title or the object.
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                if ( _Title != value ) MarkDirty( "Title" );
                _Title = value;
            }
        }

        private string _Content;
        /// <summary>
        /// Gets or sets the Description or the object.
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set
            {
                if ( _Content != value ) MarkDirty( "Content" );
                _Content = value;
            }
        }

        private string _Description;
        /// <summary>
        /// Gets or sets the Description or the object.
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if ( _Description != value ) MarkDirty( "Description" );
                _Description = value;
            }
        }

        // Pour construire le Tag Cloud, on va y mettre les mots cles
        private Collection<string> _Tags;
        public Collection<string> Tags
        {
            get { return _Tags; }
            set
            {
                if ( _Tags != value ) MarkDirty( "Tags" );
                _Tags = value;
            }
        }
            
        private string _Keywords;
        /// <summary>
        /// Gets or sets the Keywords or the object.
        /// </summary>
        public string Keywords
        {
            get { return _Keywords; }
            set
            {
                if ( _Keywords != value ) MarkDirty( "Keywords" );
                _Keywords = value;
            }
        }

        private Guid _Parent;
        /// <summary>
        /// Gets or sets the parent of the Page. It is used to construct the 
        /// hierachy of the pages.
        /// </summary>
        public Guid Parent
        {
            get { return _Parent; }
            set
            {
                if ( _Parent != value ) MarkDirty( "Parent" );
                _Parent = value;
            }
        }

        private string _Slug;
        /// <summary>
        /// Gets or sets the Slug of the Page.
        /// A Slug is the relative URL used by the pages.
        /// </summary>
        public string Slug
        {
            get
            {
                if ( string.IsNullOrEmpty( _Slug ) )
                    return Utils.RemoveIllegalCharacters( Title );

                return _Slug;
            }
            set { _Slug = value; }
        }

        private bool _IsPublished;
        /// <summary>
        /// Gets or sets whether or not this page should be published.
        /// </summary>
        public bool IsPublished
        {
            get { return _IsPublished; }
            set
            {
                if ( _IsPublished != value ) MarkDirty( "IsPublished" );
                _IsPublished = value;
            }
        }

        /// <summary>
        /// Gets whether or not this page should be shown FAUX
        /// Ne sert que pour la compatibilite avec : IPublishable
        /// </summary>
        /// <value></value>
        public bool IsVisible
        {
            get { return IsPublished; }
        }

        private bool _IsFrontPage;
        /// <summary>
        /// Gets or sets whether or not this page should be displayed on the front page.
        /// </summary>
        public bool IsFrontPage
        {
            get { return _IsFrontPage; }
            set
            {
                if ( _IsFrontPage != value ) MarkDirty( "IsFrontPage" );
                _IsFrontPage = value;
            }
        }

        private int _Rang;
        /// <summary>
        /// Rang de la page pour les trier
        /// </summary>
        public int Rang
        {
            get { return _Rang; }
            set
            {
                if ( _Rang != value ) MarkDirty( "Rang" );
                _Rang = value;
            }
        }

        private bool _ShowInList;
        /// <summary>
        /// Gets or sets whether or not this page should be in the sitemap list.
        /// </summary>
        public bool ShowInList
        {
            get { return _ShowInList; }
            set
            {
                if ( _ShowInList != value ) MarkDirty( "ShowInList" );
                _ShowInList = value;
            }
        }

        /// <summary>
        /// A relative-to-the-site-root path to the post.
        /// Only for in-site use.
        /// </summary>
        public string RelativeLink
        {
            get
            {
                string slug = Utils.RemoveIllegalCharacters( Slug ) + ".aspx"; // BlogSettings.Instance.FileExtension;
                return Utils.RelativeWebRoot + "page/" + slug;
            }
        }

        /// <summary>
        /// The absolute link to the post.
        /// </summary>
        public Uri AbsoluteLink
        {
            get { return Utils.ConvertToAbsolute( RelativeLink ); }
        }

        private static object _SyncRoot = new object();
        private static List<PagePost> _Pages;
        /// <summary>
        /// Gets list of all pages.
        /// </summary>
        public static List<PagePost> Pages
        {
            get
            {
                if ( _Pages == null )
                {
                    lock ( _SyncRoot )
                    {
                        if ( _Pages == null )
                        {
                            _Pages = XmlPagePostProvider.FillPages();
                            _Pages.Sort( PagePost.CompareRang );
                        }
                    }
                }

                return _Pages;
            }
        }

        // Comparateur pour le methode Sort
        static int CompareRang( PagePost x, PagePost y )
        {
            return x.Rang.CompareTo( y.Rang );
        }

        public static void Sort()
        {
            _Pages.Sort( PagePost.CompareRang );
        }

        // Traitement des tags
        public void RemakeTags( ref PagePost page )
        {
            page.Tags.Clear();
            if ( page.Keywords.Trim() != "" )
            {
                string[] tags = page.Keywords.Split( ',' );
                for ( int i = 0;i < tags.Length;i++ )
                {
                    page.Tags.Add( tags[ i ].Trim() );
                }
            }
        }

        /// <summary>
        /// Returns a page based on the specified id.
        /// </summary>
        public static PagePost GetPage( Guid id )
        {
            foreach ( PagePost page in Pages )
            {
                if ( page.Id == id )
                    return page;
            }

            return null;
        }

        /// <summary>
        /// Returns the front page if any is available.
        /// </summary>
        public static PagePost GetFrontPage()
        {
            foreach ( PagePost page in Pages )
            {
                if ( page.IsFrontPage )
                    return page;
            }

            return null;
        }

        String IPublishable.Author
        {
            get { return "c'est bibi"; /*BlogSettings.Instance.AuthorName;*/ }
        }

        //List<Category> IPublishable.Categories
        //{
        //    get { return null; }
        //}

        #endregion

        #region PropertiesPost

        private string _Author = "";
        /// <summary>
        /// Gets or sets the Author or the post.
        /// </summary>
        public string Author
        {
            get { return _Author; }
            set
            {
                if ( _Author != value ) MarkDirty( "Author" );
                _Author = value;
            }
        }

        private StringCollection _NotificationEmails;
        /// <summary>
        /// Gets a collection of email addresses that is signed up for 
        /// comment notification on the specific post.
        /// </summary>
        public StringCollection NotificationEmails
        {
            get
            {
                if ( _NotificationEmails == null )
                    _NotificationEmails = new StringCollection();

                return _NotificationEmails;
            }
        }

        private readonly List<Comment> _Comments;
        /// <summary>
        /// A collection of Approved comments for the post sorted by date.
        /// </summary>
        public List<Comment> ApprovedComments
        {
            get
            {
                return _Comments.FindAll( delegate( Comment obj )
                {
                    return obj.IsApproved;
                } );
            }
        }

        /// <summary>
        /// A collection of comments waiting for approval for the post, sorted by date.
        /// </summary>
        public List<Comment> NotApprovedComments
        {
            get
            {
                return _Comments.FindAll( delegate( Comment obj )
                {
                    return !obj.IsApproved;
                } );
            }
        }
        /// <summary>
        /// A Collection of Approved Comments for the post
        /// </summary>
        public List<Comment> Comments
        {
            get { return _Comments; }

        }

        // Afficher le titre dans la Page
        private bool _IsTitleVisible;
        public bool IsTitleVisible
        {
            get { return _IsTitleVisible; }
            set
            {
                if ( _IsTitleVisible != value ) MarkDirty( "IsTitleVisible" );
                _IsTitleVisible = value;
            }
        }

        // Page reservee aux membres
        private bool _IsReserved;
        public bool IsReserved
        {
            get { return _IsReserved; }
            set
            {
                if ( _IsReserved != value ) MarkDirty( "IsReserved" );
                _IsReserved = value;
            }
        }

        private bool _IsCommentsEnabled;
        /// <summary>
        /// Gets or sets the EnableComments or the object.
        /// </summary>
        public bool IsCommentsEnabled
        {
            get { return _IsCommentsEnabled; }
            set
            {
                if ( _IsCommentsEnabled != value ) MarkDirty( "IsCommentsEnabled" );
                _IsCommentsEnabled = value;
            }
        }

        private bool _IsCommentsVisible;
        /// <summary>
        /// Cacher les commentaires.
        /// </summary>
        public bool IsCommentsVisible
        {
            get { return _IsCommentsVisible; }
            set
            {
                if ( _IsCommentsVisible != value ) MarkDirty( "IsCommentsVisible" );
                _IsCommentsVisible = value;
            }
        }

        private int _DaysCommentsAreEnable;
        /// <summary>
        /// Nombre de jours ou les commentaires sont ouverts
        /// 0 : toujours ouverts
        /// </summary>
        public int DaysCommentsAreEnable
        {
            get { return _DaysCommentsAreEnable; }
            set
            {
                if ( _DaysCommentsAreEnable != value ) MarkDirty( "DaysCommentsAreEnable" );
                _DaysCommentsAreEnable = value;
            }
        }

        private float _Rating;
        /// <summary>
        /// Gets or sets the rating or the post.
        /// </summary>
        public float Rating
        {
            get { return _Rating; }
            set
            {
                if ( _Rating != value ) MarkDirty( "Rating" );
                _Rating = value;
            }
        }

        private int _Raters;
        /// <summary>
        /// Gets or sets the number of raters or the object.
        /// </summary>
        public int Raters
        {
            get { return _Raters; }
            set
            {
                if ( _Raters != value ) MarkDirty( "Raters" );
                _Raters = value;
            }
        }
        #endregion

        #region MethodPost

        /// <summary>
        /// Adds a comment to the collection and saves the post.
        /// </summary>
        /// <param name="comment">The comment to add to the post.</param>
        public void AddComment( Comment comment )
        {
            CancelEventArgs e = new CancelEventArgs();
            OnAddingComment( comment, e );
            if ( e.Cancel == false )
            {
                Comments.Add( comment );
                DataUpdate();
                OnCommentAdded( comment );
                SendNotifications( comment );
            }
        }

        private void SendNotifications( Comment comment )
        {
            SmtpServeurXml serveur = new SmtpServeurXml();

            // Pour l'admin
            MailMessage mailAdmin = new MailMessage();
            mailAdmin.From = new MailAddress( serveur.UserEmail, serveur.UserName );
            mailAdmin.Subject = Global.SettingsXml.SujetCourrielMaj + "Nouveau commentaire";
            mailAdmin.Body = "Commenté par " + comment.Author + Environment.NewLine + Environment.NewLine;
            mailAdmin.Body += comment.Content + Environment.NewLine + Environment.NewLine + AbsoluteLink.ToString();
            mailAdmin.To.Add( serveur.AdminEmail );
            Courriel.SendMailMessageAsync( mailAdmin );

            if ( NotificationEmails.Count == 0 )
                return;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress( serveur.UserEmail, serveur.UserName );
            mail.Subject = Global.SettingsXml.SujetCourrielMaj + "Nouveau commentaire";
            mail.Body = "Commenté par " + comment.Author + Environment.NewLine + Environment.NewLine;
            mail.Body += comment.Content + Environment.NewLine + Environment.NewLine + AbsoluteLink.ToString();

            foreach ( string email in NotificationEmails )
            {
                if ( email != comment.Email )
                {
                    mail.To.Clear();
                    mail.To.Add( email );
                    Courriel.SendMailMessageAsync( mail );
                }
            }
        }

        /// <summary>
        /// Removes a comment from the collection and saves the post.
        /// </summary>
        /// <param name="comment">The comment to remove from the post.</param>
        public void RemoveComment( Comment comment )
        {
            CancelEventArgs e = new CancelEventArgs();
            OnRemovingComment( comment, e );
            if ( !e.Cancel )
            {
                Comments.Remove( comment );
                DataUpdate();
                OnCommentRemoved( comment );
                comment = null;
            }
        }

        /// <summary>
        /// Approves a Comment for publication.
        /// </summary>
        /// <param name="comment">The Comment to approve</param>
        public void ApproveComment( Comment comment )
        {
            CancelEventArgs e = new CancelEventArgs();
            Comment.OnApproving( comment, e );
            if ( !e.Cancel )
            {
                int inx = Comments.IndexOf( comment );
                Comments[ inx ].IsApproved = true;
                this.DateModified = comment.DateCreated;
                this.DataUpdate();
                Comment.OnApproved( comment );
            }
        }

        /// <summary>
        /// Approves all the comments in a post.  Included to save time on the approval process.
        /// </summary>
        public void ApproveAllComments()
        {
            foreach ( Comment comment in Comments )
            {
                ApproveComment( comment );
            }
        }

        #endregion

        #region Base overrides

        /// <summary>
        /// Validates the properties on the Page.
        /// </summary>
        protected override void ValidationRules()
        {
            AddRule( "Menu", "Menu doit être défini", string.IsNullOrEmpty( Menu ) );
            AddRule( "Content", "Content must be set", string.IsNullOrEmpty( Content ) );
        }

        /// <summary>
        /// Retrieves a page form the BlogProvider
        /// based on the specified id.
        /// </summary>
        protected override PagePost DataSelect( Guid id )
        {
            return XmlPagePostProvider.SelectPage( id );
        }

        /// <summary>
        /// Updates the object in its data store.
        /// </summary>
        protected override void DataUpdate()
        {
            XmlPagePostProvider.UpdatePage( this );
        }

        /// <summary>
        /// Inserts a new page to current BlogProvider.
        /// </summary>
        protected override void DataInsert()
        {
            XmlPagePostProvider.InsertPage( this );

            if ( IsNew )
                Pages.Add( this );
        }

        /// <summary>
        /// Deletes the page from the current BlogProvider.
        /// </summary>
        protected override void DataDelete()
        {
            XmlPagePostProvider.DeletePage( this );
            if ( Pages.Contains( this ) )
                Pages.Remove( this );
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }

        #endregion

        #region IComparable<Page> Members

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the 
        /// objects being compared. The return value has the following meanings: 
        /// Value Meaning Less than zero This object is less than the other parameter.Zero 
        /// This object is equal to other. Greater than zero This object is greater than other.
        /// </returns>
        public int CompareTo( PagePost other )
        {
            return other.DateCreated.CompareTo( this.DateCreated );
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the page is being served to the output stream.
        /// </summary>
        public static event EventHandler<ServingEventArgs> Serving;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        public static void OnServing( PagePost page, ServingEventArgs arg )
        {
            if ( Serving != null )
            {
                Serving( page, arg );
            }
        }

        /// <summary>
        /// Raises the Serving event
        /// </summary>
        public void OnServing( ServingEventArgs arg )
        {
            if ( Serving != null )
            {
                Serving( this, arg );
            }
        }

        #endregion

        #region EventsPost

        /// <summary>
        /// Occurs before a new comment is added.
        /// </summary>
        public static event EventHandler<CancelEventArgs> AddingComment;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnAddingComment( Comment comment, CancelEventArgs e )
        {
            if ( AddingComment != null )
            {
                AddingComment( comment, e );
            }
        }

        /// <summary>
        /// Occurs when a comment is added.
        /// </summary>
        public static event EventHandler<EventArgs> CommentAdded;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnCommentAdded( Comment comment )
        {
            if ( CommentAdded != null )
            {
                CommentAdded( comment, new EventArgs() );
            }
        }

        /// <summary>
        /// Occurs before comment is removed.
        /// </summary>
        public static event EventHandler<CancelEventArgs> RemovingComment;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnRemovingComment( Comment comment, CancelEventArgs e )
        {
            if ( RemovingComment != null )
            {
                RemovingComment( comment, e );
            }
        }

        /// <summary>
        /// Occurs when a comment has been removed.
        /// </summary>
        public static event EventHandler<EventArgs> CommentRemoved;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnCommentRemoved( Comment comment )
        {
            if ( CommentRemoved != null )
            {
                CommentRemoved( comment, new EventArgs() );
            }
        }

        /// <summary>
        /// Occurs when a visitor rates the post.
        /// </summary>
        public static event EventHandler<EventArgs> Rated;
        /// <summary>
        /// Raises the event in a safe way
        /// </summary>
        protected virtual void OnRated( XmlPagePostProvider post )
        {
            if ( Rated != null )
            {
                Rated( post, new EventArgs() );
            }
        }
        #endregion

    }
}
