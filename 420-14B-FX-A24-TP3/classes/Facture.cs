namespace _420_14B_FX_A24_TP3.classes
{
    /// <summary>
    /// Classe représentant une facture
    /// </summary>
    public class Facture 
    {
        #region CONSTANTES

        /// <summary>
        /// Taux du TPS
        /// </summary>
        public const float TAUX_TPS = 5F;

        /// <summary>
        /// Taux du TVQ
        /// </summary>
        public const float TAUX_TVQ = 9.975F;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Identifiant unique de la facture
        /// </summary>
        private uint _id;

        /// <summary>
        /// Date de création de la facture
        /// </summary>
        private DateTime _dateCreation;

        /// <summary>
        /// Liste des produits dans la 
        /// </summary>
        private List<ProduitFacture> _produitsFacture;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient ou définit l'identifiant unique de la facture
        /// </summary>
        public uint Id
        {
            get { return _id; }
            set {
                _id = value; 
            }
        }


        /// <summary>
        /// Obtient ou définit la date de création de la facture
        /// </summary>
        public DateTime DateCreation
        {
            get { return _dateCreation; }
            set { _dateCreation = value; }
        }


        /// <summary>
        /// Obtient ou définit la liste des produits faisants partie de la facture
        /// </summary>
        public List<ProduitFacture>  ProduitsFacture
        {
            get { return _produitsFacture; }
            private set { _produitsFacture = value; }
        }


        /// <summary>
        /// Obtient le montant total de la facture
        /// </summary>
        public decimal MontantSousTotal
        {
            get {
                decimal montantSousTotal = 0;

                foreach (ProduitFacture p in ProduitsFacture)
                {
                    montantSousTotal += p.SousTotal;
                }
                return montantSousTotal;
            }        
        }


       
        /// <summary>
        /// Obtient le montant la TPS de la facture
        /// </summary>
        public decimal MontantTPS
        {
            get {
                return MontantSousTotal * (decimal)TAUX_TPS / 100;
            }
           
        }


        /// <summary>
        /// Obtient le montant de la TVQ de la facture
        /// </summary>
        public decimal MontantTVQ
        {
            get {              
                return MontantSousTotal * (decimal)TAUX_TVQ / 100;
            }
        }


        /// <summary>
        /// Obtient le montant total de la facture inclunant le montant des taxes
        /// </summary>
        public decimal MontantTotal
        {
            get {                
                return MontantSousTotal + MontantTPS + MontantTVQ;
            }
            
        }

        #endregion

        #region CONSTRUCTEURS

        /// <summary>
        /// Constructeur sans paramètre
        /// </summary>
        /// <remarks>Initialise une liste de produits vide</remarks>
        public Facture()
        {
            ProduitsFacture = new List<ProduitFacture>();
        }

       /// <summary>
       /// Constructeur avec paramètere
       /// </summary>
       /// <param name="id">Identifiant de la facture</param>
       /// <param name="dateCreation">Date de créatoin de la facture</param>
        public Facture(uint id, DateTime dateCreation)
        {
            ProduitsFacture = new List<ProduitFacture>();
            Id = id;
            DateCreation = dateCreation;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Permet d'ajout un produit à une facture
        /// </summary>
        /// <param name="produit">Le produit à ajouter à la facture</param>
        /// <param name="prixUnitaire">Le prix unitaire du produit ajouter à la facture</param>
        /// <param name="quantite">La quantite du produit achetée</param>
        /// <remarks>Si le produit existe déjà dans la facture alors la quantité est ajouté au produit existant</remarks>
        /// <exception cref="System.ArgumentNullException">Lancée si le produit est null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée si la prix unitaire est inférieur à <see cref="Produit.PRIX_MIN_VAL"/></exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée si la quantité inférieur à <see cref="ProduitFacture.QUANTITE_MIN_VAL"/></exception>
        public void AjouterProduit(Produit produit, decimal prixUnitaire, uint quantite)
        {
            if (produit is null)
            {
                throw new ArgumentNullException(nameof(produit), "Le produit ne doit pas être nul.");
            }

            if (prixUnitaire < Produit.PRIX_MIN_VAL)
            {
                throw new ArgumentOutOfRangeException(nameof(prixUnitaire), $"Le prix ne doit pas être inférieur à {Produit.PRIX_MIN_VAL}$");
            }

            if (quantite < ProduitFacture.QUANTITE_MIN_VAL)
            {
                throw new ArgumentOutOfRangeException(nameof(quantite), $"La quantité ne doit pas être inférieure à {ProduitFacture.QUANTITE_MIN_VAL}");
            }

            foreach (ProduitFacture p in ProduitsFacture)
            {
                if (p.Produit.Equals(produit))
                {
                    p.Quantite += quantite;
                    return;
                }
            }

            ProduitsFacture.Add(new ProduitFacture(produit, prixUnitaire, quantite));        
        }

        /// <summary>
        /// Permet retirer un produit d'une facture.
        /// </summary>
        /// <param name="produit">Le produit à retirer de la facture</param>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul</exception>
        ///  <exception cref="System.InvalidOperationException">Lancée lorsque le produit n'existe pas dans la facture</exception>
        public void RetirerProduit(Produit produit)
        {
            ProduitFacture produitDansFacture = null;

            if (produit is null)
            {
                throw new ArgumentNullException(nameof(produit), "Le produit ne doit pas être nul.");
            }

            foreach (ProduitFacture p in ProduitsFacture)
            {
                if (p.Produit.Equals(produit))
                {
                    produitDansFacture = p;                  
                }
            }

            if (produitDansFacture is null)
            {
                throw new InvalidOperationException("Le produit n'existe pas dans la facture.");
            }
           
            ProduitsFacture.Remove(produitDansFacture);
        }

        #endregion
    }
}
