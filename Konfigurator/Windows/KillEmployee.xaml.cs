using System;
using System.Windows;
using Konfigurator.Logic.Models.Employee;

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
        
        private void killEmployee(object sender, RoutedEventArgs e)
        {
            string employeeDateOfDeath = this.EmployeeDateOfDeath.SelectedDate.Value.ToShortDateString();
            EmployeeDB.KillEmployee(Int32.Parse(this.EmployeeId.Text), DateTime.Parse(employeeDateOfDeath));
            System.Threading.Thread.Sleep(1000);
        }
    }
}