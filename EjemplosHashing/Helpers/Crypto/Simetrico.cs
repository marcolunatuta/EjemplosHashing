using System.Security.Cryptography;

namespace EjemplosHashing.Helpers.Crypto
{
    public class Simetrico
    {
        private const string Key = "VX+F4gPX45XqG0jUk0mu5MlbisjMJBIB+r6iHOYihCg=";
        private const string IV = "6+0hjk/hXRxPRAzD/SKqdw==";

        public static void PruebaSimetricoAes()
        {
            string plainText = "Hola, este es un mensaje secreto.";
            Console.WriteLine("Texto a cifrar: " + plainText);

            var encryptedText = Encrypt(plainText, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
            Console.WriteLine("Texto cifrado: " + encryptedText);
            string decryptedText = Decrypt(encryptedText, Convert.FromBase64String(Key), Convert.FromBase64String(IV));
            Console.WriteLine("Texto descifrado: " + decryptedText);
        }

        private static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            byte[] encryptedData;

            using Aes aesAlg = Aes.Create();
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new())
            {
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                encryptedData = msEncrypt.ToArray();
            }

            return Convert.ToBase64String(encryptedData);
        }

        private static string Decrypt(string cipherData, byte[] key, byte[] iv)
        {
            byte[] cipherText = Convert.FromBase64String(cipherData);

            using Aes aesAlg = Aes.Create();

            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(cipherText);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }

        private static byte[] GenerateRandomBytes(int length)
        {
            Random random = new();
            byte[] buffer = new byte[length];
            random.NextBytes(buffer);
            return buffer;
        }
    }
}