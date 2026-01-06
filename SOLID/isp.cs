using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DesignPatterns
{
    #region VIOLATING ISP

    // Fat interface: forces all workers to implement all methods
    public interface IWorkerV
    {
        void Work();
        void Eat();
        void Sleep();
    }

    // Robot doesn't eat or sleep, but is forced to implement them
    public class RobotWorkerV : IWorkerV
    {
        public void Work()
        {
            Console.WriteLine("Robot is workin tirelessly.");
        }

        public void Eat()
        {
            throw new NotImplementedException("Robots don't eat!");
        }

        public void Sleep()
        {
            throw new NotImplementedException("Robots don't sleep!");
        }
    }

    // Human worker can implement all, but still forced
    public class HumanWorkerV : IWorkerV
    {
        void IWorkerV.Work()
        {
            Console.WriteLine("Human is working.");
        }

        void IWorkerV.Eat()
        {
            Console.WriteLine("Human is eating lunch.");
        }

        void IWorkerV.Sleep()
        {
            Console.WriteLine("Human is sleeping.");
        }
    }

    public class DemoViolation
    {
        public static void Run()
        {
            IWorkerV robot = new RobotWorkerV();
            robot.Work();
            try
            {
                robot.Eat();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    #endregion

    #region OBEYING ISP

    // Segragated interfaces: each worker only implements what they need
    public interface IWork
    {
        void Work();
    }

    public interface IEat
    {
        void Eat();
    }

    public interface ISleep
    {
        void Sleep();
    }

    public class RobotWorker : IWork
    {
        public void Work()
        {
            Console.WriteLine("Robot is working tirelessly.");
        }
    }

    public class HumanWorker : IWork, IEat, ISleep
    {
        public void Work()
        {
            Console.WriteLine("Human is working.");
        }

        public void Eat()
        {
            Console.WriteLine("Human is eating lunch.");
        }

        public void Sleep()
        {
            Console.WriteLine("Human is sleeping.");
        }
    }

    public class DemoObeying
    {
        public static void Run()
        {
            IWork robot = new RobotWorker();
            robot.Work();

            HumanWorker human = new HumanWorker();
            human.Work();
            human.Eat();
            human.Sleep();
        }
    }
    #endregion

    #region DEMO

    public class Demo
    {
        public static void Main()
        {
            Console.WriteLine("=== ISP Violation Demo ===");
            DemoViolation.Run();

            Console.WriteLine("\n=== ISP Obeying Demo ===");
            DemoObeying.Run();

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();

        }
    }

    #endregion
}
