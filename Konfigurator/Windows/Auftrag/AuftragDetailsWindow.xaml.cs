using System;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Package;
using Konfigurator.Logic.Models.PackageDetails;

namespace Konfigurator.Windows.Auftrag
{
    public partial class AuftragDetailsWindow : Window
    {
        public AuftragDetailsWindow()
        {
            InitializeComponent();
        }
        
        public void UpdatePackageDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = PackageDB.GetDataSetPackage();
                PackageDataGrid.ItemsSource = dataset.Tables["Paket"].DefaultView;
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
                //var dataset = PackageDetailsDB.GetDataSetPackageDetails(Int32.Parse(IdText.Text));
                //ArticlesDataGrid.ItemsSource = dataset.Tables["PaketDetails"].DefaultView;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}