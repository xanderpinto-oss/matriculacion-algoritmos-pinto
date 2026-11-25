Xander Boris Pinto Montes

# Mi medición — Evaluación integradora, variante B

## Protocolo que utilicé

Ejecuté el proyecto en mi computadora con la configuración Release, mediante el comando dotnet run -c Release. Para cada tamaño realicé primero una corrida de calentamiento, que no incluí en los resultados, y después tomé tres mediciones con Stopwatch. Finalmente, utilicé el promedio de esas tres corridas.

Generé los útiles con una semilla fija (n + 1000) para que ambas implementaciones recibieran los mismos datos. También mantuve el mismo presupuesto en cada comparación, calculado como un tercio del costo total. Antes de medir los cuatro tamaños, calibré la cantidad máxima para que el comprador ingenuo tardara entre 0,5 y 20 segundos. La calibración dio n = 24 en 0,700 s; no incluí ese tiempo en la tabla. A partir de ese resultado medí cuatro tamaños consecutivos: 21, 22, 23 y 24 útiles.

## Resultados que obtuve

### Comprador ingenuo

| Útiles (n) | Presupuesto (Bs) | Corrida 1 (ms) | Corrida 2 (ms) | Corrida 3 (ms) | Promedio (ms) | Crecimiento vs. fila anterior |
|---:|---:|---:|---:|---:|---:|---:|
| 21 | 161 | 79,387 | 83,110 | 83,084 | 81,860 | — |
| 22 | 195 | 167,180 | 167,237 | 167,240 | 167,219 | 2,04x |
| 23 | 228 | 340,817 | 338,897 | 341,517 | 340,411 | 2,04x |
| 24 | 214 | 698,425 | 696,165 | 689,392 | 694,661 | 2,04x |

### Comprador mejorado

| Útiles (n) | Presupuesto (Bs) | Corrida 1 (ms) | Corrida 2 (ms) | Corrida 3 (ms) | Promedio (ms) | Crecimiento vs. fila anterior |
|---:|---:|---:|---:|---:|---:|---:|
| 21 | 161 | 0,039 | 0,043 | 0,055 | 0,046 | — |
| 22 | 195 | 0,029 | 0,036 | 0,028 | 0,031 | 0,68x |
| 23 | 228 | 0,041 | 0,037 | 0,047 | 0,042 | 1,33x |
| 24 | 214 | 0,033 | 0,035 | 0,033 | 0,034 | 0,81x |

Al comparar los resultados, comprobé que las dos implementaciones obtuvieron la misma utilidad máxima en los cuatro tamaños:

| Útiles (n) | Utilidad ingenua | Utilidad mejorada | Útiles elegidos por cada solución | Mi comprobación |
|---:|---:|---:|---:|---|
| 21 | 612 | 612 | 10 | Coinciden |
| 22 | 634 | 634 | 9 | Coinciden |
| 23 | 875 | 875 | 12 | Coinciden |
| 24 | 705 | 705 | 11 | Coinciden |

También ejecuté los casos iniciales del enunciado. Obtuve **8 de 8 pruebas correctas** con CompradorIngenuo y **8 de 8 pruebas correctas** con CompradorMejorado. En cada implementación comprobé los seis casos y las dos consultas adicionales de EstaEnLaCompra.

## Mi prueba con 1.000 útiles y presupuesto de 10.000 Bs

Después ejecuté la solución mejorada con 1.000 útiles y un presupuesto de 10.000 Bs. Para esta prueba también realicé un calentamiento y tres corridas medidas. Estos fueron los tiempos que obtuve:

| Corrida | Tiempo (ms) |
|---:|---:|
| 1 | 14,987 |
| 2 | 13,206 |
| 3 | 15,698 |
| **Promedio** | **14,630** |

Con esos datos, mi solución alcanzó una utilidad máxima de **35.071 puntos** y seleccionó **518 útiles**.

## Mi conclusión

En mis pruebas, el comprador mejorado consiguió la misma utilidad que el ingenuo en los cuatro tamaños y pasó sus ocho verificaciones iniciales. Observé que el ingenuo tardó aproximadamente el doble cada vez que agregué un útil: sus cocientes fueron 2,04x, 2,04x y 2,04x. Este comportamiento coincide con el crecimiento exponencial que espero al revisar todos los subconjuntos.

Mi solución mejorada fue mucho más rápida. Sin embargo, sus cocientes (0,68x, 1,33x y 0,81x) no siguen un crecimiento regular. Esto puede pasar porque sus tiempos son muy pequeños: una diferencia de unas pocas centésimas de milisegundo cambia bastante el cociente. También pueden influir el calentamiento del entorno, otros procesos del sistema operativo y la precisión del reloj. Por eso tomo estos resultados como mediciones de mi computadora y no como una regla exacta. En la prueba más grande, con 1.000 útiles y presupuesto de 10.000 Bs, obtuve 35.071 puntos con un promedio de 14,630 ms.
