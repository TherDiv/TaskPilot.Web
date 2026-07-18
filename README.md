# TaskPilot

Sistema web de gestión y seguimiento de actividades académicas, desarrollado con **C#**, **ASP.NET MVC** y **ASP.NET Web API**, siguiendo una arquitectura por capas y la metodología ágil **Scrum**.

## 🏗️ Arquitectura

El proyecto está organizado en capas, separando responsabilidades entre presentación, lógica de negocio y acceso a datos:

```
TaskPilot.Solution
├── TaskPilot.Entidades      → Clases de dominio (Tarea, Usuario, TareaUsuario)
├── TaskPilot.AccesoDatos    → Acceso a la base de datos (ADO.NET, SqlClient)
├── TaskPilot.LogicaNegocio  → Reglas de negocio (validaciones, cálculo de prioridad)
├── TaskPilot.Utiles         → Utilidades comunes (conexión a BD)
├── TaskPilot.Web            → Aplicación ASP.NET MVC (interfaz de usuario)
└── TaskPilot.API            → API REST (ASP.NET Web API)
```

`TaskPilot.Web` y `TaskPilot.API` son independientes entre sí, pero ambos consumen las mismas capas de `Entidades`, `AccesoDatos`, `LogicaNegocio` y `Utiles`, evitando duplicar lógica.

### Modelo de base de datos

Tablas implementadas actualmente:

- **Usuario**: `IdUsuario`, `NombreCompleto`, `Correo`, `Usuario`, `Clave`, `Rol`
- **Tarea**: `IdTarea`, `Titulo`, `Descripcion`, `FechaInicio`, `FechaFin`, `Estado`, `Prioridad`
- **TareaUsuario**: tabla intermedia (`IdTarea`, `IdUsuario`) para la relación muchos a muchos entre tareas y usuarios

Tablas contempladas en el diseño del sistema, pendientes de implementación:

- **Estado**: `IdEstado`, `NombreEstado`
- **LogActividad**: `IdLog`, `Descripcion`, `Fecha`, `Usuario`

## 🚀 Puesta en marcha

### Requisitos previos

- Visual Studio 2019 o superior (con carga de trabajo ASP.NET y desarrollo web)
- .NET Framework 4.8
- SQL Server (Express o superior) y SQL Server Management Studio (opcional, para administrar la BD)

### 1. Clonar el repositorio

```bash
git clone https://github.com/TherDiv/TaskPilot.Web.git TaskPilot.Solution
cd TaskPilot.Solution
```

Al clonar obtienes los 6 proyectos de la solución (`TaskPilot.Web`, `TaskPilot.API`, `TaskPilot.Entidades`, `TaskPilot.AccesoDatos`, `TaskPilot.LogicaNegocio`, `TaskPilot.Utiles`) junto con el archivo `TaskPilot.Web.sln` en la raíz.

### 2. Crear la base de datos

Crea una base de datos llamada `TaskPilotDB` en tu instancia de SQL Server y ejecuta el siguiente script para crear las tablas:

```sql
CREATE TABLE Usuario (
    IdUsuario      INT IDENTITY(1,1) PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Correo         VARCHAR(100) NULL,
    Usuario        VARCHAR(50)  NOT NULL,
    Clave          VARCHAR(100) NOT NULL,
    Rol            VARCHAR(20)  NOT NULL
);

CREATE TABLE Tarea (
    IdTarea     INT IDENTITY(1,1) PRIMARY KEY,
    Titulo      VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500) NULL,
    FechaInicio DATE NULL,
    FechaFin    DATE NULL,
    Estado      VARCHAR(20) NULL,
    Prioridad   VARCHAR(20) NULL
);

CREATE TABLE TareaUsuario (
    IdTarea   INT,
    IdUsuario INT,
    PRIMARY KEY (IdTarea, IdUsuario),
    FOREIGN KEY (IdTarea) REFERENCES Tarea(IdTarea),
    FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
```

> Nota: este script cubre las tablas que el código actualmente usa (`Usuario`, `Tarea`, `TareaUsuario`). Las tablas `Estado` y `LogActividad` están contempladas en el diseño del informe técnico pero todavía no tienen soporte en el código; agrégalas cuando se implemente esa funcionalidad.

Opcionalmente, puedes poblar la base con datos de prueba (usuarios y tareas de ejemplo) para probar el login, el CRUD, el dashboard y la API sin partir de cero.

### 3. Configurar la cadena de conexión

La cadena de conexión debe existir en **ambos** proyectos (`TaskPilot.Web` y `TaskPilot.API`), ya que cada uno lee su propio `Web.config` en tiempo de ejecución.

En `TaskPilot.Web/Web.config` **y** `TaskPilot.API/Web.config`, agrega dentro de `<configuration>`:

```xml
<connectionStrings>
    <add
        name="cnTaskPilot"
        connectionString="Data Source=TU_SERVIDOR;Initial Catalog=TaskPilotDB;Integrated Security=True"
        providerName="System.Data.SqlClient"
    />
</connectionStrings>
```

Reemplaza `TU_SERVIDOR` por el nombre de tu instancia de SQL Server (por ejemplo, `localhost\SQLEXPRESS`).

### 4. Restaurar paquetes NuGet

Desde Visual Studio: clic derecho en la solución → **Restaurar paquetes NuGet**.

### 5. Ejecutar el proyecto

- Para levantar solo la aplicación web: marca `TaskPilot.Web` como proyecto de inicio y ejecuta (F5).
- Para levantar la web y la API al mismo tiempo: en las propiedades de la solución, configura **múltiples proyectos de inicio** (`TaskPilot.Web` y `TaskPilot.API`).

### 6. Iniciar sesión

Si cargaste el script de datos de prueba, puedes iniciar sesión con:

| Usuario | Contraseña | Rol |
|---|---|---|
| `esther` | `123456` | ADMIN |
| `joseph` | `123456` | USER |

## 🔌 Endpoints de la API

Base URL: `https://localhost:<puerto>/api`

| Método | Endpoint | Descripción |
|---|---|---|
| GET | `/tareas` | Lista todas las tareas |
| GET | `/tareas/{id}` | Obtiene el detalle de una tarea |
| POST | `/tareas` | Crea una nueva tarea |
| PUT | `/tareas/{id}` | Actualiza una tarea existente |
| DELETE | `/tareas/{id}` | Elimina una tarea |

> Un `UsuarioDTO` ya está definido en `TaskPilot.API/Models` (sin exponer la contraseña), listo para cuando se agregue el controlador de usuarios de la API.

Proyecto académico desarrollado para el curso de Formación de Empresas de Software.
