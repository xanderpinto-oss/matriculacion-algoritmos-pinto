'use strict';
const fs = require('node:fs');
const os = require('node:os');
const path = require('node:path');
const { performance } = require('node:perf_hooks');
const N = 559, RUNS = 3, ROUNDS = 100, SEED = 15112026;
const OUT = path.join(__dirname, 'resultados_c4.csv');

function makeStudents() {
  const careers = ['Mecánica Automotriz','Sistemas Informáticos','Electromecánica','Electrónica','Mecánica Industrial'];
  const a = Array.from({length:N}, (_,i) => ({codigo:'EST-'+String(i+1).padStart(4,'0'), carrera:careers[i%5], estado:i%7===0?'PENDIENTE':'REGISTRADO'}));
  let s = SEED >>> 0;
  for (let i=a.length-1;i>0;i--) { s=(Math.imul(1664525,s)+1013904223)>>>0; const j=s%(i+1); [a[i],a[j]]=[a[j],a[i]]; }
  return a;
}
function linearSearch(a,key) { for (let i=0;i<a.length;i++) if (a[i].codigo===key) return a[i]; return null; }
function binarySearch(a,key) { let lo=0,hi=a.length-1; while(lo<=hi){const m=Math.floor((lo+hi)/2),v=a[m].codigo;if(v===key)return a[m];if(v<key)lo=m+1;else hi=m-1;}return null; }
function mergeSortRecords(data) {
  const a=data.slice(), aux=new Array(data.length);
  function split(lo,hi){if(hi-lo<=1)return;const mid=Math.floor((lo+hi)/2);split(lo,mid);split(mid,hi);let i=lo,j=mid,k=lo;while(i<mid&&j<hi)aux[k++]=a[i].codigo<=a[j].codigo?a[i++]:a[j++];while(i<mid)aux[k++]=a[i++];while(j<hi)aux[k++]=a[j++];for(let p=lo;p<hi;p++)a[p]=aux[p];}
  split(0,a.length);return a;
}
function avg(a){return a.reduce((s,x)=>s+x,0)/a.length;}
function measure(fn,data,keys){let check=0;const start=performance.now();for(let r=0;r<ROUNDS;r++)for(const key of keys){const found=fn(data,key);if(found)check+=found.codigo.length;}const ms=performance.now()-start;const total=ROUNDS*keys.length;if(check!==total*8)throw Error('Búsqueda incorrecta');return ms*1000/total;}
function main(){
  const original=makeStudents(), keys=original.map(x=>x.codigo);
  let sorted=mergeSortRecords(original);for(let i=0;i<4;i++)sorted=mergeSortRecords(original);
  const sortTimes=[];for(let i=0;i<RUNS;i++){const t=performance.now();sorted=mergeSortRecords(original);sortTimes.push(performance.now()-t);}const sortMs=avg(sortTimes);
  if(!keys.every(k=>linearSearch(original,k)&&binarySearch(sorted,k)))throw Error('Falló validación inicial');
  for(let r=0;r<10;r++)for(const k of keys){linearSearch(original,k);binarySearch(sorted,k);}
  const lineal=[],binaria=[],rows=[];
  for(let r=1;r<=RUNS;r++){const l=measure(linearSearch,original,keys),b=measure(binarySearch,sorted,keys);lineal.push(l);binaria.push(b);rows.push('559,Búsqueda lineal,'+r+','+l.toFixed(6)+',SI');rows.push('559,Búsqueda binaria,'+r+','+b.toFixed(6)+',SI');}
  const al=avg(lineal),ab=avg(binaria),ratio=al/ab,breakEven=Math.ceil(sortMs*1000/(al-ab));
  fs.writeFileSync(OUT,'registros,algoritmo,corrida,tiempo_us_por_busqueda,validacion\n'+rows.join('\n')+'\n559,Búsqueda lineal,PROMEDIO,'+al.toFixed(6)+',SI\n559,Búsqueda binaria,PROMEDIO,'+ab.toFixed(6)+',SI\n','utf8');
  console.log(JSON.stringify({registros:N,consultas_por_corrida:ROUNDS*N,corridas:RUNS,promedios_us:{lineal:al,binaria:ab},cociente_lineal_sobre_binaria:ratio,preparacion_ordenamiento_ms:sortMs,consultas_para_amortizar_ordenamiento:breakEven,entorno:{plataforma:os.platform(),version:os.release(),arquitectura:os.arch(),cpu:os.cpus()[0]?.model,nucleos:os.cpus().length,node:process.version}},null,2));
}
main();