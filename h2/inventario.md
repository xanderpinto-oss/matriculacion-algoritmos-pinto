Xander Boris Pinto Montes

# H2 — Inventario del caso de matriculación

## 1. Datos del caso

El caso que continúo trabajando es el proceso de matriculación de estudiantes de una institución que ofrece carreras técnicas. En el H1 se indicó una población de 559 estudiantes y cinco carreras: Mecánica Automotriz, Sistemas Informáticos, Electromecánica, Electrónica y Mecánica Industrial.

La muestra disponible contiene 20 matrículas demostrativas, cuatro por carrera. No representa la distribución real de los 559 estudiantes. Cada matrícula maneja estos 11 campos: `id_matricula`, `codigo_estudiante`, `carrera`, `gestion`, `semestre`, `turno`, `fecha_solicitud`, `documentacion_completa`, `pago_confirmado`, `estado_matricula` y `tiempo_atencion_min`.

En la muestra, `gestion` aparece como `POR_CONFIRMAR`, `fecha_solicitud` como `FECHA_DE_EJEMPLO` y `tiempo_atencion_min` como `POR_MEDIR`. Por eso no los considero valores reales confirmados. Para organizar el registro usaré `id_matricula` como clave única.

## 2. Operaciones del sistema

| Operación | Cómo se aplica al caso | Frecuencia estimada |
|---|---|---|
| Buscar por clave | Consultar una matrícula por su `id_matricula` y verificar si ya existe antes de registrar o revisar un trámite. | Alta; es una consulta habitual durante el registro y revisión. |
| Insertar | Guardar una nueva solicitud de matrícula cuando se reciben los datos del estudiante. | Alta; ocurre por cada nueva matrícula. |
| Borrar | Retirar un registro cuando se detecta un duplicado o se autoriza corregir/anular una solicitud. | Baja; no es parte de cada matrícula. |
| Obtener el menor o el siguiente | Ubicar la primera matrícula por clave o avanzar a la siguiente para revisar registros en orden. | Baja; consulta administrativa. |
| Consultar un rango | Obtener matrículas cuyos identificadores estén entre dos claves, para revisar un segmento de registros. | Ocasional; útil en revisiones parciales. |
| Recorrer en orden | Listar las matrículas ordenadas por `id_matricula` para control y revisión. | Ocasional; se utiliza en listados o reportes. |

## 3. Operaciones que más se realizan

Por el flujo descrito en el H1, las operaciones principales son insertar nuevas matrículas y buscar registros para revisar la información y evitar duplicados. La eliminación es menos frecuente porque solo se realizaría ante una corrección o anulación autorizada. Las consultas de menor/siguiente, rango y recorrido ordenado son operaciones de apoyo para revisar y organizar los registros.

Las frecuencias anteriores son una clasificación cualitativa del flujo de trabajo; en el H1 no se proporcionaron conteos diarios reales para cada operación. La población de 559 estudiantes sí fue indicada, pero no debe confundirse con una medición de cuántas operaciones se hacen por día.
