using System;
using System.Diagnostics;
namespace Perfomance
{
    class Program
    {
        static void Main()
        {
            Process process = Process.GetCurrentProcess();
            
            var rnd = new Random();
            process.Refresh();
            var beforeClasses = process.PrivateMemorySize64;
            C[] classes = new C[100000];
            for (int i = 0; i < classes.Length; i++)
            {
                classes[i] = new C(rnd.Next());
            }
            process.Refresh();
            var afterClasses = process.PrivateMemorySize64;
            var deltaClasses = afterClasses - beforeClasses;

            Console.WriteLine($"Delta for initializing classes is {deltaClasses}");

            process.Refresh();
            var beforeStructs = process.PrivateMemorySize64;
            S[] structs = new S[100000];
            for (int i = 0; i < structs.Length; i++)
            {
                structs[i] = new S(rnd.Next());
            }
            process.Refresh();
            var afterStructs = process.PrivateMemorySize64;
            var deltaStructs = afterStructs - beforeStructs;

            Console.WriteLine($"Delta for initializing structs is {deltaStructs}");
            Console.WriteLine($"Delta between structs and classes is {deltaClasses - deltaStructs}");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Array.Sort<C>(classes);
            stopwatch.Stop();
            Console.WriteLine($"Time used for sorting classes is {Environment.NewLine} Hours:{stopwatch.Elapsed.Hours} Minutes:{stopwatch.Elapsed.Minutes} Seconds:{stopwatch.Elapsed.Seconds} Milliseconds:{stopwatch.Elapsed.Milliseconds / 10}");

            stopwatch.Reset();
            stopwatch.Start();
            Array.Sort<S>(structs);
            stopwatch.Stop();
            Console.WriteLine($"Time used for sorting structs is {Environment.NewLine} Hours:{stopwatch.Elapsed.Hours} Minutes:{stopwatch.Elapsed.Minutes} Seconds:{stopwatch.Elapsed.Seconds} Milliseconds:{stopwatch.Elapsed.Milliseconds/10}");
        }
    }
}
