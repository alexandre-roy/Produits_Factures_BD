using _420_14B_FX_A24_TP3.classes;
using _420_14B_FX_TP3_A23.classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace _420_14B_FX_A24_TP3
{
    /// <summary>
    /// Logique d'interaction pour FormProduit.xaml
    /// </summary>
    public partial class FormProduit : Window
    {

        private IConfiguration _configuration;

        #region ATTRIBUT

        private EtatFormulaire _etat;

        private Produit _produit;

        #endregion

        #region INITIALISATION

        public FormProduit(EtatFormulaire etat, Produit produit = null)
        {
            _etat = etat;
            _produit = produit;

            _configuration = new ConfigurationBuilder().AddJsonFile(DAL.APPSETTINGS_FILE, false, true).Build();

            InitializeComponent();

            cboCategoriesP.ItemsSource = DAL.ObtenirListeCategories();
        }

        #endregion

        #region MÉTHODES
        private void AfficherImage(string cheminFichier)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(cheminFichier);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();

            imgProduit.Source = bi;
        }
        #endregion

        #region ACTIONS_FORMULAIRE

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboCategoriesP.ItemsSource = DAL.ObtenirListeCategories();

            if (_etat == EtatFormulaire.Ajouter)
            {
                btnAjouterModifierSupprimer.Content = "Ajouter";
                lblTitre.Text = "Ajout d'un produit";
            }
            if (_etat == EtatFormulaire.Modifier)
            {
                btnAjouterModifierSupprimer.Content = "Modifier";
                lblTitre.Text = "Modification d'un produit";

                txtCode.Text = _produit.Code;
                txtNom.Text = _produit.Nom;
                txtPrix.Text = _produit.Prix.ToString();
                cboCategoriesP.SelectedItem = _produit.Categorie;
                imgProduit.Source = new BitmapImage(new Uri(_configuration[DAL.IMAGE_PATH] + _produit.Image));
            }
        }

        #endregion

        private void btnAjouterModifierSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if(_etat == EtatFormulaire.Ajouter)
            {
                BitmapImage bi = imgProduit.Source as BitmapImage;
                string image = "";
                if (bi != null)
                {
                    image = bi.UriSource.LocalPath;
                }
                string extension = System.IO.Path.GetExtension(image);
                string nomImage = Guid.NewGuid().ToString() + extension;
                string cheminImage = _configuration[DAL.IMAGE_PATH];
                if (!string.IsNullOrEmpty(image))
                {
                    File.Copy(image, cheminImage + nomImage);
                }
                if (ValiderFormulaire())
                {
                    image = bi.UriSource.LocalPath;
                    Produit produit = new Produit(0, txtCode.Text, txtNom.Text, (Categorie)cboCategoriesP.SelectedItem, decimal.Parse(txtPrix.Text), nomImage);

                    DAL.AjouterProduit(produit);
                    this.DialogResult = true;
                }
            }
            else if (_etat == EtatFormulaire.Modifier)
            {
                BitmapImage bi = imgProduit.Source as BitmapImage;
                string image = bi.UriSource.LocalPath;
                string extension = System.IO.Path.GetExtension(image);
                string nomImage = Guid.NewGuid().ToString() + extension;
                string cheminImage = _configuration[DAL.IMAGE_PATH];
                File.Copy(image, cheminImage + nomImage);

                if (ValiderFormulaire())
                {
                    _produit.Code = txtCode.Text;
                    _produit.Nom = txtNom.Text;
                    _produit.Prix = decimal.Parse(txtPrix.Text);
                    _produit.Categorie = (Categorie)cboCategoriesP.SelectedItem;
                    _produit.Image = nomImage;
                    DAL.ModifierProduit(_produit);

                    this.DialogResult = true;
                }
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = false;
        }
        private void btnAjouterImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "image PNG | *.png| image JPG | *.jpg| image AVIF | *.avif";

                if (openFileDialog.ShowDialog() == true)
                {
                    AfficherImage(openFileDialog.FileName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite :\n" + ex.Message, "Ajout d'une image");
            }
        }

        private bool ValiderFormulaire()
        {
            bool estValide = false;
            string messageErreur = "";
            if (_etat == EtatFormulaire.Ajouter)
            {
                if (DAL.ObtenirProduit(txtCode.Text) != null)
                {
                    messageErreur += "- Le code du produit existe déjà.\n";
                }
            }
            if (_etat == EtatFormulaire.Modifier)
            {
                if (DAL.ObtenirProduit(txtCode.Text) != null && txtCode.Text != _produit.Code)
                {
                    messageErreur += "- Le code du produit existe déjà.\n";
                }
            }
           
            if (string.IsNullOrWhiteSpace(txtCode.Text) || txtCode.Text.Length < Produit.CODE_NB_CARAC_MIN || txtCode.Text.Length > Produit.CODE_NB_CARAC_MAX)
            {
                messageErreur += $"- Le code du produit doit contenir entre {Produit.CODE_NB_CARAC_MIN} et {Produit.CODE_NB_CARAC_MAX} caracteres.\n";
            }
            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Length < Produit.NOM_NB_CARAC_MIN || txtNom.Text.Length > Produit.NOM_NB_CARAC_MAX)
            {
                messageErreur += $"- Le nom du produit doit contenir entre {Produit.CODE_NB_CARAC_MIN} et {Produit.NOM_NB_CARAC_MAX} caracteres.\n";
            }
            decimal prix;
            if (decimal.TryParse(txtPrix.Text, out prix))
            {
                if (decimal.Parse(txtPrix.Text) <= 0)
                {
                    messageErreur += "- Le prix du produit doit être supérieur à 0.\n";
                }
            }
            else
            {
                messageErreur += "- Le prix du produit doit être inscrit en format monetaire.\n";
            }

            if ((Categorie)cboCategoriesP.SelectedItem == null)
            {
                messageErreur += "- Vous devez selectionner la catégorie du  produit.\n";
            }
            if (imgProduit.Source == null)
            {
                messageErreur += "- Vous devez selectionner l'image du produit.\n";
            }
            if (messageErreur == "")
            {
                estValide = true;
            }
            if (!estValide)
            {
                MessageBox.Show(messageErreur, "Validation du produit");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
