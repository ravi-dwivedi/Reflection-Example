using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleClass
{
    public class Class2
    {
        public int data;
        public string name;
        public Class2()
        {
            data = 23;
            name = "Rohan";
        }
        public int DATA
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        public string NAME
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int multiply(int num1, int num2)
        {
            return num1 * num2;

        }
        public override string ToString()
        {
            return this.data + " " + this.name;
        }
    }
    public class Class1
    {
        public int data;
        public string name;
        public Class1()
        {
            data = 1;
            name = "Ravi";
        }
        public int DATA
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }
        public string NAME
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public int multiply(int num1, int num2)
        {
            return num1 * num2;

        }
        public override string ToString()
        {
            return this.data + " " + this.name;
        }

    }
}
