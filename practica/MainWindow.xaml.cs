using System;
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
using System.Collections.ObjectModel;
using practica;

namespace Practica
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly ObservableCollection<ObservableCollection<string>> matrix = new ObservableCollection<ObservableCollection<string>>();
        private readonly ObservableCollection<ObservableCollection<string>> matrix2 = new ObservableCollection<ObservableCollection<string>>();
        private readonly ObservableCollection<ObservableCollection<string>> matrix3 = new ObservableCollection<ObservableCollection<string>>();
        private int n = 0;



        private void Create(int countColumn) // заполнение значений в матрицу и грид
        {
            matrix.Clear();
            matrix2.Clear();
            matrix3.Clear();
            matrixA.Columns.Clear();
            matrixB.Columns.Clear();
            matrixC.Columns.Clear();

            for (int i = 0; i < n; i++)
            {
                var m = new ObservableCollection<string>();
                var m2 = new ObservableCollection<string>();
                var m3 = new ObservableCollection<string>();

                for (int j = 0; j < n + 1; j++)
                {
                    m.Add("0");
                    m2.Add("0");
                    m3.Add("0");
                }
                matrix.Add(m);
                matrix2.Add(m2);
                matrix3.Add(m3);
            }

            matrixA.ItemsSource = matrix;
            matrixB.ItemsSource = matrix2;
            matrixC.ItemsSource = matrix3;

            if (countColumn > 0)
            {

                for (int i = 0; i < countColumn; i++)
                {
                    DataGridTextColumn column = new DataGridTextColumn
                    {
                        Header =  (i + 1).ToString(),
                        Binding = new Binding(String.Format("[{0}]", i))
                    };
                    matrixA.Columns.Add(column);
                }
            }

            DataGridTextColumn column1 = new DataGridTextColumn
            {
                Header = "1",
                Binding = new Binding(String.Format("[0]")) 
            };
            matrixB.Columns.Add(column1);



            DataGridTextColumn column2 = new DataGridTextColumn
            {
                Header = "1",
                Binding = new Binding(String.Format("[0]"))
            };
            matrixC.Columns.Add(column2);
        }


        private void Generate_Click(object sender, RoutedEventArgs e)  //генерация матрицы
        {

            if (Int32.TryParse(N_Size.Text, out n))
            {
                if ((int.Parse(N_Size.Text) > 0))
                    Create(n);
                else
                    MessageBox.Show("Введите число больше нуля");
            }
            else
            {
                MessageBox.Show("Некорректный ввод размерности");
            }
        }



        private void DG_LoadingRow(object sender, DataGridRowEventArgs e) =>
            e.Row.Header = e.Row.GetIndex() + 1;

        private void Calculate_OnClick(object sender, RoutedEventArgs e)
        {
            float accuracy = float.Parse(AccuracyInputBox.Text);
            ObservableCollection<float> matrixAiParsed = matrix.ParseToFloat();
            ObservableCollection<float> matrixBiParsed = matrix2.ParseToFloat();
            matrixAiParsed.MultiplyByNumber(accuracy);
            matrixBiParsed.Substraction(matrixAiParsed);
            // создаём вектор невязок
            var vectorRk = new ObservableCollection<float>(matrixBiParsed);
            
            
            
            int a = 0;

            #region oldFunctions

            //Func<ObservableCollection<float>, float,ObservableCollection<float>> MultiplyByNumber = (source, number) =>
            //{
            //    for (int i = 0; i < source.Count; i++)
            //        source[i] *= number;
            //
            //    return source;
            //};
            //Func<ObservableCollection<ObservableCollection<string>>, ObservableCollection<float>> MatrixParser = (source) =>
            //{
            //    var result = new ObservableCollection<float>();
            //    foreach (ObservableCollection<string> collection in source)
            //    {
            //        foreach (string item in collection)
            //        {
            //            result.Add(float.Parse(item));
            //            break;
            //        }
            //    }
            //    return result;
            //};

            #endregion
        }
    }
}



