using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DesignPatterns
{
    #region VIOLATING OCP
    // This violates OCP because adding new shapes requires modifyin the calculator.
    public class CircleV
    {
        public double Radius { get; set; }
    }

    public class SquareV
    {
        public double Side { get; set; }
    }

    public class ShapeAreaCalculatorV
    {
        public double CalculateArea(object shape)
        {
            if (shape is CircleV c)
                return Math.PI * c.Radius * c.Radius;
            else if (shape is SquareV s)
                return s.Side * s.Side;

            // Adding a new shape requires editing this method -> violates OCP.
            throw new NotSupportedException("Shape not supported");
        }
    }
    #endregion

    #region OBEYING OCP
    // This obeys OCP by using abstraction (interface + polymorphism).
    public interface IShape
    {
        double Area();
    }

    public class Circle : IShape
    {
        public double Radius { get; set; }
        public double Area() => Math.PI * Radius * Radius;
    }

    public class Square : IShape
    {
        public double Side { get; set; }
        public double Area() => Side * Side;
    }

    // Adding new shape without modifying existing code
    public class Triangle : IShape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public double Area() => 0.5 * Base * Height;
    }

    public class  ShapeAreaCalculator
    {
        public double CalculateArea(IShape shape) => shape.Area();
    }
    #endregion

    #region DEMO

    public class Demo
    {
        public static void Main()
        {
            Console.WriteLine("=== OCP Violation Demo ===");
            var calcV = new ShapeAreaCalculatorV();
            Console.WriteLine("Circle area: " + calcV.CalculateArea(new CircleV { Radius = 5 }));
            Console.WriteLine("Square area: " + calcV.CalculateArea(new SquareV { Side = 4 }));

            Console.WriteLine("\n=== OCP Obeying Demo ===");
            var calc = new ShapeAreaCalculator();

            IShape circle = new Circle { Radius = 5 };
            IShape square = new Square { Side = 4 };
            IShape triangle = new Triangle { Base = 6, Height = 3 };

            Console.WriteLine("Circle area: " + calc.CalculateArea(circle));
            Console.WriteLine("Square area: " + calc.CalculateArea(square));
            Console.WriteLine("Triangle area: " + calc.CalculateArea(triangle));
            Console.ReadLine();
        }
    }
    #endregion
}
