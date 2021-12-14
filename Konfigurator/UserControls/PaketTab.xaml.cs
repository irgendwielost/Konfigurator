using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
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
            
            //Fill DataGridView
            var dataset = PackageDB.GetDataSetPackage();
            DataGrid.ItemsSource = dataset.Tables["Paket"].DefaultView;
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
            
           
            //Display Items in Textbox
            IdText.Text = id;
            NameText.Text = name;
            //isAvailableText.SelectedText = isAvailable;
        }
    }

    
}
