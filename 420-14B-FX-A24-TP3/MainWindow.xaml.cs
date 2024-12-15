using _420_14B_FX_A24_TP3.classes;
using _420_14B_FX_TP3_A23.classes;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _420_14B_FX_A24_TP3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IConfiguration _configuration;

        private Facture _factureCourante;

        #region INITIALISATION

        public MainWindow()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile(DAL.APPSETTINGS_FILE, false, true).Build();

            InitializeComponent();             
        }

        #endregion

        #region MÉTHODES

        public void AfficherProduits()
        {
            wpProduits.Children.Clear();

            List<Produit> lstProduits = new List<Produit>();

            lstProduits = DAL.ObtenirListeProduits(txtRechercher.Text, (Categorie)cboCategories.SelectedItem);
            

            foreach (Produit p in lstProduits)
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.LightGray);
                border.BorderThickness = new Thickness(1);
                border.CornerRadius = new CornerRadius(10);
                border.Margin = new Thickness(5);
                border.Width = 140;
                border.Height = 275;

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;

                BitmapImage biProduit = new BitmapImage();
                biProduit.BeginInit();
                biProduit.UriSource = new Uri(_configuration[DAL.IMAGE_PATH] + p.Image);
                biProduit.CacheOption = BitmapCacheOption.OnLoad;
                biProduit.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                biProduit.EndInit();

                Image imgProduit = new Image();
                imgProduit.Source = biProduit;
                imgProduit.Margin = new Thickness(5);
                imgProduit.HorizontalAlignment = HorizontalAlignment.Center;
                imgProduit.VerticalAlignment = VerticalAlignment.Center;
                imgProduit.Width = 125;
                imgProduit.Height = 125;
                imgProduit.Tag = p;
                imgProduit.MouseLeftButtonDown += imgProduit_MouseLeftButtonDown;

                TextBlock txtNom = new TextBlock();
                txtNom.Text = p.Nom;
                txtNom.FontSize = 15;
                txtNom.Margin = new Thickness(0, 0, 0, 20);
                txtNom.TextWrapping = TextWrapping.Wrap;
                txtNom.LineHeight = 10;
                txtNom.Height = 50;
                txtNom.Margin = new Thickness(5);

                TextBlock txtPrix = new TextBlock();
                txtPrix.Text = $"{p.Prix:C}";
                txtPrix.FontSize = 14;
                txtPrix.Foreground = new SolidColorBrush(Colors.Green);
                txtPrix.FontWeight = FontWeights.Bold;
                txtPrix.TextAlignment = TextAlignment.Right;
                txtPrix.Margin = new Thickness(5);

                StackPanel stackPanelBoutons = new StackPanel();
                stackPanelBoutons.Orientation = Orientation.Horizontal;
                stackPanelBoutons.HorizontalAlignment = HorizontalAlignment.Right;
                stackPanelBoutons.Margin = new Thickness(0, 10, 5, 0);

                Image btnModifier = new Image();
                btnModifier.Height = 30;
                btnModifier.Width = 30;
                btnModifier.HorizontalAlignment = HorizontalAlignment.Right;
                btnModifier.Source = new BitmapImage(new Uri("/Resources/edit.png", UriKind.Relative));
                btnModifier.Margin = new Thickness(0, 0, 0, 0);
                btnModifier.MouseLeftButtonDown += new MouseButtonEventHandler(btnModifier_MouseLeftButtonDown);
                btnModifier.Tag = p;

                Image btnSupprimer = new Image();
                btnSupprimer.Height = 25;
                btnSupprimer.Width = 25;
                btnSupprimer.HorizontalAlignment = HorizontalAlignment.Right;
                btnSupprimer.Source = new BitmapImage(new Uri("/Resources/delete.png", UriKind.Relative));
                btnSupprimer.Margin = new Thickness(5, 0, 0, 2);
                btnSupprimer.MouseLeftButtonDown += new MouseButtonEventHandler(btnSupprimer_MouseLeftButtonDown);
                btnSupprimer.Tag = p;

                stackPanel.Children.Add(imgProduit);
                stackPanel.Children.Add(txtNom);
                stackPanel.Children.Add(txtPrix);
                stackPanelBoutons.Children.Add(btnModifier);
                stackPanelBoutons.Children.Add(btnSupprimer);
                stackPanel.Children.Add(stackPanelBoutons);

                border.Child = stackPanel;

                wpProduits.Children.Add(border);
            }
        }

        #endregion

        #region ACTIONS_FORMULAIRE

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<Categorie> categories = new List<Categorie>();

            Categorie categorieToutes = new Categorie(0, "Toutes");

            _factureCourante = new Facture();

            categories.Add(categorieToutes);

            categories.AddRange(DAL.ObtenirListeCategories());

            cboCategories.ItemsSource = categories;

            cboCategories.SelectedIndex = 0;

            AfficherProduits();

            DataContext = null;

            DataContext = _factureCourante;
        }
        
        #endregion

        private void imgProduit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image imgProduit = (Image)sender;
            Produit produit = (Produit)imgProduit.Tag;
            bool existe = false;
            foreach (var prod in _factureCourante.ProduitsFacture)
            {
                if (prod.Produit == produit) 
                {
                    existe = true;
                    prod.Quantite += 1;
                }
            }
            if (!existe)
            {
                _factureCourante.AjouterProduit(produit, produit.Prix, 1);
            }
            lstFactures.ItemsSource = _factureCourante.ProduitsFacture;
            lstFactures.Items.Refresh();
            DataContext = null;
            DataContext = _factureCourante;
        }

        private void btnEnlever_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = e.Source as Image;
            ProduitFacture prod = img.Tag as ProduitFacture;
            for (int i = 0; i < _factureCourante.ProduitsFacture.Count; i++)
            {
                ProduitFacture prodf = _factureCourante.ProduitsFacture[i];

                if (prodf.Produit.Equals(prod.Produit))
                {
                    if (_factureCourante.ProduitsFacture[i].Quantite == 1)
                    {
                        _factureCourante.RetirerProduit(prod.Produit);
                    }
                    else
                    {
                        _factureCourante.ProduitsFacture[i].Quantite -= 1;
                    }
                    lstFactures.Items.Refresh();
                    DataContext = null;
                    DataContext = _factureCourante;
                }
            }
        }

        private void btnAjouter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = e.Source as Image;
            ProduitFacture prod = img.Tag as ProduitFacture;
            for (int i = 0; i < _factureCourante.ProduitsFacture.Count; i++)
            {
                ProduitFacture prodf = _factureCourante.ProduitsFacture[i];

                if (prodf.Produit.Equals(prod.Produit))
                {
                    _factureCourante.ProduitsFacture[i].Quantite += 1;
                    lstFactures.Items.Refresh();
                    DataContext = null;
                    DataContext = _factureCourante;
                }
            }
        }

        private void btnNouveauProduit_Click(object sender, RoutedEventArgs e)
        {
            FormProduit nouveauProduit = new FormProduit(EtatFormulaire.Ajouter);

            nouveauProduit.ShowDialog();

            AfficherProduits();

            if (nouveauProduit.DialogResult == true)
            {
                MessageBox.Show("Produit ajouté avec succès!", "Ajout d'un produit");
            }
        }

        private void btnRechercherProduit_Click(object sender, RoutedEventArgs e)
        {
            AfficherProduits();
        }

        private void btnModifier_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        { 
            Image imgProduit = (Image)sender;
            Produit produit = (Produit)imgProduit.Tag;

            FormProduit ModificationProduit = new FormProduit(EtatFormulaire.Modifier, produit);

            ModificationProduit.ShowDialog();

            AfficherProduits();

            if (ModificationProduit.DialogResult == true)
            {
                MessageBox.Show("Produit modifié avec succès!", "Modification d'un produit");
            }
        }

        private void btnSupprimer_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce produit?", "Supression d'un produit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            Image imgProduit = (Image)sender;
            Produit produit = (Produit)imgProduit.Tag;

            DAL.SupprimerProduit(produit);

            MessageBox.Show("Produit supprimé avec succès!", "Suppression d'un produit");
            
            AfficherProduits();
        }

        private void btnNouvelleFacture_Click(object sender, RoutedEventArgs e)
        {
            _factureCourante = new Facture();

            DataContext = _factureCourante;

            lstFactures.ItemsSource = null;
        }


    }
}