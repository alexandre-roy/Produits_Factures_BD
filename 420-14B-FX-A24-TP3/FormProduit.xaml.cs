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
            cboCategories.ItemsSource = DAL.ObtenirListeCategories();

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
                cboCategories.SelectedItem = _produit.Categorie;
                imgProduit.Source = new BitmapImage(new Uri(_configuration[DAL.IMAGE_PATH] + _produit.Image));
            }
        }

        #endregion

        private void btnAjouterModifierSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if(_etat == EtatFormulaire.Ajouter)
            {
                BitmapImage bi = imgProduit.Source as BitmapImage;
                string image = bi.UriSource.LocalPath;
                string extension = System.IO.Path.GetExtension(image);
                string nomImage = Guid.NewGuid().ToString() + extension;
                string cheminImage = _configuration[DAL.IMAGE_PATH];

                File.Copy(image, cheminImage + nomImage);

                Produit produit = new Produit(0, txtCode.Text, txtNom.Text, (Categorie)cboCategories.SelectedItem, decimal.Parse(txtPrix.Text), nomImage);

                DAL.AjouterProduit(produit);

                this.DialogResult = true;
            }
            else if (_etat == EtatFormulaire.Modifier)
            {
                BitmapImage bi = imgProduit.Source as BitmapImage;
                string image = bi.UriSource.LocalPath;
                string extension = System.IO.Path.GetExtension(image);
                string nomImage = Guid.NewGuid().ToString() + extension;
                string cheminImage = _configuration[DAL.IMAGE_PATH];

                File.Copy(image, cheminImage + nomImage);

                _produit.Code = txtCode.Text;
                _produit.Nom = txtNom.Text;
                _produit.Prix = decimal.Parse(txtPrix.Text);
                _produit.Categorie = (Categorie)cboCategories.SelectedItem;
                _produit.Image = nomImage;

                DAL.ModifierProduit(_produit);

                this.DialogResult = true;
            }
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
    }
}
