# 中文数字与整形变量之间的转换

#### 介绍
中文数字如：“二百五”与整形变量如250之间的转换

#### 函数说明
在静态类ChineseConvert.ChineseNumber中的ToUlong函数实现中文数字到ulong整型变量的转换


#### 示例代码
``` C#
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
```

#### 输出结果
``` C#
中文数字转数值测试：
贰佰伍  = 250
三千万  = 30000000
肆万万  = 400000000
亿      = 100000000
廿三    = 23
7亿02   = 700000002
7亿2万  = 700020000
千百    = 1100
⑲兆     = 19000000
14亿两千二百万零5= 1422000005
14两二零5       = 142205
```
