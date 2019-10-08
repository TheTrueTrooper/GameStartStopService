using GameInstancerNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.ConfigLoaders
{
    class GameConfigLoader : IGameConfig
    {
        public IGameModel this[string ID] => throw new NotImplementedException();

        public List<IGameModel> Games => throw new NotImplementedException();

        public IGameModel GetGameByName(string GameName)
        {
            throw new NotImplementedException();
        }
    }
}
