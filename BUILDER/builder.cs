using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text;

namespace DesignPatterns
{
    #region VIOLATION BUILDER

    // Violation: messy constructor with many parameters
    public class CarV
    {
        public string Make { get; }
        public string Model { get; }
        public string Color { get; }
        public int Year { get; }
        public bool HasSunroof { get; }

        public CarV(string make, string model, string color, int year, bool hasSunroof)
        {
            Make = make;
            Model = model;
            Color = color;
            Year = year;
            HasSunroof = hasSunroof;
        }

        public override string ToString()
        {
            return $"{Year} {Color} {Make} {Model} (Sunroof: {HasSunroof})";
        }
    }

    public class DemoViolation
    {
        public static void Run()
        {
            // Hard to read, error-prone
            var car = new CarV("Toyota", "Corolla", "Blue", 2024, true);
            Console.WriteLine(car);
        }
    }

    #endregion

    #region OBEYING BUILDER


    // ✅ Obeying: Builder encapsulates construction logic
    public class Car
    {
        public string Make { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public int Year { get; private set; }
        public bool HasSunroof { get; private set; }

        private Car() { }

        public override string ToString()
        {
            return $"{Year} {Color} {Make} {Model} (Sunroof: {HasSunroof})";
        }


        // Nested Builder
        public class Builder
        {
            private readonly Car _car = new Car();

            public Builder SetMake(string make)
            {
                _car.Make = make;
                return this;
            }
            public Builder SetModel(string model)
            {
                _car.Model = model;
                return this;
            }
            public Builder SetColor(string color)
            {
                _car.Color = color;
                return this;
            }
            public Builder SetYear(int year)
            {
                _car.Year = year;
                return this;
            }
            public Builder SetSunroof(bool hasSunroof)
            {
                _car.HasSunroof = hasSunroof;
                return this;
            }

            public Car Build() => _car;
        }
    }

    public class DemoObeying
    {
        public static void Run()
        {
            var car = new Car.Builder()
                .SetMake("Toyota")
                .SetModel("Corolla")
                .SetColor("Blue")
                .SetYear(2024)
                .SetSunroof(true)
                .Build();

            Console.WriteLine(car);
        }
    }

    #endregion

    #region Demo
    public class Demo
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Builder Violation Demo ===");
            DemoViolation.Run();

            Console.WriteLine("\n=== Builder Obeying Demo ===");
            DemoObeying.Run();

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }
    }

    #endregion
}
