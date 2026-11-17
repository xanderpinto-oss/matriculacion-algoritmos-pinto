// Xander Boris Pinto Montes
// Parcial 1 — Variante B. Ejecutar en .NET en configuración Release.
using System;
using System.Diagnostics;
using System.Linq;

static class CajaP1
{
    public static bool HayComprobantesRepetidosPorPares(int[] comprobantes)
    {
        for (int a = 0; a < comprobantes.Length; a++)
            for (int b = a + 1; b < comprobantes.Length; b++)
                if (comprobantes[a] == comprobantes[b]) return true;
        return false;
    }

    // Cura O(n log n): ordena una copia y no modifica el arreglo recibido.
    public static bool HayComprobantesRepetidosCurado(int[] comprobantes)
    {
        int[] copia = (int[])comprobantes.Clone();
        Array.Sort(copia);
        for (int i = 1; i < copia.Length; i++)
            if (copia[i] == copia[i - 1]) return true;
        return false;
    }

    public static int[] GenerarSinRepetidos(int n, int semilla = 77)
    {
        int[] datos = Enumerable.Range(1000, n).ToArray();
        Random azar = new Random(semilla);
        for (int i = n - 1; i > 0; i--)
        {
            int k = azar.Next(i + 1);
            (datos[i], datos[k]) = (datos[k], datos[i]);
        }
        return datos;
    }
}

class Programa
{
    static double Medir(Func<int[], bool> algoritmo, int[] datos)
    {
        var reloj = Stopwatch.StartNew();
        bool resultado = algoritmo(datos);
        reloj.Stop();
        if (resultado) throw new Exception("La lista de medición debía estar sin repetidos.");
        return reloj.Elapsed.TotalMilliseconds;
    }

    static void Main()
    {
        (int[] datos, bool esperado, string nombre)[] casos =
        {
            (new[] {1010,1003,1009,1003,1005}, true, "repetido en el medio"),
            (new[] {1010,1003,1009,1004,1005}, false, "sin repetidos"),
            (Array.Empty<int>(), false, "lista vacía"),
            (new[] {1008}, false, "un elemento"),
            (new[] {1004,1001,1002,1006,1004}, true, "repetido en extremos")
        };

        foreach (var c in casos)
        {
            bool obtenido = CajaP1.HayComprobantesRepetidosCurado(c.datos);
            Console.WriteLine($"{c.nombre}: esperado={c.esperado}, obtenido={obtenido}, {(obtenido == c.esperado ? "OK" : "ERROR")}");
            if (obtenido != c.esperado) throw new Exception("Falló un caso de prueba.");
        }

        int[] tamanos = { 8000, 16000, 32000, 64000 };
        double anteriorPares = 0, anteriorCura = 0;

        int[] calentamiento = CajaP1.GenerarSinRepetidos(2000);
        Medir(CajaP1.HayComprobantesRepetidosPorPares, calentamiento);
        Medir(CajaP1.HayComprobantesRepetidosCurado, calentamiento);

        Console.WriteLine("\nn,pares_prom_ms,pares_x_anterior,cura_prom_ms,cura_x_anterior");
        foreach (int n in tamanos)
        {
            int[] datos = CajaP1.GenerarSinRepetidos(n);
            double[] pares = new double[3], cura = new double[3];
            for (int r = 0; r < 3; r++)
            {
                pares[r] = Medir(CajaP1.HayComprobantesRepetidosPorPares, datos);
                cura[r] = Medir(CajaP1.HayComprobantesRepetidosCurado, datos);
            }
            double promPares = pares.Average(), promCura = cura.Average();
            string ratioPares = anteriorPares == 0 ? "-" : (promPares / anteriorPares).ToString("F2");
            string ratioCura = anteriorCura == 0 ? "-" : (promCura / anteriorCura).ToString("F2");
            Console.WriteLine($"{n},{promPares:F3},{ratioPares},{promCura:F3},{ratioCura}");
            anteriorPares = promPares;
            anteriorCura = promCura;
        }
    }
}
