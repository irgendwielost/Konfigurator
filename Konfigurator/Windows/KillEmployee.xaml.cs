using System;
using System.Windows;
using System.Windows.Controls;
using Konfigurator.Logic.Models.Employee;
using Konfigurator.UserControls;

namespace Konfigurator.Windows
{
    public partial class KillEmployee : Window
    {
        public KillEmployee(int employeeId)
        {
            InitializeComponent();
            EmployeeId.Text = employeeId.ToString();
        }
        
        
        private int EmployeeID;
        
        private MitarbeiterTab _mitarbeiterTab = new MitarbeiterTab();
        private void killEmployee(object sender, RoutedEventArgs e)
        {
            var selectedDate = this.EmployeeDateOfDeath.SelectedDate;
            if (selectedDate != null)
            {
                string employeeDateOfDeath = selectedDate.Value.ToShortDateString();
                EmployeeDB.KillEmployee(Int32.Parse(this.EmployeeId.Text), DateTime.Parse(employeeDateOfDeath));
            }

            System.Threading.Thread.Sleep(500);
            this.Close();
        }
    }
}