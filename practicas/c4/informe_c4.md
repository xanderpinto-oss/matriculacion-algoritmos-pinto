# C4 — Comparación de búsqueda lineal y búsqueda binaria

**Estudiante:** Xander Pinto Montes  
**Asignatura:** Ingeniería de Algoritmos  
**Caso:** matriculación de 559 estudiantes de cinco carreras técnicas  
**Fecha:** 15 de noviembre de 2026  
**Lenguaje:** JavaScript (Node.js)

## Objetivo

Comparé la búsqueda lineal y la búsqueda binaria para localizar códigos de estudiantes en un conjunto de 559 registros que representa el volumen del proceso de matriculación.

## Datos y protocolo

Como no se proporcionó una base institucional real y no corresponde utilizar datos personales, generé 559 registros sintéticos con códigos únicos EST-0001 a EST-0559. Los registros se distribuyeron de forma reproducible entre las cinco carreras. Para la búsqueda lineal utilicé el conjunto mezclado; para la búsqueda binaria preparé una copia ordenada por código mediante MergeSort.

Realicé el calentamiento de ambos métodos y después tres corridas medidas. En cada corrida consulté los 559 códigos, repitiendo el conjunto 100 veces (55.900 búsquedas por corrida). La preparación del arreglo ordenado quedó fuera del tiempo de búsqueda y se midió aparte. Se verificó que ambos métodos encontraran todos los códigos.

## Resultados

Promedios de las tres corridas. El tiempo representa microsegundos por búsqueda.

| Método | Tiempo promedio (µs/búsqueda) | Complejidad | Cociente respecto a binaria |
|---|---:|---:|---:|
| Búsqueda lineal | 1,473677 | O(n) | 11,35× |
| Búsqueda binaria | 0,129846 | O(log n) | 1,00× |

El cociente se calculó como tiempo lineal dividido entre tiempo binario: 11,35. La búsqueda binaria fue aproximadamente 11,35 veces más rápida. Preparar los 559 registros ordenados con MergeSort promedió 0,399801 ms; el coste se recupera aproximadamente después de 298 búsquedas.

## Decisión para mi proceso

Sí, en mi proceso conviene mantener un índice ordenado por código antes del periodo de matrícula, porque se realizan muchas consultas y la búsqueda binaria fue 11,35 veces más rápida en la prueba.  
El coste de ordenar se amortiza aproximadamente desde 298 consultas; por eso ordenaría una vez y conservaría el índice, en lugar de volver a ordenar antes de cada búsqueda.

## Entorno y alcance

La medición se ejecutó con Node.js v22.16.0 en Linux x64, procesador virtualizado AMD EPYC 9V74 y 3 núcleos lógicos disponibles. Los tiempos pueden variar en otro equipo. Los registros son sintéticos: permiten comparar algoritmos, pero no representan datos personales ni tiempos reales de atención de la institución.

## Evidencia

Los archivos busquedas.js y resultados_c4.csv contienen la implementación, las tres corridas individuales y los promedios.