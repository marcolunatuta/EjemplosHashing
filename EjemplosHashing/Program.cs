using EjemplosHashing.Helpers.Crypto;
using System.Diagnostics;

int numero = 100;
do
{
    Console.WriteLine("Selecciona el algoritmo a probar:\n" +
    "1. Simétrico.\n" +
    "2. Asimétrico.\n" +
    "3. Hashing.\n" +
    "0. Salir");

    try
    {
        numero = int.Parse(Console.ReadLine()!);
        switch (numero)
        {
            case 1:
                Console.WriteLine("Algoritmo simétrico");
                Simetrico.PruebaSimetricoAes();
                break;

            case 2:
                Console.WriteLine("Algoritmo asimétrico");
                Asimetrico.PruebaAsimetrico();
                break;

            case 3:
                Console.WriteLine("Algoritmo hashing");
                string cadena = Console.ReadLine()!;
                Hashing64.PruebaHashing(cadena);
                break;

            default:
                Console.WriteLine("Opción no encontrada");
                break;
        }
    }
    catch (Exception ex)
    {
        Debug.Write(ex.ToString());
        Console.WriteLine("Opción incorrecta");
    }
} while (numero != 0);