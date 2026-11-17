Xander Boris Pinto Montes

# P1.2 — Cura y medición

## 1. Cura
La cura copia el arreglo, ordena la copia y compara elementos consecutivos; detecta cualquier repetido en **O(n log n)** por la ordenación, sin modificar el arreglo original.

## 2. Protocolo
El programa medicion.cs usa cuatro tamaños duplicados (8.000, 16.000, 32.000 y 64.000), genera listas sin repetidos, realiza una corrida de calentamiento y tres corridas medidas por algoritmo. Calcula el promedio y el cociente respecto al tamaño anterior. Ejecutar en Release.

**La tabla debe completarse con los resultados reales del programa; no se inventan tiempos.**

| n | Por pares (promedio ms) | Veces vs. fila anterior | Cura (promedio ms) | Veces vs. fila anterior |
|---:|---:|---:|---:|---:|
| 8.000 | PENDIENTE | — | PENDIENTE | — |
| 16.000 | PENDIENTE | PENDIENTE | PENDIENTE | PENDIENTE |
| 32.000 | PENDIENTE | PENDIENTE | PENDIENTE | PENDIENTE |
| 64.000 | PENDIENTE | PENDIENTE | PENDIENTE | PENDIENTE |

### Conclusiones (completar con la tabla real)
1. La solución por pares debería crecer aproximadamente cuatro veces al duplicar n, coherente con O(n²).
2. La cura debería crecer menos que la solución cuadrática y aproximarse a O(n log n); los cocientes pueden variar por el costo de copiar y ordenar.
3. Puede existir ruido por procesos del sistema, calentamiento, memoria y planificación del procesador; por eso se promedian tres corridas.
4. Indicar el mayor promedio medido, el algoritmo y el n correspondiente después de ejecutar.

## Ejecución
Colocar medicion.cs en un proyecto de consola .NET y ejecutar: dotnet run -c Release.
