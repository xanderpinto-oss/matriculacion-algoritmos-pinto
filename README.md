# Caso de algoritmos — Sistema de matriculación

Repositorio de la práctica H1. El caso analiza el proceso de matriculación de una institución de educación técnica con una población informada de 559 estudiantes.

## Carreras consideradas

- Mecánica Automotriz
- Sistemas Informáticos
- Electromecánica
- Electrónica
- Mecánica Industrial

## Contenido

- `h1/proceso.md`: descripción del proceso en 15 líneas.
- `h1/muestra_matriculas_ejemplo.csv`: 20 filas de muestra, cuatro por carrera.

## Importante antes de entregar

La cifra de 559 estudiantes y los nombres de las cinco carreras proceden de la información proporcionada para esta práctica. Las 20 filas del CSV son demostrativas y usan códigos anónimos; no son registros reales extraídos de la institución. Para cumplir literalmente el requisito de datos reales, sustituye estas filas por 20 registros reales autorizados y anonimizados, usando el formato que maneja Secretaría.

También falta confirmar dos datos del proceso real:

1. El medio que se utiliza actualmente para matricular (papel, Excel u otro sistema).
2. El tiempo promedio real por matrícula. Mídelo con varias solicitudes y calcula el promedio; no uses un valor inventado.

El CSV propone 11 campos para estructurar el análisis. Confirma que esos campos coincidan con el formulario o registro vigente antes de presentar el archivo como formato verdadero.

## Publicar en GitHub (Windows / PowerShell)

1. Crea un repositorio público llamado `caso-algoritmos-pinto` en <https://github.com/new>.
2. No marques la opción de agregar otro README, porque este proyecto ya lo contiene.
3. Descomprime el ZIP y abre PowerShell dentro de la carpeta `caso-algoritmos-pinto`.
4. Ejecuta estos comandos, sustituyendo `TU-USUARIO` por tu usuario de GitHub:

```powershell
git init
git add .
git commit -m "Entrega H1: proceso de matriculacion"
git branch -M main
git remote add origin https://github.com/TU-USUARIO/caso-algoritmos-pinto.git
git push -u origin main
```

La fecha del commit quedará visible en el historial del repositorio, en la sección **Commits**.
