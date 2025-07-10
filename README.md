# 🎓 School Planner WPF Application

A modern WPF desktop application for students to manage their study modules, work hours, and self-study tracking. Built using C# and SQL Server, this app allows students to register/login, track academic load, and organize their semester effectively.

---

## 📌 Features

- 🔐 **User Login & Registration**  
  - Secure Base64-encoded password storage  
  - Registration auto-increments User ID

- 📚 **Module Management**  
  - Add modules with code, name, number of credits, and contact hours  
  - Store module data in a SQL Server database  
  - View all module data using dynamic lookup

- ⏱️ **Work Hour Logging**  
  - Log hours worked weekly for a specific module  
  - Calculate remaining hours automatically

- 📊 **Self-Study Hours Calculator**  
  - Calculates recommended self-study hours based on credits and weeks  
  - Displays hours remaining per week

- 📆 **Semester Setup**  
  - Input semester start date and total number of weeks

- 🔎 **Filter Module Credits**  
  - Filter modules by credit count (greater than or less than input)

- 💾 **Database-Backed Storage**  
  - Stores all modules and work data in SQL Server  
  - Loads saved data on successful login

- 🧵 **Multi-threaded Data Operations**  
  - Uses background threads to prevent UI freezing during DB operations

---

## 🛠️ Tech Stack

- **Language:** C#  
- **Framework:** .NET 6  
- **UI Framework:** WPF (Windows Presentation Foundation)  
- **Database:** SQL Server  
- **Libraries:** ADO.NET, System.Threading

---

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/SchoolPlannerApp.git
cd SchoolPlannerApp

**2. Configure the Database**
Ensure SQL Server is installed and running.

Create a database named: SCHOOL_ST10202790

Run the following SQL to set up the required tables:

sql
Copy
Edit
CREATE TABLE USERS (
    USER_ID INT PRIMARY KEY,
    NAME VARCHAR(50),
    SURNAME VARCHAR(50),
    PASSWORD VARCHAR(255)
);

CREATE TABLE MODULES (
    MODULE_CODE VARCHAR(20) PRIMARY KEY,
    USER_ID INT,
    MODULE_NAME VARCHAR(100),
    NUMBER_CREDITS INT,
    MODULE_HOURS INT
);

CREATE TABLE WORK (
    WORK_CODE VARCHAR(20) PRIMARY KEY,
    USER_ID INT,
    WORK_DATE VARCHAR(50),
    WORK_HOURS INT
);
3. Update Connection String
In your .cs files or preferably in App.config, set:

xml
Copy
Edit
<connectionStrings>
  <add name="SchoolDB" connectionString="data source=SIBUSISO; database=SCHOOL_ST10202790; Integrated Security=SSPI; TrustServerCertificate=True" providerName="System.Data.SqlClient"/>
</connectionStrings>
4. Run the App
Open the solution in Visual Studio 2022, build it, and run the app.

**###💡 Usage**
Register using the Register screen if you’re a new user

Login with your ID, name, surname, and password

Add modules and work hours from the MainApp screen

Calculate and track self-study hours

View or filter module credit data

