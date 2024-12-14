using System;
using System.ComponentModel;

namespace _420_14B_FX_A24_TP3.classes
{
    /// <summary>
    /// Classe représentant un produit
    /// </summary>
    public class Produit
    {

        #region CONSTANTES

        public const int CODE_NB_CARAC_MAX = 10;
        public const int CODE_NB_CARAC_MIN = 5;
        public const int NOM_NB_CARAC_MAX = 30;
        public const int NOM_NB_CARAC_MIN = 3;
        public const decimal PRIX_MIN_VAL = 0;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Identifiant unique du produit
        /// </summary>
        private uint _id;

        /// <summary>
        /// Code du produit
        /// </summary>
        private string _code;

        /// <summary>
        ///Nom du produit
        /// </summary>
        private string _nom;

        /// <summary>
        /// Catégorie du produit
        /// </summary>
        private Categorie _categorie;

       
        /// <summary>
        /// Prix de vente du produit
        /// </summary>
        private decimal _prix;

        /// <summary>
        /// Nom de l'image du produit
        /// </summary>
        private string _image;

        /// <summary>
        /// Obtient ou définit l'indentifiant unique du produit.
        /// </summary>
        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient ou définit le code du produit.
        /// </summary>
        /// <remarks>Le code doit toujours être en majuscule et sans espaces inutiles</remarks>
        /// <exception cref="System.ArgumentException">Lancée lorsque le code est nul ou vide</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">/// Lancée lorsque le code ne contient pas entre <see cref="CODE_NB_CARC_MIN"/> et <see cref="CODE_NB_CARC_MAX"/> caractères.</exception>

        public string Code
        {
            get { return _code; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Code), "Le code ne doit pas être nul ou des espaces vides.");
                }
                if (value.Trim().Length > CODE_NB_CARAC_MAX || value.Trim().Length < CODE_NB_CARAC_MIN)
                {
                    throw new ArgumentOutOfRangeException(nameof(Code), $"Le code doit être entre {CODE_NB_CARAC_MIN} et {CODE_NB_CARAC_MAX} caractères.");
                }
                _code = value.Trim().ToUpper();
            }
        }

        /// <summary>
        /// Obtient ou définit le nom du produit
        /// </summary>
        /// <remarks>Le nom ne doit pas contenir d'espaces inutiles</remarks>
        /// <exception cref="System.ArgumentException">Lancée lorsque le nom est nul ou vide</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque le nom ne contient pas entre <see cref="NOM_NB_CARC_MIN"/> et <see cref="NOM_NB_CARC_MAX"/> </exception>
        public string Nom
        {
            get { return _nom; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Nom), "Le nom ne doit pas être nul ou des espaces vides.");
                }
                if (value.Trim().Length > NOM_NB_CARAC_MAX|| value.Trim().Length < NOM_NB_CARAC_MIN)
                {
                    throw new ArgumentOutOfRangeException(nameof(Nom), $"Le nom doit être entre {NOM_NB_CARAC_MIN} et {NOM_NB_CARAC_MAX} caractères.");
                }
                _nom = value.Trim();
            }
        }

        /// <summary>
        /// Obtient ou définit la catérorie à laquelle appartient le produit
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque la catégorie est nulle</exception>
        public Categorie Categorie
        {
            get { return _categorie; }
            set 
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Categorie), "La catégorie ne doit pas être nulle.");
                }
                _categorie = value;
            }
        }


        /// <summary>
        /// Obtient ou définit le prix de vente du produit
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancées lorsque le prix est inférieur ou égale à <see cref="PRIX_MIN_VAL"/></exception>
        public decimal Prix
        {
            get { return _prix; }
            set
            {
                if (value <= PRIX_MIN_VAL)
                {
                    throw new ArgumentOutOfRangeException(nameof(Prix), $"Le prix doit être au moins {PRIX_MIN_VAL}$.");
                }
                _prix = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le nom du fichier image du produit.
        /// </summary>
        /// <exception cref="System.ArgumentException">Lancée lorsque l'image est nulle, vide ou ne contenir que des espaces</exception>
        public String Image
        {
            get { return _image; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Image), "Le nom du fichier image ne doit pas être nul ou des espaces vides.");
                }
                _image = value.Trim();
            }
        }

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Permet de construire un nouveau produit
        /// </summary>
        /// <param name="code">identifiant unique du produit</param>
        /// <param name="nom">Nom du produit</param>
        /// <param name="categorie">Catégorie du produit</param>
        /// <param name="prix">Prix de vente du produit</param>
        /// <param name="image">Nom du fichier image du produit</param>
        public Produit(uint id, string code, String nom, Categorie categorie, decimal prix, string image)
        {
            Id = id;
            Code = code;
            Nom = nom;
            Categorie = categorie;
            Prix = prix;
            Image = image;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir une représentation du produit.
        /// </summary>
        /// <returns>Un représentation de ***** sous forme de chaîne de caractères.</returns>
        public override string ToString()
        {
            return $"{Code} {Nom} {Prix}";
        }

        /// <summary>
        /// Permet de vérifier si deux objets de type Produit sont égaux.
        /// </summary>
        /// <param name="obj">Objet de type Produit à comparer avec l'objet courant</param>
        /// <returns>true si les deux objets sont égaux i.e que leur code sont identique; false autrement.</returns>
        public override bool Equals(Object? obj)
        {
            if (obj is Produit other)
            {
                return Code == other.Code;
            }
            return false;
        }

        /// <summary>
        /// Définition de l'opérateur d'égalité entre 2 produits
        /// </summary>
        /// <param name="a">Produit à gauche de l'opérateur</param>
        /// <param name="b">Produit à droite de l'opérateur</param>
        /// <returns>True si égals, false sinon</returns>
        public static bool operator ==(Produit a, Produit b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return a.Code == b.Code && a.Nom == b.Nom && a.Categorie == b.Categorie && a.Prix == b.Prix && a.Image == b.Image;
        }
        

        /// <summary>
        /// Définition de l'opérateur non égale entre 2 produits
        /// </summary>
        /// <param name="a">Produit à gauche de l'opérateur</param>
        /// <param name="b">Produit à droite de l'opérateur</param>
        /// <returns>True si différents, false sinon</returns>
        public static bool operator !=(Produit a, Produit b)
        {
            if (ReferenceEquals(a, b))
            {
                return false;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return true;
            }
            return !(a.Code == b.Code && a.Nom == b.Nom && a.Categorie == b.Categorie && a.Prix == b.Prix && a.Image == b.Image);
        }

        #endregion
    }
} 