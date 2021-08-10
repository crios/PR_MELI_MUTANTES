# PR_MELI_MUTANTES


[[_TOC_]]

## INTRODUCCIÓN

Mutantes, es el proyecto que permite detectar si un humano es mutante basándose en su secuencia de ADN.


## DATOS DE ENTRADA

como parámetro un array de Strings que representan cada fila de una tabla de (NxN) con la secuencia del ADN. Las letras de los Strings solo pueden ser: (A,T,C,G), las
cuales representa cada base nitrogenada del ADN.

Ejemplo (Caso mutante):
String[] dna = {"ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"};

## API

El API se encuentra aloja en AZURE, para acceder a ella se debe usar la siguiente url:
 **http://mutantes.azurewebsites.net/**



## Servicios

El proyecto lo componen dos servicios:


- **/api/Mutant/stats**: Permite consulta las estadisticas el proyecto
Verbo: GET
No recibe parámetros.


- **/api/Mutant**: Permite verrificar si un humano es mutante:
Verbo: POST
Recibe un json con la siguiente estructura:
```{
    "dna":
    [
        "ATGCGA",
        "CAGTGC",
        "TTATGT",
        "AGAAGG",
        "CCCCTA",
        "TCACTG"
    ]
}


