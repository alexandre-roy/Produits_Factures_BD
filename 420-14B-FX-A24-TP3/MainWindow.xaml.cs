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
                border.BorderBrush = new SolidColorBrush(Colors.Black);
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
                btnModifier.Height = 25;
                btnModifier.Width = 25;
                btnModifier.HorizontalAlignment = HorizontalAlignment.Right;
                btnModifier.Source = biProduit;
                btnModifier.Margin = new Thickness(0, 0, 5, 0);
                btnModifier.MouseLeftButtonDown += new MouseButtonEventHandler(btnModifier_MouseLeftButtonDown);
                btnModifier.Tag = p;

                Image btnSupprimer = new Image();
                btnSupprimer.Height = 25;
                btnSupprimer.Width = 25;
                btnSupprimer.HorizontalAlignment = HorizontalAlignment.Right;
                btnSupprimer.Source = biProduit;
                btnSupprimer.Margin = new Thickness(5, 0, 0, 0);
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
            cboCategories.ItemsSource = DAL.ObtenirListeCategories();

            AfficherProduits();
        }
        
        #endregion

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

            txtRechercher.Text = "";
            cboCategories.SelectedItem = null;
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

        }
    }
}