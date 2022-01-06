﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Konfigurator.Annotations;
using Konfigurator.Logic.Models.Package;
using Package = System.IO.Packaging.Package;

namespace Konfigurator.UserControls
{
    public partial class PaketTab : UserControl
    {
        public PaketTab()
        {
            InitializeComponent();
            UpdateDataGrid();
            
        }

        public void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = PackageDB.GetDataSetPackage();
                DataGrid.ItemsSource = dataset.Tables["Paket"].DefaultView;
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
            
            //Selected Item | Available
            bool? available = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as CheckBox)?.IsChecked;
           
            //Display Items in Textbox
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            AvailableCheck.IsChecked = available;
            IsItAvailable();
        }

        public void IsItAvailable()
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

        private void AddPackage(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;
            
            PackageDB.CreatePackage(new Logic.Models.Package.Package(Int32.Parse(id), name, available));
            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
        }

  
    }

    
}
