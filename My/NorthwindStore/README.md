<p>Model生成指令操作</p>
Scaffold-DbContext "Data Source=C501A117;Database=Northwind;TrustServerCertificate=True;User ID=Syooian;Password=a123456" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -NoOnConfiguring -UseDatabaseNames -NoPluralize -Force
TrustServerCertificate=True：信任伺服器憑證
-NoOnConfiguring：不要在DBContext內生成OnConfiguring方法