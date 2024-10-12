using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace _316a_Vorobiev_Dmitriy_Variant_2
{
    public partial class Window2 : Window
    {
        private BackgroundWorker worker;
        private double x;
        private double epsilon;

        public Window2(double x, double epsilon)
        {
            InitializeComponent();
            this.x = x;
            this.epsilon = epsilon;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;
            worker.RunWorkerCompleted += Completed;
            worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            double sum_result = 0;
            double result;
            int k = 0;
            do
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true; 
                    return;
                }
                double chislitel = Math.Pow(-1, k) * Math.Pow(x, 4 * k + 3);
                double znamenatel = Factorial(2 * k + 1) * (4 * k + 3);
                result = chislitel / znamenatel;
                sum_result += result;
                worker.ReportProgress(k, new { Iteration = k + 1, Result = result, Sum = sum_result });
                k++;
                System.Threading.Thread.Sleep(100);
            } while (Math.Abs(result) >= epsilon);
            e.Result = sum_result;
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var data = (dynamic)e.UserState;
            table.AppendText($"Итерация: {data.Iteration}, Результат: {data.Result}, Сумма: {data.Sum}\n");

            if (table.Visibility == Visibility.Visible)
            {
                table.ScrollToEnd();
            }
        }

        private void Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                table.AppendText("Операция прервана пользователем\n");
            }
            else
            {
                table.AppendText($"Операция выполнена, итоговый результат = {e.Result}\n");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
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
    }
}
