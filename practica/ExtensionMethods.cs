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
        public static float MultiplyVectors
            (this ObservableCollection<float> source, ObservableCollection<float> factor)
        {
            var result = 0f;
            for(int i = 0; i < source.Count; i++)
            {
                result+=source[i] *= factor[i];
            }
            return result;
        }
    }
}
