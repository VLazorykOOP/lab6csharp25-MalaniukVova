using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5
{
    // 1.9 / 2.9 — Ієрархія продуктів
    class Product
    {
        public string Name;
        public double Price;

        public Product() => Console.WriteLine("Product()");
        public Product(string name, double price)
        {
            Name = name; Price = price;
            Console.WriteLine("Product(string, double)");
        }

        public virtual void Show() => Console.WriteLine($"Product: {Name}, Price: {Price}");
        ~Product() => Console.WriteLine("~Product()");
    }

    class Item : Product
    {
        public string Manufacturer;
        public Item() => Console.WriteLine("Item()");
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
        ~Item() => Console.WriteLine("~Item()");
    }

    class Toy : Item
    {
        public string Material;
        public Toy() => Console.WriteLine("Toy()");
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
        ~Toy() => Console.WriteLine("~Toy()");
    }

    class DairyProduct : Item
    {
        public DateTime ExpiryDate;
        public DairyProduct(string name, double price, string manufacturer, DateTime expiry)
            : base(name, price, manufacturer)
        {
            ExpiryDate = expiry;
            Console.WriteLine("DairyProduct(...)");
        }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Expiry: {ExpiryDate.ToShortDateString()}");
        }
        ~DairyProduct() => Console.WriteLine("~DairyProduct()");
    }

    // Абстрактний клас Client
    abstract class Client
    {
        public abstract void Show();
        public abstract bool IsFromDate(DateTime date);
    }

    class Depositor : Client
    {
        public string Name;
        public DateTime OpenDate;
        public double Amount;
        public double Interest;

        public Depositor(string name, DateTime date, double amount, double interest)
        {
            Name = name; OpenDate = date; Amount = amount; Interest = interest;
        }

        public override void Show() =>
            Console.WriteLine($"Depositor: {Name}, Date: {OpenDate.ToShortDateString()}, Sum: {Amount}, Interest: {Interest}%");

        public override bool IsFromDate(DateTime date) => OpenDate.Date == date.Date;
    }

    class Creditor : Client
    {
        public string Name;
        public DateTime CreditDate;
        public double Amount;
        public double Interest;
        public double Remainder;

        public Creditor(string name, DateTime date, double amount, double interest, double remainder)
        {
            Name = name; CreditDate = date; Amount = amount; Interest = interest; Remainder = remainder;
        }

        public override void Show() =>
            Console.WriteLine($"Creditor: {Name}, Date: {CreditDate.ToShortDateString()}, Credit: {Amount}, Left: {Remainder}");

        public override bool IsFromDate(DateTime date) => CreditDate.Date == date.Date;
    }

    class Organization : Client
    {
        public string Name;
        public DateTime OpenDate;
        public string Account;
        public double Balance;

        public Organization(string name, DateTime date, string account, double balance)
        {
            Name = name; OpenDate = date; Account = account; Balance = balance;
        }

        public override void Show() =>
            Console.WriteLine($"Organization: {Name}, Account: {Account}, Open: {OpenDate.ToShortDateString()}, Balance: {Balance}");

        public override bool IsFromDate(DateTime date) => OpenDate.Date == date.Date;
    }

    // Структура Patient
    struct Patient
    {
        public string Surname, Address, Card, Insurance;
        public Patient(string s, string a, string c, string i)
        {
            Surname = s; Address = a; Card = c; Insurance = i;
        }
        public void Show() => Console.WriteLine($"{Surname}, {Address}, Card: {Card}, Insurance: {Insurance}");
    }

    // Клас-колекція з реалізацією IEnumerable<Patient>
    class PatientCollection : IEnumerable<Patient>
    {
        private List<Patient> patients = new List<Patient>();

        public void Add(Patient p) => patients.Add(p);
        public void RemoveByCard(string card) => patients.RemoveAll(p => p.Card == card);
        public void InsertAtStart(Patient p) => patients.Insert(0, p);

        public IEnumerator<Patient> GetEnumerator() => patients.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Task 1.9 / 2.9 ===");
            Toy toy = new Toy("LEGO", 599.99, "LEGO Group", "Plastic");
            toy.Show();

            DairyProduct milk = new DairyProduct("Молоко", 39.90, "Галичина", DateTime.Now.AddDays(10));
            milk.Show();

            Console.WriteLine("\n=== Task 3.9 ===");
            Client[] clients = {
                new Depositor("Іванов", new DateTime(2024,1,1), 10000, 5),
                new Creditor("Петренко", new DateTime(2023,12,20), 20000, 12, 5000),
                new Organization("ТОВ Соняшник", new DateTime(2023,6,15), "ACC123", 180000)
            };

            foreach (var client in clients)
                client.Show();

            Console.WriteLine("\n=== Search by date 01.01.2024 ===");
            foreach (var client in clients)
                if (client.IsFromDate(new DateTime(2024, 1, 1)))
                    client.Show();

            Console.WriteLine("\n=== Task 4.9 + IEnumerable ===");

            PatientCollection patientCollection = new PatientCollection();
            patientCollection.Add(new Patient("Іванов", "Чернівці", "C001", "S100"));
            patientCollection.Add(new Patient("Петренко", "Київ", "C002", "S101"));
            patientCollection.Add(new Patient("Шевченко", "Львів", "C003", "S102"));

            // Видалення за карткою
            patientCollection.RemoveByCard("C002");

            // Додавання нових
            patientCollection.InsertAtStart(new Patient("Новий", "Одеса", "C004", "S103"));
            patientCollection.InsertAtStart(new Patient("ЩеНовий", "Харків", "C005", "S104"));

            // Перебір через foreach (IEnumerable)
            foreach (var p in patientCollection)
                p.Show();
        }
    }
}
