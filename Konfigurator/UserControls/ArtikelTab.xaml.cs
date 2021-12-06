using System.Windows.Controls;
using System.Windows.Media;
using Konfigurator.Logic.Models.Article;

namespace Konfigurator.UserControls
{
    public partial class ArtikelTab : UserControl
    {
        public ArtikelTab()
        {
            InitializeComponent();
            //Fill DataGridView
            var dataset = ArticleDB.GetDataSetArticle();
            DataGrid.ItemsSource = dataset.Tables["Artikel"].DefaultView;
            
            isItAvailable();
        }

        public bool isAvailable;
        public void isItAvailable()
        {
            
            if (isAvailable)
            {
                isAvailableText.Text = "Verügbar";
                isAvailableText.Foreground = Brushes.Green;
            }
            else
            {
                isAvailableText.Text = "Nicht Verfügbar";
                isAvailableText.Foreground = Brushes.Red;
            }
        }
    }
}