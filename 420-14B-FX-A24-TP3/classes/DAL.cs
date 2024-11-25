using _420_14B_FX_A24_TP3.classes;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace _420_14B_FX_TP3_A23.classes
{
    public static class DAL
    {
        #region CONSTANTES

        private const string APPSETTINGS_FILE = "appsettings.json";
        private const string CONNECTION_STRING = "DefaultConnection";
        private const string IMAGE_PATH = "images:path";

        #endregion

        #region ATTRIBUT

        private static IConfiguration _configuration;

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Constructeur static permettant de charger les configurations de l'application
        /// </summary>
        static DAL()
        {
            //todo: implementer le constructeur de DAL
            throw new NotImplementedException();

        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir liste des catégories provenant de la base de données triée par nom croissant
        /// </summary>
        /// <returns>Liste de Categorie</returns>
        public static List<Categorie> ObtenirListeCategories()
        {
            //todo : Implémenter ObtenirListeCategories
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet d'obtenir un produit à partir de son code
        /// </summary>
        /// <param name="code">Code unique du produit</param>
        /// <returns>Le produit trouvé. Sinon null.</returns>
        public static Produit ObtenirProduit(string code)
        {

            //todo : Implémenter ObtenirProduit
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet l'ajout d'un produit à la base de données
        /// </summary>
        /// <param name="produit">Produit à ajouter</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul.</exception>
        public static void AjouterProduit(Produit produit)
        {
            //todo : Implémenter AjouterProduit
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet d'obtenir la liste des produits dans la base de données selon certains critères de recherche.
        /// triés par nom croissant
        /// </summary>
        /// <param name="nomProduit">Partie du nom du produit à chercher</param>
        /// <param name="categorie">Catégorie du produit</param>
        /// <returns>La liste des produits trouvés selon les critères de recherche. Si aucun critère n'est spécifié alors on retourne tous les produits</returns>
        /// <remarks>La liste des produits est toujours triée en ordre croissant de nom.</remarks>
        public static List<Produit> ObtenirListeProduits(string nomProduit = "", Categorie categorie = null)
        {
            //todo : Implémenter ObtenirListeProduits
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet d'obtenir un produit dans la base de données à partir de son identifiant unique
        /// </summary>
        /// <param name="id">Identifiant unique du produit</param>
        /// <returns>Le produit trouvé ou Null si aucun produit trouvé.</returns>
        public static Produit ObtenirProduit(uint id)
        {

            ////todo : Implémenter ObtenirProduit
            throw new NotImplementedException();
        }


        /// <summary>
        /// Permet de modifier un produit dans la base de donnnées
        /// </summary>
        /// <param name="produit">Produit à modifier</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul</exception>
        /// <exception cref="InvalidOperationException">Lancée lorsque le produit n'existe pas dans la base de donnée.</exception>
        public static void ModifierProduit(Produit produit)
        {
            //todo : Implémenter ModifierProduit
            throw new NotImplementedException();

        }


        /// <summary>
        /// Permet de supprimer un produit dans la base de donnée
        /// </summary>
        /// <param name="produit">Le produite à supprimer</param>
        /// <remarks>L'image du produit est également supprimée</remarks>
        /// <exception cref="ArgumentNullException">Lancée lorsque le produit est null</exception>
        /// <exception cref="System.InvalidOperationException">Lancée lorque le produit existe dans au moins une facture</exception>
        /// <returns>Vrai si le produit a été supprimé, faux sinon</returns>

        public static bool SupprimerProduit(Produit produit)
        {

            //todo : Implémenter SupprimerProduit
            throw new NotImplementedException();

        }


        /// <summary>
        /// Permet d'ajouter une facture dans la base de données
        /// </summary>
        /// <param name="facture">facture à ajouter</param>
        /// <exception cref="System.ArgumentNullException">Lancée si la facture est nulle.</exception>
        /// <exception cref="System.ArgumentNullException">Lancée si la liste des produitsFacture est nulle ou vide.</exception>
        public static void AjouterFacture(Facture facture)
        {
            //todo : Implémenter AjouterFacture
            throw new NotImplementedException();
        }

        /// <summary>
        /// Permet d'obtenir une facture dans la base de donnée à partir de son identifiant unique
        /// </summary>
        /// <param name="idFacture">Identifant uniquie de la facture</param>
        /// <remarks>Les produits faisant partie de la facture doivent être ajouté à la facture.</remarks>
        /// <returns>La facture trouvée. Null si aucune facture n'est trouvée</returns>
        public static Facture ObtenirFacture(uint idFacture)
        {

            //todo : Implémenter ObtenirFacture
            throw new NotImplementedException();
        }

        #endregion
    }
}
