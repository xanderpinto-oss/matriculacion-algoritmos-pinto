Xander Boris Pinto Montes

# P1.3 — Elección para el almacén

Datos: n = 250.000 códigos y q = 8.000 consultas. log₂(250.000) ≈ 17,93.

## Costos aproximados
1. **Recorrido en cada consulta:** 8.000 × 250.000 = **2.000.000.000 operaciones** en el peor caso.
2. **Ordenar una vez con O(n log n) y buscar en binario:** ordenación ≈ 250.000 × 17,93 = 4.482.500; consultas ≈ 8.000 × 17,93 = 143.440. Total aproximado: **4.625.940**. Para no duplicar el listado, usaría un algoritmo in situ como heapsort (memoria auxiliar O(1)) y luego búsqueda binaria.
3. **Ordenación cuadrática y búsqueda binaria:** ordenación ≈ 250.000² = **62.500.000.000**; consultas ≈ 143.440. Total aproximado: **62.500.143.440**.

Son estimaciones de operaciones; las constantes dependen de la implementación.

## Elección
Elijo la **opción 2**: ordenar una sola vez con un algoritmo O(n log n) in situ (por ejemplo, heapsort) y hacer las 8.000 búsquedas binarias. Evita los miles de millones de revisiones de la opción 1 y el costo cuadrático de la opción 3, sin duplicar el listado.

## ¿Desde cuántas consultas cambiaría de opinión?
El punto de equilibrio aproximado entre recorrer y ordenar es:
q · n = n log₂(n) + q log₂(n)
q = [n log₂(n)] / [n − log₂(n)] ≈ **18 consultas**.
Con menos de unas 18 consultas podría convenir el recorrido directo para evitar ordenar. Desde aproximadamente 18, ordenar y buscar binariamente resulta más conveniente. Para 8.000 consultas, la opción 2 gana ampliamente.

La medición de P1.2 permite observar la diferencia experimental entre crecimiento cuadrático y n log n. Incorporaré los tiempos reales obtenidos en mi equipo al defender esta decisión.
