using Microsoft.AspNetCore.Mvc;

namespace CSharp.Controllers
{
    public class IDNumberController : Controller
    {
        public string CheckIDNumber(string ID)
        {
            //A123456789
            //格式檢查
            // 1.長度必須是10個字元
            // 2.第一個字元必須是英文字母
            // 3.第二個字元必須是1或2數字
            // 4.第三個以後的字元必須0~9是數字

            if (string.IsNullOrEmpty(ID))
                return "這請輪入身分證字號";

            if (ID.Length != 10)
                return "這是不合法的身分證字號";




            string letters = "ABCDEFGHJKLMNPQRSTUVXYWZIO";

            if (letters.IndexOf(ID[0]) == -1)
                return "這是不合法的身分證字號";

            if (ID[1] != '1' && ID[1] != '2')
                return "這是不合法的身分證字號";

            for (int i = 2; i < ID.Length; i++)
            {
                if (ID[i] < '0' || ID[i] > '9')
                    return "這是不合法的身分證字號";
            }
            ///////////////////////////////////////////////////////////////////
            ///

            int letterNum = letters.IndexOf(ID[0]) + 10;

            int n1 = letterNum / 10;
            int n2 = letterNum % 10;

            //使命必達寫法
            //int n = n1 * 1 + n2 * 9 + ID[1] * 8 + ID[2] * 7 + ID[3] * 6 + ID[4] * 5 + ID[5] * 4 +
            //    ID[6] * 3 + ID[7] * 2 + ID[8] * 1 + ID[9] * 1;


            //純粹烗技
            int n = n1 * 1 + n2 * 9;

            for (int i = 1; i <= 8; i++)
            {
                n += (ID[i]-'0') * (9 - i);

            }

            n += ID[9]-'0';

            if (n % 10 == 0)
                return "這是合法的身分證字號";

            return "這是不合法的身分證字號";
        }
    }
}
