using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Nwc.XmlRpc;

namespace XMLRPCServer
{
    class Server
    {

        const int PORT = 5050;

        static public void WriteEntry(String msg, EventLogEntryType type)
        {
            if (type != EventLogEntryType.Information) // ignore debug msgs
                Console.WriteLine("{0}: {1}", type, msg);
        }
        static void Main(string[] args)
        {
            Logger.Delegate = new Logger.LoggerDelegate(WriteEntry);

            XmlRpcServer server = new XmlRpcServer(PORT);
            server.Add("sample", new Server());
            Console.WriteLine("Web Server Running on port {0} ... Press ^C to Stop...", PORT);
            server.Start();
        }

        [XmlRpcExposed]
        public ArrayList Matrix (ArrayList arr, int size) {
            int min = (int)arr[0];
            int i, j;
            int x = 0;
            int y = 0;
            for (j = 0; j < size; j++) {
                for (i = 0; i < size; i++) {
                    if ((int)arr[i + j * size] <= min) {
                        x = i;
                        y = j;
                        min = (int)arr[i + j * size];
                    }
                }
            }
            if (x >= y)
            {
                x = x - y;
                y = 0;
                do
                {
                    arr[x + y * size] = 0;
                    x += 1;
                    y += 1;
                } while ((x + y * size) % size != 0);
            }
            else {
                y = y - x;
                x = 0;
                do
                {
                    arr[x + y * size] = 0;
                    x += 1;
                    y += 1;
                } while (y != size);
            }
            bool flag = false;
            for (int k = size - 1; k > 0; k--)//Заполнение элементов массива, лежащих ниже главной диагонали. Переменная k показывает номер диагонали с левого нижнео края
            {
                if (flag)
                    break;
                i = k;
                j = 0;
                while (i < size)
                {
                    if ((int)arr[j + i * size] == 0) {
                        flag = true;
                        break;
                    }
                    arr[j + i * size] = (int)arr[j + i * size] * (int)arr[j + i * size];
                    i++;
                    j++;
                    if (j == size)
                        break;
                }
            }
            j = 0;
            for (i = 0; i < size; i++)//Заполнение элементов массива, лежащих на главной диагонали
            {
                if (flag)
                    break;
                if ((int)arr[j + i * size] == 0)
                {
                    flag = true;
                    break;
                }
                arr[j + i * size] = (int)arr[j + i * size] * (int)arr[j + i * size];
                j++;
            }
            for (int k = 1; k < size; k++)//Заполнение элементов массива, лежащих выше главной диагонали. Переменная k определяет номер диагонали
            {
                if (flag)
                    break;
                i = 0;
                j = k;
                while ((j < size) && (i < size))
                {
                    if ((int)arr[j + i * size] == 0)
                    {
                        flag = true;
                        break;
                    }
                    arr[j + i * size] = (int)arr[j + i * size] * (int)arr[j + i * size];
                    i++;
                    j++;
                }
            }
            arr.Add(min);
            return arr;
        }
    }
}
