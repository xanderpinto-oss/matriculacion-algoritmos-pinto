Xander Boris Pinto Montes

# P1.2 — Cura y medición

## 1. Cura
La cura copia el arreglo, ordena la copia y compara elementos consecutivos. Así detecta cualquier valor repetido en **O(n log n)** por la ordenación, sin modificar el arreglo original. La copia requiere **O(n)** memoria adicional.

## 2. Protocolo
El programa `medicion.cs` usa cuatro tamaños duplicados (8.000, 16.000, 32.000 y 64.000), genera listas sin repetidos, realiza una corrida de calentamiento y tres corridas medidas por algoritmo. Calcula el promedio y el cociente respecto al tamaño anterior. La ejecución se realizó en modo Release con .NET SDK 11.0.100.

## 3. Resultados reales

| n | Por pares (promedio ms) | Veces vs. fila anterior | Cura (promedio ms) | Veces vs. fila anterior |
|---:|---:|---:|---:|---:|
| 8.000 | 14,506 | — | 0,562 | — |
| 16.000 | 59,794 | 4,12 | 0,624 | 1,11 |
| 32.000 | 228,931 | 3,83 | 1,704 | 2,73 |
| 64.000 | 908,473 | 3,97 | 2,593 | 1,52 |

## 4. Validación
Las cinco pruebas funcionales terminaron correctamente:

| Caso | Resultado esperado | Resultado obtenido |
|---|---:|---:|
| Repetido en el medio | True | True — OK |
| Sin repetidos | False | False — OK |
| Lista vacía | False | False — OK |
| Un elemento | False | False — OK |
| Repetido en extremos | True | True — OK |

## 5. Conclusiones
1. Al duplicar el tamaño, el algoritmo por pares multiplicó su tiempo aproximadamente por 4,12, 3,83 y 3,97. Este comportamiento es consistente con su complejidad **O(n²)**.
2. La cura tuvo tiempos mucho menores. Sus cocientes fueron 1,11, 2,73 y 1,52; no siguen un patrón uniforme porque los tiempos son muy pequeños y pueden verse afectados por la precisión del cronómetro, la carga del sistema y otros costos de ejecución. Su complejidad teórica sigue siendo **O(n log n)**.
3. Para 64.000 elementos, el algoritmo por pares tardó **908,473 ms**, mientras que la cura tardó **2,593 ms**. En esta medición, la cura fue aproximadamente **350 veces más rápida**.
4. Las cinco pruebas funcionales pasaron, incluyendo lista vacía, un solo elemento y duplicados en distintas posiciones. Esto aporta evidencia de que la cura conserva la corrección para los casos probados.

## Ejecución
Colocar `medicion.cs` en un proyecto de consola .NET y ejecutar en PowerShell:

```powershell
dotnet run -c Release
```
