Xander Boris Pinto Montes

# P1.1 — Análisis
## a) Complejidad en el peor caso

| Fragmento | Costo | Justificación |
|---|---:|---|
| F1 | O(n) | El ciclo recorre los n elementos una sola vez y realiza trabajo constante por iteración. |
| F2 | O(n²) | Los dos ciclos anidados pueden ejecutar n · n comparaciones, cada una de costo constante. |
| F3 | O(log n) | En cada iteración k se duplica, por lo que alcanza n después de aproximadamente log₂(n) iteraciones. |
| F4 | O(n log n) | El ciclo externo se ejecuta n veces y, en cada vuelta, k se divide entre 3 hasta 1, realizando O(log₃ n) iteraciones. |

## b) Recurrencias

### (i) T(n) = 4T(n/3) + n
- a = 4, b = 3, d = 1.
- b^d = 3; a = 4 > 3.
- Domina la recursión: **T(n) = Θ(n^(log₃ 4))**.

### (ii) T(n) = 2T(n/2) + n²
- a = 2, b = 2, d = 2.
- b^d = 4; a = 2 < 4.
- Domina el trabajo no recursivo: **T(n) = Θ(n²)**.
