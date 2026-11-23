Xander Boris Pinto Montes

# H2 — Medición de rendimiento

## 1. Objetivo

En esta parte comparé dos formas de trabajar con las matrículas de mi caso: una lista simple (`RegistroLista`) y un árbol AVL (`RegistroAVL`). Las dos soluciones manejan el mismo registro y utilizan `id_matricula` como clave.

## 2. Cómo realicé la prueba

Ejecuté el programa en modo Release. Para cada tamaño hice primero una corrida de calentamiento, que no tomé en cuenta, y después tres corridas medidas con `Stopwatch`. Al final calculé el promedio de los tres tiempos.

La carga realiza las mismas operaciones para las dos estructuras: insertar las matrículas, buscarlas por clave, consultar el menor, buscar el siguiente, consultar un rango, recorrer los registros en orden y borrar algunos registros. Los datos son generados por el programa tomando como referencia los campos y categorías de la muestra del H1; son datos de prueba y no datos personales reales.

Probé cuatro tamaños que se duplican: 559, 1.118, 2.236 y 4.472 matrículas. En cada repetición el valor de control fue igual para las dos estructuras y para las tres corridas de cada tamaño: 1.477, 2.930, 5.837 y 11.651, respectivamente. Esto me permitió comprobar que las operaciones de la carga se completaron.

## 3. Resultados de la lista ingenua

Los tiempos están expresados en milisegundos (ms). El crecimiento se calculó dividiendo el promedio del tamaño actual entre el promedio del tamaño anterior.

| Matrículas | Corrida 1 | Corrida 2 | Corrida 3 | Promedio | Crecimiento vs. fila anterior |
|---:|---:|---:|---:|---:|---:|
| 559 | 8,988 | 9,839 | 9,176 | 9,335 | — |
| 1.118 | 40,609 | 27,646 | 26,699 | 31,651 | 3,39x |
| 2.236 | 76,022 | 96,856 | 97,910 | 90,263 | 2,85x |
| 4.472 | 99,733 | 94,696 | 85,228 | 93,219 | 1,03x |

## 4. Resultados del árbol AVL

| Matrículas | Corrida 1 | Corrida 2 | Corrida 3 | Promedio | Crecimiento vs. fila anterior |
|---:|---:|---:|---:|---:|---:|
| 559 | 0,647 | 0,570 | 0,912 | 0,710 | — |
| 1.118 | 1,627 | 1,666 | 1,547 | 1,613 | 2,27x |
| 2.236 | 4,705 | 2,083 | 2,807 | 3,198 | 1,98x |
| 4.472 | 1,898 | 2,901 | 1,976 | 2,258 | 0,71x |

## 5. Comparación entre las dos soluciones

Para comparar los promedios dividí el tiempo de la lista entre el tiempo del AVL en cada tamaño.

| Matrículas | Promedio lista | Promedio AVL | La lista tardó aproximadamente |
|---:|---:|---:|---:|
| 559 | 9,335 ms | 0,710 ms | 13,15 veces más |
| 1.118 | 31,651 ms | 1,613 ms | 19,62 veces más |
| 2.236 | 90,263 ms | 3,198 ms | 28,22 veces más |
| 4.472 | 93,219 ms | 2,258 ms | 41,28 veces más |

## 6. Conclusión

En mis pruebas el árbol AVL fue más rápido que la lista en los cuatro tamaños, porque mantiene las claves ordenadas y balanceadas y puede buscar, insertar y borrar en O(log n). La lista necesita recorrer registros para buscar, comprobar duplicados y borrar, por lo que esas operaciones cuestan O(n), y en conjunto se espera un crecimiento mayor al aumentar los datos. Sin embargo, mis tiempos no muestran una tendencia perfectamente regular: en la última duplicación la lista subió solo 1,03x y el AVL bajó a 0,71x; considero que hubo ruido por el tiempo corto de las pruebas y las condiciones de ejecución. Por eso tomo estos resultados como una comparación de esta computadora y no como una prueba exacta de la complejidad teórica.
