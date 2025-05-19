using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CheckIDCS
    {
        public static void CheckID(string ID)
        {
            Console.WriteLine("ID : " + ID);

            //var Head = ID.Substring(0, 1);
            var H = "ABCDEFGHJKLMNPQRSTUVXYWZIO";

            //Console.WriteLine("H : " + H.IndexOf(ID[0]));

            string NewID = (H.IndexOf(ID[0]) + 10).ToString() + ID.Substring(1, ID.Length - 1);


            //NewID += ID.Substring(1, ID.Length - 1);
            Console.WriteLine("NewID : " + NewID);

            int Sum = 0;
            int[] W = new int[] { 1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1 };
            for (int a = 0; a < NewID.Length; a++)
            {
                Sum += int.Parse(NewID.Substring(a, 1)) * W[a];

                //Console.WriteLine(NewID[a] + " " + W[a] + " " + NewID[a] * W[a]);
                //Sum += NewID[a] * W[a];
            }

            Console.WriteLine("Sum : " + Sum + ", 餘 : " + (Sum % 10));
        }
    }
}
