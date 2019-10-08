using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePipeLine;

namespace GameStartAndStopPipeClientNS
{
    /// <summary>
    /// 
    /// </summary>
    public class GameStartAndStopPipeClient
    {
        JSONRequestClientPipe Client = new JSONRequestClientPipe(StaticSharedVars.ServerName);

        public TimeSpan TimeOut
        {
            get
            {
                return Client.StandardTimeout;
            }
            set
            {
                Client.StandardTimeout = value;
            }
        }

        public PipeJSONResponse<PlayGameReturn> PlayGame(string GameGUID)
        {
            return Client.SendCommandRequest<PlayInput, PlayGameReturn>(new PipeJSONAction<PlayInput>() { ActionName = "PlayGame", ActionData = new PlayInput() { GameGUID = GameGUID } });
        }

        public PipeJSONResponse<CanPlayReturn> CanPlayGame(string GameGUID)
        {
            return Client.SendCommandRequest<PlayInput, CanPlayReturn>(new PipeJSONAction<PlayInput>() { ActionName = "CanPlayGame", ActionData = new PlayInput() { GameGUID = GameGUID } });
        }

        public PipeJSONResponse<GetMachineGamesReturn> GetGames()
        {
            return Client.SendCommandRequest<object, GetMachineGamesReturn>(new PipeJSONAction<object>() { ActionName = "GetGames", ActionData = new { } });
        }
    }
}
