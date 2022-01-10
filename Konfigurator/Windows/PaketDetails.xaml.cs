using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Konfigurator.Logic.Models.Article;
using Konfigurator.Logic.Models.PackageDetails;

namespace Konfigurator.Windows
{
    public partial class PaketDetails : Window
    {
        public PaketDetails(int packageId)
        {
            PackageId = packageId;
            InitializeComponent();
            PackageIdText.Text = PackageId.ToString();
            UpdateDataGrid();
        }

        public int PackageId;
        public void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = ArticleDB.GetDataSetArticle();
                ArticleDataGrid.ItemsSource = dataset.Tables["Artikel"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        //On Selected Datagrid Row
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = ArticleDataGrid.SelectedItem;

            //Selected Item | id
            string id = (ArticleDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | name
            string name = (ArticleDataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | price
            string price = (ArticleDataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | Available
            bool? available = (ArticleDataGrid.SelectedCells[3].Column.GetCellContent(item) as CheckBox)?.IsChecked;

            //Display Items in Textbox
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            if (price != null) PriceText.Text = price;
            AvailableCheck.IsChecked = available;
            isItAvailable();
        }

        private void AddArticleToPackage(object sender, RoutedEventArgs e)
        {
            var ArticleId = IdText.Text;
            var ArticleName = NameText.Text;
            var ArticleAmount = this.ArticleAmount.Text;
            var ArticlePrice = PriceText.Text;
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;
            
            PackageDetailsDB.CreatePackageDetails(new PackageDetails(PackageId, Int32.Parse(ArticleId), 
                Int32.Parse(ArticleAmount), double.Parse(ArticlePrice), ArticleName, available));

        }

        //Do we need this? 
        private void RemoveArticleFromPackage(object sender, RoutedEventArgs e)
        {
            PackageDetailsDB.DeleteArticleFromPackage(PackageId, Int32.Parse(IdText.Text));
            UpdateDataGrid();
        }
        public void isItAvailable()
        {
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;

            if (available)
            {
                AvailableText.Text = "Verfügbar";
                AvailableText.Foreground = Brushes.Green;
            }
            else
            {
                AvailableText.Text = "Nicht Verfügbar";
                AvailableText.Foreground = Brushes.Red;
            }
        }
        
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}