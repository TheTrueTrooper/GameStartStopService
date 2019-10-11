using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStartStopService.SocketServer
{
    class MasterSocket
    {
        Socket ClientSocket;
        IPEndPoint ClientIP;

        Thread TxThread;
        Thread RxThread;

        byte[] SendDataBuffer = new byte[1024];
        byte[] ReceiveDataBuffer = new byte[1024];

        public MasterSocket(Socket ListerSocket)
        {
            ClientSocket = ListerSocket.Accept();
            ClientIP = (IPEndPoint)ClientSocket.RemoteEndPoint;

            TxThread = new Thread(new ParameterizedThreadStart(TransmitThreadLoop));
            RxThread = new Thread(new ParameterizedThreadStart(ReadThreadLoop));
        }

        private static void ReadThreadLoop(object obj)
        {
            int ReceivedDataLength = 0;
            MasterSocket This = (MasterSocket)obj;
            lock(This.ReceiveDataBuffer)
                ReceivedDataLength = This.ClientSocket.Receive(This.ReceiveDataBuffer);
        }

        private static void TransmitThreadLoop(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
