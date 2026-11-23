// Xander Boris Pinto Montes
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hito2Matriculacion;

// Los campos corresponden a la muestra de matrículas del H1.
public sealed record Matricula(
    string IdMatricula,
    string CodigoEstudiante,
    string Carrera,
    string Gestion,
    int Semestre,
    string Turno,
    string FechaSolicitud,
    bool DocumentacionCompleta,
    bool PagoConfirmado,
    string EstadoMatricula,
    string TiempoAtencionMin);

public interface IRegistroMatriculas
{
    Matricula? Buscar(string clave);
    bool Insertar(Matricula matricula);
    bool Borrar(string clave);
    Matricula? Menor();
    Matricula? Siguiente(string clave);
    List<Matricula> Rango(string desde, string hasta);
    List<Matricula> EnOrden();
    int Cantidad { get; }
}

// Solución ingenua: una lista sin orden por clave.
public sealed class RegistroLista : IRegistroMatriculas
{
    private readonly List<Matricula> datos = new();
    private static readonly StringComparer Comparador = StringComparer.Ordinal;

    public int Cantidad => datos.Count;

    public Matricula? Buscar(string clave)
    {
        foreach (Matricula m in datos)
            if (Comparador.Equals(m.IdMatricula, clave)) return m;
        return null;
    }

    public bool Insertar(Matricula matricula)
    {
        if (Buscar(matricula.IdMatricula) != null) return false;
        datos.Add(matricula);
        return true;
    }

