using System;
using System.Collections.Generic;

namespace Lab5Interfaces
{
    // === Інтерфейси ===
    public interface IBankClient
    {
        void Show();
    }

    public interface IDateMatchable
    {
        bool IsFromDate(DateTime date);
    }

    // === Абстрактний клас ===
    abstract class Client : IBankClient, IDateMatchable
    {
        public abstract void Show();
        public abstract bool IsFromDate(DateTime date);
    }

    // === Класи-нащадки ===
    class Depositor : Client
    {
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public double Amount { get; set; }
        public double Interest { get; set; }

        public Depositor(string name, DateTime date, double amount, double interest)
        {
            Name = name;
            OpenDate = date;
            Amount = amount;
            Interest = interest;
        }

        public override void Show()
        {
            Console.WriteLine($"Depositor: {Name}, Open Date: {OpenDate:dd.MM.yyyy}, Amount: {Amount}, Interest: {Interest}%");
        }

        public override bool IsFromDate(DateTime date) => OpenDate.Date == date.Date;
    }

    class Creditor : Client
    {
        public string Name { get; set; }
        public DateTime CreditDate { get; set; }
        public double Amount { get; set; }
        public double Interest { get; set; }
        public double Remainder { get; set; }

        public Creditor(string name, DateTime date, double amount, double interest, double remainder)
        {
            Name = name;
            CreditDate = date;
            Amount = amount;
            Interest = interest;
            Remainder = remainder;
        }

        public override void Show()
        {
            Console.WriteLine($"Creditor: {Name}, Credit Date: {CreditDate:dd.MM.yyyy}, Amount: {Amount}, Interest: {Interest}%, Remainder: {Remainder}");
        }

        public override bool IsFromDate(DateTime date) => CreditDate.Date == date.Date;
    }

    class Organization : Client
    {
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public string Account { get; set; }
        public double Balance { get; set; }

        public Organization(string name, DateTime date, string account, double balance)
        {
            Name = name;
            OpenDate = date;
            Account = account;
            Balance = balance;
        }

        public override void Show()
        {
            Console.WriteLine($"Organization: {Name}, Open Date: {OpenDate:dd.MM.yyyy}, Account: {Account}, Balance: {Balance}");
        }

        public override bool IsFromDate(DateTime date) => OpenDate.Date == date.Date;
    }

    // === Точка входу ===
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Task 2.9 — Clients ===");

            Client[] clients = new Client[]
            {
                new Depositor("Іванов", new DateTime(2024, 1, 1), 10000, 5),
                new Creditor("Петренко", new DateTime(2023, 12, 20), 20000, 12, 5000),
                new Organization("ТОВ Соняшник", new DateTime(2023, 6, 15), "ACC123", 180000)
            };

            Console.WriteLine("\n--- All Clients ---");
            foreach (var client in clients)
                client.Show();

            Console.WriteLine("\n--- Clients from 01.01.2024 ---");
            DateTime searchDate = new DateTime(2024, 1, 1);
            foreach (var client in clients)
                if (client.IsFromDate(searchDate))
                    client.Show();
        }
    }
}
