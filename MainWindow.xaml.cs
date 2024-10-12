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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _316a_Vorobiev_Dmitriy_Variant_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ochistit_Button_Click(object sender, RoutedEventArgs e)
        {
            Chislo_X.Text = "";
            Tochnost.Text = "";
            Label4.Content = "";
        }

        private void Vipolnit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Tochnost.Text, out double epsilon) && double.TryParse(Chislo_X.Text, out double x) && x != 0 && epsilon > 0)
            {
                double result = Raschet(x, epsilon);
                Label4.Content = result.ToString();
            }
            else
            {
                Label4.Content = "Ошибка ввода!";
            }
        }
        private double Raschet(double x, double epsilon)
        {
            StreamWriter writer = new StreamWriter("PROMEJUTOCHNIE_RACHETI.txt");
            writer.WriteLine("Итерация\tРезультат\t\tСумма");
            double sum_result = 0;
            double result;
            int k = 0;
            do
            {
                double chislitel = Math.Pow(-1, k) * Math.Pow(x, 4 * k + 3);
                double znamenatel = Factorial(2 * k + 1) * (4 * k + 3);
                result = chislitel / znamenatel;
                sum_result += result;
                k++;
                writer.WriteLine($"{k,-10}\t\t{result,-20}\t{sum_result,-20}");
            } while (Math.Abs(result) >= epsilon);
            writer.Close();
            return sum_result;
        }

        public double Factorial(int n)
        {
            double result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private void Raschet_Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "PROMEJUTOCHNIE_RACHETI.txt";
            Window1 window1 = new Window1(filePath);
            window1.Show();
        }

        private void Otdelniy_Potok_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(Tochnost.Text, out double epsilon) && double.TryParse(Chislo_X.Text, out double x) && x != 0 && epsilon > 0)
            {
                Window2 window2 = new Window2(x, epsilon);
                window2.Show();
            }
            else
            {
                Label4.Content = "Ошибка ввода!";
            }
        }
    }
}
