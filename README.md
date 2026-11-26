# Análisis de Algoritmos — Caso de matriculación

**Estudiante:** Xander Boris Pinto Montes  
**Repositorio:** [matriculacion-algoritmos-pinto](https://github.com/xanderpinto-oss/matriculacion-algoritmos-pinto)

## Descripción del caso

En este trabajo tomo como referencia el proceso de matriculación de una institución de educación técnica. La población de referencia es de **559 estudiantes**, distribuidos en cinco carreras:

- Mecánica Automotriz
- Sistemas Informáticos
- Electromecánica
- Electrónica
- Mecánica Industrial

El objetivo es aplicar conceptos de análisis de algoritmos, comparar alternativas y justificar las decisiones considerando la complejidad teórica y las mediciones obtenidas.

## Índice del repositorio

| Carpeta | Contenido |
|---|---|
| [h1/](h1/) | Descripción del proceso de matriculación y muestra de datos. |
| [h2/](h2/) | Inventario, comparación de estructuras de datos, implementación y mediciones de lista simple frente a árbol AVL. |
| [practicas/](practicas/) | Prácticas C4, C5 y C6. Incluye un README propio con el índice de esa sección. |
| [parcial1/](parcial1/) | Análisis, elección de algoritmo y mediciones del primer parcial. |
| [parcial2/](parcial2/) | Elección de estructuras, implementación y mediciones del segundo parcial. |
| [integradora/](integradora/) | Análisis, código y mediciones del problema integrador de selección de útiles escolares con presupuesto limitado. |

## H1 — Proceso de matriculación

- [Descripción del proceso](h1/proceso.md)
- [Muestra de matrículas en CSV](h1/muestra_matriculas_ejemplo.csv)

La muestra contiene 20 filas demostrativas, cuatro por carrera. **No son registros reales de la institución.** La cifra de 559 estudiantes es la población de referencia del caso, no el tamaño de la muestra CSV. Algunos campos operativos, como la fecha de solicitud y el tiempo de atención, están pendientes de confirmación o medición. Por eso no presento esos valores como datos institucionales comprobados.

## H2 — Estructuras de datos

- [Inventario de operaciones](h2/inventario.md)
- [Descripción de las estructuras](h2/estructura.md)
- [Protocolo y resultados de medición](h2/medicion.md)
- [Código en C#](h2/Program.cs)
- [Proyecto .NET](h2/H2.csproj)

Comparé una lista simple con un árbol AVL. En las pruebas, con 4.472 registros, la lista obtuvo un promedio de **93,219 ms** y el AVL **2,258 ms**. La diferencia observada favoreció al AVL para las operaciones de búsqueda y actualización consideradas. Los tiempos dependen del equipo y de las condiciones de ejecución; la complejidad se justifica además por la estructura de cada algoritmo.

## Prácticas

- [Índice de prácticas](practicas/README.md)
- [C4 — Búsqueda](practicas/c4/)
- [C5 — Ordenamiento](practicas/c5/)
- [C6 — Elección de algoritmo](practicas/c6/)

En estas prácticas trabajé comparaciones entre búsqueda lineal y binaria, algoritmos de ordenamiento y la justificación de una elección según su complejidad y comportamiento medido.

## Parciales

- [Parcial 1](parcial1/)
- [Parcial 2](parcial2/)

Estas carpetas contienen los análisis, elecciones, implementaciones y/o mediciones correspondientes a cada entrega.

## Trabajo integrador

- [Análisis](integradora/analisis.md)
- [Mediciones](integradora/medicion.md)
- [Código C#](integradora/examen-int-B.cs)
- [Proyecto .NET](integradora/Integradora.csproj)

El problema integrador consiste en seleccionar útiles escolares sin superar un presupuesto y maximizando la utilidad. La solución mejorada utiliza programación dinámica para el problema de mochila 0/1 y un HashSet para consultar si un artículo está seleccionado.

En la prueba con 1.000 artículos y presupuesto de 10.000 Bs, el promedio medido fue de **14,630 ms**, con una utilidad de **35.071**. Los resultados y el protocolo están desarrollados en el documento de medición.

## Cómo ejecutar los proyectos C#

Se necesita tener instalado el SDK de .NET. Desde PowerShell, entrar a la carpeta del proyecto y ejecutar:

```powershell
dotnet run -c Release
```

Por ejemplo, para H2:

```powershell
cd h2
dotnet run -c Release
```

Para la integradora:

```powershell
cd integradora
dotnet run -c Release
```

## Alcance y limitaciones

- Las mediciones corresponden a ejecuciones realizadas en mi computadora y pueden variar en otro equipo.
- Los tiempos medidos no sustituyen el análisis teórico de complejidad.
- La muestra de 20 matrículas es demostrativa y no debe confundirse con una extracción real de la institución.
- En el contenido publicado actualmente no aparece una carpeta identificada como H3. La carpeta `integradora/` corresponde al problema de mochila de útiles escolares y se presenta como trabajo integrador, no como H3.

## Uso de herramientas de apoyo

Utilicé ChatGPT como apoyo para comprender enunciados, revisar conceptos de complejidad, organizar la documentación y recibir explicaciones durante el desarrollo. Las decisiones finales, la revisión del código y las pruebas que presento deben poder ser explicadas por mí; los tiempos indicados corresponden a las ejecuciones realizadas en mi equipo.
