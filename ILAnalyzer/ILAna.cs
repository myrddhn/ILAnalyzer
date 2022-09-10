using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILAnalyzer
{
    class ILAna
    {
        static void Main(string[] args)
        {
            string module = "";
            bool dumpIL = false;


            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ILAna.exe [module] [-d] <dump IL>");
                Environment.Exit(255);
            }

            if (args.Length == 1)
            {
                module = args[0];
            }

            if (args.Length == 2)
            {
                module = args[0];
                dumpIL = (args[1] == "-d");
            }

            ModuleDefinition mHandle = ModuleDefinition.ReadModule(module);

            int totalInstructions = 0;
            int totalFields = 0;
            int totalVariables = 0;
            foreach (TypeDefinition type in mHandle.Types)
            {
                totalFields += type.Fields.Count;
                Console.WriteLine(type.FullName);
                foreach (var method in type.Methods)
                {
                    totalVariables += method.Body.Variables.Count;
                    Console.WriteLine("  Method: " + method.Name);
                    totalInstructions += method.Body.Instructions.Count;
                    Console.WriteLine("    Instructions: " + method.Body.Instructions.Count);
                    Console.WriteLine("    Variables   : " + method.Body.Variables.Count);
                    if (dumpIL)
                    {
                        foreach (var inst in method.Body.Instructions)
                        {
                            Console.WriteLine("      " + inst.Next /*+  inst.OpCode + "\t\t" + inst.Operand*/);
                        }
                    }
                }
            }

            Console.WriteLine("==========================================================");
            Console.WriteLine("Total instructions: " + totalInstructions);
            Console.WriteLine("Total fields      : " + totalFields);
            Console.WriteLine("Total variables   : " + totalVariables);
            Console.WriteLine("==========================================================");

        }
    }
}
