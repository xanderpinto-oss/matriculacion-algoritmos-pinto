Xander Boris Pinto Montes

# Análisis — Evaluación integradora, variante B

## 1. Identificación del problema

El caso de las compras de una escuela es el problema clásico de la **mochila 0/1**. Cada útil tiene un costo en bolivianos y una utilidad. Para cada útil solo hay dos alternativas: comprarlo completo o no comprarlo. Se busca maximizar la suma de utilidades sin superar el presupuesto.

La entrada contiene n útiles, con \`CostoBs\` y \`Utilidad\`, y un presupuesto B. La salida debe contener la utilidad máxima y los códigos de los útiles que forman esa solución. No se puede fraccionar un artículo ni comprarlo más de una vez.

## 2. Costo de la solución ingenua

\`CompradorIngenuo\` recorre las \`2^n\` máscaras, una por cada subconjunto posible. Para cada máscara vuelve a recorrer los n útiles para sumar costo y utilidad. Por eso realiza un trabajo proporcional a:

\[
T(n)=n\,2^n=\Theta(n2^n)
\]

La cantidad de subconjuntos se duplica al agregar un útil y, además, cada subconjunto se revisa recorriendo la lista. Una forma de escribir la recurrencia del trabajo es:

\[
T(n)=2T(n-1)+\Theta(2^n),\qquad T(0)=\Theta(1)
\]

Al desarrollar la recurrencia se obtiene \(T(n)=\Theta(n2^n)\). **No corresponde aplicar directamente el teorema maestro**: este se formula para recurrencias con subproblemas de tamaño reducido por un factor, como \(T(n)=aT(n/b)+f(n)\). Aquí el tamaño baja restando uno, no dividiéndose entre una constante mayor que uno. Se resuelve desarrollando la suma o la recurrencia anterior.

El método ingenuo usa espacio auxiliar lineal para la lista de códigos elegidos, aparte de la entrada.

## 3. Contraejemplo de una estrategia voraz

No basta con elegir primero el artículo de mayor utilidad ni el de mejor relación utilidad/costo. En el primer caso de prueba:

| Útil | Costo (Bs) | Utilidad | Utilidad/costo |
|---|---:|---:|---:|
| U1 | 20 | 120 | 6,00 |
| U2 | 40 | 200 | 5,00 |
| U3 | 60 | 240 | 4,00 |

Con presupuesto de **100 Bs**, un voraz por relación utilidad/costo escoge U1 y U2: gasta 60 Bs y obtiene 320 puntos. Sin embargo, la combinación U2 y U3 gasta exactamente 100 Bs y obtiene **440 puntos**. Un voraz por utilidad individual también empieza con U3 y luego U1, y solo alcanza 360 puntos. Por tanto, esas decisiones locales no garantizan el óptimo global.

## 4. Estrategia elegida: programación dinámica 0/1

Defino \`dp[c]\` como la mayor utilidad alcanzable con una capacidad de c bolivianos, después de procesar los artículos anteriores. Para un útil con costo w y utilidad v, la transición es:

\[
DP_i[c]=\max(DP_{i-1}[c],\ DP_{i-1}[c-w]+v)
\]

cuando \(w\le c\); si no cabe, se conserva el valor anterior. En el código comprimo la dimensión de artículos y recorro las capacidades **de mayor a menor**. Así, el artículo actual no se puede volver a utilizar en la misma iteración, respetando la regla 0/1.

Para reconstruir los códigos elegidos guardo una matriz booleana \`tomar[i,c]\`, que marca cuándo el artículo i mejoró la solución para la capacidad c. Después recorro los artículos en orden inverso y descuento el costo de cada artículo marcado.

Sea \(B' = \min(B,\sum w_i)\), considerando solo útiles de utilidad positiva. El costo temporal es **O(n · B')** y el espacio es **O(n · B')** por la matriz booleana de reconstrucción, más **O(B')** por el arreglo de mejores utilidades. Es una complejidad pseudo-polinomial: depende del valor numérico del presupuesto, no solo de la cantidad de artículos. Para estos tamaños es mucho mejor que enumerar \(2^n\) subconjuntos.

También agregué una optimización: si el presupuesto alcanza para pagar todos los útiles con utilidad positiva, se devuelven directamente esos útiles en **O(n)**, sin crear la tabla dinámica.

## 5. Estructura para \`EstaEnLaCompra\`

En \`CompradorMejorado\` utilizo \`HashSet<string>\` con comparación ordinal para almacenar los códigos seleccionados. La inserción y la consulta \`Contains\` tienen costo **O(1) promedio**. Por ello \`EstaEnLaCompra(codigo)\` no recorre una lista completa; contrasta con \`List<string>.Contains\` de la solución ingenua, cuyo costo es O(k) para k códigos elegidos.

## 6. Los seis casos de prueba

| Caso | Presupuesto | Utilidad óptima | Códigos esperados |
|---|---:|---:|---|
| El voraz se equivoca | 100 | 440 | U2, U3 |
| Cuatro útiles | 16 | 20 | U2, U4 |
| Presupuesto cero | 0 | 0 | ninguno |
| Sin útiles | 30 | 0 | ninguno |
| Nada cabe | 60 | 0 | ninguno |
| Entran todos | 200 | 52 | U1, U2, U3 |

El programa prueba estos seis casos con ambas implementaciones y además realiza dos verificaciones de pertenencia (U2 sí está y U1 no está) para cada comprador.

## 7. ¿Qué ocurre con un presupuesto de 1.000.000.000 Bs?

No se debe crear una tabla con mil millones de columnas. La implementación limita la capacidad efectiva a \(B'=\min(B,\sum w_i)\). Si el presupuesto es al menos la suma de los costos de todos los útiles con utilidad positiva, todos ellos caben y se devuelven directamente en O(n). En el lote generado para 1.000 útiles, cada costo está entre 4 y 49 Bs, así que el costo total no puede superar 49.000 Bs. Con un presupuesto de 1.000.000.000 Bs se comprarían todos los útiles de utilidad positiva y se usaría la salida rápida O(n).

Si en otro conjunto el costo total superara el presupuesto, la programación dinámica seguiría dependiendo de \(B'\). En ese escenario un presupuesto enorme puede consumir mucha memoria y tiempo; para importes y costos arbitrariamente grandes habría que considerar otra formulación, por ejemplo programación dinámica por utilidad o un algoritmo aproximado, según los límites y la precisión exigida.
