'use strict';
// Taller C5: Torneo de algoritmos de ordenamiento
// Ejecutar desde esta carpeta: node torneo.js

const fs = require('node:fs');
const path = require('node:path');
const os = require('node:os');
const { performance } = require('node:perf_hooks');

const SIZES = [10000, 20000, 40000, 80000];
const RUNS = 3;
const SEED = 20261112;
const CSV = path.join(__dirname, 'resultados_c5.csv');

function createData(n, seed = SEED) {
  let state = (seed + n) >>> 0;
  const a = new Array(n);
  for (let i = 0; i < n; i++) {
    state = (Math.imul(1664525, state) + 1013904223) >>> 0;
    a[i] = state % 1000000;
  }
  return a;
}

// Competidor 1: Insertion Sort - O(n^2) promedio y peor caso.
function insertionSort(a) {
  for (let i = 1; i < a.length; i++) {
    const value = a[i];
    let j = i - 1;
    while (j >= 0 && a[j] > value) {
      a[j + 1] = a[j];
      j--;
    }
    a[j + 1] = value;
  }
  return a;
}

// Competidor 2: Merge Sort - O(n log n).
function mergeSort(a) {
  const aux = new Array(a.length);
  function divideMerge(start, end) {
    if (end - start <= 1) return;
    const mid = Math.floor((start + end) / 2);
    divideMerge(start, mid);
    divideMerge(mid, end);
    if (a[mid - 1] <= a[mid]) return;

    let i = start, j = mid, k = start;
    while (i < mid && j < end) {
      aux[k++] = a[i] <= a[j] ? a[i++] : a[j++];
    }
    while (i < mid) aux[k++] = a[i++];
    while (j < end) aux[k++] = a[j++];
    for (let p = start; p < end; p++) a[p] = aux[p];
  }
  divideMerge(0, a.length);
  return a;
}

// Competidor 3: método nativo de JavaScript.
function nativeSort(a) {
  return a.sort((x, y) => x - y);
}

const algorithms = [
  { name: 'Insertion Sort', sort: insertionSort },
  { name: 'Merge Sort', sort: mergeSort },
  { name: 'Array.sort()', sort: nativeSort },
];

function isSorted(a) {
  for (let i = 1; i < a.length; i++) {
    if (a[i - 1] > a[i]) return false;
  }
  return true;
}

function appendCsv(row) {
  fs.appendFileSync(CSV, row + '\n', 'utf8');
}

function warmUp() {
  console.log('\nCALENTAMIENTO: dos corridas de 2.000 elementos por algoritmo');
  for (const alg of algorithms) {
    for (let r = 0; r < 2; r++) {
      const sample = createData(2000, SEED + r);
      alg.sort(sample);
      if (!isSorted(sample)) throw new Error('Fallo en calentamiento: ' + alg.name);
    }
    console.log('  ' + alg.name + ': correcto');
  }
}

function showMachine() {
  console.log('\n===== EQUIPO =====');
  console.log('Sistema: ' + os.platform() + ' ' + os.release());
  console.log('Arquitectura: ' + os.arch());
  console.log('Procesador: ' + (os.cpus()[0]?.model || 'No identificado'));
  console.log('RAM: ' + (os.totalmem() / 1024 ** 3).toFixed(2) + ' GB');
  console.log('Node.js: ' + process.version);
}

function runTournament() {
  showMachine();
  fs.writeFileSync(CSV, 'cantidad,algoritmo,corrida,tiempo_ms,validacion\n', 'utf8');
  warmUp();

  const summary = [];
  for (const n of SIZES) {
    console.log('\n===== PRUEBA CON ' + n + ' ELEMENTOS =====');
    const original = createData(n);
    const row = { Cantidad: n };

    for (const alg of algorithms) {
      const times = [];
      for (let r = 1; r <= RUNS; r++) {
        console.log(alg.name + ': corrida ' + r + '/' + RUNS);
        // La copia se realiza antes del cronómetro.
        const copy = original.slice();
        const start = performance.now();
        alg.sort(copy);
        const elapsed = performance.now() - start;

        if (!isSorted(copy)) {
          throw new Error(alg.name + ' no ordenó correctamente ' + n + ' elementos.');
        }

        times.push(elapsed);
        appendCsv(n + ',' + alg.name + ',' + r + ',' + elapsed.toFixed(3) + ',SI');
        console.log('  ' + elapsed.toFixed(3) + ' ms | verificación: OK');
      }

      const average = times.reduce((sum, value) => sum + value, 0) / RUNS;
      row[alg.name + ' (ms)'] = average.toFixed(3);
      appendCsv(n + ',' + alg.name + ',PROMEDIO,' + average.toFixed(3) + ',SI');
      console.log('  Promedio: ' + average.toFixed(3) + ' ms');
    }

    summary.push(row);
  }

  console.log('\n===== PROMEDIOS EN MILISEGUNDOS =====');
  console.table(summary);
  console.log('\nResultados guardados en: ' + CSV);
  console.log('Todas las verificaciones de orden fueron correctas.');
}

try {
  runTournament();
} catch (error) {
  console.error('\nTORNEO DETENIDO: ' + error.message);
  console.error('El CSV conserva las mediciones completadas.');
  process.exitCode = 1;
}