    public bool Borrar(string clave)
    {
        for (int i = 0; i < datos.Count; i++)
        {
            if (Comparador.Equals(datos[i].IdMatricula, clave))
            {
                datos.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public Matricula? Menor()
    {
        if (datos.Count == 0) return null;
        Matricula menor = datos[0];
        for (int i = 1; i < datos.Count; i++)
            if (Comparador.Compare(datos[i].IdMatricula, menor.IdMatricula) < 0)
                menor = datos[i];
        return menor;
    }

    public Matricula? Siguiente(string clave)
    {
        Matricula? candidato = null;
        foreach (Matricula m in datos)
        {
            if (Comparador.Compare(m.IdMatricula, clave) > 0 &&
                (candidato == null || Comparador.Compare(m.IdMatricula, candidato.IdMatricula) < 0))
                candidato = m;
        }
        return candidato;
    }

    public List<Matricula> Rango(string desde, string hasta)
    {
        var resultado = new List<Matricula>();
        foreach (Matricula m in datos)
            if (Comparador.Compare(m.IdMatricula, desde) >= 0 &&
                Comparador.Compare(m.IdMatricula, hasta) <= 0)
                resultado.Add(m);

        resultado.Sort((a, b) => Comparador.Compare(a.IdMatricula, b.IdMatricula));
        return resultado;
    }

    public List<Matricula> EnOrden()
    {
        var resultado = new List<Matricula>(datos);
        resultado.Sort((a, b) => Comparador.Compare(a.IdMatricula, b.IdMatricula));
        return resultado;
    }
}

// Solución adecuada: árbol AVL ordenado por IdMatricula.
public sealed class RegistroAVL : IRegistroMatriculas
{
    private sealed class Nodo
    {
        public Matricula Valor;
        public Nodo? Izquierdo;
        public Nodo? Derecho;
        public int Altura = 1;
        public Nodo(Matricula valor) => Valor = valor;
    }

    private Nodo? raiz;
    private int cantidad;
    private static readonly StringComparer Comparador = StringComparer.Ordinal;

    public int Cantidad => cantidad;

    private static int Altura(Nodo? n) => n?.Altura ?? 0;
    private static int Balance(Nodo n) => Altura(n.Izquierdo) - Altura(n.Derecho);

    private static void ActualizarAltura(Nodo n) =>
        n.Altura = 1 + Math.Max(Altura(n.Izquierdo), Altura(n.Derecho));

    private static Nodo RotarDerecha(Nodo y)
    {
        Nodo x = y.Izquierdo!;
        Nodo? medio = x.Derecho;
        x.Derecho = y;
        y.Izquierdo = medio;
        ActualizarAltura(y);
        ActualizarAltura(x);
        return x;
    }

    private static Nodo RotarIzquierda(Nodo x)
    {
        Nodo y = x.Derecho!;
        Nodo? medio = y.Izquierdo;
        y.Izquierdo = x;
        x.Derecho = medio;
        ActualizarAltura(x);
        ActualizarAltura(y);
        return y;
    }

    private static Nodo Equilibrar(Nodo n)
    {
        ActualizarAltura(n);
        int balance = Balance(n);

        if (balance > 1)
        {
            if (Balance(n.Izquierdo!) < 0)
                n.Izquierdo = RotarIzquierda(n.Izquierdo!);
            return RotarDerecha(n);
        }

        if (balance < -1)
        {
            if (Balance(n.Derecho!) > 0)
                n.Derecho = RotarDerecha(n.Derecho!);
            return RotarIzquierda(n);
        }

        return n;
    }

    public Matricula? Buscar(string clave)
    {
        Nodo? actual = raiz;
        while (actual != null)
        {
            int cmp = Comparador.Compare(clave, actual.Valor.IdMatricula);
            if (cmp == 0) return actual.Valor;
            actual = cmp < 0 ? actual.Izquierdo : actual.Derecho;
        }
        return null;
    }

    public bool Insertar(Matricula matricula)
    {
        bool insertado = false;
        raiz = Insertar(raiz, matricula, ref insertado);
        if (insertado) cantidad++;
        return insertado;
    }

    private static Nodo Insertar(Nodo? nodo, Matricula valor, ref bool insertado)
    {
        if (nodo == null)
        {
            insertado = true;
            return new Nodo(valor);
        }

        int cmp = Comparador.Compare(valor.IdMatricula, nodo.Valor.IdMatricula);
        if (cmp == 0) return nodo;
        if (cmp < 0) nodo.Izquierdo = Insertar(nodo.Izquierdo, valor, ref insertado);
        else nodo.Derecho = Insertar(nodo.Derecho, valor, ref insertado);

        return insertado ? Equilibrar(nodo) : nodo;
    }

    public bool Borrar(string clave)
    {
        bool borrado = false;
        raiz = Borrar(raiz, clave, ref borrado);
        if (borrado) cantidad--;
        return borrado;
    }

    private static Nodo? Borrar(Nodo? nodo, string clave, ref bool borrado)
    {
        if (nodo == null) return null;

        int cmp = Comparador.Compare(clave, nodo.Valor.IdMatricula);
        if (cmp < 0)
            nodo.Izquierdo = Borrar(nodo.Izquierdo, clave, ref borrado);
        else if (cmp > 0)
            nodo.Derecho = Borrar(nodo.Derecho, clave, ref borrado);
        else
        {
            borrado = true;
            if (nodo.Izquierdo == null) return nodo.Derecho;
            if (nodo.Derecho == null) return nodo.Izquierdo;

            Nodo sucesor = MinimoNodo(nodo.Derecho);
            nodo.Valor = sucesor.Valor;
            bool eliminadoSucesor = false;
            nodo.Derecho = Borrar(nodo.Derecho, sucesor.Valor.IdMatricula, ref eliminadoSucesor);
        }

        return Equilibrar(nodo);
    }

    private static Nodo MinimoNodo(Nodo nodo)
    {
        Nodo actual = nodo;
        while (actual.Izquierdo != null) actual = actual.Izquierdo;
        return actual;
    }

    public Matricula? Menor() => raiz == null ? null : MinimoNodo(raiz).Valor;

    public Matricula? Siguiente(string clave)
    {
        Nodo? actual = raiz;
        Nodo? candidato = null;
        while (actual != null)
        {
            if (Comparador.Compare(actual.Valor.IdMatricula, clave) > 0)
            {
                candidato = actual;
                actual = actual.Izquierdo;
            }
            else actual = actual.Derecho;
        }
        return candidato?.Valor;
    }

    public List<Matricula> Rango(string desde, string hasta)
    {
        var resultado = new List<Matricula>();
        Rango(raiz, desde, hasta, resultado);
        return resultado;
    }

    private static void Rango(Nodo? nodo, string desde, string hasta, List<Matricula> salida)
    {
        if (nodo == null) return;
        int contraDesde = Comparador.Compare(nodo.Valor.IdMatricula, desde);
        int contraHasta = Comparador.Compare(nodo.Valor.IdMatricula, hasta);
        if (contraDesde > 0) Rango(nodo.Izquierdo, desde, hasta, salida);
        if (contraDesde >= 0 && contraHasta <= 0) salida.Add(nodo.Valor);
        if (contraHasta < 0) Rango(nodo.Derecho, desde, hasta, salida);
    }

    public List<Matricula> EnOrden()
    {
        var resultado = new List<Matricula>(cantidad);
        EnOrden(raiz, resultado);
        return resultado;
    }

    private static void EnOrden(Nodo? nodo, List<Matricula> salida)
    {
        if (nodo == null) return;
        EnOrden(nodo.Izquierdo, salida);
        salida.Add(nodo.Valor);
        EnOrden(nodo.Derecho, salida);
    }
}

public static class Programa
{
    private static readonly string[] Carreras =
    {
        "Mecánica Automotriz", "Sistemas Informáticos", "Electromecánica",
        "Electrónica", "Mecánica Industrial"
    };

    private static readonly string[] Turnos = { "Mañana", "Tarde", "Noche" };

    // Registros sintéticos basados en los campos y categorías de la muestra H1.
    private static Matricula CrearMatricula(int i)
    {
        bool documentos = i % 4 != 2;
        bool pago = i % 3 != 1;
        string estado = !documentos ? "OBSERVADA" :
            !pago ? "PENDIENTE_PAGO" : i % 5 == 0 ? "EN_REVISION" : "COMPLETA";

        return new Matricula(
            $"MAT-DEMO-{i + 1:D6}",
            $"EST-DEMO-{i + 1:D6}",
            Carreras[i % Carreras.Length],
            "POR_CONFIRMAR",
            (i % 4) + 1,
            Turnos[i % Turnos.Length],
            "FECHA_DE_EJEMPLO",
            documentos,
            pago,
            estado,
            "POR_MEDIR");
    }

    private static string Clave(int i) => $"MAT-DEMO-{i + 1:D6}";

    // Misma carga para ambas estructuras: inserciones, búsquedas, sucesor,
    // mínimo, rango, recorrido ordenado y bajas.
    public static long EjecutarCarga(IRegistroMatriculas registro, int n)
    {
        long control = 0;

        for (int i = 0; i < n; i++)
            if (!registro.Insertar(CrearMatricula(i)))
                throw new InvalidOperationException("No se pudo insertar una matrícula nueva.");

        for (int i = 0; i < n; i++)
            if (registro.Buscar(Clave(i)) != null) control++;
            else throw new InvalidOperationException("Falló una búsqueda por clave.");

        Matricula? menor = registro.Menor();
        if (menor == null || menor.IdMatricula != Clave(0))
            throw new InvalidOperationException("El menor registro no coincide.");
        control++;

        int paso = Math.Max(1, n / 20);
        for (int i = 0; i < n - 1; i += paso)
        {
            Matricula? siguiente = registro.Siguiente(Clave(i));
            if (siguiente == null || string.CompareOrdinal(siguiente.IdMatricula, Clave(i)) <= 0)
                throw new InvalidOperationException("Falló la consulta del siguiente registro.");
            control++;
        }

        var rango = registro.Rango(Clave(n / 4), Clave((3 * n) / 4));
        control += rango.Count;
        for (int i = 1; i < rango.Count; i++)
            if (string.CompareOrdinal(rango[i - 1].IdMatricula, rango[i].IdMatricula) > 0)
                throw new InvalidOperationException("El rango no está ordenado.");

        var ordenados = registro.EnOrden();
        if (ordenados.Count != n) throw new InvalidOperationException("El recorrido perdió registros.");
        for (int i = 1; i < ordenados.Count; i++)
            if (string.CompareOrdinal(ordenados[i - 1].IdMatricula, ordenados[i].IdMatricula) >= 0)
                throw new InvalidOperationException("El recorrido en orden no está ordenado o tiene duplicados.");
        control += ordenados.Count;

        int eliminados = 0;
        for (int i = 0; i < n; i += 10)
        {
            if (!registro.Borrar(Clave(i)))
                throw new InvalidOperationException("Falló la eliminación de una matrícula.");
            eliminados++;
        }
        control += eliminados;

        if (registro.Cantidad != n - eliminados)
            throw new InvalidOperationException("La cantidad final no coincide.");

        return control;
    }

    private static double Medir(string nombre, Func<IRegistroMatriculas> crear, int n)
    {
        // Una corrida de calentamiento no incluida en los tiempos.
        EjecutarCarga(crear(), n);

        double suma = 0;
        long controlEsperado = -1;
        Console.WriteLine($"\n{nombre} | n = {n:N0}");

        for (int i = 1; i <= 3; i++)
        {
            var registro = crear();
            var reloj = Stopwatch.StartNew();
            long control = EjecutarCarga(registro, n);
            reloj.Stop();

            if (controlEsperado == -1) controlEsperado = control;
            if (control != controlEsperado)
                throw new InvalidOperationException("Las repeticiones no produjeron el mismo control.");

            double ms = reloj.Elapsed.TotalMilliseconds;
            suma += ms;
            Console.WriteLine($"  Corrida {i}: {ms:F3} ms | control: {control}");
        }

        double promedio = suma / 3.0;
        Console.WriteLine($"  Promedio: {promedio:F3} ms");
        return promedio;
    }

    private static void ProbarOperaciones(string nombre, Func<IRegistroMatriculas> crear)
    {
        var r = crear();
        r.Insertar(CrearMatricula(2));
        r.Insertar(CrearMatricula(0));
        r.Insertar(CrearMatricula(1));
        bool ok = r.Buscar(Clave(1)) != null &&
                  r.Menor()?.IdMatricula == Clave(0) &&
                  r.Siguiente(Clave(0))?.IdMatricula == Clave(1) &&
                  r.Rango(Clave(0), Clave(1)).Count == 2 &&
                  r.EnOrden().Count == 3 &&
                  r.Borrar(Clave(1)) &&
                  r.Buscar(Clave(1)) == null &&
                  r.Cantidad == 2;
        Console.WriteLine($"{nombre}: {(ok ? "operaciones correctas" : "ERROR en pruebas")}");
        if (!ok) throw new InvalidOperationException("La prueba funcional falló en " + nombre);
    }

    public static void Main()
    {
        Console.WriteLine("HITO 2 - MATRICULACION DE ESTUDIANTES");
        ProbarOperaciones("Lista ingenua", () => new RegistroLista());
        ProbarOperaciones("Arbol AVL", () => new RegistroAVL());

        int[] tamanos = { 559, 1118, 2236, 4472 };
        double? anteriorLista = null;
        double? anteriorAvl = null;

        Console.WriteLine("\nTiempos en milisegundos. Cada tamaño tiene 1 calentamiento y 3 corridas medidas.");
        foreach (int n in tamanos)
        {
            double lista = Medir("Lista ingenua", () => new RegistroLista(), n);
            Console.WriteLine(anteriorLista.HasValue
                ? $"  Crecimiento lista vs tamaño anterior: {lista / anteriorLista.Value:F2}x"
                : "  Crecimiento lista: primera fila");
            anteriorLista = lista;

            double avl = Medir("Arbol AVL", () => new RegistroAVL(), n);
            Console.WriteLine(anteriorAvl.HasValue
                ? $"  Crecimiento AVL vs tamaño anterior: {avl / anteriorAvl.Value:F2}x"
                : "  Crecimiento AVL: primera fila");
            anteriorAvl = avl;
        }

        Console.WriteLine("\nFin de H2. Copia los tiempos impresos a medicion.md.");
    }
}
