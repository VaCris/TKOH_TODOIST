# TKOH To-Doist

¡Una aplicación de gestión de tareas y actividades potente y sencilla, diseñada para ayudarte a organizar tu día y a seguir el progreso con un sistema de puntuación!

## 🚀 Características Principales

Basado en la estructura del código (Controladores y Servicios), TKOH To-Doist ofrece las siguientes funcionalidades clave para la gestión de tareas:

* **Gestión de Asignaciones (Assignments):** Crea, edita y gestiona tareas individuales y su estado.
* **Conjuntos de Actividades (Activity Sets):** Agrupa tareas o actividades relacionadas en conjuntos lógicos para una mejor organización.
* **Plantillas (Templates):** Define plantillas de tareas reutilizables para optimizar la creación de actividades recurrentes.
* **Sistema de Puntuación (Score History):** Realiza un seguimiento del progreso y la finalización de tareas a través de un historial de logros y puntuación.
* **Autenticación Segura:** Funcionalidades de registro e inicio de sesión de usuarios.
* **Control de Usuarios y Roles:** Administración de usuarios, asignación de roles y permisos para definir niveles de acceso y responsabilidades.
* **Panel de Control (Dashboard):** Una vista centralizada para el resumen de actividades, tareas pendientes y progreso general.

## 🛠️ Tecnologías Utilizadas

Este proyecto es una aplicación web ASP.NET Core que utiliza el patrón MVC (Model-View-Controller) y una arquitectura de servicios para interactuar con una API/capa de datos externa.

* **Backend:** ASP.NET Core MVC (C#).
* **Frontend:** HTML, CSS y JavaScript, con integración de Bootstrap.
* **Patrones:** Uso de DTOs (Data Transfer Objects) y una capa de servicios (`Services`) clara para la lógica de negocio y la comunicación con el backend (simulada por `ConnectorAPI.cs`).

## 📦 Configuración y Ejecución

Para ejecutar este proyecto localmente, necesitarás tener instalado el SDK de .NET Core.

### Requisitos

* .NET Core SDK (la versión compatible con el proyecto, revisar el archivo `TKOH/TKOH.csproj`).
* Un IDE como Visual Studio o Visual Studio Code.

### Pasos de Instalación

1.  **Clonar el repositorio:**
    ```bash
    git clone [URL_DEL_REPOSITORIO]
    cd tkoh_todoist/TKOH
    ```
2.  **Restaurar dependencias:**
    ```bash
    dotnet restore
    ```
3.  **Ejecutar la aplicación:**
    ```bash
    dotnet run
    ```
La aplicación se iniciará, generalmente, en un puerto como `http://localhost:5000` o `https://localhost:7000`.

## 📄 Licencia

Este proyecto está bajo la licencia **MIT**. Consulta el archivo `LICENSE.txt` para más detalles.

---

## 👨‍💻 Autor

**¡Hecho por Studios TKOH!**
