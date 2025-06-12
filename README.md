# Model建置方式：
1. Database First (DBFirst)
	先有資料庫，再依資料庫建立模型
2. Code First
	先寫模型類別，再依類別建立資料庫

# 資料存取層 (Data Access Layer DAL)

## nvarchar vs. varchar
n : Unicode格式
varchar 內中文字通常會佔 2 個位元組（byte）