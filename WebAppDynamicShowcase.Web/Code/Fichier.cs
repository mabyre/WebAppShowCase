// 
// Collection de fichiers pour les trier par date de dernier acces
// DateTime GetLastWriteTime( string path );
//
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

///<summary>
/// Description d'un Fichier
///</summary>

namespace Fichiers
{
    public class Fichier : IComparer<Fichier>
    {
        public Fichier()
        {
            _nom = "";
            _dateDerniereEcriture = Tools.DateInit;
        }

        public Fichier
        (
            string Nom,
            DateTime dateDerniereEcriture
        )
        {
            _nom = Nom;
            _dateDerniereEcriture = dateDerniereEcriture;
        }

        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private DateTime _dateDerniereEcriture;
        public DateTime DateDerniereEcriture
        {
            get { return _dateDerniereEcriture; }
            set { _dateDerniereEcriture = value; }
        }

        // Methode lie a IComparer<Fichier>
        public int Compare( Fichier x, Fichier y )
        {
            return x.DateDerniereEcriture.CompareTo( y.DateDerniereEcriture );
        }
    }

    public class FichierCollection : List<Fichier>
    {
        private List<Fichier> _collection = null;

        public FichierCollection()
        {
            _collection = new List<Fichier>();
        }

        public static FichierCollection GetAll( string dirName )
        {
            FichierCollection list = new FichierCollection();
            string[] fichiers = Directory.GetFiles( dirName );

            foreach ( string f in fichiers )
            {
                Fichier curr = new Fichier( f, File.GetLastWriteTime( f ) );
                list.Add( curr );
            }
            IComparer<Fichier> c = new Fichier();
            list.Sort( c );
            list.Reverse();
            return list;
        }
    }
}
