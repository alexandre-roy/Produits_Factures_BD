
using System;



namespace _420_14B_FX_A24_TP3.classes
{
    /// <summary>
    /// Classe représentant un produit
    /// </summary>
    public class Produit
    {

      

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
                //Todo: Implémenter validation code produit.
                _code = value;
            }

        }

        /// <summary>
        /// Obtient ou définit le nom du produit
        /// </summary>
        /// <remarks>Le nom ne doit pas contenir d'espaces inutiles</remarks>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le nom est nul ou vide</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque le nom ne contient pas entre <see cref="NOM_NB_CARC_MIN"/> et <see cref="NOM_NB_CARC_MAX"/> </exception>
        public string Nom
        {
            get { return _nom; }
            set 
            {
                //todo: implémenter validation nom;
                _nom = value;
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
                //todo: implémenter validation catégorie
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
                //todo: implémenter validation prix
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
                //todo: implémenter validation image
                _image = value;


            }
        }

   

  
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
            //todo: implémenter Constructeur Produit
            throw new NotImplementedException();
        }

      

  

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
            //todo : Implémenter Equals pour Produit
            throw new NotImplementedException();


        }

        /// <summary>
        /// Définition de l'opérateur d'égalité entre 2 produits
        /// </summary>
        /// <param name="a">Produit à gauche de l'opérateur</param>
        /// <param name="b">Produit à droite de l'opérateur</param>
        /// <returns>True si égals, false sinon</returns>
        public static bool operator ==(Produit a, Produit b)
        {
            //todo : Implémenter == pour Produit
            throw new NotImplementedException();
        }

        /// <summary>
        /// Définition de l'opérateur non égale entre 2 produits
        /// </summary>
        /// <param name="a">Produit à gauche de l'opérateur</param>
        /// <param name="b">Produit à droite de l'opérateur</param>
        /// <returns>True si différents, false sinon</returns>
        public static bool operator !=(Produit a, Produit b)
        {
            //todo : Implémenter != pour Produit
            throw new NotImplementedException();
        }





    } 
} 