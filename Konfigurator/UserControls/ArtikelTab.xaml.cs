﻿using System;
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
            //TextBoxes
            
            
            InitializeComponent();
            //Fill DataGridView
            var dataset = ArticleDB.GetDataSetArticle();
            DataGrid.ItemsSource = dataset.Tables["Artikel"].DefaultView;
            
            isItAvailable();
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
            
            //Selected Item | id
            string price = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;
           
           
            //Display Items in Textbox
            IdText.Text = id;
            NameText.Text = name;
            PriceText.Text = price;
            //isAvailableText.SelectedText = isAvailable;
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
      
        private void AddArticle_OnClick(Article article, RoutedEventArgs e)
        {
            MessageBox.Show("Hurensohn");
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var idTextBox = IdText.Text;
            var nameTextBox = NameText.Text;
            var priceTextBox = PriceText.Text;
            
            ArticleDB.CreateArticle(new Article(Int32.Parse(idTextBox), ToString(NameText.Text) , Double.Parse(priceTextBox), true ) );
        }
    }
}