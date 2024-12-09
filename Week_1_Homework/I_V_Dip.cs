namespace Week_1_Homework
{
    // Example demonstrating a violation of DIP
    
    public class LowLevelModule
    {
        public void DoSomething()
        {
            Console.WriteLine("Low-level module is doing something.");
        }
    }

    public class HighLevelModule
    {
        private readonly LowLevelModule _lowLevelModule = new();

        public void Execute()
        {
            _lowLevelModule.DoSomething();
        }
    }

    // Refactored code adhering to DIP
    public interface IService
    {
        void DoSomething();
    }

    public class LowLevelModuleRefactored : IService
    {
        public void DoSomething()
        {
            Console.WriteLine("Low-level module is doing something.");
        }
    }

    public class HighLevelModuleRefactored(IService service)
    {
        private readonly IService _service = service;

        public void Execute()
        {
            _service.DoSomething();
        }
    }
}


