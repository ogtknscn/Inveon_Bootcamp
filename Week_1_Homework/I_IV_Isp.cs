using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1_Homework
{
    // Example demonstrating a violation of ISP
    public interface IWorker
    {
        void Work();
        void Eat();
    }

    public class HumanWorker : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Human is working.");
        }

        public void Eat()
        {
            Console.WriteLine("Human is eating.");
        }
    }

    public class RobotWorker : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Robot is working.");
        }

        public void Eat()
        {
            throw new NotImplementedException("Robots do not eat.");
        }
    }

    // Refactored code adhering to ISP
    public interface IWorkable
    {
        void Work();
    }

    public interface IEatable
    {
        void Eat();
    }

    public class HumanWorkerRefactored : IWorkable, IEatable
    {
        public void Work()
        {
            Console.WriteLine("Human is working.");
        }

        public void Eat()
        {
            Console.WriteLine("Human is eating.");
        }
    }

    public class RobotWorkerRefactored : IWorkable
    {
        public void Work()
        {
            Console.WriteLine("Robot is working.");
        }
    }
}

