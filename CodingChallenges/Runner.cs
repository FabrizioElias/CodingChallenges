using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CodingChallenges
{
    internal class Runner
    {
        public (object? Result, TimeSpan elapsedTime) Execute(string classNamespace, byte[] compiledAssembly, object?[]? arguments)
        {
            var resultObject = LoadAndExecute(classNamespace, compiledAssembly, arguments);

            for (var i = 0; i < 8 && resultObject.AssemblyRef.IsAlive; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //Console.WriteLine(resultObject.AssemblyRef.IsAlive ? "Unloading failed!" : "Unloading success!");
            return (resultObject.Result, resultObject.ElapsedTime);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static (WeakReference AssemblyRef, object? Result, TimeSpan ElapsedTime) LoadAndExecute(string classNamespace, byte[] compiledAssembly, object?[]? arguments)
        {
            ResultClass? result = null;
            using (var asm = new MemoryStream(compiledAssembly))
            {
                var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

                var assembly = assemblyLoadContext.LoadFromStream(asm);

                var entry = assembly.DefinedTypes.FirstOrDefault(dt => dt.Namespace == classNamespace && dt.Name == "ChallengeClass")?.GetMethod("ChallengeRunner");

                if (entry != null)
                {
                    if (entry.GetParameters().Length > 0)
                    {
                        //Result is of type (dynamic output, TimeSpan elapsedTime)
                        result = (ResultClass)entry.Invoke(null, arguments);
                    }
                    else
                    {
                        result = (ResultClass)entry.Invoke(null, null);
                    }
                }
                else
                    Console.WriteLine("Challenge function not found!");

                assemblyLoadContext.Unload();

                return (new WeakReference(assemblyLoadContext), result?.Output, result?.ElapsedTime ?? TimeSpan.MaxValue);
            }
        }
    }

    public class ResultClass
    {
        public dynamic? Output { get; set; }
        public TimeSpan ElapsedTime { get; set; }
    }
}
