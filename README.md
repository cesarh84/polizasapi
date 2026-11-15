# polizasapi
test de polizas api

## PASOS PARA INSTALACION DE BASE DE DATOS
1. Abrir el proyecto de visual studio
2. Abrir la consola de administrador de paquetes
3. Ejecutar el comando "Add-Migration Tablas" para crear el archivo que generara toda la estructura de las tablas que se requieren para el sistema.
4. Ejecutar el commando "Update-Database" para ejecutar y crear todo en la base de datos

## CONSIDERACIONES IMPORTANTES ANTES DE LA INSTALACION DE LA BASE DE DATOS
1. Se tiene que tener configurado correctamente la conexion a la base datos, esto se puede ajustar en el archivo "appSettings.Development.json", SE DEBERA DE CAMBIAR SOLAMENTE LOS VALORES DE LA PROPIEDAD LLAMADA: DEFAULTCONNECTION".

## PASOS PARA EL USO O PRUEBA DE LOS ENDPOINT CON SWAGGER
1. Abrir el proyecto en visual studio
2. Ejecutar el proyecto
3. Una vez abierto en la ventana del navegador que abra, ahi solo agregar a la ruta que ya muestra lo siguiente: "swagger/index.html" y dar enter, y se abrira el portal de pruebas con swagger.
4. Se pueden probar todos los enpoints

