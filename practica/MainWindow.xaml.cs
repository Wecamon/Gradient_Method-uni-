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

        private void Calculate_OnClick(object sender, RoutedEventArgs e)
        {
            int iterations = int.Parse(AccuracyInputBox.Text);
            var vectorResult = new ObservableCollection<float>(matrix3.ParseVectorToFloat());
            var vectorBiParsed = matrix2.ParseVectorToFloat();
            var matrixAiParsed = matrix.ParseMatrixToFloat();
            Func
                <ObservableCollection<ObservableCollection<float>>,
                ObservableCollection<float>,
                ObservableCollection<float>>
                MultiplyByVector =
                (source, vector) =>
                {
                    var result = new ObservableCollection<float>();
                    var temp = 0f;
                    for (int i = 0; i < vector.Count; i++)
                    {
                        temp = 0f;
                        for (int j = 0; j < vector.Count; j++)
                        {
                            temp += source[i][j] * vector[j];
                        }
                        result.Add(temp);
                    }
                    return result;
                };
            Func
                <ObservableCollection<float>,
                ObservableCollection<float>,
                ObservableCollection<float>>
                VectorSubstraction =
                (source, substValue) =>
                {
                    var result = new ObservableCollection<float>();
                    for (int i = 0; i < source.Count; i++)
                    {
                        result.Add(source[i] - substValue[i]);
                    }
                    return result;
                };
            Func
                    <ObservableCollection<float>,
                    float, ObservableCollection<float>>
                    VectorByNumber =
                    (source, number) =>
                    {
                        for (int i = 0; i < source.Count; i++)
                            source[i] *= number;
                        return source;
                    };
            Func
                <ObservableCollection<float>,
                ObservableCollection<float>,
                ObservableCollection<float>>
                VectorSums =
                (vec1, vec2) =>
                {
                    var result = new ObservableCollection<float>();
                    for (int i = 0; i < vec1.Count; i++)
                        result.Add(vec1[i] + vec2[i]);
                    return result;
                };
            for (int k = 0; k < iterations; k++)
            {
                var vectorAnX = MultiplyByVector(matrixAiParsed, vectorResult);

                // создаём вектор невязок
                var vectorRk = VectorSubstraction(vectorBiParsed, vectorAnX);

                // создаём поинтер на матрицу A
                var denominated = MultiplyByVector(matrixAiParsed, vectorRk).MultiplyVectors(vectorRk);
                var denominator = MultiplyByVector(matrixAiParsed, vectorRk)
                    .MultiplyVectors(MultiplyByVector(matrixAiParsed, vectorRk));

                var stepAlphaK = denominated / denominator;

                vectorResult = VectorSums(vectorResult, VectorByNumber(vectorRk, stepAlphaK));
            }
        }

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

                for (int j = 0; j < n; j++)
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
    }
}
