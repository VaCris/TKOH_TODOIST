<p align="center">
  <a href="https://studios-tkoh.azurewebsites.net/" target="_blank">
    <img src="https://drive.google.com/uc?export=view&id=1TuT30CiBkinh85WuTvjKGKN47hCyCS0Z" width="300" alt="Studios TKOH Logo">
  </a>
</p>

# 🎯 TKOH To-Doist

**Language:** **English** | [Español (Spanish)](README-ES.md)

A powerful and modern **To-Do list and productivity application** built with **ASP.NET Core MVC**. TKOH To-Doist is designed to help users organize their daily tasks, manage complex activities, and track personal achievement progress with a unique scoring and reward system.

## ✨ Core Functionalities

The application is structured around a comprehensive set of features to maximize productivity and user management:

### Productivity & Task Management
* **Assignment Management:** Full CRUD (Create, Read, Update, Delete) functionality for individual tasks and assignments.
* **Activity Sets:** Organize related tasks into logical groups, promoting better project and long-term goal management.
* **Templates:** Create and reuse task templates to rapidly set up recurring activities or standard projects.
* **Scoring System (`Score History`):** Tracks and logs user progress, task completion, and earned achievements, fostering motivation and engagement.

### Platform & User Control
* **Centralized Dashboard:** A single, intuitive view summarizing overall progress, pending tasks, and key statistics.
* **Secure Authentication:** Robust user registration and login functionalities managed via the `AuthService`.
* **User and Role Administration:** Comprehensive administrative tools (`UserController`, `RoleController`) for managing users, assigning custom roles, and defining granular access permissions (`RolePermissions.cs`).

## ⚙️ Technology Stack

TKOH To-Doist is a powerful web application leveraging Microsoft's ecosystem for a reliable and scalable platform.

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Backend Framework** | **ASP.NET Core MVC (C#)** | Provides a robust Model-View-Controller architecture for the web application. |
| **Logic Layer** | **Services Layer** | Dedicated service classes (`AssignmentService.cs`, `AuthService.cs`, etc.) encapsulate business logic and data manipulation. |
| **Data Interface** | **ConnectorAPI** | A simulated or dedicated API connector (`ConnectorAPI.cs`) handles communication with an external API or data repository. |
| **Data Models** | **DTOs (Data Transfer Objects)** | Used extensively to ensure clean and structured data communication between the application layers and the external API. |
| **Frontend** | **HTML, CSS, JavaScript** | Standard web technologies for the user interface, with Bootstrap for responsive styling. |

## 📦 Getting Started

Follow these steps to set up and run the TKOH To-Doist application locally.

### Prerequisites

* [.NET Core SDK](https://dotnet.microsoft.com/download) (Version compatible with `TKOH/TKOH.csproj`).
* A suitable IDE like Visual Studio or VS Code.

### Installation Steps

1.  **Clone the Repository:**
    ```bash
    git clone [REPOSITORY_URL]
    cd tkoh_todoist/TKOH
    ```
2.  **Restore Dependencies:**
    Navigate to the project directory and restore all required NuGet packages:
    ```bash
    dotnet restore
    ```
3.  **Run the Application:**
    Execute the project. The application will compile and launch a local web server:
    ```bash
    dotnet run
    ```
The application will typically be accessible via a link in your console, usually `http://localhost:5000` or `https://localhost:7000`.

### Configuration
* Review `appsettings.json` and `appsettings.Development.json` to configure API base URLs (`ApiSettings.cs`) and other environment-specific settings.

## 📄 License

This project is released under the terms of the **MIT License**. For full details, see the accompanying `LICENSE.txt` file.

***

<p align="center">
  <sub>🛠️ Developed with 💙 by <strong>Studios TKOH</strong></sub><br>
  <a href="https://studios-tkoh.azurewebsites.net/" target="_blank">🌐 studios-tkoh.azurewebsites.net</a>
</p>
