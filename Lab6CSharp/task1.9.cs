using System;
using System.Collections.Generic;

namespace Lab5Interfaces
{
    // === Інтерфейси ===
    public interface IProduct
    {
        string Name { get; set; }
        double Price { get; set; }
        void Show();
    }

    public interface IManufacturerInfo
    {
        string Manufacturer { get; set; }
    }

    public interface IMaterialInfo
    {
        string Material { get; set; }
    }

    public interface IExpiryInfo
    {
        DateTime ExpiryDate { get; set; }
    }

    // === Клас Product ===
    class Product : IProduct, IComparable<Product>, IDisposable
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product()
        {
            Console.WriteLine("Product()");
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
            Console.WriteLine("Product(string, double)");
        }

        public virtual void Show()
        {
            Console.WriteLine($"Product: {Name}, Price: {Price}");
        }

        public int CompareTo(Product other)
        {
            return Price.CompareTo(other.Price);
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing Product: {Name}");
        }

        ~Product()
        {
            Console.WriteLine("~Product()");
        }
    }

    // === Клас Item ===
    class Item : Product, IManufacturerInfo
    {
        public string Manufacturer { get; set; }

        public Item() : base()
        {
            Console.WriteLine("Item()");
        }

        public Item(string name, double price, string manufacturer)
            : base(name, price)
        {
            Manufacturer = manufacturer;
            Console.WriteLine("Item(string, double, string)");
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Manufacturer: {Manufacturer}");
        }

        ~Item()
        {
            Console.WriteLine("~Item()");
        }
    }

    // === Клас Toy ===
    class Toy : Item, IMaterialInfo
    {
        public string Material { get; set; }

        public Toy() : base()
        {
            Console.WriteLine("Toy()");
        }

        public Toy(string name, double price, string manufacturer, string material)
            : base(name, price, manufacturer)
        {
            Material = material;
            Console.WriteLine("Toy(string, double, string, string)");
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Material: {Material}");
        }

        ~Toy()
        {
            Console.WriteLine("~Toy()");
        }
    }

    // === Клас DairyProduct ===
    class DairyProduct : Item, IExpiryInfo
    {
        public DateTime ExpiryDate { get; set; }

        public DairyProduct(string name, double price, string manufacturer, DateTime expiryDate)
            : base(name, price, manufacturer)
        {
            ExpiryDate = expiryDate;
            Console.WriteLine("DairyProduct(...)");
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Expiry: {ExpiryDate.ToShortDateString()}");
        }

        ~DairyProduct()
        {
            Console.WriteLine("~DairyProduct()");
        }
    }

    // === Main ===
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Task 1.9 — Interfaces ===");

            Toy toy = new Toy("LEGO", 599.99, "LEGO Group", "Plastic");
            toy.Show();

            Console.WriteLine();

            DairyProduct milk = new DairyProduct("Молоко", 39.90, "Галичина", DateTime.Now.AddDays(10));
            milk.Show();

            // Demonstrate IComparable
            Console.WriteLine("\n=== Sorting Products ===");
            List<Product> products = new List<Product> {
                new Product("Хліб", 25.50),
                new Product("Цукор", 40.00),
                new Product("Сіль", 20.00)
            };

            products.Sort();
            foreach (var p in products) p.Show();

            // Dispose demo
            using (Product p = new Product("DisposableExample", 100)) { }
        }
    }
}
