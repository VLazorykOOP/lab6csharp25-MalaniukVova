using System;
using System.Collections.Generic;

namespace Lab5
{
    // Інтерфейси .NET
    public interface IComparable
    {
        int CompareTo(object obj);
    }

    public interface ICloneable
    {
        object Clone();
    }

    // Інтерфейс користувача
    public interface IShowable
    {
        void Show();
    }

    class Product : IShowable, ICloneable, IComparable
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
        public object Clone() => MemberwiseClone();
        public int CompareTo(object obj)
        {
            if (obj is Product other)
                return Price.CompareTo(other.Price);
            return 0;
        }
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

    abstract class Client : IShowable
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

    struct Patient
    {
        public string Surname, Address, Card, Insurance;
        public Patient(string s, string a, string c, string i)
        {
            Surname = s; Address = a; Card = c; Insurance = i;
        }
        public void Show() => Console.WriteLine($"{Surname}, {Address}, Card: {Card}, Insurance: {Insurance}");
    }

    public class CustomDivideByZeroException : Exception
    {
        public CustomDivideByZeroException() : base("Помилка: ділення на нуль (користувацький виняток).") { }
        public CustomDivideByZeroException(string message) : base(message) { }
    }

    public static class MathHelper
    {
        public static double SafeDivide(double a, double b)
        {
            if (b == 0)
                throw new CustomDivideByZeroException($"Спроба поділити {a} на нуль.");
            return a / b;
        }
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

            foreach (var client in clients) client.Show();

            Console.WriteLine("\n=== Search by date 01.01.2024 ===");
            foreach (var client in clients)
                if (client.IsFromDate(new DateTime(2024, 1, 1))) client.Show();

            Console.WriteLine("\n=== Task 4.9 ===");
            List<Patient> patients = new List<Patient> {
                new Patient("Іванов", "Чернівці", "C001", "S100"),
                new Patient("Петренко", "Київ", "C002", "S101"),
                new Patient("Шевченко", "Львів", "C003", "S102"),
            };

            patients.RemoveAll(p => p.Card == "C002");
            patients.Insert(0, new Patient("Новий", "Одеса", "C004", "S103"));
            patients.Insert(0, new Patient("ЩеНовий", "Харків", "C005", "S104"));

            foreach (var p in patients) p.Show();

            Console.WriteLine("\n=== Task 3.2 — Обробка винятків ===");
            double[] numerators = { 10, 20, 30 };
            double[] denominators = { 2, 0, 5 };

            for (int i = 0; i < numerators.Length; i++)
            {
                try
                {
                    double result = MathHelper.SafeDivide(numerators[i], denominators[i]);
                    Console.WriteLine($"{numerators[i]} / {denominators[i]} = {result}");
                }
                catch (CustomDivideByZeroException ex)
                {
                    Console.WriteLine($"[Custom Exception] {ex.Message}");
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine($"[Standard Exception] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Unexpected Error] {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("-> Операція завершена.\n");
                }
            }
        }
    }
}