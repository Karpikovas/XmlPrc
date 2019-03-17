using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwc.XmlRpc;
using System.Diagnostics;

namespace XMLRPCClient
{
    class Program
    {
        static ServersideObject obj;
        /// <summary>Simple logging to Console.</summary>
        /// 
        static public void WriteEntry(String msg, EventLogEntryType type)
        {
            if (type != EventLogEntryType.Information) // ignore debug msgs
                Console.WriteLine("{0}: {1}", type, msg);
        }
        
        static void Main(string[] args)
        {
            obj = new ServersideObject("http://127.0.0.1:5050");
            ArrayList newArr = null;
            ArrayList arr = new ArrayList();
            while (true)
            {
                Console.Write("Введите размер матрицы: ");
                int size = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите матрицу: ");
                string[] str_arr;
                for (int i = 0; i < size; ++i)
                {
                    str_arr = (Console.ReadLine()).Split(' ');
                    for (int j = 0; j < size; j++)
                        arr.Add(Convert.ToInt32(str_arr[j]));
                }
                Console.Clear();
                Console.WriteLine("Исходная матрица:\n");
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                        Console.Write(arr[j + i * size] + "\t");
                    Console.WriteLine("\n");
                }
                newArr = obj.Matrix(arr, size);
                Console.WriteLine("\nМинимальный элемент: " + newArr[size*size] + "\n");
                Console.WriteLine("Результирующая матрица:\n");
                newArr.RemoveAt(size * size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                        Console.Write(newArr[j + i * size] + "\t");
                    Console.WriteLine("\n");
                }
                Console.WriteLine("\n\nНажмите для продолжения... ");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
