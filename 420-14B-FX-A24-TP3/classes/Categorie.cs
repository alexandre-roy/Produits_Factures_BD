using System.Globalization;

namespace _420_14B_FX_A24_TP3.classes
{
    /// <summary>
    /// Classe représentant une catégorie
    /// </summary>
    public class Categorie
    {
        #region ATTRIBUTS

        /// <summary>
        /// Identifiant unique de la catégorie
        /// </summary>
        private uint _id;

        /// <summary>
        /// Nom de la catégorie
        /// </summary>
        private string _nom;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient ou définit l'identifiant unique de la catégorie
        /// </summary>
        public uint Id
        {
            get { return _id; }
            set { _id = value; }
        }


        /// <summary>
        /// Obtient ou définit le nom de la catégorie
        /// </summary><
        /// <exception cref="System.ArgumentException">Lancée lorsque le nom et nul ou vide</exception>
        public string Nom
        {
            get { return _nom; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Nom), "Le nom ne doit pas être nul ou des espaces vides.");
                }
                _nom = value.Trim();
            }
        }

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Constructeur paramètré
        /// </summary>
        /// <param name="id">Identifiant de la catégorie</param>
        /// <param name="nom">Nom de la catégorie</param>
        public Categorie(uint id, string nom)
        {
            Nom = nom;
            Id = id;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Représentation de l'objet sous forme de chaîne de carcatère.
        /// </summary>
        /// <returns>Retourne le nom de la catégorie</returns>
        public override string ToString()
        {
            return Nom;
        }

        /// <summary>
        /// Permet de vérifier si deux objets de type Categorie sont égaux.
        /// </summary>
        /// <param name="obj">Objet de type Categorie à comparer avec l'objet courant</param>
        /// <returns>true si les deux objets sont égaux; false, autrement.</returns>
        /// <remarks>Deux catégories sont égales si elle ont le même nom.</remarks>
        public override bool Equals(Object obj)
        {
            if (obj is Categorie other)
            {
                int comparaison = string.Compare(Nom, other.Nom, CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase);

                if (comparaison == 0)
                {
                    return true;
                }              
            }
            return false;
        }

        /// <summary>
        /// Définition de l'opérateur d'égalité entre deux catégories
        /// </summary>
        /// <param name="a">Categorie à gauche de l'opérateur</param>
        /// <param name="b">Categorie à droite de l'opérateur</param>
        /// <returns>True si égals, false sinon</returns>
        public static bool operator ==(Categorie a, Categorie b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            return string.Compare(a.Nom, b.Nom, CultureInfo.InvariantCulture,
            CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

        /// <summary>
        /// Définition de l'opérateur non égale entre deux catégories
        /// </summary>
        /// <param name="a">Categorie à gauche de l'opérateur</param>
        /// <param name="b">Categorie à droite de l'opérateur</param>
        /// <returns>True si différents, false sinon</returns>
        public static bool operator !=(Categorie a, Categorie b)
        {
            if (ReferenceEquals(a, b))
            {
                return false;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return true;
            }
            return !(string.Compare(a.Nom, b.Nom, CultureInfo.InvariantCulture,
            CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0);
        }

        #endregion
    }
}
