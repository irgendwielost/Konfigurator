namespace Konfigurator.Models.EmployeeToOrder
{
    public class EmployeeToOrder
    {
        public EmployeeToOrder(int orderId, int employeeId)
        {
            OrderID = orderId;
            EmployeeID = employeeId;
        }

        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
    }
}