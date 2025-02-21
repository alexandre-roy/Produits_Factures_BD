﻿using _420_14B_FX_A24_TP3.classes;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace _420_14B_FX_TP3_A23.classes
{
    public static class DAL
    {
        #region CONSTANTES

        /// <summary>
        /// Fichier appsettings
        /// </summary>
        public const string APPSETTINGS_FILE = "appsettings.json";

        /// <summary>
        /// Chaine de caractères pour la connection
        /// </summary>
        private const string CONNECTION_STRING = "DefaultConnection";

        /// <summary>
        /// Chaine de caractères pour le chemin des images
        /// </summary>
        public const string IMAGE_PATH = "images:path";

        #endregion

        #region ATTRIBUT

        /// <summary>
        /// Connection
        /// </summary>
        private static IConfiguration _configuration;

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Constructeur static permettant de charger les configurations de l'application
        /// </summary>
        static DAL()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(APPSETTINGS_FILE, false, true).Build();
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'obtenir liste des catégories provenant de la base de données triée par nom croissant
        /// </summary>
        /// <returns>Liste de Categorie</returns>
        public static List<Categorie> ObtenirListeCategories()
        {
            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            List<Categorie> categories = new List<Categorie>();

            try
            {
                cn.Open();

                string requete = "SELECT Id, Nom FROM categories ORDER BY Nom";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categorie categorie = new Categorie(dr.GetUInt32(0), dr.GetString(1));

                    categories.Add(categorie);
                }
                dr.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return categories;
        }

        /// <summary>
        /// Permet d'obtenir un produit à partir de son code
        /// </summary>
        /// <param name="code">Code unique du produit</param>
        /// <returns>Le produit trouvé. Sinon null.</returns>
        public static Produit ObtenirProduit(string code)
        {
            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            Produit produit = null;

            try
            {
                cn.Open();

                string requete = $"SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE Code = '{code}'";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    uint categorieId = dr.GetUInt32(5);

                    List<Categorie> categories = ObtenirListeCategories();

                    Categorie categorie = null;

                    foreach (Categorie c in categories)
                    {
                        if(c.Id == categorieId)
                        {
                            categorie = c;
                        }
                    }

                    produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), categorie, dr.GetDecimal(3), dr.GetString(4));
                }
                dr.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return produit;
        }

        /// <summary>
        /// Permet l'ajout d'un produit à la base de données
        /// </summary>
        /// <param name="produit">Produit à ajouter</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul.</exception>
        public static void AjouterProduit(Produit produit)
        {
            if (produit is null)
            {
                throw new ArgumentNullException(nameof(produit), "Le produit ne doit pas être nul");
            }

            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            try
            {
                cn.Open();

                string requete = $"INSERT INTO produits(Code, Nom, Prix, Image, IdCategorie) VALUES(@code, @nom, @prix, @image, @categorie)";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@code", produit.Code);
                cmd.Parameters.AddWithValue("@nom", produit.Nom);                
                cmd.Parameters.AddWithValue("@prix", produit.Prix);
                cmd.Parameters.AddWithValue("@image", produit.Image);
                cmd.Parameters.AddWithValue("@categorie", produit.Categorie.Id);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }
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
            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            List<Produit> produits = new List<Produit>();

            try
            {
                cn.Open();

                string requete = "";

                if (string.IsNullOrWhiteSpace(nomProduit) && categorie.Nom != "Toutes")
                {
                    requete = $"SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE IdCategorie = {categorie.Id} ORDER BY Nom";
                }
                else if (!string.IsNullOrWhiteSpace(nomProduit) && categorie.Nom != "Toutes")
                {
                    requete = $"SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE Nom LIKE '%{nomProduit}%' AND IdCategorie = {categorie.Id} ORDER BY Nom";
                }
                else if (!string.IsNullOrWhiteSpace(nomProduit) && categorie.Nom == "Toutes")
                {
                    requete = $"SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE Nom LIKE '%{nomProduit}%' ORDER BY Nom";
                }
                else
                {
                    requete = "SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits ORDER BY Nom";
                }

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    uint categorieId = dr.GetUInt32(5);

                    List<Categorie> categories = ObtenirListeCategories();

                    Categorie categorieItem = null;

                    foreach (Categorie c in categories)
                    {
                        if (c.Id == categorieId)
                        {
                            categorieItem = c;
                        }
                    }

                    Produit produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), categorieItem, dr.GetDecimal(3), dr.GetString(4));

                    produits.Add(produit);
                }
                dr.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return produits;
        }

        /// <summary>
        /// Permet d'obtenir un produit dans la base de données à partir de son identifiant unique
        /// </summary>
        /// <param name="id">Identifiant unique du produit</param>
        /// <returns>Le produit trouvé ou Null si aucun produit trouvé.</returns>
        public static Produit ObtenirProduit(uint id)
        {
            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            Produit produit = null;

            try
            {
                cn.Open();

                string requete = $"SELECT Id, Code, Nom, Prix, Image, IdCategorie FROM produits WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", id);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    uint categorieId = dr.GetUInt32(5);

                    List<Categorie> categories = ObtenirListeCategories();

                    Categorie categorie = null;

                    foreach (Categorie c in categories)
                    {
                        if (c.Id == categorieId)
                        {
                            categorie = c;
                        }
                    }

                    produit = new Produit(dr.GetUInt32(0), dr.GetString(1), dr.GetString(2), categorie, dr.GetDecimal(3), dr.GetString(4));
                }
                dr.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return produit;
        }


        /// <summary>
        /// Permet de modifier un produit dans la base de donnnées
        /// </summary>
        /// <param name="produit">Produit à modifier</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul</exception>
        /// <exception cref="InvalidOperationException">Lancée lorsque le produit n'existe pas dans la base de donnée.</exception>
        public static void ModifierProduit(Produit produit)
        {
            if (produit is null)
            {
                throw new ArgumentNullException(nameof(produit), "Le produit ne doit pas être nul");
            }

            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            try
            {
                cn.Open();

                string requeteExiste = "SELECT COUNT(Id) FROM produits WHERE produits.id = @id";

                string requete = "UPDATE produits SET Code = @code, Nom = @nom, Prix = @prix, Image = @image, IdCategorie = @categorie WHERE produits.Id = @id";

                MySqlCommand cmdExiste = new MySqlCommand(requeteExiste, cn);

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmdExiste.Parameters.AddWithValue("@id", produit.Id);

                cmd.Parameters.AddWithValue("@id", produit.Id);
                cmd.Parameters.AddWithValue("@code", produit.Code);
                cmd.Parameters.AddWithValue("@nom", produit.Nom);
                cmd.Parameters.AddWithValue("@prix", produit.Prix);
                cmd.Parameters.AddWithValue("@image", produit.Image);
                cmd.Parameters.AddWithValue("@categorie", produit.Categorie.Id);

                int existe = Convert.ToInt32(cmdExiste.ExecuteScalar());

                if (existe == 0)
                {
                    throw new InvalidOperationException("Le produit n'existe pas dans la base de donnée.");
                }

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }
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
            if (produit is null)
            {
                throw new ArgumentNullException(nameof(produit), "Le produit ne doit pas être nul");
            }

            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            try
            {
                cn.Open();

                string requeteFacture = "DELETE FROM produitsfactures WHERE IdProduit = @id";

                MySqlCommand comd = new MySqlCommand(requeteFacture, cn);

                comd.Parameters.AddWithValue("@id", produit.Id);

                comd.ExecuteNonQuery();

                string requete = "DELETE FROM produits WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", produit.Id);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }


        /// <summary>
        /// Permet d'ajouter une facture dans la base de données
        /// </summary>
        /// <param name="facture">facture à ajouter</param>
        /// <exception cref="System.ArgumentNullException">Lancée si la facture est nulle.</exception>
        /// <exception cref="System.ArgumentNullException">Lancée si la liste des produitsFacture est nulle ou vide.</exception>
        public static void AjouterFacture(Facture facture)
        {
            if (facture is null)
            {
                throw new ArgumentNullException(nameof(facture), "La facture ne doit pas être nulle");
            }

            if (facture.ProduitsFacture is null)
            {
                throw new ArgumentNullException(nameof(facture), "ProduitFacture ne doit pas être nul");
            }

            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            try
            {
                cn.Open();

                string requete = $"INSERT INTO factures(Id, Date, MontantSousTotal, MontantTPS, MontantTVQ, MontantTotal) VALUES(@id, @dateCreation, @montantSousTotal, @montantTPS, @montantTVQ, @montantTotal)";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", facture.Id);
                cmd.Parameters.AddWithValue("@dateCreation", facture.DateCreation);
                cmd.Parameters.AddWithValue("@montantSousTotal", facture.MontantSousTotal);
                cmd.Parameters.AddWithValue("@montantTPS", facture.MontantTPS);
                cmd.Parameters.AddWithValue("@montantTVQ", facture.MontantTVQ);
                cmd.Parameters.AddWithValue("@montantTotal", facture.MontantTotal);

                cmd.ExecuteNonQuery();

                string requeteId = "SELECT LAST_INSERT_ID();";
                MySqlCommand cmdId = new MySqlCommand(requeteId, cn);
                facture.Id = Convert.ToUInt32(cmdId.ExecuteScalar());

                foreach (var produit in facture.ProduitsFacture)
                {
                    string requeteProduit = @"INSERT INTO produitsfactures(IdFacture, IdProduit, PrixUnitaire, Quantite) 
                                      VALUES(@idFacture, @idProduit, @prixUnitaire, @quantite)";

                    MySqlCommand cmdProduit = new MySqlCommand(requeteProduit, cn);

                    cmdProduit.Parameters.AddWithValue("@idFacture", facture.Id);
                    cmdProduit.Parameters.AddWithValue("@idProduit", produit.Produit.Id);
                    cmdProduit.Parameters.AddWithValue("@prixUnitaire", produit.PrixUnitaire);
                    cmdProduit.Parameters.AddWithValue("@quantite", produit.Quantite);


                    cmdProduit.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Permet d'obtenir une facture dans la base de donnée à partir de son identifiant unique
        /// </summary>
        /// <param name="idFacture">Identifant uniquie de la facture</param>
        /// <remarks>Les produits faisant partie de la facture doivent être ajouté à la facture.</remarks>
        /// <returns>La facture trouvée. Null si aucune facture n'est trouvée</returns>
        public static Facture ObtenirFacture(uint idFacture)
        {
            if (idFacture == null)
            {
                throw new ArgumentNullException(nameof(idFacture), "L'id de la facture ne doit pas être nulle");
            }
            MySqlConnection cn = new MySqlConnection(_configuration.GetConnectionString(CONNECTION_STRING));

            Facture facture = null;

            try
            {
                cn.Open();

                string requete = $"SELECT Id, Date, MontantSousTotal, MontantTPS, MontantTVQ, MontantTotal FROM factures WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(requete, cn);

                cmd.Parameters.AddWithValue("@id", idFacture);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    uint id = dr.GetUInt32(0);
                    DateTime date = dr.GetDateTime(1);
                    facture = new Facture(id, date);
                }
                dr.Close();

                string requetet = $"SELECT IdFacture, IdProduit, PrixUnitaire, Quantite FROM produitsFactures WHERE IdFacture = @id";
                MySqlCommand cd = new MySqlCommand(requetet, cn);
                cd.Parameters.AddWithValue("@id", idFacture);
                MySqlDataReader dar = cd.ExecuteReader();

                List<ProduitFacture> produitsFacture = new List<ProduitFacture>();

                while (dar.Read())
                {
                    uint idProduit = dar.GetUInt32(1);
                    decimal prixUnitaire = dar.GetDecimal(2);
                    uint quantite = dar.GetUInt32(3);

                    Produit produit = ObtenirProduit(idProduit);

                    ProduitFacture prodFacture = new ProduitFacture(produit, prixUnitaire, quantite);
                    produitsFacture.Add(prodFacture);
                }
                dar.Close();
                foreach (var prodFact in produitsFacture)
                {
                    facture.AjouterProduit(prodFact.Produit, prodFact.PrixUnitaire, prodFact.Quantite);
                }

            }
            catch
            {
                throw;
            }
            finally
            {
                if (cn is not null && cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return facture;
        }
    
        #endregion
    }
}
