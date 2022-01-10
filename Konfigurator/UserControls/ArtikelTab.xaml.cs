using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Konfigurator.Logic.Models.Article;
using Prism.Commands;

namespace Konfigurator.UserControls
{
    public partial class ArtikelTab : UserControl
    {
        public ArtikelTab()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = ArticleDB.GetDataSetArticle();
                DataGrid.ItemsSource = dataset.Tables["Artikel"].DefaultView;
                isItAvailable();
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
            object item = DataGrid.SelectedItem;

            //Selected Item | id
            string id = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | name
            string name = (DataGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | price
            string price = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | Available
            bool? available = (DataGrid.SelectedCells[3].Column.GetCellContent(item) as CheckBox)?.IsChecked;

            //Display Items in Textbox
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            if (price != null) PriceText.Text = price;
            AvailableCheck.IsChecked = available;
            isItAvailable();
        }


        public void isItAvailable()
        {
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;

            if (available)
            {
                isAvailableText.Text = "Verfügbar";
                isAvailableText.Foreground = Brushes.Green;
            }
            else
            {
                isAvailableText.Text = "Nicht Verfügbar";
                isAvailableText.Foreground = Brushes.Red;
            }
        }
        //Edit Article
        private void EditArticle(object sender, RoutedEventArgs e)
        {
            var idTextBox = IdText.Text;
            var nameTextBox = NameText.Text;
            var priceTextBox = PriceText.Text;
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;
            
            ArticleDB.UpdateArticle(new Article(Int32.Parse(idTextBox), nameTextBox, double.Parse(priceTextBox), available));
            
            System.Threading.Thread.Sleep(500);
            UpdateDataGrid();
        }
        private void KillArticle(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            
            ArticleDB.KillArticle(Int32.Parse(id));
            
            System.Threading.Thread.Sleep(500);
            UpdateDataGrid();
        }
        
        //Add Article to DataBase
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var idTextBox = IdText.Text;
            var nameTextBox = NameText.Text;
            var priceTextBox = PriceText.Text;
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;

            ArticleDB.CreateArticle(new Article(Int32.Parse(idTextBox), nameTextBox, double.Parse(priceTextBox),
                available));
            System.Threading.Thread.Sleep(500);
            UpdateDataGrid();
        }
    }
}