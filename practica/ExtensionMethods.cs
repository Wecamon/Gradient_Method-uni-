using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace practica
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<ObservableCollection<float>> ParseMatrixToFloat(this ObservableCollection<ObservableCollection<string>> source)
        {
            var result = new ObservableCollection<ObservableCollection<float>>();

            foreach(var col in source) {
                var temp = new ObservableCollection<float>();
                foreach (var item in col) 
                {
                    temp.Add(float.Parse(item));
                }
                result.Add(temp);
            }
            return result;
        }
        public static ObservableCollection<float> ParseVectorToFloat(this IEnumerable<IEnumerable<string>> source)
        {
            var result = new ObservableCollection<float>();
            foreach (ObservableCollection<string> collection in source)
            {
                foreach (string item in collection)
                {
                    result.Add(float.Parse(item));
                    break;
                }
            }
            return result;
        }
        public static ObservableCollection<float> MultiplyByVector
            (this ObservableCollection<ObservableCollection<float>> source, ObservableCollection<float> vector)
        {
            var result = new ObservableCollection<float>();
            var temp=0f;
            for(int i = 0; i < vector.Count; i++)
            {
                temp = 0f;
                for(int j = 0; j < vector.Count; j++)
                {
                    temp += source[i][j] * vector[j];
                }
                result.Add(temp);
            }
            return result;
        }

        public static void VectorSubstraction
            (this ObservableCollection<float> source, ObservableCollection<float> substValue)
        {
            for (int i = 0; i < source.Count; i++)
            {
                source[i] -= substValue[i];
            }
        }
    }
}
