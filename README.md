# Endpoint para almacenar datos y verificar si el día es feriado

Este proyecto implementa un endpoint que recibe el nombre de una persona, una fecha y almacena los datos en una base de datos utilizando SQL Server LocalDB y Entity Framework. También genera un ID autoincremental y verifica si el día ingresado corresponde a un feriado legal en Chile mediante el uso del patrón de diseño CQRS y los principios SOLID.

# Requisitos
Para ejecutar este proyecto, es necesario tener instalado lo siguiente:

.NET 5.0 SDK
SQL Server LocalDB
Visual Studio o Visual Studio Code

## Endpoints Disponibles
### Registrar Persona
Permite registrar información de una persona y almacenarla en la base de datos. También verifica si la fecha proporcionada corresponde a un feriado legal en Chile.

URL: POST /api/personas

#### Request Body:

```json
{
    "id": 1,
    "nombre": "Juan Perez",
    "fechaNacimiento": "1990-06-22"
}
```
#### Response:
```json
{
    "id": 1
}
```
En caso de error, se devuelve un código de error correspondiente.

### Consultar Persona
Permite obtener la información de una persona registrada en la base de datos.

URL: GET /api/personas/{id}

#### Response:
```json
{
    "id": 1,
    "nombre": "Juan Perez",
    "fechaNacimiento": "1990-06-22"
}
```

En caso de que el ID proporcionado no corresponda a una persona registrada, se devuelve un código de error correspondiente.

### Eliminar Persona
Permite eliminar la información de una persona registrada en la base de datos.

URL: DELETE /api/personas/{id}

Response:
```json
{
    "message": "La persona ha sido eliminada exitosamente."
}
```

### Verificación de Feriados
Para verificar si una fecha corresponde a un feriado legal en Chile, se debe consumir el siguiente servicio:

URL: https://apis.digital.gob.cl/fl/feriados/{año}/{mes}/{día}

Se debe reemplazar {año}, {mes} y {día} por los valores correspondientes a la fecha que se desea verificar.

El servicio devuelve un objeto JSON con la información del feriado correspondiente, si existe, o un objeto vacío si la fecha no es feriado.

### Patrones de Diseño y Principios
La API utiliza los patrones de diseño CQRS (Command Query Responsibility Segregation) y los principios SOLID (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion) para lograr una arquitectura modular y escalable.

### Comandos de Entity Framework
Para utilizar Entity Framework en la API, se deben ejecutar los siguientes comandos en la consola de administración de paquetes de Visual Studio:
```sh
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
```

