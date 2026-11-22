P2.2 / P2.3 — MEDICIÓN DE RENDIMIENTO
Estudante: Xander Boris Pinto Montes

1. Objetivo
Comparar el tiempo de ejecución de la solución original GestorIngenuo, que utiliza una lista, con la solución GestorMejorado, que utiliza cinco colas FIFO y un HashSet. La comparación se realizó con diferentes cantidades de pacientes y con las mismas operaciones para ambos gestores.
2. Procedimiento de medición
Para cada tamaño de prueba se ejecutó la siguiente carga de trabajo:
Registrar todos los pacientes, asignándoles nombres P0, P1, P2 y así sucesivamente, y niveles de urgencia entre 1 y 5.
Realizar 30.000 consultas mediante EstaEsperando mientras los pacientes todavía se encuentran en espera.
Atender a todos los pacientes registrados.
Se probaron 5.000, 10.000 y 20.000 pacientes. Para cada gestor y cada tamaño se hizo una ejecución inicial de calentamiento que no se incluyó en los resultados y después tres ejecuciones medidas con Stopwatch. El promedio corresponde a la suma de los tres tiempos dividida entre tres.
La columna Control mostró el mismo resultado en las tres repeticiones de cada tamaño: 53.890 para 5.000 pacientes, 78.890 para 10.000 y 138.890 para 20.000. Esto permite comprobar que las cargas terminaron correctamente y produjeron el mismo valor de control en ambos gestores.
3. Resultados obtenidos
Los tiempos están expresados en milisegundos (ms).
Pacientes	Gestor	Medición 1	Medición 2	Medición 3	Promedio
5.000	Ingenuo	204,348	233,294	200,235	212,626
5.000	Mejorado	1,867	1,883	2,409	2,053
10.000	Ingenuo	482,037	452,393	485,315	473,248
10.000	Mejorado	4,193	2,374	1,704	2,757
20.000	Ingenuo	837,193	802,614	785,317	808,375
20.000	Mejorado	4,057	2,808	7,612	4,826
4. Comparación de promedios
Pacientes	GestorIngenuo	GestorMejorado	Relación de tiempos
5.000	212,626 ms	2,053 ms	103,57 veces
10.000	473,248 ms	2,757 ms	171,65 veces
20.000	808,375 ms	4,826 ms	167,50 veces

La relación se calculó dividiendo el promedio de GestorIngenuo entre el promedio de GestorMejorado. Por ejemplo, para 5.000 pacientes: 212,626 / 2,053 = aproximadamente 103,57.
5. Análisis
En las tres cantidades probadas, GestorMejorado terminó la carga en menos tiempo que GestorIngenuo. La diferencia fue especialmente clara al aumentar la cantidad de pacientes: el gestor original necesita recorrer la lista para localizar al paciente más urgente y también para comprobar si un nombre está esperando. Estas operaciones pueden recorrer una parte grande de la lista.
En cambio, GestorMejorado distribuye los pacientes en cinco colas, una por nivel de urgencia. Como existen solamente cinco niveles, revisar las colas para encontrar la primera no vacía requiere como máximo cinco comprobaciones. El HashSet permite consultar si un paciente está esperando en tiempo promedio constante.
Los tiempos individuales de GestorMejorado presentan variaciones, sobre todo porque son muy cortos y pueden verse afectados por las tareas del sistema, la memoria y el funcionamiento del entorno .NET. Por eso se utilizaron tres repeticiones y se compararon los promedios, en lugar de sacar conclusiones a partir de una sola ejecución.
6. Conclusiones
Los dos gestores superaron las pruebas funcionales: 11 de 11 pasos correctos en cada uno.
Con la carga que incluye registros, 30.000 consultas y atenciones, GestorMejorado fue entre aproximadamente 103,57 y 171,65 veces más rápido en los tamaños medidos.
La mejora se debe a que las colas FIFO separan los niveles de urgencia y el HashSet evita recorrer toda la lista para consultar si un paciente está esperando.
Los resultados corresponden a esta computadora y a las cantidades probadas. No se debe asumir que los mismos tiempos se obtendrán en otros equipos.
La prueba más grande realizada fue de 20.000 pacientes; no se midió todavía una carga de 100.000 pacientes.											
