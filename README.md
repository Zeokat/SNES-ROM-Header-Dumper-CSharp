SNES ROM Header Dumper C#
=========================

Es un código muy sencillo que nos permite leer los headers o cabeceras de las ROMS de Super Nintendo y obtener la información del cartucho.

Hay dos tipos de cabeceras una que se conoce como cabecera smc, que no la tienen todos las roms y añade información extra. Esta cabecera se encuentra al inicio de los datos de la ROM y consiste en 512 bytes extra.
La otra cabecera es la propia de los cartuchos y consiste en 64 bytes con datos como la región, vectores de interrupción, versión, checksum, etc...

Las ROMS que no poseen el header o cabecera smc se las denomina "headerless ROM". La cabecera smc no es propia de los dumps originales ya que es información extra que añaden los dispositivos de copia de cartuchos de SNES.

El código está diseñado con dumps de roms de SNES con extensión smc y sfc.

Por el momento esto es todo, el código es bastante descriptivo y quizás más adelante programa algún tipo de interfaz gráfica para este código que nos permita obtener la información de la ROM con un par de clicks.

Mientras tanto seguiré jugando con algún [emulador de Super Nintendo](http://www.vozidea.com/mejores-emuladores-de-super-nintendo-para-pc) a mis juegos favoritos de SNES. El código es de libre distribución y uso.

La documentación en español acerca de las roms de Super Nintendo es más bien escasa o inexistente, por lo que en un futuro trataré de ir agregando informaión a mi [blog personal](http://www.vozidea.com/).
