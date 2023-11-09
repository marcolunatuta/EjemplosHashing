using System.Security.Cryptography;
using System.Text;

namespace EjemplosHashing.Helpers.Crypto
{
    public class Hashing64
    {
        public static void PruebaHashing(string userInput)
        {
            string inputString = "Hola, esto es un ejemplo de hashing en C#";

            string hashedString = ComputeHash(inputString);

            Console.WriteLine("Texto original: " + inputString);
            Console.WriteLine("Texto hasheado (SHA-256): " + hashedString);

            // Puedes verificar si un hash coincide con un valor original
            if (VerifyHash(userInput, hashedString))
            {
                Console.WriteLine("El hash coincide con el texto original.");
            }
            else
            {
                Console.WriteLine("El hash no coincide con el texto original.");
            }
        }

        private static string ComputeHash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2")); // Convierte a formato hexadecimal
            }

            return builder.ToString();
        }

        private static bool VerifyHash(string input, string hashedString)
        {
            string newHash = ComputeHash(input);
            return newHash == hashedString;
        }
    }
}