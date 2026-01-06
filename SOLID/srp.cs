using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DesignPatterns
{
    #region VIOLATING SRP
    public class ProductManager
    {
        public void AddProduct(string name, decimal price)
        {
            Console.WriteLine($"Adding product: {name}, Price: {price}");
            SaveToDatabase(name, price);
            PrintReportName(name, price);
        }

        private void PrintReportName(string name, decimal price)
        {
            Console.WriteLine("Saving to database...");
        }

        private void SaveToDatabase(string name, decimal price)
        {
            Console.WriteLine("Printing report...");
        }
    }
    #endregion

    #region OBEYING SRP
    public class ProductService
    {
        private readonly IProductRepository _repository;
        private readonly IReportGenerator _reportGenerator;

        public ProductService(
            IProductRepository repository,
            IReportGenerator reportGenerator)
        {
            _repository = repository;
            _reportGenerator = reportGenerator;
        }

        public void AddProduct(string name, decimal price)
        {
            Console.WriteLine($"Adding product: {name}, Price: {price}");
            _repository.Save(name, price);
            _reportGenerator.Generate(name, price);
        }
    }

    public interface IProductRepository
    {
        void Save(string name, decimal price);
    }

    public class ProductRepository : IProductRepository
    {
        public void Save(string name, decimal price)
        {
            Console.WriteLine("Saving to database ...");
        }
    }

    public interface IReportGenerator
    {
        void Generate(string name, decimal price);
    }

    public class ReportGenerator : IReportGenerator
    {
        public void Generate(string name, decimal price)
        {
            Console.WriteLine("Generating report ...")
        }
    }
    #endregion
}
