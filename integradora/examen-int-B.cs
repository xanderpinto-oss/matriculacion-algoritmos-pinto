// Xander Boris Pinto Montes
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Integradora.VarianteB;

public sealed record Util(string Codigo, int CostoBs, int Utilidad);

public interface IComprador
{
    (int utilidad, List<string> codigos) Elegir(Util[] utiles, int presupuestoBs);
    bool EstaEnLaCompra(string codigo);
}

// SOLUCION INGENUA: revisa los 2^n subconjuntos y calcula cada uno.
public sealed class CompradorIngenuo : IComprador
{
    private List<string> _elegidos = new();

    public (int utilidad, List<string> codigos) Elegir(Util[] utiles, int presupuestoBs)
    {
        int n = utiles.Length, mejor = 0;
        long mejorMascara = 0;

        for (long mascara = 0; mascara < (1L << n); mascara++)
        {
            int costo = 0, utilidad = 0;
            for (int i = 0; i < n; i++)
            {
                if ((mascara & (1L << i)) != 0)
                {
                    costo += utiles[i].CostoBs;
                    utilidad += utiles[i].Utilidad;
                }
            }

            if (costo <= presupuestoBs && utilidad > mejor)
            {
                mejor = utilidad;
                mejorMascara = mascara;
            }
        }

        _elegidos = new List<string>();
        for (int i = 0; i < n; i++)
        {
            if ((mejorMascara & (1L << i)) != 0)
                _elegidos.Add(utiles[i].Codigo);
        }

        return (mejor, new List<string>(_elegidos));
    }

    public bool EstaEnLaCompra(string codigo) => _elegidos.Contains(codigo);
}

// SOLUCION MEJORADA: programación dinámica 0/1 + HashSet para consulta O(1) promedio.
public sealed class CompradorMejorado : IComprador
{
    private HashSet<string> _enCompra = new(StringComparer.Ordinal);

    public (int utilidad, List<string> codigos) Elegir(Util[] utiles, int presupuestoBs)
    {
        ArgumentNullException.ThrowIfNull(utiles);
        if (presupuestoBs < 0)
            throw new ArgumentOutOfRangeException(nameof(presupuestoBs), "El presupuesto no puede ser negativo.");

        _enCompra = new HashSet<string>(StringComparer.Ordinal);
        var elegidos = new List<string>();
        if (utiles.Length == 0)
            return (0, elegidos);

        // No conviene comprar artículos con utilidad cero o negativa.
        long costoTotalUtilesPositivos = 0;
        foreach (var item in utiles)
        {
            if (item.CostoBs < 0)
                throw new ArgumentException("El costo de un útil no puede ser negativo.", nameof(utiles));
            if (item.Utilidad > 0)
                costoTotalUtilesPositivos += item.CostoBs;
        }

        // Si todo lo útil cabe, no hace falta construir la tabla de programación dinámica.
        if (presupuestoBs >= costoTotalUtilesPositivos)
        {
            int utilidadTotal = 0;
            foreach (var item in utiles)
            {
                if (item.Utilidad > 0)
                {
                    elegidos.Add(item.Codigo);
                    _enCompra.Add(item.Codigo);
                    utilidadTotal += item.Utilidad;
                }
            }
            return (utilidadTotal, elegidos);
        }

        // La capacidad efectiva nunca necesita superar la suma de costos disponibles.
        int capacidad = (int)Math.Min((long)presupuestoBs, costoTotalUtilesPositivos);
        int[] mejor = new int[capacidad + 1];

        // tomar[i, c] indica si el artículo i mejoró la solución para capacidad c.
        // Se usa para reconstruir los artículos sin guardar una tabla int de n x capacidad.
        bool[,] tomar = new bool[utiles.Length, capacidad + 1];

        for (int i = 0; i < utiles.Length; i++)
        {
            Util item = utiles[i];
            if (item.Utilidad <= 0 || item.CostoBs > capacidad)
                continue;

            // Recorrer hacia abajo evita reutilizar el mismo artículo más de una vez.
            for (int c = capacidad; c >= item.CostoBs; c--)
            {
                int candidato = mejor[c - item.CostoBs] + item.Utilidad;
                if (candidato > mejor[c])
                {
                    mejor[c] = candidato;
                    tomar[i, c] = true;
                }
            }
        }

        int restante = capacidad;
        for (int i = utiles.Length - 1; i >= 0; i--)
        {
            if (tomar[i, restante])
            {
                elegidos.Add(utiles[i].Codigo);
                _enCompra.Add(utiles[i].Codigo);
                restante -= utiles[i].CostoBs;
            }
        }

        return (mejor[capacidad], elegidos);
    }

