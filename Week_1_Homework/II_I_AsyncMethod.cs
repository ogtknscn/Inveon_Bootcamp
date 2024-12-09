using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1_Homework
{
    // Example demonstrating asynchronous programming with synchronous and asynchronous methods

    public class AsyncExamples
    {
        // Synchronous method
        public static void PerformLongOperation()
        {
            Console.WriteLine("Operation started.");
            Thread.Sleep(3000); // Simulates a long-running operation
            Console.WriteLine("Operation completed.");
        }

        // Asynchronous method
        public static async Task PerformLongOperationAsync()
        {
            Console.WriteLine("Async operation started.");
            await Task.Delay(3000); // Simulates a long-running operation asynchronously
            Console.WriteLine("Async operation completed.");
        }

        // Example of Task static methods
        public static async Task UseTaskStaticMethods()
        {
            // Run tasks in parallel
            var task1 = Task.Run(() => Console.WriteLine("Task 1 is running."));
            var task2 = Task.Run(() => Console.WriteLine("Task 2 is running."));

            await Task.WhenAll(task1, task2);
            Console.WriteLine("All tasks completed.");

            // Delay for a specific duration
            await Task.Delay(2000);
            Console.WriteLine("Task.Delay completed.");
        }

        // Async/Await with error handling
        public static async Task PerformOperationWithErrorHandlingAsync()
        {
            try
            {
                Console.WriteLine("Operation with error handling started.");
                await Task.Run(() => throw new InvalidOperationException("Something went wrong!"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Operation with error handling finished.");
            }
        }
    }
}
