using S6_ASPSEC_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class EncryptTest
    {
        [Fact]
        public void Encrypt_ShiftsCharactersCorrectly_WithPositiveKey()
        {
            // Arrange
            string input = "Hello123";
            int key = 5;
            string expected = "Mjqqt678";

            // Act
            string result =  EncryptionFunctions.Encrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encrypt_ShiftsCharactersBack_WithNegativeKey()
        {
            // Arrange
            string input = "Mjqqt678";
            int key = -5;
            string expected = "Hello123";

            // Act
            string result = EncryptionFunctions.Encrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Encrypt_DoesNotChangeSpecialCharacters()
        {
            // Arrange
            string input = "Hello, World! 123";
            int key = 3;
            string expected = "Khoor, Zruog! 456"; // Alleen letters en cijfers veranderen

            // Act
            string result = EncryptionFunctions.Encrypt(input, key);

            // Assert
            Assert.Equal(expected, result);
        }

    }
}
