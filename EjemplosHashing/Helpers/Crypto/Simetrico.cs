using System.Security.Cryptography;
using System.Text;

namespace EjemplosHashing.Helpers.Crypto
{
    public class Simetrico
    {
        public static void PruebaSimetricoAes()
        {
            string key = "EstaEsMiClaveSecreta|YTeAguantas"; // Debe ser una clave segura
            string plainText = "Hola, este es un mensaje secreto.";

            string encryptedText = Encrypt(plainText, key);
            Console.WriteLine("Texto cifrado: " + encryptedText);

            string decryptedText = Decrypt(encryptedText, key);
            Console.WriteLine("Texto descifrado: " + decryptedText);
        }

        private static string Encrypt(string plainText, string key)
        {
            using Aes aesAlg = Aes.Create();
            var arreglo = Encoding.UTF8.GetBytes(key);
            aesAlg.Key = arreglo;
            aesAlg.GenerateIV();

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
            using StreamWriter swEncrypt = new(csEncrypt);
            swEncrypt.Write(plainText);

            byte[] iv = aesAlg.IV;
            byte[] encryptedData = msEncrypt.ToArray();
            return Convert.ToBase64String(iv.Concat(encryptedData).ToArray());
        }

        private static string Decrypt(string cipherText, string key)
        {
            using Aes aesAlg = Aes.Create();
            byte[] iv = Convert.FromBase64String(cipherText).ToArray();
            byte[] cipherBytes = Convert.FromBase64String(cipherText).Skip(16).ToArray();

            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(cipherBytes);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}