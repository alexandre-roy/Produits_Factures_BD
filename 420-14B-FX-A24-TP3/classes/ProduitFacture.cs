using System;
using System.ComponentModel;

namespace _420_14B_FX_A24_TP3.classes
{
    /// <summary>
    /// Classe représentant les produits faisant patie d'une facture
    /// </summary>
    public class ProduitFacture
    {
        #region CONSTANTES

        public const int QUANTITE_MIN_VAL = 1;

        #endregion

        #region ATTRIBUTS

        /// <summary>
        /// Produit 
        /// </summary>
        private Produit _produit;

        /// <summary>
        /// Prix unitaire du produit
        /// </summary>
        private decimal _prixUnitaire;


        /// <summary>
        /// Quantité du produit acheté
        /// </summary>
        private uint _quantite;

        #endregion

        #region PROPRIÉTÉS

        /// <summary>
        /// Obtient ou défini le produit à ajouter à la facture
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque le produit est nul.</exception>
        public Produit Produit
        {
            get { return _produit; }
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Produit), "Le produit ne doit pas être nul.");
                }               
                _produit = value;
            }
        }

        /// <summary>
        /// Obtient ou définit le prix unitaire du produit
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancées lorsque le prix est inférieur ou égale à <see cref="PRIX_MIN_VAL"/></exception>
        public decimal PrixUnitaire
        {
            get { return _prixUnitaire; }
            set
            {
                //Todo : Implémenter la validation du prix unitaire
                _prixUnitaire = value;
            }
        }

        /// <summary>
        /// Obtient ou définit la quantité du produit achetée
        /// </summary>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la quantité est inférieur à ou égale à <see cref="QUANTITE_MIN_VAL"/> .</exception>
        public uint Quantite
        {

            get { return _quantite; }
            set
            {
                if (value < QUANTITE_MIN_VAL)
                {
                    throw new ArgumentOutOfRangeException(nameof(Produit), $"Il doit y avoir au moins {QUANTITE_MIN_VAL} produit.");
                }
                _quantite = value;
            }
        }
    
        /// <summary>
        /// Obtient le sous-total pour le produit selon le prix unitaire et la quantité achetée.
        /// </summary>
        public decimal SousTotal
        {
            get {
                return PrixUnitaire * Quantite;
            }

        }

        #endregion

        #region CONSTRUCTEUR

        /// <summary>
        /// Constructeur avec paramètres
        /// </summary>
        /// <param name="produit">Produit à ajouter à la facture</param>
        /// <param name="prixUnitaire">Prix unitaire du produit</param>
        /// <param name="quantite">Quantité du produit</param>
        public ProduitFacture(Produit produit, decimal prixUnitaire, uint quantite)
        {
            Produit = produit;
            PrixUnitaire = prixUnitaire;
            Quantite = quantite;
        }

        #endregion
    }
}
