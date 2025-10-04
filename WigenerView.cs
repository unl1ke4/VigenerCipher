namespace WigenerCipher
{
    public class WigenerCipherView
    {
        public string GetInputText()
        {
            string text;
            while (true)
            {
                Console.Write("Enter text: ");
                text = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(text) && text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    break;

                Console.WriteLine("Error: text must contain only letters and spaces!");
            }
            return text;
        }

        public string GetKey()
        {
            string key;
            while (true)
            {
                Console.Write("Enter key: ");
                key = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(key) && key.All(char.IsLetter))
                    break;

                Console.WriteLine("Error: key must contain only letters!");
            }
            return key;
        }

        public void ShowResult(string result)
        {
            Console.WriteLine("Result: " + result);
        }

        public int GetChoice()
        {
            int choice;
            while (true)
            {
                Console.WriteLine("1 – Encrypt");
                Console.WriteLine("2 – Decrypt");
                Console.Write("Your choice: ");

                string input = Console.ReadLine();
                if (int.TryParse(input, out choice) && (choice == 1 || choice == 2))
                    break;

                Console.WriteLine("Error: enter only 1 or 2!");
            }
            return choice;
        }
    }
}
