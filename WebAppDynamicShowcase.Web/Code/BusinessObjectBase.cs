#region Using

using System;
using System.Text;
using System.Collections.Specialized;
using System.Globalization;

#endregion


/// <summary>
/// This is the base class from which most business objects will be derived. 
/// To create a business object, inherit from this class.
/// </summary>
/// <typeparam name="TYPE">The type of the derived class.</typeparam>
/// <typeparam name="KEY">The type of the Id property.</typeparam>
[Serializable]
public abstract class BusinessObjectBase<TYPE, KEY> : IDisposable where TYPE : BusinessObjectBase<TYPE, KEY>, new()
{

    #region Properties

    private KEY _Id;
    /// <summary>
    /// Gets the unique Identification of the object.
    /// </summary>
    public KEY Id
    {
        get { return _Id; }
        set { _Id = value; }
    }

    private DateTime _DateCreated = DateTime.MinValue;
    /// <summary>
    /// The date on which the instance was created.
    /// </summary>
    public DateTime DateCreated
    {
        get
        {
            if ( _DateCreated == DateTime.MinValue )
                return _DateCreated;

            return _DateCreated.AddHours( 0 /*BlogSettings.Instance.Timezone*/ );
        }
        set { _DateCreated = value; }
    }

    private DateTime _DateModified = DateTime.MinValue;
    /// <summary>
    /// The date on which the instance was modified.
    /// </summary>
    public DateTime DateModified
    {
        get
        {
            if ( _DateModified == DateTime.MinValue )
                return _DateModified;

            return _DateModified.AddHours( 0 /*BlogSettings.Instance.Timezone*/ );
        }
        set { _DateModified = value; }
    }

    #endregion

    #region IsNew, IsDeleted, IsDirty

    private bool _IsNew = true;
    /// <summary>
    /// Gets if this is a new object, False if it is a pre-existing object.
    /// </summary>
    public bool IsNew
    {
        get { return _IsNew; }
    }

    private bool _IsDeleted;
    /// <summary>
    /// Gets if this object is marked for deletion.
    /// </summary>
    public bool IsDeleted
    {
        get { return _IsDeleted; }
    }

    private bool _IsDirty = true;
    /// <summary>
    /// Gets if this object's data has been changed.
    /// </summary>
    public virtual bool IsDirty
    {
        get { return _IsDirty; }
    }

    /// <summary>
    /// Marks the object for deletion. It will then be 
    /// deleted when the object's Save() method is called.
    /// </summary>
    public void Delete()
    {
        _IsDeleted = true;
        _IsDirty = true;
    }

    private StringCollection _DirtyProperties = new StringCollection();
    /// <summary>
    /// A collection of the properties that have 
    /// been marked as being dirty.
    /// </summary>
    protected StringCollection DirtyProperties
    {
        get { return _DirtyProperties; }
    }

    /// <summary>
    /// Marks an object as being dirty, or changed.
    /// </summary>
    /// <param name="propertyName">The name of the property to mark dirty.</param>
    protected void MarkDirty( string propertyName )
    {
        _IsDirty = true;
        if ( !_DirtyProperties.Contains( propertyName.ToLowerInvariant() ) )
        {
            _DirtyProperties.Add( propertyName.ToLowerInvariant() );
        }
        OnMarkedDirty();
    }

    /// <summary>
    /// Marks the object as being an clean, 
    /// which means not dirty.
    /// </summary>
    public void MarkOld()
    {
        _IsDirty = false;
        _IsNew = false;
        _DirtyProperties.Clear();
    }

    /// <summary>
    /// Check whether or not the specified property has been changed
    /// </summary>
    /// <param name="propertyName">The name of the property to check.</param>
    protected bool IsPropertyDirty( string propertyName )
    {
        return DirtyProperties.Contains( propertyName.ToLowerInvariant() );
    }

