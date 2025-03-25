using System.Security.Cryptography;
using System.Data.SqlClient;


namespace S6_ASPSEC_02
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Voornaam: ");
            string firstName = Console.ReadLine();
            Console.Write("Achternaam: ");
            string lastName = Console.ReadLine();
            Console.Write("Straat: ");
            string street = Console.ReadLine();
            Console.Write("Huisnummer: ");
            string houseNumber = Console.ReadLine();
            Console.Write("Postcode: ");
            string postalCode = Console.ReadLine();
            Console.Write("Woonplaats: ");
            string city = Console.ReadLine();
            Console.Write("Creditcardnummer: ");
            string creditCardNumber = Console.ReadLine();

            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();

                byte[] encrypted = Encrypt(creditCardNumber, aes.Key, aes.IV);
                SaveToDatabase(firstName, lastName, street, houseNumber, postalCode, city, encrypted, aes.Key, aes.IV);
            }
        }

        static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Close();
                return ms.ToArray();
            }
        }

        static void SaveToDatabase(string firstName, string lastName, string street, string houseNumber, string postalCode, string city, byte[] encryptedCreditCard, byte[] key, byte[] iv)
        {
            string connectionString = "Server=localhost;Database=ASPSEC2;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Persons (FirstName, LastName, Street, HouseNumber, PostalCode, City, EncryptedCreditCard, AESKey, AESIV) VALUES (@FirstName, @LastName, @Street, @HouseNumber, @PostalCode, @City, @EncryptedCreditCard, @AESKey, @AESIV)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Street", street);
                    command.Parameters.AddWithValue("@HouseNumber", houseNumber);
                    command.Parameters.AddWithValue("@PostalCode", postalCode);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@EncryptedCreditCard", encryptedCreditCard);
                    command.Parameters.AddWithValue("@AESKey", key);
                    command.Parameters.AddWithValue("@AESIV", iv);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}