using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1_Homework
{
    // Example demonstrating a violation of Liskov Substitution Principle (LSP)
    public class Duck
    {
        public virtual void Quack()
        {
            Console.WriteLine("Quack!");
        }

        public virtual void Swim()
        {
            Console.WriteLine("Swimming in the pond.");
        }
    }

    public class EngineeredDuck : Duck
    {
        public override void Quack()
        {
            Console.WriteLine("Mechanical Quack!");
        }

        public override void Swim()
        {
            throw new NotImplementedException("Engineered ducks cannot swim.");
        }
    }

    // Refactored code adhering to LSP
    public interface IQuackable
    {
        void Quack();
    }

    public interface ISwimmable
    {
        void Swim();
    }

    public class RealDuck : IQuackable, ISwimmable
    {
        public void Quack()
        {
            Console.WriteLine("Quack!");
        }

        public void Swim()
        {
            Console.WriteLine("Swimming in the pond.");
        }
    }

    public class RobotDuck : IQuackable
    {
        public void Quack()
        {
            Console.WriteLine("Mechanical Quack!");
        }
    }
}
