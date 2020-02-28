using System;
using System.IO;
using System.Reflection;

namespace LateBindingApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("***** Fun with Late Binding *****");
            // Try to load a local copy of CarLibrary.
            Assembly a;
            try
            {
                a = Assembly.Load("CarLibrary");
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (a != null)
                CreateUsingLateBinding(a);

            Console.ReadLine();
        }

        static void CreateUsingLateBinding(Assembly asm)
        {
            try
            {
                // Get metadata for the Minivan type.
                Type miniVan = asm.GetType("CarLibrary.MiniVan");

                // Create a Minivan instance on the fly.
                object obj = Activator.CreateInstance(miniVan);
                Console.WriteLine($"Created a {obj} using late binding!");
                // Get info for TurboBoost.
                MethodInfo mi = miniVan.GetMethod("TurboBoost");

                // Invoke method ('null' for no parameters).
                mi.Invoke(obj, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
    }
}
