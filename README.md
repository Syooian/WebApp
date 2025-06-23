# Model建置方式：
1. Database First (DBFirst)
	先有資料庫，再依資料庫建立模型
2. Code First
	先寫模型類別，再依類別建立資料庫

# 資料存取層 (Data Access Layer DAL)

## nvarchar vs. varchar
n : Unicode格式
varchar 內中文字通常會佔 2 個位元組（byte）

## 必要的例外處理，需自行加入
1. 操作資料庫
2. 檔案處理

## ViewData生命週期
僅能跨一個Action(e.g.點下切換頁面按鈕後顯示的第一個頁面)

## 路由參數
asp-route-id：預設路由模板，使用該方法時網址會變成AAA/BBB\
asp-route-自訂參數名稱：未在路由中定義參數名稱，使用該方法時網址會變成AAA/?自訂參數名稱=參數值