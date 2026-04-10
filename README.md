# 🏦 Banking System (C# WPF + SQL Server)

## 📌 Overview
This project is a desktop banking system developed using **C# (WPF)** and **SQL Server**.  
It allows staff to manage customer accounts and perform core banking operations such as deposits, withdrawals, and transfers.

---

## 🔐 Login Details
Username: admin
Password: 1234


---

## ✨ Features
- User login authentication  
- Create new account  
- Edit account details  
- Deposit funds  
- Withdraw funds  
- Transfer funds (internal and external)  
- View transaction history  
- Export account data to XML  
- Deactivate account (soft delete using `IsActive`)  
- Basic form validation  

---

## 🛠️ Technologies Used
- C# (WPF)
- SQL Server
- ADO.NET

---

## 🏗️ System Architecture
This system follows a **3-tier architecture**:

- **Presentation Layer** – WPF UI  
- **Business Layer** – Handles logic and validation  
- **Data Layer** – SQL Server database access  

---

## 🗄️ Database Setup

### ✅ Option 1 (Recommended)
1. Open SQL Server Management Studio (SSMS)  
2. Right-click **Databases**  
3. Click **Restore Database**  
4. Select **Device**  
5. Choose `dbs.bak`  
6. Restore database with name: `dbs`  
7. Open project in Visual Studio  
8. Run the application  

---

### ⚙️ Option 2
1. Open SQL Server  
2. Run `setup.sql`  
3. This will create all tables and stored procedures  

---

## ⚠️ Important
- The connection string is stored in `App.config`  
- Update it if running on a different SQL Server instance  

---

## 🗃️ Database Name

---

## ✨ Features
- User login authentication  
- Create new account  
- Edit account details  
- Deposit funds  
- Withdraw funds  
- Transfer funds (internal and external)  
- View transaction history  
- Export account data to XML  
- Deactivate account (soft delete using `IsActive`)  
- Basic form validation  

---

## 🛠️ Technologies Used
- C# (WPF)
- SQL Server
- ADO.NET

---

## 🏗️ System Architecture
This system follows a **3-tier architecture**:

- **Presentation Layer** – WPF UI  
- **Business Layer** – Handles logic and validation  
- **Data Layer** – SQL Server database access  

---

## 🗄️ Database Setup

### ✅ Option 1 (Recommended)
1. Open SQL Server Management Studio (SSMS)  
2. Right-click **Databases**  
3. Click **Restore Database**  
4. Select **Device**  
5. Choose `dbs.bak`  
6. Restore database with name: `dbs`  
7. Open project in Visual Studio  
8. Run the application  

---

### ⚙️ Option 2
1. Open SQL Server  
2. Run `setup.sql`  
3. This will create all tables and stored procedures  

---

## ⚠️ Important
- The connection string is stored in `App.config`  
- Update it if running on a different SQL Server instance  

---

## 🗃️ Database Name
