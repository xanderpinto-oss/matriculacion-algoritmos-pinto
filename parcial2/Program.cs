
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Parcial2.VarianteA;

// CONTRATO DEL GESTOR
public interface IGestorTurnos
{
    void Registrar(string nombre, int urgencia);
    string? AtenderSiguiente();
    bool EstaEsperando(string nombre);
    int Esperando { get; }
}

// MODELO DEL PACIENTE
public sealed record Paciente(string Nombre, int Urgencia, int Llegada);


// SOLUCION ORIGINAL: LISTA SIMPLE
public sealed class GestorIngenuo : IGestorTurnos
{
    private readonly List<Paciente> _lista = new();
    private int _llegadas;

    public void Registrar(string nombre, int urgencia)
    {
        _lista.Add(new Paciente(nombre, urgencia, _llegadas++));
    }

    public string? AtenderSiguiente()
    {
        if (_lista.Count == 0)
            return null;

        int mejor = 0;

        for (int i = 1; i < _lista.Count; i++)
        {
            var p = _lista[i];
            var m = _lista[mejor];

            if (p.Urgencia < m.Urgencia ||
                (p.Urgencia == m.Urgencia && p.Llegada < m.Llegada))
            {
                mejor = i;
            }
        }

        string nombre = _lista[mejor].Nombre;
        _lista.RemoveAt(mejor);

        return nombre;
    }

    public bool EstaEsperando(string nombre)
    {
        foreach (var p in _lista)
        {
            if (p.Nombre == nombre)
                return true;
        }

        return false;
    }

    public int Esperando => _lista.Count;
}


// SOLUCION MEJORADA
public sealed class GestorMejorado : IGestorTurnos
{
    // Cinco colas, una por cada nivel de urgencia.
    private readonly Queue<string>[] _colas =
    {
        new Queue<string>(),
        new Queue<string>(),
        new Queue<string>(),
        new Queue<string>(),
        new Queue<string>()
    };

    // Conjunto para consultar los pacientes que esperan.
    private readonly HashSet<string> _enEspera =
        new HashSet<string>(StringComparer.Ordinal);

    public void Registrar(string nombre, int urgencia)
    {
        if (urgencia < 1 || urgencia > 5)
            throw new ArgumentOutOfRangeException(nameof(urgencia));

        if (!_enEspera.Add(nombre))
            throw new ArgumentException("El paciente ya está esperando.");

        _colas[urgencia - 1].Enqueue(nombre);
    }

    public string? AtenderSiguiente()
    {
        for (int i = 0; i < 5; i++)
        {
            if (_colas[i].Count > 0)
            {
                string nombre = _colas[i].Dequeue();
                _enEspera.Remove(nombre);
                return nombre;
            }
        }

        return null;
    }

    public bool EstaEsperando(string nombre)
    {
        return _enEspera.Contains(nombre);
    }

    public int Esperando => _enEspera.Count;
}


// PROGRAMA PRINCIPAL Y PRUEBAS
public static class Programa
{
    private const int CONSULTAS_ESPERA = 30000;

    private static readonly
        (string op, string arg, int urg, string esperado)[] Guion =
    {
        ("registrar", "Ana", 3, ""),
        ("registrar", "Beto", 1, ""),
        ("registrar", "Carla", 3, ""),
        ("registrar", "Diego", 2, ""),
        ("esperando", "", 0, "4"),
        ("esta", "Beto", 0, "True"),
        ("esta", "Zoe", 0, "False"),
        ("atender", "", 0, "Beto"),
        ("esta", "Beto", 0, "False"),
        ("atender", "", 0, "Diego"),
        ("registrar", "Elena", 1, ""),
        ("atender", "", 0, "Elena"),
        ("atender", "", 0, "Ana"),
        ("atender", "", 0, "Carla"),
        ("atender", "", 0, "(nadie)"),
        ("esperando", "", 0, "0"),
    };

    // EJECUTAR UNA OPERACION DEL GUION
    private static string Ejecutar(
        IGestorTurnos g, string op, string arg, int urg)
    {
        switch (op)
        {
            case "registrar":
                g.Registrar(arg, urg);
                return "";

            case "atender":
                return g.AtenderSiguiente() ?? "(nadie)";

            case "esta":
                return g.EstaEsperando(arg).ToString();

            default:
                return g.Esperando.ToString();
        }
    }

