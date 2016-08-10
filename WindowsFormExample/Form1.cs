using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormExample
{
    public partial class Form1 : Form
    {
        Assembly sourceAssembly;
        Type typeSelected;
        MethodInfo methodInfos;
        Type[] returnType;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult dr = fileDialog.ShowDialog();
            if(DialogResult.OK==dr)
            {
                textBox1.Text = fileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          sourceAssembly = Assembly.LoadFrom(textBox1.Text);
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var types = (from t in sourceAssembly.GetTypes()
                         where t.IsClass
                         select t).ToArray();
            listBox1.Items.AddRange(types);
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            typeSelected =  sourceAssembly.GetType(listBox1.SelectedItem.ToString());

            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static; 
            MethodInfo[] methodInfos = typeSelected.GetMethods(eFlags);

            //typeSelected.GetMethod("add2Numbers", eFlags);

            ConstructorInfo[] ctorInfos = typeSelected.GetConstructors();
            foreach(MethodInfo method in methodInfos)
            {
                listBox2.Items.Add(method.Name);
            }


        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedMethod = listBox2.SelectedItem.ToString();
            BindingFlags eFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;

            methodInfos = typeSelected.GetMethod(selectedMethod, eFlags);
            ParameterInfo[] paramInfo = methodInfos.GetParameters();
            returnType = new Type[paramInfo.Length];
            
            int y = 0;
            int index = 0;
            foreach(ParameterInfo param in paramInfo)
            {
                returnType[index++] = param.ParameterType;
                int x = 0;
                Label lb = new Label();
                lb.Text = param.Name.ToString();

                lb.Location = new Point(x,y);
                x += lb.Location.X + lb.Size.Width + 10;
                TextBox txt = new TextBox();
                txt.Location = new Point(x,y);
                txt.Name = "txt";
                panel1.Controls.Add(lb);
                panel1.Controls.Add(txt);
                y += 50;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = 0;
            Object obj = Activator.CreateInstance(typeSelected);
            List<Object> arrParam = new List<Object>();
            foreach(var item in panel1.Controls.Find("txt",false))
            {
                var converter = TypeDescriptor.GetConverter(returnType[index++]);
                arrParam.Add(converter.ConvertFrom(item.Text.ToString()));
            }

            object result = methodInfos.Invoke(obj,arrParam.ToArray());



            Label lb = new Label();
            lb.Text = "Result :-> ";
            lb.Location = new Point(0, 0);
            panel2.Controls.Add(lb);
            lb = new Label();
            lb.Text = result.ToString();
            lb.Location = new Point(0, 50);
            panel2.Controls.Add(lb);

        }

    }
}
