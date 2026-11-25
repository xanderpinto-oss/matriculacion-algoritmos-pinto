Xander Boris Pinto Montes

# Mi análisis — Evaluación integradora, variante B

## 1. Cómo entendí el problema

Al revisar el caso de las compras de la escuela, identifiqué que se trata del problema clásico de la **mochila 0/1**. Cada útil tiene un costo en bolivianos y un puntaje de utilidad. Para cada artículo solo puedo tomar una decisión: comprarlo completo o dejarlo fuera. Mi objetivo es elegir una combinación que me dé la mayor utilidad posible sin superar el presupuesto.

Como entrada recibo los n útiles, cada uno con sus campos CostoBs y Utilidad, además del presupuesto B. Como resultado debo devolver la utilidad máxima y los códigos de los útiles que seleccioné. También debo respetar que no puedo fraccionar un útil ni comprarlo más de una vez.

## 2. Análisis del costo de la solución ingenua

Al revisar CompradorIngenuo, observé que recorre las 2^n máscaras posibles, es decir, todos los subconjuntos que se pueden formar con los útiles. Para cada máscara vuelve a recorrer los n artículos y suma sus costos y utilidades. Por eso, el trabajo total es proporcional a:

\[
T(n)=n\,2^n=\Theta(n2^n)
\]

La cantidad de subconjuntos se duplica cuando agrego un útil. Además, dentro de cada subconjunto se revisa la lista de artículos. Para expresar este crecimiento mediante una recurrencia, la planteo así:

\[
T(n)=2T(n-1)+\Theta(2^n),\qquad T(0)=\Theta(1)
\]

Al desarrollar la recurrencia llego a T(n) = Θ(n · 2^n). **No puedo aplicar directamente el teorema maestro**, porque la recurrencia no divide el tamaño del problema entre una constante; pasa de n a n - 1. El teorema maestro se utiliza en recurrencias del tipo T(n) = aT(n/b) + f(n), donde el subproblema reduce su tamaño por un factor. En este caso resuelvo la recurrencia desarrollándola, y obtengo el mismo costo que al contar los 2^n subconjuntos y el recorrido de n elementos por cada uno.

En cuanto al espacio auxiliar de la solución ingenua, observo que conserva los códigos seleccionados en una lista, por lo que utiliza espacio O(n) en el peor caso, sin contar los datos de entrada.

## 3. Por qué no elegí una estrategia voraz

Antes de elegir mi solución, comprobé que no podía resolver el problema tomando siempre el artículo con mejor utilidad/costo. Para mostrarlo utilicé el primer caso de prueba del enunciado:

| Útil | Costo (Bs) | Utilidad | Utilidad/costo |
|---|---:|---:|---:|
| U1 | 20 | 120 | 6,00 |
| U2 | 40 | 200 | 5,00 |
| U3 | 60 | 240 | 4,00 |

Con un presupuesto de **100 Bs**, si ordeno por la relación utilidad/costo, primero elijo U1 y después U2. Gasto 60 Bs y consigo 320 puntos. Sin embargo, al comparar esa selección con U2 y U3, veo que esta última combinación cuesta exactamente 100 Bs y consigue **440 puntos**. También probé la regla de elegir primero la mayor utilidad individual: escogería U3 y luego U1, con un total de 360 puntos. Estos resultados me muestran que una decisión local no garantiza la mejor combinación final, por eso descarté el método voraz.

## 4. Estrategia que elegí: programación dinámica 0/1

Elegí **programación dinámica 0/1** porque necesito comparar las combinaciones posibles sin enumerar explícitamente todos los subconjuntos. Defino dp[c] como la mayor utilidad que puedo alcanzar con una capacidad de c bolivianos, después de procesar los artículos anteriores. Para cada útil, con costo w y utilidad v, uso esta transición:

\[
DP_i[c]=\max(DP_{i-1}[c],\ DP_{i-1}[c-w]+v)
\]

Aplico la segunda alternativa solamente cuando w ≤ c; si el útil no cabe, conservo el valor anterior. En mi código comprimí la dimensión de artículos y recorro las capacidades **de mayor a menor**. Hago ese recorrido descendente para evitar que el mismo artículo se utilice más de una vez en una misma iteración, tal como exige el problema 0/1.

