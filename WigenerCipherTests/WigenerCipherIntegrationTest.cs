using WigenerCipher;

namespace WigenerCipherTests
{
    // Мок для View, який замінює роботу з консоллю
    public class MockView : WigenerCipherView
    {
        private readonly Queue<string> _inputs;
        public string? Output { get; private set; }

        public MockView(IEnumerable<string> inputs)
        {
            _inputs = new Queue<string>(inputs);
        }

        public override string GetInputText() => _inputs.Dequeue();
        public override string GetKey() => _inputs.Dequeue();
        public override int GetChoice() => int.Parse(_inputs.Dequeue());
        public override void ShowResult(string result) => Output = result;
    }

    public class WigenerCipherIntegrationTests
    {
        [Fact]
        public void Controller_EncryptsAndDecrypts_Correctly()
        {
            // Вхідні дані для шифрування: 
            // 1 (режим Encrypt), "HELLO" (текст), "KEY" (ключ), "2" (вихід)
            string inputEncrypt =
                "1\n" +      // GetChoice()
                "HELLO\n" +  // GetInputText()
                "KEY\n" +    // GetKey()
                "2\n";       // Console.ReadLine() — вихід


            using (var input = new StringReader(inputEncrypt))
            using (var output = new StringWriter())
            {
                Console.SetIn(input);
                Console.SetOut(output);

                var view = new WigenerCipherView();
                var controller = new WigenerCipherController(view);
                controller.Run();

                string consoleOutput = output.ToString();
                Assert.Contains("Result: RIJVS", consoleOutput);
            }

            // Тепер тестуємо дешифрування
            string inputDecrypt =
                "2\n" +      // GetChoice()
                "RIJVS\n" +  // GetInputText()
                "KEY\n" +    // GetKey()
                "2\n";       // вихід

            using (var input = new StringReader(inputDecrypt))
            using (var output = new StringWriter())
            {
                Console.SetIn(input);
                Console.SetOut(output);

                var view = new WigenerCipherView();
                var controller = new WigenerCipherController(view);
                controller.Run();

                string consoleOutput = output.ToString();
                Assert.Contains("Result: HELLO", consoleOutput);
            }
        }
    }
}
