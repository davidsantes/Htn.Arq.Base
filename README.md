# Arquitectura de referencia

## Objetivo del Documento

El objetivo de este documento es indicar las caracter�sticas principales de esta prueba de concepto de una arquitectura de referencia.

## Control de versiones
| Versi�n | Fecha | Autor   | Observaciones |
| ------- | ------- | ------- | ------------- |
| 0.1     | 23/10/2023 | David S | Versi�n inicial   |

## �ndice

1. Un vistazo general
2. Capas transversales
3. Capa de dominio (XXX.Domain)
4. Capa de aplicaci�n (XXX.Application)
5. Capa de infraestructura (XXX.Infrastructure)
6. Capa web - Web API (XXX.WebApi)
7. Capa web - Web (XXX.Web)
8. Capa task (XXX.WorkerService)

## 1. Un vistazo general

Algunas de las caracter�sticas de esta arquitectura son:
- **Arquitectura enfocada a dominio** ([Domain Driven Design - DDD](https://learn.microsoft.com/es-es/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)), con capas de:
    - Dominio: proyectos XXX.Domain
    - Aplicaci�n: proyectos XXX.Application
    - Infraestructura: proyectos XXX.Infrastructure
- **Proyectos transversales (1.0 - Cross cutting Layer)**, para reutilizaci�n de inyecci�n de dependencia y mensajes.
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

**Definici�n**:
Proyectos que se pueden utilizar en varias capas diferentes. Podr�an ser susceptibles de crear un paquete nuget.

## 3. Capa dominio (XXX.Domain)

**Definici�n**: es el coraz�n del sistema y contiene las entidades, agregados y objetos de valor que representan el conocimiento del negocio.

**Caracter�sticas principales**: 
- Depende de: nada. No depende de capas superiores como la capa de aplicaci�n o infraestructura.
- Contiene las entidades del dominio.
- Define las reglas de negocio y l�gica de dominio.
- **Patr�n Result**: la clase Result.cs permite manejar los resultados de operaciones o funciones de una manera robusta. 
En vez de que la operaci�n devuelva un valor `true`, devolver� `Result<bool>`. De esta manera podr� tener mensajes en el caso de que algo no haya funcionado correctamente.

## 4. Capa aplicaci�n (XXX.Application)

**Definici�n**: se encarga de coordinar las operaciones del sistema y act�a como un intermediario entre la capa de dominio y la capa de presentaci�n.

**Caracter�sticas principales**: 
- Depende de: capa de dominio.
- Define casos de uso y servicios de aplicaci�n.
- Implementa la l�gica de aplicaci�n y orquesta las operaciones del sistema.
- Utiliza DTOs para comunicarse con la capa de presentaci�n. Estos DTOs est�n separados en ```Response``` para devoluci�n (una query tipo "Get") y ```Request``` (escritura).

## 5. Capa infraestructura (XXX.Infrastructure)

**Definici�n**: se encarga de la implementaci�n de detalles t�cnicos y la interacci�n con recursos externos, como bases de datos, servicios web y sistemas de almacenamiento.

**Caracter�sticas principales**:
- Depende de: capa de dominio.
- Contiene la implementaci�n de la persistencia de datos, como la conexi�n a la base de datos.
- Gestiona la comunicaci�n con servicios externos y recursos t�cnicos.

## 6. Capa web - Web API (XXX.WebApi)

**Definici�n**: web api de ejemplo.
**Caracter�sticas principales**:
- **Documentaci�n de API**: se ha configurado la Api para que recoja los comentarios puestos en ```<summary>```, configurando en el proyecto:
    ```xml
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    ```
- **HealthChecks**: se ha creado ```MyCustomHealthCheck```, que permitir�a chequear lo que quisi�ramos del producto (acceso a servicios, bdd�). 
    La salida la produce si ponemos `https://localhost:XXXX/_health`, en formato Json, mediante el nuget `AspNetCore.HealthChecks.UI.Client`:

## 7. Capa web - Web (XXX.Web)

**Definici�n**: prueba de concepto sencilla para una web hecha con Blazor Server que llama a un servicio y devuelve datos.

## 8. Capa task (XXX.WorkerService)

**Definici�n**: prueba de concepto sencilla para un servicio de background que llama a un servicio y devuelve datos.

## Enlaces de inter�s

**Generales**:
- [Arquitecturas de aplicaciones web comunes](https://learn.microsoft.com/es-es/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- [Uso de Central Package Management (CPM) para paquetes Nuget](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)
- [Serilog](https://serilog.net/)
- [Graylog](https://graylog.org/)

**Testing:**
- [XUnit](https://xunit.net/)
- [Automapper](https://automapper.org/)
- [Fluent Assertions](https://fluentassertions.com/) 