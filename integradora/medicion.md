Xander Boris Pinto Montes

# Medición — Evaluación integradora, variante B

## Protocolo

Ejecuté el programa en mi computadora con la configuración `Release`, usando `dotnet run -c Release`. Para cada tamaño se hizo una corrida de calentamiento que no se incluyó en los resultados y después tres corridas medidas con `Stopwatch`. El promedio se calculó con esas tres corridas.

Los útiles se generan con una semilla fija (`n + 1000`), de modo que las dos implementaciones trabajan con los mismos datos y el mismo presupuesto, que corresponde a un tercio del costo total. El programa calibró el tamaño máximo para que la solución ingenua quedara dentro del intervalo pedido de 0,5 a 20 segundos. La calibración dio `n = 24` en `0,700 s`; esa corrida no se incluyó en la tabla. Por eso medí cuatro tamaños consecutivos: 21, 22, 23 y 24 útiles.

## Resultados de las cuatro pruebas

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

En las cuatro filas el programa confirmó que las dos soluciones obtuvieron la misma utilidad máxima:

| Útiles (n) | Utilidad ingenua | Utilidad mejorada | Útiles elegidos por cada solución | Resultado |
|---:|---:|---:|---:|---|
| 21 | 612 | 612 | 10 | Coinciden |
| 22 | 634 | 634 | 9 | Coinciden |
| 23 | 875 | 875 | 12 | Coinciden |
| 24 | 705 | 705 | 11 | Coinciden |

Además, las pruebas iniciales dieron **8 de 8 correctas** para `CompradorIngenuo` y **8 de 8 correctas** para `CompradorMejorado`. En cada comprador se revisaron los seis casos del enunciado y dos consultas de `EstaEnLaCompra`.

## Solución mejorada con 1.000 útiles y presupuesto de 10.000 Bs

También ejecuté la solución mejorada con 1.000 útiles y un presupuesto de 10.000 Bs. Los datos se generaron con la misma semilla fija del programa.

| Corrida | Tiempo (ms) |
|---:|---:|
| 1 | 14,987 |
| 2 | 13,206 |
| 3 | 15,698 |
| **Promedio** | **14,630** |

La utilidad máxima obtenida fue **35.071 puntos** y se eligieron **518 útiles**.

## Conclusión

En mis pruebas, el comprador mejorado obtuvo la misma utilidad que el ingenuo en los cuatro tamaños y también pasó sus ocho verificaciones. El ingenuo tardó aproximadamente el doble cada vez que agregué un útil: sus cocientes fueron 2,04x, 2,04x y 2,04x, lo que concuerda con el crecimiento exponencial de revisar todos los subconjuntos. El mejorado fue mucho más rápido, pero sus cocientes (0,68x, 1,33x y 0,81x) no crecen de forma regular; como sus tiempos son muy pequeños, una diferencia de unas pocas centésimas de milisegundo produce bastante variación. También puede influir el calentamiento del entorno, el sistema operativo y la precisión de la medición, así que no tomo esos cocientes como una regla exacta. Finalmente, con 1.000 útiles y presupuesto de 10.000 Bs el mejorado obtuvo 35.071 puntos en un promedio de 14,630 ms.
