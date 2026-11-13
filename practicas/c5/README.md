# Taller C5 - Torneo de algoritmos

## Objetivo
Comparar Insertion Sort (O(n²)), Merge Sort (O(n log n)) y el método nativo `Array.sort()` de JavaScript.

## Requisitos
Tener Node.js instalado. Comprueba la instalación con:

```powershell
node --version
```

## Ejecución
Abre PowerShell dentro de esta carpeta y ejecuta:

```powershell
node torneo.js
```

## Protocolo implementado
1. Muestra sistema operativo, procesador, memoria RAM y versión de Node.js.
2. Calienta cada algoritmo con dos arreglos de 2.000 elementos.
3. Prueba arreglos de 10.000, 20.000, 40.000 y 80.000 elementos.
4. Genera datos pseudoaleatorios reproducibles; en cada tamaño los tres algoritmos reciben los mismos valores.
5. Realiza tres corridas por algoritmo y tamaño.
6. Copia el arreglo antes de iniciar el cronómetro, para medir solamente el ordenamiento.
7. Verifica que cada resultado esté ordenado antes de aceptar la medición.
8. Calcula los promedios y guarda todas las corridas en `resultados_c5.csv`.

## Competidores
- **Insertion Sort:** O(n²) en promedio y peor caso.
- **Merge Sort:** O(n log n).
- **Array.sort():** se mide la implementación disponible en la versión del motor JavaScript de Node.js instalada.

## Archivos generados
Al ejecutar el programa se crea `resultados_c5.csv`, con columnas para cantidad, algoritmo, corrida, tiempo en milisegundos y validación.

## Advertencia
Insertion Sort con 80.000 valores aleatorios puede tardar bastante más que los otros algoritmos. No cierres la terminal mientras trabaja. Si lo interrumpes con Ctrl+C, el CSV conserva las mediciones ya registradas.

Los tiempos dependen del equipo y deben obtenerse ejecutando el programa en la computadora donde se realiza la práctica. No se deben inventar resultados.