    /// <summary>
    /// Check whether or not the specified properties has been changed
    /// </summary>
    /// <param name="propertyNames">The names of the properties to check.</param>
    /// <returns>True if all of the specified properties have been changed.</returns>
    protected bool IsPropertyDirty( string[] propertyNames )
    {
        foreach ( string name in propertyNames )
        {
            if ( !DirtyProperties.Contains( name.ToLowerInvariant() ) )
            {
                return false;
            }
        }

        return true;
    }

    #endregion

    #region Validation

    private StringDictionary _BrokenRules = new StringDictionary();

    /// <summary>
    /// Add or remove a broken rule.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <param name="errorMessage">The description of the error</param>
    /// <param name="isBroken">True if the validation rule is broken.</param>
    protected void AddRule( string propertyName, string errorMessage, bool isBroken )
    {
        if ( isBroken )
        {
            _BrokenRules[ propertyName ] = errorMessage;
        }
        else
        {
            if ( _BrokenRules.ContainsKey( propertyName ) )
            {
                _BrokenRules.Remove( propertyName );
            }
        }
    }

    /// <summary>
    /// Reinforces the business rules by adding additional rules to the 
    /// broken rules collection.
    /// </summary>
    protected abstract void ValidationRules();

    /// <summary>
    /// Gets whether the object is valid or not.
    /// </summary>
    public bool IsValid
    {
        get
        {
            ValidationRules();
            return this._BrokenRules.Count == 0;
        }
    }

