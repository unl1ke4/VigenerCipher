using System;

namespace WigenerCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var view = new WigenerCipherView();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
