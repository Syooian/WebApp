# 新增資料庫登入使用者
1. 伺服器：安全性->登入->新增登入->選擇SQL Server驗證後輸入帳密、取消強制執行密碼原則
2. 伺服器：屬性->安全性->伺服器驗證：SQL Server及Windows驗證模式
3. 開啟SQL Server 2022 安全管理員：重啟MSSQLSERVER

* 自動建立模型
Scaffold-DbContext "Data Source=C501A117;Database=dbStudents;TrustServerCertificate=True;User ID=Syooian;Password=a123456" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force