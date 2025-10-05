using VigenereCipher;

namespace VigenereCipherTests
{
    public class VigenereCipherModelTest
    {
        private readonly VigenereCipherModel _model = new VigenereCipherModel();

        [Fact]
        public void Encrypt_ReturnsExpectedCipherText()
        {
            // Arrange
            string text = "HELLO";
            string key = "KEY";

            // Act
            string result = _model.Encrypt(text, key);

            // Assert
            Assert.Equal("RIJVS", result);
        }

        [Fact]
        public void Decrypt_ReturnsOriginalText()
        {
            string cipher = "RIJVS";
            string key = "KEY";

            string result = _model.Decrypt(cipher, key);

            Assert.Equal("HELLO", result);
        }

        [Fact]
        public void Encrypt_IgnoresNonLetters()
        {
            string text = "HI THERE!";
            string key = "KEY";

            string result = _model.Encrypt(text, key);

            Assert.Contains("!", result); // символ не зник
        }

        [Fact]
        public void Encrypt_EmptyInput_ReturnsEmpty()
        {
            string result = _model.Encrypt("", "KEY");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Decrypt_WithMixedCase_WorksCorrectly()
        {
            string text = "HelloWorld";
            string key = "key";

            string encrypted = _model.Encrypt(text, key);
            string decrypted = _model.Decrypt(encrypted, key);

            Assert.Equal(text, decrypted);
        }
    }
}