También necesitaba devolver los códigos de los artículos elegidos, no solamente la utilidad. Para conseguirlo guardo una matriz booleana tomar[i,c], donde marco cuándo el artículo i mejora la solución para la capacidad c. Después recorro los artículos en orden inverso y voy descontando el costo de cada artículo que marqué.

Para analizar el costo, llamo B' a la capacidad efectiva, que calculo como el menor valor entre el presupuesto y la suma de los costos de los útiles con utilidad positiva. Mi solución realiza **O(n · B')** operaciones en el peor caso. En espacio utiliza **O(n · B')** por la matriz booleana que necesito para reconstruir la selección, más **O(B')** por el arreglo de mejores utilidades. Esta complejidad es pseudo-polinomial porque depende del valor numérico del presupuesto y no solamente de la cantidad de útiles. Aun así, para los tamaños y costos de este ejercicio es mucho más práctica que revisar 2^n subconjuntos.

Además, agregué una salida rápida: si compruebo que el presupuesto alcanza para pagar todos los útiles con utilidad positiva, los selecciono directamente. De esa manera, ese caso se resuelve en **O(n)** y no necesito construir la tabla de programación dinámica.

## 5. Estructura que utilicé para EstaEnLaCompra

Para guardar los códigos elegidos y responder la consulta EstaEnLaCompra(codigo), utilicé un HashSet<string> con comparación ordinal. Elegí esta estructura porque Contains permite consultar si un código está seleccionado en **O(1) promedio**, sin recorrer todos los códigos.

La solución ingenua utiliza List<string>.Contains, que puede tardar O(k) si tengo que revisar una lista de k códigos. Con mi HashSet, la consulta de pertenencia es más directa y cumple el requisito de O(1) promedio.

## 6. Casos que utilicé para comprobar el resultado

Antes de medir, revisé los seis casos de prueba incluidos en el enunciado. Estos son los resultados esperados que utilicé para comprobar las dos implementaciones:

| Caso | Presupuesto | Utilidad óptima | Códigos esperados |
|---|---:|---:|---|
| El voraz se equivoca | 100 | 440 | U2, U3 |
| Cuatro útiles | 16 | 20 | U2, U4 |
| Presupuesto cero | 0 | 0 | ninguno |
| Sin útiles | 30 | 0 | ninguno |
| Nada cabe | 60 | 0 | ninguno |
| Entran todos | 200 | 52 | U1, U2, U3 |

En el programa ejecuté estos seis casos tanto con CompradorIngenuo como con CompradorMejorado. También añadí dos comprobaciones de pertenencia: después de elegir los útiles del primer caso, compruebo que U2 sí esté en la compra y que U1 no esté. Así reviso tanto la utilidad y los códigos elegidos como el funcionamiento de EstaEnLaCompra.

## 7. Qué pasaría con un presupuesto de 1.000.000.000 Bs

También analicé qué ocurriría si el presupuesto aumentara a **1.000.000.000 Bs**. En mi implementación no creo una tabla con mil millones de columnas, porque primero calculo la capacidad efectiva B'. Si el presupuesto alcanza para cubrir la suma de los costos de todos los útiles con utilidad positiva, mi programa los selecciona directamente en O(n), sin ejecutar la tabla dinámica.

En el lote de 1.000 útiles que genero para las pruebas, cada costo está entre 4 y 49 Bs. Por lo tanto, el costo total no puede superar 49.000 Bs. Con un presupuesto de 1.000.000.000 Bs, todos esos útiles con utilidad positiva cabrían y mi solución utilizaría la salida rápida de O(n).

Si trabajara con otro conjunto donde la suma de costos fuera mayor que el presupuesto, la programación dinámica seguiría dependiendo de B'. En ese caso, un presupuesto muy grande sí podría aumentar bastante el consumo de memoria y el tiempo. Para trabajar con importes mucho mayores tendría que analizar otra alternativa, como programación dinámica por utilidad o un algoritmo aproximado, dependiendo de los límites del problema y de si necesito una respuesta exacta.
