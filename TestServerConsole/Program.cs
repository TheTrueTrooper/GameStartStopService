using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServicePipeLine;

namespace TestServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //MasterSocketServer Server = new MasterSocketServer();
            Application.EnableVisualStyles();
            Application.Run(new DummyForm());

        }
        
    }
}
