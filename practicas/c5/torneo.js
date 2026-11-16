'use strict';
const fs=require('node:fs');
const os=require('node:os');
const path=require('node:path');
const {performance}=require('node:perf_hooks');
const SIZES=[559,1118,2236,4472], RUNS=3, REPEATS=30, SEED=20261115;
const OUT=path.join(__dirname,'resultados_c5.csv');
function createData(n){let s=(SEED+n)>>>0;return Array.from({length:n},()=>{s=(Math.imul(1664525,s)+1013904223)>>>0;return s%1000000;});}
function insertionSort(a){for(let i=1;i<a.length;i++){const v=a[i];let j=i-1;while(j>=0&&a[j]>v){a[j+1]=a[j];j--;}a[j+1]=v;}return a;}
function mergeSort(a){const n=a.length;let src=a,dst=new Array(n);for(let w=1;w<n;w*=2){for(let l=0;l<n;l+=2*w){const m=Math.min(l+w,n),r=Math.min(l+2*w,n);let i=l,j=m,k=l;while(i<m&&j<r)dst[k++]=src[i]<=src[j]?src[i++]:src[j++];while(i<m)dst[k++]=src[i++];while(j<r)dst[k++]=src[j++];} [src,dst]=[dst,src];}if(src!==a)for(let i=0;i<n;i++)a[i]=src[i];return a;}
function quickSort(a){const stack=[[0,a.length-1]];while(stack.length){let [lo,hi]=stack.pop();while(lo<hi){let i=lo,j=hi;const p=a[Math.floor((lo+hi)/2)];while(i<=j){while(a[i]<p)i++;while(a[j]>p)j--;if(i<=j){[a[i],a[j]]=[a[j],a[i]];i++;j--;}}if(j-lo<hi-i){if(i<hi)stack.push([i,hi]);hi=j;}else{if(lo<j)stack.push([lo,j]);lo=i;}}}return a;}
function isSorted(a){for(let i=1;i<a.length;i++)if(a[i-1]>a[i])return false;return true;}
function avg(a){return a.reduce((s,x)=>s+x,0)/a.length;}
const algorithms=[['Insertion Sort',insertionSort],['MergeSort',mergeSort],['QuickSort',quickSort]];
function main(){const csv=['cantidad,algoritmo,corrida,tiempo_ms,validacion'],table=[];for(const n of SIZES){const original=createData(n),row={Cantidad:n};for(const [name,sort] of algorithms){for(let w=0;w<10;w++){const sample=original.slice();sort(sample);if(!isSorted(sample))throw Error('Calentamiento incorrecto: '+name);}const times=[];for(let run=1;run<=RUNS;run++){const copies=Array.from({length:REPEATS},()=>original.slice());const start=performance.now();for(const copy of copies)sort(copy);const elapsed=(performance.now()-start)/REPEATS;if(!copies.every(isSorted))throw Error('Ordenamiento incorrecto: '+name);times.push(elapsed);csv.push(n+','+name+','+run+','+elapsed.toFixed(6)+',SI');}const m=avg(times);row[name+' (ms)']=Number(m.toFixed(6));csv.push(n+','+name+',PROMEDIO,'+m.toFixed(6)+',SI');}table.push(row);}fs.writeFileSync(OUT,csv.join('\n')+'\n','utf8');console.log(JSON.stringify({entorno:{plataforma:os.platform()+' '+os.release(),arquitectura:os.arch(),cpu:os.cpus()[0]?.model,nucleos_logicos:os.cpus().length,node:process.version},corridas:RUNS,ordenamientos_por_corrida:REPEATS,cantidades:SIZES,resultados:table,csv:OUT},null,2));}
main();