using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nwc.XmlRpc;

namespace XMLRPCClient
{
    class ServersideObject
    {
        XmlRpcRequest client;
        String host = "http://127.0.0.1:5050";

        public ServersideObject()
        {
             client = new XmlRpcRequest();
        }
        public ServersideObject(String host)
        {
            this.host = host;
            client = new XmlRpcRequest();
        }
        
        public ArrayList Matrix(ArrayList arr, int size)
        {
            XmlRpcResponse response;
            ArrayList matrix = null;
            client.MethodName = "sample.Matrix";
            client.Params.Clear();
            client.Params.Add(arr);
            client.Params.Add(size);
            //Console.WriteLine(client);
            response = client.Send(host);
            if (response.IsFault)
            {
                Console.WriteLine("Fault {0}: {1}", response.FaultCode, response.FaultString);
                return null;
            }
            else
            {
                matrix = (ArrayList)response.Value;
                return matrix;
            }
        }
    }
}