    /// /// <summary>
    /// If the object has broken business rules, use this property to get access
    /// to the different validation messages.
    /// </summary>
    public string ValidationMessage
    {
        get
        {
            ValidationRules();
            StringBuilder sb = new StringBuilder();
            foreach ( string messages in this._BrokenRules.Values )
            {
                sb.AppendLine( messages );
            }

            return sb.ToString();
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Loads an instance of the object based on the Id.
    /// </summary>
    /// <param name="id">The unique identifier of the object</param>
    public static TYPE Load( KEY id )
    {
        TYPE instance = new TYPE();
        instance = instance.DataSelect( id );
        instance.Id = id;

        if ( instance != null )
        {
            instance.MarkOld();
            return instance;
        }

        return null;
    }

    /// <summary>
    /// Saves the object to the database.
    /// </summary>
    virtual public void Save()
    {
        if ( !IsValid && !IsDeleted )
            throw new InvalidOperationException( ValidationMessage );

        if ( IsDisposed && !IsDeleted )
            throw new InvalidOperationException( string.Format( CultureInfo.InvariantCulture, "You cannot save a disposed {0}", this.GetType().Name ) );

        if ( IsDirty )
        {
            Update();
        }
    }

    /// <summary>
    /// Is called by the save method when the object is old and dirty.
    /// </summary>
    private void Update()
    {
        SaveAction action = SaveAction.None;

        if ( this.IsDeleted )
        {
            if ( !this.IsNew )
            {
                action = SaveAction.Delete;
                OnSaving( this, action );
                DataDelete();
            }
        }
        else
        {
            if ( this.IsNew )
            {
                if ( _DateCreated == DateTime.MinValue )
                    _DateCreated = DateTime.Now;

                _DateModified = DateTime.Now;
                action = SaveAction.Insert;
                OnSaving( this, action );
                DataInsert();
            }
            else
            {
                this._DateModified = DateTime.Now; ;
                action = SaveAction.Update;
                OnSaving( this, action );
                DataUpdate();
            }

            MarkOld();
        }

        OnSaved( this, action );
    }

    #endregion

    #region Data access

    /// <summary>
    /// Retrieves the object from the data store and populates it.
    /// </summary>
    /// <param name="id">The unique identifier of the object.</param>
    /// <returns>True if the object exists and is being populated successfully</returns>
    protected abstract TYPE DataSelect( KEY id );

    /// <summary>
    /// Updates the object in its data store.
    /// </summary>
    protected abstract void DataUpdate();

    /// <summary>
    /// Inserts a new object to the data store.
    /// </summary>
    protected abstract void DataInsert();

    /// <summary>
    /// Deletes the object from the data store.
    /// </summary>
    protected abstract void DataDelete();

    #endregion

    #region IDisposable

    private bool _IsDisposed;
    /// <summary>
    /// Gets or sets if the object has been disposed.
    /// <remarks>
    /// If the objects is disposed, it must not be disposed a second
    /// time. The IsDisposed property is set the first time the object
    /// is disposed. If the IsDisposed property is true, then the Dispose()
    /// method will not dispose again. This help not to prolong the object's
    /// life if the Garbage Collector.
    /// </remarks>
    /// </summary>
    protected bool IsDisposed
    {
        get { return _IsDisposed; }
    }

    /// <summary>
    /// Disposes the object and frees ressources for the Garbage Collector.
    /// </summary>
    /// <param name="disposing">If true, the object gets disposed.</param>
    protected virtual void Dispose( bool disposing )
    {
        if ( this.IsDisposed )
            return;

        if ( disposing )
        {
            _DirtyProperties.Clear();
            _BrokenRules.Clear();
            _IsDisposed = true;
        }
    }

    /// <summary>
    /// Disposes the object and frees ressources for the Garbage Collector.
    /// </summary>
    public void Dispose()
    {
        Dispose( true );
        GC.SuppressFinalize( this );
    }

    #endregion

    #region Equality overrides

    /// <summary>
    /// A uniquely key to identify this particullar instance of the class
    /// </summary>
    /// <returns>A unique integer value</returns>
    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    /// <summary>
    /// Comapares this object with another
    /// </summary>
    /// <param name="obj">The object to compare</param>
    /// <returns>True if the two objects as equal</returns>
    public override bool Equals( object obj )
    {
        if ( obj == null )
        {
            return false;
        }

        if ( obj.GetType() == this.GetType() )
        {
            return obj.GetHashCode() == this.GetHashCode();
        }

        return false;
    }

    /// <summary>
    /// Checks to see if two business objects are the same.
    /// </summary>
    public static bool operator ==( BusinessObjectBase<TYPE, KEY> first, BusinessObjectBase<TYPE, KEY> second )
    {
        if ( Object.ReferenceEquals( first, second ) )
        {
            return true;
        }

        if ( ( object )first == null || ( object )second == null )
        {
            return false;
        }

        return first.GetHashCode() == second.GetHashCode();
    }

    /// <summary>
    /// Checks to see if two business objects are different.
    /// </summary>
    public static bool operator !=( BusinessObjectBase<TYPE, KEY> first, BusinessObjectBase<TYPE, KEY> second )
    {
        return !( first == second );
    }

    #endregion

    #region Events

    /// <summary>
    /// Occurs when the class is Saved
    /// </summary>
    public static event EventHandler<SavedEventArgs> Saved;
    /// <summary>
    /// Raises the Saved event.
    /// </summary>
    protected static void OnSaved( BusinessObjectBase<TYPE, KEY> businessObject, SaveAction action )
    {
        if ( Saved != null )
        {
            Saved( businessObject, new SavedEventArgs( action ) );
        }
    }

    /// <summary>
    /// Occurs when the class is Saved
    /// </summary>
    public static event EventHandler<SavedEventArgs> Saving;
    /// <summary>
    /// Raises the Saving event
    /// </summary>
    protected static void OnSaving( BusinessObjectBase<TYPE, KEY> businessObject, SaveAction action )
    {
        if ( Saving != null )
        {
            Saving( businessObject, new SavedEventArgs( action ) );
        }
    }

    /// <summary>
    /// Occurs when this instance is marked dirty. 
    /// It means the instance has been changed but not saved.
    /// </summary>
    public event EventHandler<EventArgs> MarkedDirty;
    /// <summary>
    /// Raises the MarkedDirty event safely.
    /// </summary>
    protected virtual void OnMarkedDirty()
    {
        if ( MarkedDirty != null )
        {
            MarkedDirty( this, new EventArgs() );
        }
    }

    #endregion

}
