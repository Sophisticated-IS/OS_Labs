using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LogicLib
{
    public sealed class MainLogic
    {

        public BigInteger MultiplyBigIntegers(BigInteger a,BigInteger b) => a*b;

        public double[] GetUniqueValues(string FileInput,string FileOutput)
        {
            var mass = File.ReadAllText(FileInput);
            var arr = mass.Split(' ').Select(double.Parse);
            var resArray = arr.Distinct().ToArray();

            var sb = new StringBuilder();
            foreach (var number in resArray)
            {
                sb.Append(number + " ");
            }
            File.WriteAllText(FileOutput,sb.ToString());
            return resArray;
        }

        public string GetMostFrequentWord(string FileName)
        {
            var text = File.ReadAllText(FileName).Replace(",","")
                .Replace(".","").Replace("!","");
            
            var frequencies = new Dictionary<string, int>();

            foreach (var word in text.Split(' ').Select(str=>str.ToUpperInvariant()))
            {
                if (!frequencies.ContainsKey(word))
                {
                    frequencies.Add(word,1);
                }
                else
                {
                    frequencies[word]++;
                }
            }

            var maxValue = frequencies.Max(pair => pair.Value);
            var res = frequencies.First(w => w.Value == maxValue);
            return res.Key;
        }

        public Tuple<double, double> GetMaximumsMatrix(int[,] matrix)
        {
            var listLowerMainDiagonal = new List<double>();
            var listUpperPobochDiagonal = new List<double>();
            var n = matrix.GetLength(0);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    
                    if (i>j) listLowerMainDiagonal.Add(matrix[i,j]);
                    
                    if (i+j<n-1) listUpperPobochDiagonal.Add(matrix[i,j]);
                    
                }
            }

            var maxMainDiag = listLowerMainDiagonal.Max();
            var maxPobochDiag = listUpperPobochDiagonal.Max();
            
            return new Tuple<double, double>(maxMainDiag,maxPobochDiag);
        }
        
        public List<uint> SieveEratosthenes(uint n)
        {
            var numbers = new List<uint>();
            //заполнение списка числами от 2 до n-1
            for (var i = 2u; i < n; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2u; j < n; j++)
                {
                    //удаляем кратные числа из списка
                    numbers.Remove(numbers[i] * j);
                }
            }

            return numbers;
        }

    }
}