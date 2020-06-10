using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("中文数字转数值测试：");
            Console.WriteLine("贰佰伍\t= " + ChineseNumber.ToUlong("贰佰五"));
            Console.WriteLine("三千万\t= " + ChineseNumber.ToUlong("三千万"));
            Console.WriteLine("肆万万\t= " + ChineseNumber.ToUlong("肆万万"));
            Console.WriteLine("亿\t= " + ChineseNumber.ToUlong("亿"));
            Console.WriteLine("廿三\t= " + ChineseNumber.ToUlong("廿三"));
            Console.WriteLine("7亿02\t= " + ChineseNumber.ToUlong("7亿02"));
            Console.WriteLine("7亿2万\t= " + ChineseNumber.ToUlong("7亿2万"));
            Console.WriteLine("千百\t= " + ChineseNumber.ToUlong("千百"));
            Console.WriteLine("⑲兆\t= " + ChineseNumber.ToUlong("⑲兆"));
            Console.WriteLine("14亿两千二百万零5= " + ChineseNumber.ToUlong("14亿两千二百万零5"));
            Console.WriteLine("14两二零5\t= " + ChineseNumber.ToUlong("14两二零5"));
            Console.Read();
        }
    }
}
