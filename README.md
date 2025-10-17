# TKOH To-Doist

**Language:** **English** | [Español (Spanish)](README-ES.md)

A powerful and simple task and activity management application, designed to help you organize your day and track progress with a scoring system!

## 🚀 Key Features

Based on the code structure (Controllers and Services), TKOH To-Doist offers the following key functionalities for task management:

* **Assignment Management:** Create, edit, and manage individual tasks and their status.
* **Activity Sets:** Group related tasks or activities into logical sets for better organization.
* **Templates:** Define reusable task templates to streamline the creation of recurring activities.
* **Scoring System (Score History):** Track progress and task completion through an achievement and scoring history.
* **Secure Authentication:** User registration and login functionalities.
* **User and Role Control:** User administration, role assignment, and permissions to define access levels and responsibilities.
* **Dashboard:** A centralized view for the summary of activities, pending tasks, and overall progress.

## 🛠️ Technologies Used

This project is an ASP.NET Core web application that uses the MVC (Model-View-Controller) pattern and a service layer to interact with an external API/data layer.

* **Backend:** ASP.NET Core MVC (C#).
* **Frontend:** HTML, CSS, and JavaScript, with Bootstrap integration.
* **Patterns:** Use of DTOs (Data Transfer Objects) and a clear service layer (`Services`) for business logic and communication with the backend (simulated by `ConnectorAPI.cs`).

## 📦 Setup and Execution

To run this project locally, you will need to have the .NET Core SDK installed.

### Requirements

* .NET Core SDK (check the `TKOH/TKOH.csproj` file for the compatible version).
* An IDE like Visual Studio or Visual Studio Code.

### Installation Steps

1.  **Clone the repository:**
    ```bash
    git clone [REPOSITORY_URL]
    cd tkoh_todoist/TKOH
    ```
2.  **Restore dependencies:**
    ```bash
    dotnet restore
    ```
3.  **Run the application:**
    ```bash
    dotnet run
    ```
The application will typically start on a port like `http://localhost:5000` or `https://localhost:7000`.

## 📄 License

This project is licensed under the **MIT** license. See the `LICENSE.txt` file for more details.

---

## 👨‍💻 Author

**Made by Studios TKOH!**
