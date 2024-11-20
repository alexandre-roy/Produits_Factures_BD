
using _420_14B_FX_A24_TP3.classes;
using Xunit;

namespace _420_14B_FX_A24_TP3_Tests
{
    public class CategorieTests
    {
       


        [Fact]
        public void SetNom_Devrait_Lancer_ArgumentException_Quand_Null_Ou_Vide_Ou_Espaces()
        {
            //Arrange
            //Arrange Act et Assert

           

            //Test code vide ou null
            Assert.Throws<ArgumentException>(() => new Categorie(1, null));
            Assert.Throws<ArgumentException>(() => new Categorie(1, ""));
            Assert.Throws<ArgumentException>(() => new Categorie(1, " "));

        }

        [Fact]
        public void SetNom_Devrait_Retitrer_Espaces_Inutiles()
        {
            //Arrange
          

            string nom = " T-Shirt ";
            string resultatAttendu = "T-Shirt";

            //Act
            Categorie categorie = new Categorie(1, nom);

            //Assert
            Assert.Equal(resultatAttendu, categorie.Nom);

        }

        [Fact]
        public void Equals_Devrait_Retourner_True_Si_Categories_Identique()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Vêtements d’hiver");
            Categorie categorie2 = new Categorie(2, "Vetements d’hiver");
            Categorie categorie3 = new Categorie(3, "vêtements d’hiver");
            Categorie categorie4 = new Categorie(4, " Vêtements d’hiver");
            Categorie categorie5 = new Categorie(5, "Vêtements d’hiver ");

            //Act
            bool egal1 = categorie1.Equals(categorie2);
            bool egal2 = categorie1.Equals(categorie3);
            bool egal3 = categorie1.Equals(categorie4);
            bool egal4 = categorie1.Equals(categorie5);


            //Assert
            Assert.True(egal1);
            Assert.True(egal2);
            Assert.True(egal3);
            Assert.True(egal4);


        }


        [Fact]
        public void Equals_Devrait_Retourner_False_Si_Categories_Diffentes()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Chaussures");
            Categorie categorie2 = new Categorie(1, "Pantalons");
            Categorie categorie3 = new Categorie(2, "Pantalons");
            string objDifferent = "";

            //Act & Assert     
            Assert.False(categorie1.Equals(categorie2));
            Assert.False(categorie1.Equals(categorie3));
            Assert.False(categorie1.Equals(null));
            Assert.False(categorie1.Equals(objDifferent));



        }

      

        [Fact]
        public void Operateur_Egal_Devrait_Retourner_True_Si_Categories_Identique()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Vêtements d’hiver");
            Categorie categorie2 = new Categorie(2, "Vetements d’hiver");
            Categorie categorie3 = new Categorie(3, "vêtements d’hiver");
            Categorie categorie4 = new Categorie(4, " Vêtements d’hiver");
            Categorie categorie5 = new Categorie(5, "Vêtements d’hiver ");


            //Act & Assert
            Assert.True(categorie1 == categorie2);
            Assert.True(categorie1 == categorie3);
            Assert.True(categorie1 == categorie4);
            Assert.True(categorie1 == categorie5);
 


        }


        [Fact]
        public void Operateur_Egal_Devrait_Retourner_False_Si_Categories_Diffentes()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Chaussures");
            Categorie categorie2 = new Categorie(1, "Pantalons");
            Categorie categorie3 = new Categorie(2, "Pantalons");

            //Act & Assert     
            Assert.False(categorie1 == categorie2);
            Assert.False(categorie1 == categorie3);
            Assert.False(categorie1 == null);
            Assert.False(null == categorie1);



        }


        [Fact]
        public void Operateur_Non_Egal_Devrait_Retourner_False_Si_Categories_Identique()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Vêtements d’hiver");
            Categorie categorie2 = new Categorie(2, "Vetements d’hiver");
            Categorie categorie3 = new Categorie(3, "vêtements d’hiver");
            Categorie categorie4 = new Categorie(4, " Vêtements d’hiver");
            Categorie categorie5 = new Categorie(5, "Vêtements d’hiver ");



            //Act & Assert
            Assert.False(categorie1 != categorie2);
            Assert.False(categorie1 != categorie3);
            Assert.False(categorie1 != categorie4);
            Assert.False(categorie1 != categorie5);
    

        }


        [Fact]
        public void Operateur_Non_Egal_Devrait_Retourner_True_Si_Categories_Diffentes()
        {
            //Arrange
            Categorie categorie1 = new Categorie(1, "Chaussures");
            Categorie categorie2 = new Categorie(1, "Pantalons");
            Categorie categorie3 = new Categorie(2, "Pantalons");

            //Act & Assert     
            Assert.True(categorie1 != categorie2);
            Assert.True(categorie1 != categorie3);
            Assert.True(categorie1 != null);
            Assert.True(null != categorie1);



        }
    }
}
