using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Konfigurator.Logic.Models.Employee;
using Konfigurator.Windows;

namespace Konfigurator.UserControls
{
    public partial class MitarbeiterTab : UserControl
    {
        public MitarbeiterTab()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            //Fill DataGridView
            try
            {
                var dataset = EmployeeDB.GetDataSetEmployee();
                DataGrid.ItemsSource = dataset.Tables["Mitarbeiter"].DefaultView;
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
            bool? isEmployed = (DataGrid.SelectedCells[2].Column.GetCellContent(item) as CheckBox)?.IsChecked;
           
            //Display Items in Textbox
            if (id != null) IdText.Text = id;
            if (name != null) NameText.Text = name;
            employedCheck.IsChecked = isEmployed;
            IsEmployed();
        }

        //Set employedText to "Angestellt" or "Nicht Angestellt"
        private void IsEmployed()
        {
            bool available = employedCheck.IsChecked != null && (bool)employedCheck.IsChecked;

            if (available)
            {
                employedText.Text = "Angestellt";
                employedText.Foreground = Brushes.Green;
            }
            else
            {
                employedText.Text = "Nicht Angestellt";
                employedText.Foreground = Brushes.Red;
            }
        }
        
        //Hides the password Column 
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Mitarbeiter_Passwort")
            {
                e.Cancel = true;
            }
            
        }
        //Edit Employee
        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            bool employed = employedCheck.IsChecked != null && (bool)employedCheck.IsChecked;
            var employedDateSelectedDate = this.employedDate.SelectedDate;
            
            if (employedDateSelectedDate != null)
            {
                var employedDate = employedDateSelectedDate.Value.ToShortDateString();
            
                EmployeeDB.UpdateEmployee(new Employee(Int32.Parse(id), name, name, employed, 
                    DateTime.Parse(employedDate), null));
                
                System.Threading.Thread.Sleep(100);
                UpdateDataGrid();
            }
            else
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Das Datum wurde nicht  eingegeben\n" +
                                "========");;
            }
            
            
        }
        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            var name = NameText.Text;
            bool employed = employedCheck.IsChecked != null && (bool)employedCheck.IsChecked;
            var employedDateSelectedDate = this.employedDate.SelectedDate;
            if (employedDateSelectedDate != null)
            {
                var employedDate = employedDateSelectedDate.Value.ToShortDateString();
            
                EmployeeDB.CreateEmployee(new Employee(Int32.Parse(id), name, name, employed, 
                    DateTime.Parse(employedDate), null) );
            }
            else
            {
                MessageBox.Show("======== Ein Fehler ist Aufgetreten: ========\n" +
                                "Das Datum wurde nicht eingegeben\n" +
                                "========");;
            }

            System.Threading.Thread.Sleep(1000);
            UpdateDataGrid();
        }

        private void KillEmployee(object sender, RoutedEventArgs e)
        {
            var id = IdText.Text;
            
            KillEmployee killEmployeeWindow = new KillEmployee(Int32.Parse(id));
            killEmployeeWindow.Show();
        }
    }
}