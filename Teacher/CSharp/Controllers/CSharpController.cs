using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
    public class CSharpController : Controller
    {

        //在類別裡的一個方法,它叫Action
        public IActionResult Index()
        {
            return View();
        }


        public string Hello()
        {
            return "Hello World!";
        }

        public void Number()
        {
            int a = 123;  //32位元帶正負號整數
            short b = 123; //16位元帶正負號整數
            long c = 123; //64位元帶正負號整數

            float d = 12.23f;  //單精準度浮點數
            double e = 12.23;  //倍精準度浮點數


        }

        public void Var()
        {
            string a = "123";

            bool b = true;


        }

        public string Stament()
        {
            int a = 123;
            a += 10;
            a++;

            return "a變數的值為" + a;
        }



        //有參數的Action
        public string IfStatement(int score)
        {
            if (score >= 90 && score <= 100)
            {
                return "優等";
            }
            else if (score >= 80)
            {
                return "甲等";
            }
            else if (score >= 70)
            {
                return "乙等";
            }
            else if (score >= 60)
            {
                return "丙等";
            }
            else if (score >= 0 && score < 60)
                return "丁等";


            return "請輸正0~100的整數值!!";

        }

        public string SwitchStatement(int score)
        {
            int s = score / 10;  //  94/10=>9

            switch (s)
            {
                case 10:
                case 9:
                    return "優等";
                case 8:
                    return "甲等";
                case 7:
                    return "乙等";
                case 6:
                    return "丙等";
                case 5:
                case 4:
                case 3:
                case 2:
                case 1:
                case 0:
                    return "丁等";

            }


            return "請輸正0~100的整數值!!";

        }

        //for敘述
        public string ForeachStatement()
        {
            string[] Rainbow = ["紅", "橙", "黃", "綠", "藍", "靛", "紫"];
            string result = "";

            foreach (string item in Rainbow)
            {
                result += item;
            }

            return result;
        }

        public void ForStatement()
        {
            string[] Rainbow = ["紅", "橙", "黃", "綠", "藍", "靛", "紫"];

            string[] RainbowColor = { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };

            string result = "";

            for (int i = 0; i < Rainbow.Length; i++)
            {
                //result += Rainbow[i]+ RainbowColor[i];

                //result += "<span style='color:" + RainbowColor[i] + "'>" + Rainbow[i] + "</span>";

                result += $"<span style='color:{RainbowColor[i]}'>{Rainbow[i]}</span>";
            }

            Response.ContentType = "text/html; charset=utf-8"; //設定回傳的內容類型為html
            Response.WriteAsync(result);


        }


        //while敘述
        public int WhileStatement()
        {
            int n = 1, sum = 0;

            while (n <= 1000)
            {
                sum += n;
                n++;
            }


            return sum;
        }

        //do...while敘述
        public int DoWhileStatement()
        {
            int n = 1, sum = 0;

            do
            {
                sum += n;
                n++;
            } while (n <= 1000);


            return sum;
        }
    }
}