    // HashSet: O(1) promedio, sin recorrer todos los códigos elegidos.
    public bool EstaEnLaCompra(string codigo) => _enCompra.Contains(codigo);
}

public static class Programa
{
    private static Util[] Lote(params (string c, int costo, int u)[] datos) =>
        datos.Select(d => new Util(d.c, d.costo, d.u)).ToArray();

    private static readonly (string que, Util[] utiles, int presupuesto, int esperada, string[] codigos)[] Casos =
    {
        ("el voraz se equivoca", Lote(("U1", 20, 120), ("U2", 40, 200), ("U3", 60, 240)), 100, 440, new[] { "U2", "U3" }),
        ("cuatro útiles", Lote(("U1", 4, 6), ("U2", 6, 8), ("U3", 8, 10), ("U4", 10, 12)), 16, 20, new[] { "U2", "U4" }),
        ("presupuesto cero", Lote(("U1", 5, 10), ("U2", 7, 12)), 0, 0, Array.Empty<string>()),
        ("sin útiles", Lote(), 30, 0, Array.Empty<string>()),
        ("nada cabe", Lote(("U1", 80, 90), ("U2", 100, 100)), 60, 0, Array.Empty<string>()),
        ("entran todos", Lote(("U1", 10, 20), ("U2", 14, 24), ("U3", 6, 8)), 200, 52, new[] { "U1", "U2", "U3" })
    };

    private sealed record ResultadoMedicion(double PromedioMs, int Utilidad);

    private static void Probar(string titulo, Func<IComprador> fabrica)
    {
        int bien = 0, total = 0;
        foreach (var (que, utiles, presupuesto, esperada, codigos) in Casos)
        {
            var comprador = fabrica();
            var (utilidad, elegidos) = comprador.Elegir(utiles, presupuesto);
            total++;
            bool ok = utilidad == esperada && elegidos.OrderBy(x => x).SequenceEqual(codigos.OrderBy(x => x));
            if (ok) bien++;
            else Console.WriteLine($"   MAL en «{que}»: esperado {esperada} con [{string.Join(",", codigos)}], obtuvo {utilidad} con [{string.Join(",", elegidos)}]");
        }

        var c = fabrica();
        c.Elegir(Casos[0].utiles, Casos[0].presupuesto);
        total += 2;
        if (c.EstaEnLaCompra("U2")) bien++; else Console.WriteLine("   MAL en EstaEnLaCompra(U2): esperado True");
        if (!c.EstaEnLaCompra("U1")) bien++; else Console.WriteLine("   MAL en EstaEnLaCompra(U1): esperado False");
        Console.WriteLine($"{titulo}: {bien} de {total} pruebas correctas");
    }

    private static Util[] CrearLote(int n)
    {
        var azar = new Random(n + 1000);
        return Enumerable.Range(0, n)
            .Select(i => new Util("K" + i, azar.Next(4, 50), azar.Next(10, 100)))
            .ToArray();
    }

    private static int PresupuestoDePrueba(Util[] utiles) => utiles.Sum(u => u.CostoBs) / 3;

    private static ResultadoMedicion Medir(Func<IComprador> fabrica, Util[] utiles, int presupuesto)
    {
        // Una corrida de calentamiento que no entra en el promedio.
        fabrica().Elegir(utiles, presupuesto);

        double sumaMs = 0;
        int utilidadReferencia = -1;
        for (int corrida = 1; corrida <= 3; corrida++)
        {
            IComprador comprador = fabrica();
            var reloj = Stopwatch.StartNew();
            var (utilidad, codigos) = comprador.Elegir(utiles, presupuesto);
            reloj.Stop();

            if (utilidadReferencia == -1) utilidadReferencia = utilidad;
            if (utilidad != utilidadReferencia)
                throw new InvalidOperationException("La utilidad cambió entre corridas.");

            sumaMs += reloj.Elapsed.TotalMilliseconds;
            Console.WriteLine($"      Corrida {corrida}: {reloj.Elapsed.TotalMilliseconds:F3} ms | utilidad: {utilidad} | artículos: {codigos.Count}");
        }

        return new ResultadoMedicion(sumaMs / 3.0, utilidadReferencia);
    }

