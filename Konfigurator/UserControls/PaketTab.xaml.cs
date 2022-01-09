using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Konfigurator.Annotations;
using Konfigurator.Logic.Models.Package;
using Konfigurator.Logic.Models.PackageDetails;
using Konfigurator.Windows;
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
        
        public void UpdateArticleInPackageDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = PackageDetailsDB.GetDataSetPackageDetails(Int32.Parse(IdText.Text));
                ArticleInPackageDataGrid.ItemsSource = dataset.Tables["PaketDetails"].DefaultView;
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
            UpdateArticleInPackageDataGrid();
        }
        
        //Hides the password Column 
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Paket_ID")
            {
                e.Cancel = true;
            }

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

        //Edit Package 
        private void EditPackage(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;
            
            PackageDB.UpdatePackage(new Logic.Models.Package.Package(Int32.Parse(id), name, available));
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }
        
        private void AddPackage(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            bool available = AvailableCheck.IsChecked != null && (bool)AvailableCheck.IsChecked;
            
            PackageDB.CreatePackage(new Logic.Models.Package.Package(Int32.Parse(id), name, available));
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }

        //Opens PackageDetailsWindow
        private void AddArticleToPackage(object sender, RoutedEventArgs e)
        {
            PaketDetails packageDetails = new PaketDetails(Int32.Parse(IdText.Text));
            packageDetails.Show();
        }
        
        private void KillPackage(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            PackageDB.KillPackage(Int32.Parse(id));
            
            System.Threading.Thread.Sleep(100);
            UpdateDataGrid();
        }
  
    }

    
}
