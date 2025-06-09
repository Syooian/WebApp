選擇DBFirst_Test為啟動專案
套件管理主控台的預設專案選擇DBFirst_Test
Scaffold-DbContext生成Model操作：\
Scaffold-DbContext "Server='127.0.0.1';Database='dbstudents';User='root';Password='autc007'" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force

建立Controller時跳出錯誤，要求要安裝Microsoft.VisualStudio.Web.CodeGeneration.Design套件，且不知道何嘗試開啟tstudents.cs Model時跳出錯誤
嘗試安裝8.0.7版套件時不給裝，於是先安裝最新版(9.0.0)，但是遇到版本相衝，後再降為8.0.7
此時tstudents.cs Model又可開啟了

## 建立Controller時的錯誤
執行選取的程式碼產生器時發生錯誤:
'請安裝套件 Microsoft.VisualStudio.Web.CodeGeneration.Design，然後再試一次。'
