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

        #region ACTIONS_FORMULAIRE

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboCategories.ItemsSource = DAL.ObtenirListeCategories();

            List<Produit> lstProduits = new List<Produit>();

            lstProduits = DAL.ObtenirListeProduits("", new Categorie(0,"0"));

            foreach (Produit p in lstProduits)
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(2);
                border.CornerRadius = new CornerRadius(10);
                border.Margin = new Thickness(5);
                border.Padding = new Thickness(3);
                border.Width = 140;
                border.Height = 250;                

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                stackPanel.Margin = new Thickness(5);
                
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
                txtNom.TextAlignment = TextAlignment.Center;
                txtNom.TextWrapping = TextWrapping.Wrap;

                TextBlock txtPrix = new TextBlock();             
                txtPrix.Text = $"{p.Prix:C}";
                txtPrix.Foreground = new SolidColorBrush(Colors.Green);
                txtPrix.FontWeight = FontWeights.Bold;
                txtPrix.TextAlignment = TextAlignment.Right;         

                stackPanel.Children.Add(imgProduit);
                stackPanel.Children.Add(txtNom);
                stackPanel.Children.Add(txtPrix);

                border.Child = stackPanel;

                wpProduits.Children.Add(border);
            }
        }
        

        #endregion

        private void btnNouveauProduit_Click(object sender, RoutedEventArgs e)
        {
            FormProduit nouveauProduit = new FormProduit(EtatFormulaire.Ajouter);

            nouveauProduit.ShowDialog();

            if (nouveauProduit.DialogResult == true)
            {
                MessageBox.Show("Produit ajouté avec succès!", "Ajout d'un produit");
            }
        }
    }
}