## 新增資料庫登入使用者
1. 伺服器：安全性->登入->新增登入->選擇SQL Server驗證後輸入帳密、取消強制執行密碼原則
2. 伺服器：屬性->安全性->伺服器驗證：SQL Server及Windows驗證模式
3. 開啟SQL Server 2022 安全管理員：重啟MSSQLSERVER

## 自動建立模型 1.2.3
1. 使用套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)
2. 輸入以下內容：Scaffold-DbContext "Data Source=C501A117;Database=dbStudents;TrustServerCertificate=True;User ID=Syooian;Password=a123456" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force

## CRUD (增刪修查)
利用模型自動產生
1. 將Controller中建立_context的過程皆註解掉後，直接new新的dbStudentsContext()
2. 加入控制器->使用Entity Framework執行檢視的MVC控制器
3. 模型類別：tStudent
4. DbContext：dbStudentsContext
5. 在dbStudentsContext.cs終將預設的dbStudentsContext註解後直接創建一個空建構子，再新增OnConfiguring方法
6. 執行tStudents/Index.cshtml(使用瀏覽器檢視)