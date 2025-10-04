using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WigenerCipher
{
    public class WigenerCipherController
    {
         private readonly WigenerCipherModel _model;
        private readonly WigenerCipherView _view;

        public WigenerCipherController(WigenerCipherView view)
        {
            _model = new WigenerCipherModel();
            _view = view;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                int choice = _view.GetChoice();
                string text = _view.GetInputText();
                string key = _view.GetKey();

                string result;

                if (choice == 1)
                    result = _model.Encrypt(text, key);
                else if (choice == 2)
                    result = _model.Decrypt(text, key);
                else
                    result = "Wrong choice";

                _view.ShowResult(result);

                Console.WriteLine("\nWhat do you want to do next?");
                Console.WriteLine("1 – Return to mode selection");
                Console.WriteLine("2 – Exit the program");
                Console.Write("Your choice: ");

                string next = Console.ReadLine();
                if (next != "1")
                    running = false;
            }
        }
    }
}
