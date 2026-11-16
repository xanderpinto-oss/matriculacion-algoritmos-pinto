# C5 — Torneo de algoritmos de ordenamiento: resultados y conclusiones

**Estudiante:** Xander Pinto Montes  
**Asignatura:** Ingeniería de Algoritmos  
**Caso:** organización de registros del proceso de matriculación  
**Fecha:** 15 de noviembre de 2026  
**Lenguaje:** JavaScript (Node.js)

## Objetivo

Comparé un algoritmo cuadrático (Insertion Sort) con dos algoritmos eficientes (MergeSort y QuickSort), aumentando progresivamente la cantidad de elementos para observar cómo cambia el tiempo de ejecución.

## Protocolo experimental

Utilicé arreglos pseudoaleatorios reproducibles con la misma semilla para cada tamaño; los tres algoritmos recibieron los mismos valores. Probé 559, 1.118, 2.236 y 4.472 elementos. Calenté cada algoritmo para cada tamaño antes de medir, realicé tres corridas y en cada corrida promedié 30 ordenamientos de copias independientes. La copia se preparó antes de iniciar el cronómetro. Después de cada ordenamiento comprobé que el arreglo estuviera en orden ascendente. El CSV conserva las tres mediciones y el promedio final.

## Tabla de resultados

Promedios en milisegundos por ordenamiento.

| Cantidad de elementos | Insertion Sort (ms) | MergeSort (ms) | QuickSort (ms) |
|---:|---:|---:|---:|
| 559 | 0,071823 | 0,035143 | 0,040997 |
| 1.118 | 0,242508 | 0,040281 | 0,105419 |
| 2.236 | 0,971882 | 0,095387 | 0,158051 |
| 4.472 | 4,431690 | 0,373476 | 0,599680 |

## Conclusiones

1. En las cuatro cantidades probadas, MergeSort obtuvo el menor tiempo promedio. Con 4.472 elementos necesitó 0,373476 ms, frente a 0,599680 ms de QuickSort y 4,431690 ms de Insertion Sort.
2. El crecimiento de Insertion Sort fue el más marcado: al aumentar el arreglo de 559 a 4.472 elementos, su promedio pasó de 0,071823 ms a 4,431690 ms. Esto concuerda con su complejidad cuadrática O(n²).
3. MergeSort mantuvo tiempos bajos al crecer la entrada, coherentes con O(n log n). Además, su implementación es estable y conserva el orden relativo de elementos con la misma clave.
4. QuickSort tiene un comportamiento promedio O(n log n), pero su peor caso puede llegar a O(n²). En esta ejecución fue más lento que MergeSort en los tamaños comparados.
5. Para mi caso de matriculación, el torneo respalda usar MergeSort para organizar los códigos de estudiantes y preparar una lista ordenada para búsquedas binarias. Insertion Sort puede servir para arreglos muy pequeños o casi ordenados, pero no sería mi primera elección si el registro crece.

## Equipo y alcance

Las mediciones se ejecutaron con Node.js v22.16.0 en Linux x64, sobre un procesador virtualizado AMD EPYC 9V74 con 3 núcleos lógicos disponibles. Los tiempos dependen del equipo. Los datos son sintéticos y reproducibles; 559 corresponde a la población informada para el caso y las cantidades superiores son escenarios de crecimiento. No se usaron datos personales ni se midieron tiempos administrativos reales.