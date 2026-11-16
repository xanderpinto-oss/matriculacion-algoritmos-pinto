# C6 — Elección del algoritmo de ordenamiento para mi caso

**Estudiante:** Xander Pinto Montes  
**Asignatura:** Ingeniería de Algoritmos  
**Caso:** proceso de matriculación de 559 estudiantes  
**Fecha:** 15 de noviembre de 2026  
**Lenguaje:** JavaScript (Node.js)

## Mi elección: MergeSort

Para organizar los registros del proceso de matriculación elijo **MergeSort**. Mi caso cuenta con 559 estudiantes distribuidos en cinco carreras técnicas: Mecánica Automotriz, Sistemas Informáticos, Electromecánica, Electrónica y Mecánica Industrial. La cantidad actual no es demasiado grande, pero el registro puede aumentar en futuras gestiones y necesito un método con rendimiento predecible.

## Razones

- **Rendimiento garantizado:** MergeSort tiene complejidad O(n log n) en el caso promedio y en el peor caso. Evita el crecimiento cuadrático de Insertion Sort y el peor caso O(n²) de QuickSort.
- **Estabilidad:** si dos registros tienen la misma clave, MergeSort conserva su orden relativo. Esto es útil cuando organizo matrículas por código y necesito preservar el orden previo de registros con claves iguales.
- **Búsquedas posteriores:** al mantener los registros ordenados por código de estudiante, puedo utilizar búsqueda binaria. En C4, la búsqueda binaria fue 11,35 veces más rápida que la lineal en el conjunto de 559 códigos.
- **Coste de memoria aceptable:** MergeSort utiliza memoria auxiliar O(n). Para el volumen del caso, acepto ese uso adicional a cambio de estabilidad y tiempo predecible.

## Evidencia experimental

En el torneo C5, MergeSort obtuvo el menor promedio en los cuatro tamaños medidos:

| Elementos | Insertion Sort (ms) | MergeSort (ms) | QuickSort (ms) | Mejor resultado |
|---:|---:|---:|---:|---|
| 559 | 0,071823 | 0,035143 | 0,040997 | MergeSort |
| 1.118 | 0,242508 | 0,040281 | 0,105419 | MergeSort |
| 2.236 | 0,971882 | 0,095387 | 0,158051 | MergeSort |
| 4.472 | 4,431690 | 0,373476 | 0,599680 | MergeSort |

Para 4.472 elementos, MergeSort tardó 0,373476 ms, aproximadamente 11,9 veces menos que Insertion Sort y 1,6 veces menos que QuickSort en el promedio medido. La ventaja se mantuvo al incrementar la cantidad de elementos.

C4 complementa esta evidencia: una vez preparado el orden por código, la búsqueda binaria promedió 0,129846 microsegundos por consulta, mientras que la búsqueda lineal promedió 1,473677 microsegundos. El cociente fue 11,35 a 1.

## Aplicación a mi proceso

Al cargar o actualizar el listado de estudiantes, ordenaría los registros por código mediante MergeSort y conservaría ese orden como índice de consulta. Cuando el estudiante solicite su matrícula o el personal necesite verificar un registro, aplicaría búsqueda binaria sobre el listado ya ordenado. Así evito ordenar nuevamente en cada consulta y aprovecho el resultado de C4.

## Limitaciones y conclusión

Las pruebas se realizaron con registros y claves sintéticos porque no se proporcionó una base institucional real. Las mediciones se obtuvieron con Node.js v22.16.0 en Linux x64 y un procesador virtualizado AMD EPYC 9V74 con 3 núcleos lógicos disponibles; otro equipo puede producir tiempos distintos.

Elijo MergeSort porque combina tiempo O(n log n) garantizado, estabilidad y resultados favorables en las cuatro cantidades probadas. Para mi proceso necesito mantener un orden por código que permita localizar repetidamente a los 559 estudiantes y que pueda seguir funcionando si el registro crece. MergeSort, junto con búsqueda binaria sobre el índice ordenado, es la alternativa que mejor equilibra rendimiento, previsibilidad y utilidad para este caso.