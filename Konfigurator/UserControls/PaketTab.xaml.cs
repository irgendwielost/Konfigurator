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
    }

    
}
