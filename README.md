# Arquitectura de referencia

## Objetivo del Documento

El objetivo de este documento es indicar las características principales de esta prueba de concepto de una arquitectura de referencia.

## Control de versiones
| Versión | Fecha | Autor   | Observaciones |
| ------- | ------- | ------- | ------------- |
| 0.1     | 23/10/2023 | David S | Versión inicial   |

## Índice

0. Prerequisitos
1. Un vistazo general
2. Capas transversales
3. Capa de dominio (XXX.Domain)
4. Capa de aplicación (XXX.Application)
5. Capa de infraestructura (XXX.Infrastructure)
6. Capa web - Web API (XXX.WebApi)
7. Capa web - Web (XXX.Web)
8. Capa task (XXX.WorkerService)

## 0. Prerequisitos
Será necesario que en local esté configurada la base de datos **[TiendaDb]**. Se anexan los Scripts para poder generar la base de datos (tanto esquema como carga de datos) 
En función de la versión de SQL Server, la cadena de conexión podría variar. Por ejemplo:
```json
"Server=DESKTOP-SRUHI3C\\SQLEXPRESS;Database=TiendaDb;Trusted_Connection=True;TrustServerCertificate=True;"
```
o:
```json
"Server=(LocalDb)\\MSSQLLocalDB;Database=TiendaDb;Integrated Security=True"
```

## 1. Un vistazo general

