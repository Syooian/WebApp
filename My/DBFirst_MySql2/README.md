## Scaffold-DbContext生成Model操作
<ol>
	<li>選擇DBFirst_MySql2為啟動專案</li>
	<li>檢視->其他視窗->開啟套件管理器主控台</li>
	<li>套件管理器主控台的預設專案選擇DBFirst_MySql2</li>
	<li>執行以下指令：Scaffold-DbContext "Server='127.0.0.1';Database='dbstudents';User='root';Password='autc007'" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force</li>
</ol>