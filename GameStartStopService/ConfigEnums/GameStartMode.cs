﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.ConfigEnums
{
    [JsonConverter(typeof(StringEnumConverter))]
    enum GameStartMode
    {
        SingleGameStarter,
        MultiSocketStarterMaster,
        MultiSocketStarterSlave,
        AttendantChargeDeskOnly
    }
}
