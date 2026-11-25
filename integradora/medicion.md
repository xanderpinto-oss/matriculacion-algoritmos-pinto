Xander Boris Pinto Montes

# Medición — Evaluación integradora, variante B

## Protocolo

Las mediciones se realizan en la computadora donde se ejecute el proyecto, en configuración `Release`. Para cada tamaño el programa ejecuta una corrida de calentamiento que no se registra y después tres corridas medidas con `Stopwatch`; muestra el promedio de las tres. Los datos se generan con una semilla fija (`n + 1000`) para que las dos implementaciones reciban exactamente los mismos útiles y el mismo presupuesto (un tercio del costo total).

El programa calibra el tamaño máximo para que la medición de la solución ingenua quede, cuando sea posible, entre 0,5 y 20 segundos. Esa calibración no forma parte de la tabla. A partir del máximo elegido mide cuatro tamaños consecutivos: el máximo menos tres, el máximo menos dos, el máximo menos uno y el máximo. El tamaño se calibra en la computadora de ejecución; no se inventan tiempos ni tamaños para otra máquina. En cada fila el programa verifica que ambas implementaciones producen la misma utilidad e imprime los cocientes entre promedios consecutivos.

## Resultados de cuatro tamaños

**Pendiente de ejecutar en la computadora donde se entrega.** No coloqué tiempos inventados: al ejecutar `dotnet run -c Release`, el programa imprime los tamaños, las tres corridas, los promedios, los cocientes de crecimiento y la utilidad de control para completar esta tabla con datos reales.

| Útiles (n) | Ingenuo promedio (ms) | Crecimiento ingenuo | Mejorado promedio (ms) | Crecimiento mejorado | Verificación |
|---:|---:|---:|---:|---:|---|
| Tamaño 1 | Pendiente | — | Pendiente | — | Pendiente de ejecución |
| Tamaño 2 | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente de ejecución |
| Tamaño 3 | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente de ejecución |
| Tamaño 4 | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente de ejecución |

La consola indica en cada fila `VERIFICACIÓN: misma utilidad` cuando el resultado de los dos compradores coincide. Copio esa utilidad junto con los promedios y los cocientes al completar la tabla.

## Solución mejorada con 1.000 útiles y presupuesto de 10.000 Bs

El programa mide `CompradorMejorado` con 1.000 útiles generados de forma reproducible y presupuesto fijo de 10.000 Bs. Ejecuta un calentamiento y tres mediciones; imprime las tres duraciones, el promedio y la utilidad obtenida.

**Tiempo promedio: pendiente de ejecutar en la computadora donde se entrega.** Se completa con el promedio que imprime el bloque `SOLUCIÓN MEJORADA: 1000 útiles y presupuesto de 10000 Bs`.

## Lectura y conclusión

La programación dinámica debería crecer mucho más lentamente que la enumeración exhaustiva al aumentar el número de útiles: la ingenua revisa (2^n) subconjuntos y recorre los artículos dentro de cada subconjunto, mientras que la mejorada trabaja en O(n · B') para la capacidad efectiva. La consulta `EstaEnLaCompra` queda en O(1) promedio por el `HashSet`.

No se debe interpretar cada cociente como una ley exacta. Los tiempos pueden variar por calentamiento del JIT, compilación, procesos del sistema operativo, administración de memoria y precisión del reloj; por eso se usa el promedio de tres corridas. La conclusión definitiva sobre el crecimiento observado se completa después de comparar los cocientes reales impresos por el programa.
