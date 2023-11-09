using System.Security.Cryptography;

namespace EjemplosHashing.Helpers.Crypto
{
    public class Asimetrico
    {
        public static void PruebaAsimetrico()
        {
            // Generar un par de claves RSA
            using RSACryptoServiceProvider rsa = new();
            try
            {
                // Obtener la clave pública y privada
                string publicKey = rsa.ToXmlString(false); // Clave pública
                string privateKey = rsa.ToXmlString(true);  // Clave privada

                // Mensaje a cifrar
                string mensajeOriginal = "Este es un mensaje secreto.";

                // Cifrar el mensaje con la clave pública
                byte[] mensajeCifrado = CifrarConClavePublica(publicKey, mensajeOriginal);

                // Descifrar el mensaje con la clave privada
                string mensajeDescifrado = DescifrarConClavePrivada(privateKey, mensajeCifrado);

                // Imprimir los resultados
                Console.WriteLine("Mensaje Original: " + mensajeOriginal);
                Console.WriteLine("Mensaje Cifrado: " + Convert.ToBase64String(mensajeCifrado));
                Console.WriteLine("Mensaje Descifrado: " + mensajeDescifrado);
            }
            finally
            {
                rsa.PersistKeyInCsp = false; // Limpiar las claves después de su uso
                rsa.Clear();
            }
        }

        // Función para cifrar datos con una clave pública
        private static byte[] CifrarConClavePublica(string publicKey, string mensaje)
        {
            using RSACryptoServiceProvider rsa = new();
            rsa.FromXmlString(publicKey);
            byte[] datos = System.Text.Encoding.UTF8.GetBytes(mensaje);
            return rsa.Encrypt(datos, false);
        }

        // Función para descifrar datos con una clave privada
        private static string DescifrarConClavePrivada(string privateKey, byte[] datosCifrados)
        {
            using RSACryptoServiceProvider rsa = new();
            rsa.FromXmlString(privateKey);
            byte[] datosDescifrados = rsa.Decrypt(datosCifrados, false);
            return System.Text.Encoding.UTF8.GetString(datosDescifrados);
        }
    }
}