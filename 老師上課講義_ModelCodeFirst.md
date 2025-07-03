# MyModel_CodeFirst專案進行步驟

**專案位置：**[\Teacher\MyModel_CodeFirst](/Teacher/MyModel_CodeFirst/)  
**講義原始檔案：**[HomeController.cs](/Teacher/MyModel_CodeFirst/Controllers/HomeController.cs)

## 1. 使用Code First建立Model及資料庫
<p>1.1 在Models資料夾裡建立Book及ReBook兩個類別做為模型</p>

* 1.1.1 在Models資料夾上按右鍵→加入→類別，檔名取名為Book.cs，按下「新增」鈕
* 1.1.2 設計Book類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示
* 1.1.3 在Models資料夾上按右鍵→加入→類別，檔名取名為ReBook.cs，按下「新增」鈕
* 1.1.4 設計ReBook類別的各屬性，包括名稱、資料類型及其相關的驗證規則及顯示名稱(Display)
* 1.1.5 撰寫兩個類別間的關聯屬性做為未來資料表之間的關聯
* 1.1.6 建立MeataData類別，把Book及ReBook類別中自己所添加的程式碼移植至MetaData類別中
* 1.1.7 使用Partial Class的特性，將MeataData類別標註於對應的Book及ReBook類別上(原始的Book及ReBook類別須在class前加上partial)  
※利用MeataData類別可以運用Partial Class的特性，將原本描述在原class中的程式碼分離出來，達到較好的程式架構，使原來的程式碼保持原始狀態※  
※這個實作技巧在DBFirst中尤為重要，因為只要重新執行DBFirst，將會把原來的模型內容覆蓋掉，而寫在模型類別中的規則就必須重新撰寫※

<p>1.2   建立DbContext類別</p>

* ※安裝下列兩個套件※
    * Microsoft.EntityFrameworkCore.SqlServer
    * Microsoft.EntityFrameworkCore.Tools  
※與DB First安裝的套件一樣※

* 1.2.1 在Models資料夾上按右鍵→加入→類別，檔名取名為GuestBookContext.cs，按下「新增」鈕
* 1.2.2 撰寫GuestBookContext類別的內容
    1. 須繼承DbContext類別
    2. 撰寫依賴注入用的建構子
    3. 描述資料庫裡面的資料表
* 1.2.3 在appsettings.json中撰寫資料庫連線字串
* 1.2.4 在Program.cs內以依賴注入的寫法註冊讀取連線字串的服務(food panda、Uber Eats)  
    ※注意程式的位置必須要在
    ```csharp
    var builder = WebApplication.CreateBuilder(args);
    ```
    這句之後且在
    ```csharp
    var app = builder.Build();
    ```
    之前

* 1.2.5 在套件管理器主控台(檢視 > 其他視窗 > 套件管理器主控台)下指令

<h3>※※※注意注意※※※ 在執行指令前請先確定專案是否正確選擇</h3>
<h3>※※※注意注意※※※ 在執行指令前請先確定專案是否正確選擇</h3>
<h3>※※※注意注意※※※ 在執行指令前請先確定專案是否正確選擇</h3>

1. Add-Migration InitialCreate  
※「InitialCreate」是自訂的名稱，若執行成功會看到「Build succeeded.」※  
※另外會看到一個Migrations的資料夾及其檔案被建立在專案中，裡面紀錄著Migration的歷程※
2. Update-database  
※第(1)項指令執行成功才能執行第(2)項指令※
3. 至SSMS中查看是否有成功建立資料庫及資料表(目前資料表內沒有資料)
