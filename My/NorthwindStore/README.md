## 課後實作練習
1. 將「英文版北風.sql」建立資料庫於SQL Server中。
2. 新建ASP.net Core MVC，專案名稱為「NorthwindStore」。
3. 使用DateBase First建立Model。
4. 建立後臺資料管理功能：建立下列資料表所需的功能：
	<ol>
		<li>商品分類管理(Categories)：List, Create, Edit</li>
		<li>商品資料管理(Products)：List, Create, Edit, Detail</li>
		<li>客戶資料管理(Customers)：List, Create, Edit, Detail</li>
		<li>員工資料管理(Employees)：List, Create, Edit, Detail</li>
		<li>供應商資料管理(Suppliers)：List, Create, Edit, Detail</li>
		<li>運送資料管理(Shippers)：List, Create, Edit</li>
	</ol>
先以EntityFramework建立全自動的CRUD(包含Controller和View)，再進行個資料表的功能調整。

5. 各管理功能畫面(View)調整
	<ol>
		<li>商品分類管理(Categories)及運送資料管理(Shippers)：</li>
		<ul>
			<li>第一畫面為List頁面，Create及Edit按鈕置於List頁面，典籍後即進入對應功能畫面。</li>
		</ul>
		<li>商品資料管理(Products)、客戶資料管理(Customers)、員工資料管理(Employees)、供應商資料管理(Suppliers)：</li>
		<ul>
			<li>第一畫面為List頁面，Create及Detail按鈕置於List頁面，點擊後即進入對應功能畫面。</li>
			<li>Edit按鈕置於Detail畫面，點擊後即進入編輯功能畫面。</li>
		</ul>
	</ol>
6. List View調整：
	<ol>
		<li>商品分類管理(Categories)及運送資料管理(Shippers)需顯示出ID欄位。</li>
		<li>商品資料管理(Products)：</li>
 		<ul>
			<li>顯示ProductID、ProductName、UnitPrice、UnitslnStock、Discontinued。</li>
		</ul>
		<li>客戶資料管理(Customers)：</li>
		<ul>
			<li>顯示CustomerID、CompanyName、ContactTitle、Phone。</li>
		</ul>
		<li>員工資料管理(Employees)：</li>
		<ul>
			<li>顯示EmployeeID、FirstName、LastName、Title、TitleOfCourtesy、HireDate。</li>
		</ul>
		<li>供應商資料管理(Suppliers)：</li>
		<ul>
			<li>顯示SupplierID、CompanyName、ContactName、ContactTitle。</li>
		</ul>
	</ol>

<p>Model生成指令操作</p>
Scaffold-DbContext "Data Source=C501A117;Database=Northwind;TrustServerCertificate=True;User ID=Syooian;Password=a123456" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
TrustServerCertificate=True：信任伺服器憑證
-NoOnConfiguring：不要在DBContext內生成OnConfiguring方法