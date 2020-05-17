using System;

namespace MyBicycle.Model
{
    public class Customers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Model { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Description_Long { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}