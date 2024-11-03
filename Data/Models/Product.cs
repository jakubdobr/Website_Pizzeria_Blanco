using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price26cm { get; set; }
        public decimal Price32cm { get; set; }
        public decimal Price42cm { get; set; }
    }
    public class Casseroles
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Prize { get; set; }
    }
    public class Fries
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Prize { get; set; }
    }
    public class Burgers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class Dinners
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class Kebab
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