Algunas de las características de esta arquitectura son:
- **Arquitectura enfocada a dominio** ([Domain Driven Design - DDD](https://learn.microsoft.com/es-es/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)), con capas de:
    - Dominio: proyectos XXX.Domain
    - Aplicaci�n: proyectos XXX.Application
    - Infraestructura: proyectos XXX.Infrastructure
- **Proyectos transversales (1.0 - Cross cutting Layer)**, para reutilizaci�n de inyecci�n de dependencia y mensajes.
- **Acceso a base de datos**, mediante Entity Framework, aunque también hay una prueba de concepto con [Dapper](https://github.com/DapperLib/Dapper).
- **Testing**, todos los proyectos tendr�n su correspondiente proyecto de test. Se utilizan las librer�as [XUnit](https://xunit.net/), [Automapper](https://automapper.org/) y [Fluent Assertions](https://fluentassertions.com/) .
- **Mapping de entidades y dtos**: uso de [Automapper](https://automapper.org/). 

- **Logs**: uso de [Serilog](https://serilog.net/): 
    - Se escribe tanto en fichero como en [Graylog](https://graylog.org/) (Portalog).
    - La salida se hace en ficheros de logs diferenciados: los logs de nivel error o superior tiene su propia salida (esto es configurable en ```appsettings.json```)
- **Validaciones de datos de entidades y dtos**: se utiliza la librer�a [Fluent validation](https://docs.fluentvalidation.net/en/latest/).
- **Control de excepciones globales**: 
    - A trav�s de un middleware ```ExceptionHandlingMiddleware``` que envuelve las llamadas. 
    - Aplica un saneamiento de excepciones para no devolver Excepciones con el mensaje en crudo, mediante la interfaz ```IExceptionPolicy```. 
- **Uso del est�ndar Problem Details**: conocido como RFC 7807, es un est�ndar de la comunidad de desarrollo web que describe un formato com�n para informar y comunicar problemas o errores en servicios web y aplicaciones.
Esa estructura devolver� ```Type``` (Tipo), ```Title``` (T�tulo), ```Status``` (Estado, 400, 500...), ```Detail``` (Detalles).
- **Paquetes Nuget**: uso de [Uso de Central Package Management (CPM)](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management): 
permite que se centralicen las versiones de los paquetes nuget a trav�s del archivo ```Directory.Packages.props```.
- **Nullable reference types**: todos los proyectos est�n definidos para que no se realice la [verificaci�n de tipos nulables](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references), evitando warnings:
```xml
<Nullable>disable</Nullable>
```

## 2. Capas transversales (XXX.Shared)

**Definición**:
Proyectos que se pueden utilizar en varias capas diferentes. Podrían ser susceptibles de crear un paquete nuget.

**Jerarquía**: 
- **Proyecto Shared.DependencyInjection**: configuración del contenedor de dependencias, separando las responsabilidades por tipos de estructuras (repositories, automapper...).
La extensión ```RegisterEntityFramework``` registra y configura el contexto de EF.
- **Proyecto Hacienda.Shared.Global.Resources**: literales compartidos (mensajes de operaciones, versión de producto, etcétera). Tenerlos en un mismo proyecto facilita la traducción.

## 3. Capa dominio (XXX.Domain)

**Definición**: es el corazón del sistema y contiene las entidades, agregados y objetos de valor que representan el conocimiento del negocio.

**Características principales**: 
- Depende de: nada. No depende de capas superiores como la capa de aplicación o infraestructura.
- Contiene las entidades del dominio.
- Define las reglas de negocio y lógica de dominio.

**Jerarquía**: 
- **Carpeta Entities**: entidades del producto.
- **Carpeta Exceptions**: define las excepciones que se van a utilizar dentro del producto. Estas excepciones podrán ser lanzadas en cualquier capa, y deberán ser gestionadas, por ejemplo, a través de un middleware.
- **Carpeta ExternalClients**: contratos con servicios externos (servicios web, grpc, etc).
- **Carpeta Repositories**: contratos con repositorios internos.
- **Carpeta ResultErrors**: la clase `Result.cs` permite manejar los resultados de operaciones o funciones de una manera robusta. 
En vez de que la operación devuelva un valor `true`, devolver� `Result<bool>`. De esta manera podrá tener mensajes en el caso de que algo no haya funcionado correctamente.

## 4. Capa aplicación (XXX.Application)

**Definición**: se encarga de coordinar las operaciones del sistema y actúa como un intermediario entre la capa de dominio y la capa de presentación.

**Caracter�sticas principales**: 
- Depende de: capa de dominio.
- Define casos de uso y servicios de aplicación.
- Implementa la l�gica de aplicaci�n y orquesta las operaciones del sistema.

**Jerarquía**:
- **Carpeta Dtos**: utiliza DTOs para comunicarse con la capa de presentación. Estos DTOs están separados en:
    - ```Request``` para pasar datos a los servicios. Por ejemplo, para enviar los datos para insertar un nuevo elemento.
    - ```Response``` para devolver estructuras de de datos desde un método. Por ejemplo, oara devolver un listado de elementos.
- **Carpeta Exceptions**: saneamiento de excepciones para mostrar a capas superiores excepciones controladas y que no contengan información crítica (líneas donde se ha producido el error, datos de la base de datos, etc).
- **Carpeta Mapping**: profiles de mapeo de datos.
- **Carpeta Middlewares**: middleware de comunicación con capas superiores.
- **Carpeta ProblemDetails**: factoría para generar problem details predefinidos.
- **Carpeta Services**: servicios de la aplicación, tanto implementación como las interfaces.
    - Servicios: realizarán validaciones de los datos proporcionados desde capas superiores mediante [FluentValidation](https://docs.fluentvalidation.net/)

## 5. Capa infraestructura (XXX.Infrastructure)

**Definiciónn**: se encarga de la implementaci�n de detalles técnicos y la interacción con recursos externos, como bases de datos, servicios web y sistemas de almacenamiento.

**Características principales**:
- Depende de: capa de dominio.
- Contiene la implementación de la persistencia de datos, como la conexión a la base de datos.
- Gestiona la comunicación con servicios externos y recursos t�cnicos.

**Jerarquía**:
- **DbContextDapper**: motor de acceso a datos a través de Dapper.
- **DbContextEf**: motor de acceso a datos a través de EF.
- **Carpeta ExternalClients**: implementación de llamada a clientes externos. Se deberá utilizar el **patrón adapter**. 
- **Carpeta Repositories**: repositorios de la aplicación, a nivel de implementación.

## 6. Capa web - Web API (XXX.WebApi)

**Definición**: web api de ejemplo.
**Características principales**:
- **Documentación de API**: se ha configurado la Api para que recoja los comentarios puestos en ```<summary>```, configurando en el proyecto:
    ```xml
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    ```

    Posteriormentes en la clase ```SwaggerExtension``` se configura cómo se anexa Swagger y qué configuración tiene.
- **HealthChecks**: se ha creado ```WebApiHealthCheck```, que permitirá chequear cualquier punto que de información del estado de la infraestructura (acceso a servicios, bdd�). 
    La salida la produce si ponemos `https://localhost:XXXX/_health`, en formato Json, mediante el nuget `AspNetCore.HealthChecks.UI.Client`:

## 7. Capa web - Web (XXX.Web)

**Definición**: prueba de concepto sencilla para una web hecha con Blazor Server que llama a un servicio y devuelve datos.

## 8. Capa task (XXX.WorkerService)

**Definición**: prueba de concepto sencilla para un servicio de background que llama a un servicio y devuelve datos.

## Enlaces de inter�s

**Generales**:
- [Arquitecturas de aplicaciones web comunes](https://learn.microsoft.com/es-es/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Uso de Central Package Management (CPM) para paquetes Nuget](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)
- [Serilog](https://serilog.net/)
- [Graylog](https://graylog.org/)

**Validaciones de datos**
- [FluentValidation](https://docs.fluentvalidation.net/)

**Base de datos**
- [Dapper](https://github.com/DapperLib/Dapper)

**Testing:**
- [XUnit](https://xunit.net/)
- [Automapper](https://automapper.org/)
- [Fluent Assertions](https://fluentassertions.com/)