    // PROBAR EL FUNCIONAMIENTO DEL GESTOR
    private static void Probar(string titulo, IGestorTurnos g)
    {
        int bien = 0;
        int total = 0;

        try
        {
            foreach (var (op, arg, urg, esperado) in Guion)
            {
                string obtenido = Ejecutar(g, op, arg, urg);

                if (op == "registrar")
                    continue;

                total++;

                if (obtenido == esperado)
                {
                    bien++;
                }
                else
                {
                    Console.WriteLine(
                        $"MAL en {op} {arg}: esperado {esperado}, obtuvo {obtenido}");
                }
            }

            Console.WriteLine(
                $"{titulo}: {bien} de {total} pasos correctos");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{titulo}: ERROR - {ex.Message}");
        }
    }

    // CARGA COMPLETA: REGISTRAR, CONSULTAR Y ATENDER
    public static long EjecutarCarga(IGestorTurnos g, int n)
    {
        if (n <= 0)
            throw new ArgumentOutOfRangeException(nameof(n));

        // 1. Registrar todos los pacientes.
        for (int i = 0; i < n; i++)
        {
            int urgencia = (int)((i * 7919L) % 5) + 1;
            g.Registrar("P" + i, urgencia);
        }

        long control = 0;

        // 2. Consultar 30.000 veces si un paciente sigue esperando.
        // Todas las consultas corresponden a pacientes registrados.
        for (int i = 0; i < CONSULTAS_ESPERA; i++)
        {
            string nombre = "P" + (i % n);

            if (!g.EstaEsperando(nombre))
            {
                throw new InvalidOperationException(
                    "No se encontró un paciente que debería estar esperando.");
            }

            control++;
        }

        // 3. Atender a todos los pacientes.
        for (int i = 0; i < n; i++)
        {
            string? atendido = g.AtenderSiguiente();

            if (atendido == null)
            {
                throw new InvalidOperationException(
                    "No se pudo atender al paciente esperado.");
            }

            control += atendido.Length;
        }

        return control;
    }

    // MEDICION DEL RENDIMIENTO
    private static void Medir(
        string nombre,
        Func<IGestorTurnos> crearGestor,
        int n)
    {
        double suma = 0;

        Console.WriteLine($"\n{nombre} - {n:N0} pacientes");
        Console.WriteLine(
            $"Consultas de EstaEsperando: {CONSULTAS_ESPERA:N0}");

        // Calentamiento: no cuenta como medicion.
        EjecutarCarga(crearGestor(), 1000);

        // Tres mediciones reales.
        for (int i = 1; i <= 3; i++)
        {
            IGestorTurnos gestor = crearGestor();

            Stopwatch reloj = Stopwatch.StartNew();

            long resultado = EjecutarCarga(gestor, n);

            reloj.Stop();

            double milisegundos = reloj.Elapsed.TotalMilliseconds;
            suma += milisegundos;

            Console.WriteLine(
                $"  Ejecucion {i}: {milisegundos:F3} ms");

            Console.WriteLine(
                $"  Control: {resultado}");
        }

        double promedio = suma / 3;

        Console.WriteLine($"  Promedio: {promedio:F3} ms");
    }

    // METODO PRINCIPAL
    public static void Main()
    {
        Console.WriteLine("===== PRUEBAS DEL PARCIAL 2 =====");

        Probar("GestorIngenuo ", new GestorIngenuo());
        Probar("GestorMejorado", new GestorMejorado());

        Console.WriteLine("===============================");

        Console.WriteLine("\n===== MEDICION DE RENDIMIENTO =====");
        Console.WriteLine(
            "Cada carga incluye registros, consultas y atenciones.");

        int[] cantidades = { 5000, 10000, 20000 };

        foreach (int n in cantidades)
        {
            Medir(
                "GestorIngenuo",
                () => new GestorIngenuo(),
                n);

            Medir(
                "GestorMejorado",
                () => new GestorMejorado(),
                n);
        }

        Console.WriteLine("\n===== FIN DE LAS MEDICIONES =====");
    }
}
