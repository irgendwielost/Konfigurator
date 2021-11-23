using System;

namespace Konfigurator.Logic.Models.Employee
{
    public class Employee
    {
        public Employee(int id, string name, string password, bool working, DateTime started, DateTime ended)
        {
            ID = id;
            Name = name;
            Password = password;
            Working = working;
            Started = started;
            Ended = ended;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Working { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
    }
}