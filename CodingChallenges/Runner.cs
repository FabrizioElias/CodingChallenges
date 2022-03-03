using System.Runtime.CompilerServices;

namespace CodingChallenges
{
    internal class Runner
    {
        public object Execute(byte[] compiledAssembly, object?[]? arguments)
        {
            var resultObject = LoadAndExecute(compiledAssembly, arguments);

            for (var i = 0; i < 8 && resultObject.AssemblyRef.IsAlive; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            //Console.WriteLine(resultObject.AssemblyRef.IsAlive ? "Unloading failed!" : "Unloading success!");
            return resultObject.Result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static (WeakReference AssemblyRef, object Result) LoadAndExecute(byte[] compiledAssembly, object?[]? arguments)
        {
            object result = null;
            using (var asm = new MemoryStream(compiledAssembly))
            {
                var assemblyLoadContext = new SimpleUnloadableAssemblyLoadContext();

                var assembly = assemblyLoadContext.LoadFromStream(asm);

                var entry = assembly.DefinedTypes.FirstOrDefault(dt => dt.Name == "ChallengeClass")?.GetMethod("ChallengeRunner");

                if (entry != null)
                {

                    result = entry.GetParameters().Length > 0
                        ? entry.Invoke(null, arguments)
                        : entry.Invoke(null, null);
                }
                else
                    Console.WriteLine("Challenge function not found!");

                assemblyLoadContext.Unload();

                return (new WeakReference(assemblyLoadContext), result);
            }
        }
    }
}
