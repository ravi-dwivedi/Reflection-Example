using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ExampleClass;


namespace ConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 user = new Class1();
            Class2 user2 = new Class2();
            Console.WriteLine(user);
            PropertyInfo[] properties = user.GetType().GetProperties();
            
            foreach(PropertyInfo pro in properties)
            {
                Console.WriteLine(pro);
                Console.WriteLine(pro.GetValue(user));
                var sourceValue = pro.GetValue(user);

                var dpi = user2.GetType().GetProperties().SingleOrDefault(p => p.Name == pro.Name);
                dpi.SetValue(user2,pro.GetValue(user));
                if (pro.GetValue(user).GetType() == typeof(string))
                {
                    pro.SetValue(user,"Hello");
                }
                else
                {
                    string val = "1000";
                    var convertor = TypeDescriptor.GetConverter(pro.GetValue(user).GetType());
                    pro.SetValue(user, convertor.ConvertFrom(val));
                }

                Console.WriteLine(pro.GetValue(user));
            }
            Console.WriteLine(user2.DATA+" "+user2.NAME);
            Console.Read();
        }
    }
}
