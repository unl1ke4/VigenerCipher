namespace VigenereCipher
{
    public class VigenereCipherModel
    {
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static char EncryptChar(char c, char k, bool decrypt = false)
        {
            try
            {
                if (!char.IsLetter(c)) return c;

                char upperC = char.ToUpper(c);
                int cIndex = Alphabet.IndexOf(upperC);
                int kIndex = Alphabet.IndexOf(char.ToUpper(k));

                if (decrypt)
                    kIndex = Alphabet.Length - kIndex;

                int newIndex = (cIndex + kIndex) % Alphabet.Length;
                char result = Alphabet[newIndex];

                return char.IsUpper(c) ? result : char.ToLower(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in EncryptChar: {e.Message}");
                return c;
            }
        }

        public string Encrypt(string text, string key) => Process(text, key, false);
        public string Decrypt(string text, string key) => Process(text, key, true);

        private string Process(string text, string key, bool decrypt)
        {
            try
            {
                if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key))
                    return string.Empty;

                var result = new System.Text.StringBuilder();
                int keyIndex = 0;

                foreach (char c in text)
                {
                    if (char.IsLetter(c))
                    {
                        result.Append(EncryptChar(c, key[keyIndex % key.Length], decrypt));
                        keyIndex++;
                    }
                    else
                    {
                        result.Append(c);
                    }
                }

                return result.ToString();
            }
            catch (Exception e)
            {
                return $"Error while processing text: {e.Message}";
            }
        }
    }
}
