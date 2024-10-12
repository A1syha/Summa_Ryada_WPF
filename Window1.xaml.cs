using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _316a_Vorobiev_Dmitriy_Variant_2
{
    public partial class Window1 : Window
    {
        public Window1(string filePath)
        {
            InitializeComponent();
            LoadFileContent(filePath);
        }
        private void LoadFileContent(string filePath)
        {
            if (File.Exists(filePath))
            {
                textBox.Text = File.ReadAllText(filePath);
            }
            else
            {
                textBox.Text = "Файл не найден.";
            }
        }
    }
}
