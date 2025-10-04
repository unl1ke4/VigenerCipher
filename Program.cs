using System;

namespace WigenerCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var view = new WigenerCipherView();
            var controller = new WigenerCipherController(view);

            controller.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