    private static double CalibrarIngenuo(int n)
    {
        Util[] utiles = CrearLote(n);
        int presupuesto = PresupuestoDePrueba(utiles);
        var reloj = Stopwatch.StartNew();
        new CompradorIngenuo().Elegir(utiles, presupuesto);
        reloj.Stop();
        return reloj.Elapsed.TotalSeconds;
    }

    private static int[] ElegirTamanos()
    {
        // La calibración solo escoge un máximo cuyo ingenuo quede entre 0,5 y 20 s.
        // No se usa como dato de la tabla; cada fila tendrá su calentamiento y 3 mediciones.
        Util[] loteCalentamiento = CrearLote(10);
        new CompradorIngenuo().Elegir(loteCalentamiento, PresupuestoDePrueba(loteCalentamiento));

        int maximo = 24;
        double segundos = CalibrarIngenuo(maximo);

        while (segundos < 0.5 && maximo < 27)
        {
            maximo++;
            segundos = CalibrarIngenuo(maximo);
        }
        while (segundos > 20.0 && maximo > 16)
        {
            maximo--;
            segundos = CalibrarIngenuo(maximo);
        }

        Console.WriteLine($"Calibración (no incluida en tabla): n={maximo}, {segundos:F3} s");
        if (segundos < 0.5 || segundos > 20.0)
            Console.WriteLine("AVISO: en este equipo la calibración no quedó dentro del intervalo solicitado.");

        return Enumerable.Range(maximo - 3, 4).ToArray();
    }

    private static void MedirComparacion()
    {
        Console.WriteLine();
        Console.WriteLine("COMPARACIÓN: calentamiento + 3 corridas medidas por estructura");
        Console.WriteLine("Los tamaños se ajustan con una calibración para que el mayor ingenuo esté entre 0,5 y 20 s.");

        double anteriorIngenuo = 0, anteriorMejorado = 0;
        foreach (int n in ElegirTamanos())
        {
            Util[] utiles = CrearLote(n);
            int presupuesto = PresupuestoDePrueba(utiles);
            Console.WriteLine($"\nTamaño n={n}, presupuesto={presupuesto}");

            Console.WriteLine("  CompradorIngenuo:");
            ResultadoMedicion ingenuo = Medir(() => new CompradorIngenuo(), utiles, presupuesto);
            Console.WriteLine($"      Promedio ingenuo: {ingenuo.PromedioMs:F3} ms");
            Console.WriteLine("  CompradorMejorado:");
            ResultadoMedicion mejorado = Medir(() => new CompradorMejorado(), utiles, presupuesto);
            Console.WriteLine($"      Promedio mejorado: {mejorado.PromedioMs:F3} ms");

            if (ingenuo.Utilidad != mejorado.Utilidad)
                throw new InvalidOperationException($"Las estructuras no coinciden para n={n}: {ingenuo.Utilidad} != {mejorado.Utilidad}.");
            Console.WriteLine($"  VERIFICACIÓN: misma utilidad = {ingenuo.Utilidad}");
            Console.WriteLine(anteriorIngenuo == 0
                ? "  Cociente vs fila anterior: primera fila"
                : $"  Crecimiento vs fila anterior: ingenuo {ingenuo.PromedioMs / anteriorIngenuo:F2}x | mejorado {mejorado.PromedioMs / anteriorMejorado:F2}x");

            anteriorIngenuo = ingenuo.PromedioMs;
            anteriorMejorado = mejorado.PromedioMs;
        }
    }

    private static void MedirMilUtiles()
    {
        const int n = 1000;
        const int presupuesto = 10000;
        Util[] utiles = CrearLote(n);
        Console.WriteLine();
        Console.WriteLine($"SOLUCIÓN MEJORADA: {n} útiles y presupuesto de {presupuesto} Bs");
        ResultadoMedicion resultado = Medir(() => new CompradorMejorado(), utiles, presupuesto);
        Console.WriteLine($"Promedio con 1.000 útiles y 10.000 Bs: {resultado.PromedioMs:F3} ms | utilidad: {resultado.Utilidad}");
    }

    public static void Main()
    {
        Console.WriteLine("EVALUACIÓN INTEGRADORA - VARIANTE B: COMPRAS DE UNA ESCUELA");
        Probar("CompradorIngenuo ", () => new CompradorIngenuo());
        Probar("CompradorMejorado", () => new CompradorMejorado());

        MedirComparacion();
        MedirMilUtiles();
      
    }
}
