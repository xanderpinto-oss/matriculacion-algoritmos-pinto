Xander Boris Pinto Montes

# H2 — Estructura de datos elegida

## 1. Estructura propuesta

Para almacenar las matrículas elijo un árbol AVL ordenado por `id_matricula`. Cada nodo guarda una matrícula y enlaces a sus hijos izquierdo y derecho. El árbol se mantiene equilibrado después de insertar o borrar, por lo que su altura se conserva en orden logarítmico.

Esta estructura se ajusta al caso porque las operaciones principales son buscar una matrícula por clave e insertar nuevos registros. También permite borrar, localizar el menor o el siguiente identificador, consultar un rango de claves y recorrer todos los registros en orden.

## 2. Costo de las operaciones

| Operación | Costo con AVL | Motivo |
|---|---:|---|
| Buscar por `id_matricula` | O(log n) | En cada paso se continúa por un solo subárbol. |
| Insertar | O(log n) | Se busca la posición y se actualiza el balance en el camino de regreso. |
| Borrar | O(log n) | Se localiza el nodo, se reorganiza si tiene dos hijos y se reequilibra el camino. |
| Menor | O(log n) | Se sigue la rama izquierda hasta el nodo más pequeño. |
| Siguiente | O(log n) | Se desciende por el árbol manteniendo el mejor candidato mayor que la clave. |
| Rango | O(log n + k) | Se llega a la primera clave del rango y se visitan los k resultados. |
| Recorrer en orden | O(n) | Se visita cada nodo una vez, en recorrido izquierdo-raíz-derecho. |

`n` representa la cantidad de matrículas almacenadas y `k` la cantidad de resultados que pertenecen al rango consultado. El espacio utilizado por el AVL es O(n).

## 3. Comparación de costos con la lista ingenua

| Operación | Lista simple | Explicación
|---|---:|---|
| Buscar por clave | O(n) | Puede revisar todos los registros. |
| Insertar (comprobando duplicado) | O(n) | Primero busca si la clave ya existe; el agregado al final es O(1) amortizado. |
| Borrar | O(n) | Busca la clave y desplaza elementos al quitarla. |
| Menor / siguiente | O(n) | Debe recorrer la lista para encontrar el candidato. |
| Rango ordenado | O(n + k log k) | Recorre la lista y ordena los k resultados encontrados. |
| Recorrer en orden | O(n log n) | Debe ordenar una copia antes de recorrerla. |

## 4. Por qué elijo AVL y no una lista

Una lista simple es fácil de implementar, pero buscar una matrícula por clave, verificar duplicados o borrar un registro requiere recorrer elementos y cuesta O(n). Además, no mantiene los registros ordenados: para obtener el listado en orden hay que ordenar una copia, con costo O(n log n).

El AVL mantiene las claves ordenadas y balanceadas. Así, las búsquedas, inserciones y eliminaciones cuestan O(log n), y las consultas por rango y recorridos ordenados se pueden resolver aprovechando el orden del árbol. Elegí AVL en vez de un árbol binario de búsqueda sin balance porque este último puede convertirse en una cadena y llegar a O(n) si las claves llegan ordenadas.

## 5. Alcance de la implementación

El código compara dos soluciones para el mismo registro de matrícula: `RegistroLista`, que usa una lista sin orden, y `RegistroAVL`, que usa el árbol balanceado. La clave de ambas implementaciones es `id_matricula`. Los registros generados para la medición son sintéticos y conservan los campos y categorías de la muestra H1; no son datos personales ni registros reales de la institución.
