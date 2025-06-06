# User API (.NET 8)

Esta es una API RESTful para la creación de usuarios, desarrollada en ASP.NET Core (.NET 8), con persistencia en SQL Server, generación de token JWT y documentación en Swagger.

##  Tecnologías

- ASP.NET Core 8
- Entity Framework Core
- SQL Server (SQLEXPRESS)
- JWT para autenticación
- Swagger UI

##  Requisitos

- .NET 8 SDK
- SQL Server Express (instancia: `SQLEXPRESS`)
- Visual Studio o VS Code
- Postman o navegador

## Configuración

1. Clona el repositorio
2. Asegúrate que tu cadena de conexión en `appsettings.json` apunte correctamente:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=UserApiDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Ejecuta las migraciones:
```bash
dotnet ef database update
```

4. Corre el proyecto:
```bash
dotnet run
```

##  Endpoint

### POST /api/users

Crea un nuevo usuario. Ejemplo de request:
```json
{
  "name": "Juan Perez",
  "email": "juan@mail.com",
  "password": "Clave#123@",
  "phones": [{ "number": "123456", "cityCode": "1", "countryCode": "57" }]
}
```



##  Validaciones

- Email debe tener formato válido
- Contraseña configurable mediante expresión regular en `appsettings.json`

##  Swagger

Accede a Swagger en:
```
https://localhost:<puerto>/swagger
```

---

##  Script SQL

```sql
CREATE DATABASE UserApiDB;
GO

USE UserApiDB;
GO

CREATE TABLE [User] (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    Password NVARCHAR(255),
    Created DATETIME,
    Modified DATETIME,
    LastLogin DATETIME,
    Token NVARCHAR(MAX),
    IsActive BIT
);

CREATE TABLE Phone (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Number NVARCHAR(20),
    CityCode NVARCHAR(10),
    CountryCode NVARCHAR(10),
    UserId UNIQUEIDENTIFIER,
    FOREIGN KEY (UserId) REFERENCES [User](Id)
);
```

##  Diagrama de arquitectura

- `UsersController` → recibe y valida solicitudes HTTP
- `JwtHelper` → genera tokens JWT
- `AppDbContext` → acceso a SQL Server
- Modelos: `User` y `Phone`
- DTOs: `CreateUserRequest`, `UserResponse`

```plaintext
[ Swagger / Postman ]
        ↓
[ UsersController ]
        ↓
[ JwtHelper ]    [ AppDbContext ]
        ↓               ↓
    JWT generado     SQL Server ← Entity Framework Core
```

---

 Generado el 2025-06-06 - para prueba técnica .NET API.
