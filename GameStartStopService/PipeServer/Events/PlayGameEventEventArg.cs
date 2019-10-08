using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStartStopService.PipeServer;

namespace GameStartStopService.PipeServer.Events
{
    public class PlayGameEventArgs : EventArgs
    {
        public PlayInput PlayInput;

        internal PlayGameEventArgs(PlayInput PlayInput)
        {
            this.PlayInput = PlayInput;
        }
    }
}
