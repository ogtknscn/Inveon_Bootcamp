using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week_1_Homework
{
    // This file demonstrates async/await with error handling in C#.

    public class AsyncAwaitExample
    {
        public async Task AsyncOperationWithTryCatch()
        {
            Console.WriteLine("Async operation with try/catch started.");

            try
            {
                // Simulating an asynchronous task that might throw an exception
                await Task.Run(() =>
                {
                    if (DateTime.Now.Second % 2 == 0)
                    {
                        throw new InvalidOperationException("Simulated exception.");
                    }
                    Console.WriteLine("Task completed successfully.");
                });
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Handled exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Async operation with try/catch finished.");
            }
        }
    }
}

