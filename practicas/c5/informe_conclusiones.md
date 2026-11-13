# Informe corto de conclusiones - Taller C5

**Estudiante:** Xander Pinto  
**Asignatura:** Ingeniería de Algoritmos  
**Práctica:** C5 - Torneo de algoritmos  
**Fecha de ejecución:** completar con la fecha real  
**Lenguaje:** JavaScript (Node.js)

## 1. Objetivo
Comparar experimentalmente Insertion Sort, Merge Sort y `Array.sort()` al ordenar arreglos de 10.000, 20.000, 40.000 y 80.000 elementos.

## 2. Equipo utilizado
Completar con los datos que imprime el programa:
- Sistema operativo:
- Procesador:
- Memoria RAM:
- Versión de Node.js:

## 3. Protocolo
Se generaron datos pseudoaleatorios reproducibles. Para cada tamaño, los tres algoritmos recibieron copias del mismo arreglo original. Se realizó calentamiento, tres corridas por algoritmo y tamaño, verificación del orden y cálculo del promedio. La copia del arreglo quedó fuera del tiempo medido.

## 4. Resultados
Copiar aquí los promedios reales que muestra la tabla final del programa. Los tiempos están expresados en milisegundos.

| Cantidad | Insertion Sort (ms) | Merge Sort (ms) | Array.sort() (ms) |
|---:|---:|---:|---:|
| 10.000 | completar | completar | completar |
| 20.000 | completar | completar | completar |
| 40.000 | completar | completar | completar |
| 80.000 | completar | completar | completar |

El archivo `resultados_c5.csv` contiene las tres corridas individuales y el promedio de cada prueba.

## 5. Conclusiones
Completar después de ejecutar el programa:

1. El algoritmo más rápido en mis pruebas fue __________.
2. Al aumentar la cantidad de datos, Insertion Sort tardó __________. Esto se relaciona con su complejidad O(n²).
3. Merge Sort mostró __________, coherente con su complejidad O(n log n).
4. `Array.sort()` obtuvo __________ en la versión de Node.js utilizada.
5. La práctica demuestra que la elección del algoritmo influye en el tiempo de ejecución cuando crece el tamaño de entrada.

**Importante:** no rellenar la tabla ni las conclusiones con tiempos inventados. Usar los resultados medidos en el propio equipo.
