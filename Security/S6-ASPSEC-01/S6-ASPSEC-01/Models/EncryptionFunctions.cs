using System.Text;

namespace S6_ASPSEC_01.Models
{
    public static class EncryptionFunctions
    {
        private const string Charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string Encrypt(string text, int key)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in text)
            {
                int index = Charset.IndexOf(c);
                if (index != -1)
                {
                    int newIndex = (index + key) % Charset.Length;
                    if (newIndex < 0) newIndex += Charset.Length; // Ondersteuning voor negatieve keys
                    encryptedText.Append(Charset[newIndex]);
                }
                else
                {
                    encryptedText.Append(c); // Ongewijzigd als het geen letter of cijfer is
                }
            }

            return encryptedText.ToString();
        }

        public static string Decrypt(string text, int key)
        {
             if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in text)
            {
                int index = Charset.IndexOf(c);
                if (index != -1)
                {
                    int newIndex = (index - key) % Charset.Length;
                    if (newIndex < 0) newIndex += Charset.Length; // Ondersteuning voor negatieve keys
                    decryptedText.Append(Charset[newIndex]);
                }
                else
                {
                    decryptedText.Append(c); // Ongewijzigd als het geen letter of cijfer is
                }
            }

            return decryptedText.ToString();
        }


    }
}
