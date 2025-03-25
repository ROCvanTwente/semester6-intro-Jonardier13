using S6_ASPSEC_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{

    public class DecryptTests
    {
        [Fact]
        public void Decrypt_ShiftsCharactersCorrectly_WithPositiveKey()
        {
            // Arrange
            string input = "Mjqqt678";
            int key = 5;
            string expected = "Hello123";

            // Act
            string result = EncryptionFunctions.Decrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decrypt_ShiftsCharactersForward_WithNegativeKey()
        {
            // Arrange
            string input = "Hello123";
            int key = -5; // Moet hetzelfde doen als Encrypt met key=5
            string expected = "Mjqqt678";

            // Act
            string result = EncryptionFunctions.Decrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decrypt_DoesNotChangeSpecialCharacters()
        {
            // Arrange
            string input = "Khoor, Zruog! 456"; // "Hello, World! 123" was versleuteld met key=3
            int key = 3;
            string expected = "Hello, World! 123"; // Speciale tekens blijven gelijk

            // Act
            string result = EncryptionFunctions.Decrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decrypt_HandlesEmptyString()
        {
            // Arrange
            string input = "";
            int key = 5;
            string expected = "";

            // Act
            string result = EncryptionFunctions.Decrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

        //[Fact]
        //public void Decrypt_HandlesLargeKey()
        //{
        //    // Arrange
        //    string input = "Mjqqt678"; // Key 5 was gebruikt
        //    int key = 65; // Is gelijk aan key 5 door modulo
        //    string expected = "Hello123";

        //    // Act
        //    string result = EncryptionFunctions.Decrypt(input, key);

        //    // Assert
        //    Assert.Equal(expected, result);
    }
}


