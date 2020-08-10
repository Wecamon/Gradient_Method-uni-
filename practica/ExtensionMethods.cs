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
        public static ObservableCollection<float> ParseToFloat(this IEnumerable<IEnumerable<string>> source)
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
        public static void MultiplyByNumber(this ObservableCollection<float> source, float number)
        {
            for (int i = 0; i < source.Count; i++)
            {
                source[i] *= number;
            }
        }

        public static void Substraction
            (this ObservableCollection<float> source, ObservableCollection<float> substValue)
        {
            for (int i = 0; i < source.Count; i++)
            {
                source[i] -= substValue[i];
            }
        }
    }
}
