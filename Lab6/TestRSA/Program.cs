using Encryptors;
using System;
using System.Security.Cryptography;

namespace TestRSA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Ingrese el texto por cifrar:");
                string text = Console.ReadLine();
                Console.WriteLine("Ingrese p:");
                int p = int.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese q:");
                int q = int.Parse(Console.ReadLine());
                var rsa = new RSAEncryptor("..//..//..");
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("Ha ocurrido un error.");
                Main(args);
            }
        }
    }
}
