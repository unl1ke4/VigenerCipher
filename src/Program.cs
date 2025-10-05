namespace VigenereCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var view = new VigenereCipherView();
            var controller = new VigenereCipherController(view);

            controller.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
