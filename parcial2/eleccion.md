Estudiante: Xander Boris Pinto Montes

1\. Estructuras que elegí

Para resolver el sistema de turnos decidí utilizar cinco colas FIFO (Queue), una para cada nivel de urgencia, y un HashSet para guardar los nombres de los pacientes que todavía están esperando.

Elegí las colas porque el sistema debe atender primero a los pacientes con mayor urgencia. Además, cuando dos pacientes tienen la misma urgencia, se debe respetar el orden en que llegaron. Una cola cumple justamente esa regla: el primero que entra es el primero que sale.

El HashSet lo utilizo para consultar rápidamente si un paciente sigue esperando. También me permite mantener actualizado el conjunto de nombres cuando se registra o se atiende a una persona.

2\. Cómo trabaja la solución

Cuando registro un paciente, reviso que su urgencia esté entre 1 y 5. Después lo agrego al final de la cola que corresponde a su urgencia y guardo su nombre en el HashSet.

Para atender al siguiente paciente, reviso las colas desde la urgencia 1 hasta la 5. La primera cola que tenga pacientes me indica a quién debo atender. Retiro al primero de esa cola y elimino su nombre del HashSet. De esta manera se respeta tanto la prioridad como el orden de llegada dentro de cada prioridad.

Para saber si un paciente está esperando, consulto el HashSet. Para obtener la cantidad de pacientes en espera, utilizo el contador de elementos del conjunto.

3\. Complejidad de las operaciones

La operación Registrar tiene un costo O(1) amortizado por el ingreso a la cola y O(1) promedio por la consulta e inserción en el HashSet. Por eso, su costo se considera O(1) promedio.

La operación AtenderSiguiente revisa como máximo cinco colas. Como la cantidad de niveles de urgencia está fija en este ejercicio, esa revisión no aumenta cuando hay más pacientes. Retirar al paciente de la cola y quitar su nombre del HashSet también tiene costo constante amortizado o promedio. Por eso, esta operación es O(1) para este caso.

La operación EstaEsperando utiliza HashSet.Contains, que normalmente tiene costo O(1) promedio. La propiedad Esperando utiliza Count, que tiene costo O(1).

En cuanto al espacio, las estructuras guardan los pacientes que todavía no fueron atendidos y sus nombres. Por lo tanto, el espacio utilizado crece de forma lineal con la cantidad de pacientes en espera: O(n).

4\. Comparación con la solución ingenua

La solución ingenua utiliza una sola lista. Cada vez que debe atender a alguien, recorre la lista completa para encontrar al paciente con mayor urgencia y, en caso de empate, al que llegó primero. Si primero se registran 100.000 pacientes y después se atienden todos, la cantidad de comparaciones para buscar al siguiente paciente puede llegar a: 99.999 + 99.998 + ... + 1. Esto equivale a 100.000 × 99.999 / 2, es decir, aproximadamente 4.999.950.000 comparaciones. Además, eliminar un elemento de una posición intermedia de la lista puede requerir mover otros elementos.

La búsqueda EstaEsperando de la solución ingenua también recorre la lista. En el peor caso, cada consulta puede revisar los 100.000 pacientes. Si se realizan 30.000 consultas mientras la lista conserva ese tamaño, se podrían hacer hasta 3.000.000.000 de comprobaciones adicionales. Esta cantidad es un escenario de peor caso; el número real depende de cuándo se hagan las consultas y de dónde se encuentre el nombre buscado.

Con mi solución mejorada, cada atención revisa como máximo cinco colas, sin importar si hay cien o cien mil pacientes. Para 100.000 atenciones, eso representa como máximo 500.000 comprobaciones de colas, además de retirar cada paciente y actualizar el HashSet. Las 30.000 consultas de existencia se resuelven con búsquedas de costo O(1) promedio.

5\. Conclusión

Considero que las cinco colas y el HashSet son adecuados para este problema porque las prioridades son solamente cinco y no cambian. Con estas estructuras puedo respetar las reglas de atención y reducir bastante el trabajo frente a la lista de la solución ingenua. Si en otro sistema hubiera muchos niveles de prioridad que pudieran cambiar, consideraría utilizar una cola de prioridad en lugar de mantener cinco colas separadas.

