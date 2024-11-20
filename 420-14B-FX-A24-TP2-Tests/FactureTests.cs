
using _420_14B_FX_A24_TP3.classes;
using System;
using Xunit;

namespace _420_14B_FX_A24_TP3_Tests
{
    public class FactureTests
    {


        /// <summary>
        /// Permet de créer une liste de ProduitFacture
        /// </summary>
        /// <returns></returns>
        private Facture CreerFacture()
        {
            Facture facture =  new Facture(1, DateTime.Now);

            Categorie categorie = new Categorie(1, "Vêtements");

            Produit produit1 = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");
            Produit produit2 = new Produit(2, "P1234", "Pantalon", categorie, 50, "pantalon.png");

            facture.AjouterProduit(produit1, produit1.Prix,1);
            facture.AjouterProduit(produit2, produit2.Prix,1);

            return facture;

        }

        [Fact]
        public void MontantSoustTotal_Devrait_Retourner_SousTotal()
        {

            //Arrange
            Facture facture = CreerFacture();

            //Modification du la quantité du 2ième produit.
            facture.ProduitsFacture[1].Quantite = 2;


            decimal resultatAttendu = 110m;

            //Act & Assert
            Assert.Equal(resultatAttendu, facture.MontantSousTotal);

        }

        [Fact]
        public void MontantTVQ_Devrait_Retourner_Montant_TVQ()
        {

            //Arrange & Act
            Facture facture = CreerFacture();

            decimal resultatAttendu = 5.985m;

            //Assert
            Assert.Equal(resultatAttendu, facture.MontantTVQ);

        }

        [Fact]
        public void MontantTPS_Devrait_Retourner_Montant_TPS()
        {

            //Arrange & Act
            Facture facture = CreerFacture();

            decimal resultatAttendu = 3m;

            //Assert
            Assert.Equal(resultatAttendu, facture.MontantTPS);

        }

        [Fact]
        public void MontantTotal_Devrait_Retourner_Total_Facture()
        {

            //Arrange & Act
            Facture facture = CreerFacture();

            decimal resultatAttendu = 68.985m;

            //Assert
            Assert.Equal(resultatAttendu, facture.MontantTotal);

        }

        [Fact]
        public void Constructeur_Sans_Param_Devrait_Creer_Facture_Avec_Liste_Produits_Vide()
        {
            //Arrange & Act
            Facture facture = new Facture();
            int resultatAttendu = 0;

            //Assert
            Assert.NotNull(facture.ProduitsFacture);
            Assert.Equal(resultatAttendu, facture.ProduitsFacture.Count);

        }

        [Fact]
        public void Constructeur_Avec_Param_Devrait_Creer_Facture_Avec_Valeurs_Et_Liste_Produits_Vide()
        {
            //Arrange & Act
            uint id = 1;
            DateTime dateCreation = DateTime.Now;
            Facture facture = new Facture(id,dateCreation);

            int resultatAttendu = 0;

            //Assert
            Assert.Equal(id, facture.Id);
            Assert.Equal(dateCreation, facture.DateCreation);
            Assert.NotNull(facture.ProduitsFacture);
            Assert.Equal(resultatAttendu, facture.ProduitsFacture.Count);

        }


        [Fact]
        public void AjouterProduit_Devrait_Lancer_ArgumentNullException_Quand_Produit_Null()
        {

            //Arrange
            Facture facture = new Facture(1, DateTime.Now);

    
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => facture.AjouterProduit(null,1m,1));


        }

        [Fact]
        public void AjouterProduit_Devrait_Lancer_ArgumentOutOfRangeException_Quand_PrixUnitaire_Inferieur_A_Min()
        {
            //Arrange
            Facture facture = new Facture(1, DateTime.Now);

            Categorie categorie = new Categorie(1, "Vêtements");

            Produit produit = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            decimal prix = Produit.PRIX_MIN_VAL - 1;

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => facture.AjouterProduit(produit, prix, 1));


        }

        [Fact]
        public void AjouterProduit_Devrait_Lancer_ArgumentOutOfRangeException_Quand_Quantite_Inferieur_A_Min()
        {
            //Arrange
            Facture facture = new Facture(1, DateTime.Now);

            Categorie categorie = new Categorie(1, "Vêtements");

            Produit produit = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            uint quantite = ProduitFacture.QUANTITE_MIN_VAL - 1;

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => facture.AjouterProduit(produit, produit.Prix, quantite));


        }

        [Fact]
        public void AjouterProduit_Devrait_Ajouter_Produit_A_Facture()
        {
            //Arrange
            Facture facture = new Facture(1, DateTime.Now);

            Categorie categorie = new Categorie(1, "Vêtements");

            Produit produit = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            uint quantite = 1;
            int nbProduitsAttendu = 1;

            //Act
            facture.AjouterProduit(produit, produit.Prix, quantite);


            //Assert
            Assert.Equal(nbProduitsAttendu, facture.ProduitsFacture.Count);
            Assert.True(facture.ProduitsFacture[0].Produit.Equals(produit));
            Assert.Equal(quantite, (facture.ProduitsFacture[0].Quantite));
            
         
        }

        [Fact]
        public void AjouterProduit_Devrait_Ajuster_Quantite_Si_Produit_Existe_Deja()
        {
            //Arrange
            Facture facture = new Facture(1, DateTime.Now);

            Categorie categorie = new Categorie(1, "Vêtements");

            Produit produit1 = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");
            Produit produit2 = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            uint quantiteAttendu = 2;
            int nbProduitsAttendu = 1;
            
            //Act
            facture.AjouterProduit(produit1, produit1.Prix, 1);
            facture.AjouterProduit(produit2, produit2.Prix,1);

            //Assert
            Assert.Equal(nbProduitsAttendu, facture.ProduitsFacture.Count);
            Assert.Equal(quantiteAttendu, (facture.ProduitsFacture[0].Quantite));


        }

        [Fact]
        public void RetirerProduit_Devrait_Lancer_ArgumentNullException_Quand_Produit_Null()
        {
            //Arrange
            Facture facture = new Facture(1, DateTime.Now);

           
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => facture.RetirerProduit(null));


        }

        [Fact]
        public void RetirerProduit_Devrait_Lancer_InvalidOperationException_Quand_Produit_Inexistant()
        {
            //Arrange
            Facture facture = CreerFacture();

   
            Categorie categorie = new Categorie(1, "Souliers");

            Produit produit = new Produit(1, "S1234", "Converse", categorie, 99, "converse.png");
          

         
            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => facture.RetirerProduit(produit));


        }

        [Fact]
        public void RetirerProduit_Devrait_Retirer_Produit_Quand_Produit_Exist()
        {
            //Arrange
            Facture facture = CreerFacture();


            Categorie categorie = new Categorie(1, "Vêtements");

            Produit produit = new Produit(1, "TS123", "T-Shirt", categorie, 10, "t-shirt.png");

            int nbProduitsAttendu = facture.ProduitsFacture.Count - 1;

            //Act
            facture.RetirerProduit(produit);

            //Act & Assert
            Assert.Equal(nbProduitsAttendu, facture.ProduitsFacture.Count);


        }


        

    }
}
