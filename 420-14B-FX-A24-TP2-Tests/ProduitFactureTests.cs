
using _420_14B_FX_A24_TP3.classes;
using System;
using Xunit;

namespace _420_14B_FX_A24_TP3_Tests
{
    public class ProduitFactureTests
    {

        /// <summary>
        /// Permet d'obtenir une catégorie
        /// </summary>
        /// <returns>Catégorie créer</returns>
        private Categorie CreerCategorie()
        {
            return new Categorie(1, "Vêtements");
        }

        /// <summary>
        /// Permet d'obtenir un produit
        /// </summary>
        /// <returns>Produit créé</returns>
        private Produit CreerProduit()
        {
            Categorie categorie = CreerCategorie();

            return new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");
        }

        [Fact]
        public void SetProduit_Devrait_Lancer_ArguementNullException_Quand_Null()
        {

            //Arrange Act et Assert
            Assert.Throws<ArgumentNullException>(() => new ProduitFacture(null,1m,1));

            
        }

        [Fact]
        public void SetProduit_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Quantite_Inferieur_A_Quantite_Min()
        {


            //Arrange
            Produit produit = CreerProduit();

            uint quantite = ProduitFacture.QUANTITE_MIN_VAL - 1;

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new ProduitFacture(produit, produit.Prix, quantite));


        }

        [Fact]
        public void SousTotal_Devrait_Retouner_SousTotal()
        {


            //Arrange
            Produit produit = CreerProduit();

            uint quantite = 2;

            ProduitFacture produitFacture = new ProduitFacture(produit, produit.Prix, quantite);

            decimal resultatAttendu = produit.Prix * quantite;

            //Act and Assert

            Assert.Equal(resultatAttendu, produitFacture.SousTotal);


        }

        [Fact]
        public void Constructeur_Devrait_Creer_ProduitFacture_Avec_Valeurs()
        {


            //Arrange
            Produit produit = CreerProduit();

            uint quantite = 2;

            ProduitFacture produitFacture = new ProduitFacture(produit, produit.Prix, quantite);

            //Act and Assert

            Assert.True(produit.Equals(produitFacture.Produit));
            Assert.Equal(produit.Prix, produitFacture.PrixUnitaire);
            Assert.Equal(quantite, produitFacture.Quantite);



        }


    }
}
