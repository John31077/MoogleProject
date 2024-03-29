Proyecto de Programación I.
> Facultad de Matemática y Computación - Universidad de La Habana.
> Cursos 2021, 2022.
> Johnangel Crespo Leal, C112.
****************************************************************************************************************************************************************************
Moogle! es una aplicación *totalmente original* cuyo propósito es buscar inteligentemente un texto en un conjunto de documentos.
Es una aplicación web, desarrollada con tecnología .NET C+ore 6.0, específicamente usando Blazor como *framework* web para la interfaz gráfica, y en el lenguaje C#.
****************************************************************************************************************************************************************************
Dato: El contenido a buscar debe de ser documentos .txt que se encuentren en la carpeta Content.(No recomiendo buscar sin documento alguno)
**************************
*  INSTRUCCIONES DE USO  *
**************************
****************************************************************************************************************************************************************************
1- Para abrir el proyecto se tiene que entrar a la consola de Visual Code y escribir el siguiente código en dependencia del sistema operativo:

     EN WINDOWS:
  - "dotnet watch run --project MoogleServer" (sin el - delante y sin las comillas)
     
     EN LINUX:
  - "make dev" (sin las comillas)


 Una vez abierto el proyecto, deberá de aparecer una pantalla color blanco y un logo en la parte centro-superior que dirá Moogle!,
   a su vez debajo del logo habrá un rectángulo que dirá "Introduzca su Búsqueda" y al derecha otro rectángulo más pequeño en azul que dirá buscar.


2- En el rectángulo que dice "Introduzca su Búsqueda" haga literalmente lo que dice, escribe alguna palabra o frase a buscar (preferiblemente algo coherente aunque también puedes
   insertar algo como ",..,#"$$"#$:;:#"$:" pero no esperes grandes cantidades de resultados).



3- Una vez insertado lo que desea buscar pulse el botón azul que aparece en la ezquina derecha y deberá de aparecer en el borde izquierdo de la interfaz los resultados de su 
   búsqueda (siempre que su búsqueda coincida con algún documento). Estos resultados se muestran de la siguiente forma:
   
   .(Título)
   ...(fragmento de texto que coincide con alguna parte de su búsqueda)

   Información extra: Si no se introduce ninguna consulta el buscador te informará que no has insertado ninguna consulta.
                      Si se utiliza el proyecto en Linux existe la posibilidad de que la dirección de la carpeta content se tenga que cambiar manualmente.
***************************************************************************************************************************************************************************************
*  DATOS EXTRAS A TENER EN CUENTA  *
************************************

El buscador, al menos por ahora, solo hace lo que se ha mencionado anteriormente pero en un futuro cercano podría agregarse algunas mejoras como son las siguientes:
- Un sistema de sugerencias por si el usuario escribe una palabra "mal", se le sugiera una palabra más coherente acorde a los documentos.
- Operadores para hacer la búsqueda más inteligente y precisa.
 

 