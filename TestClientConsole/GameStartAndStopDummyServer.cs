using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartAndStopPipeClientNS
{
    //public class GameStartAndStopDummyServerController
    //{
    //    //JSONPipeServer Server = new JSONPipeServer(StaticSharedVars.ServerName);

    //    [PipeFunction("PlayGame")]
    //    public PipeJSONResponse<PlayGameReturn> PlayGame(PipeJSONAction<PlayInput> This)
    //    {
    //        Console.WriteLine($"Play game called\nWith Data:\n{This}");
    //        PipeJSONResponse<PlayGameReturn> Return = new PipeJSONResponse<PlayGameReturn>() { ActionName = This.ActionName, RequestStatus = PipeJSONResponseStatus.Success };
    //        Return.ActionData = new PlayGameReturn()
    //        {
    //            ActivationDate = DateTime.Now,
    //            Comments = "machine purchase",
    //            CheckNum = 0,
    //            Number = "",
    //            IsActive = true,
    //            IsDeleted = false,
    //            Customer = new CustomerData(),
    //            LimitBalance = 0,
    //            ExpiryDate = DateTime.Now,
    //            ID = new Guid()
    //        };
    //        Console.WriteLine($"Retuning with:\n{Return}");
    //        return Return;
    //    }

    //    [PipeFunction("CanPlayGame")]
    //    public PipeJSONResponse<CanPlayReturn> CanPlayGame(PipeJSONAction<PlayInput> This)
    //    {
    //        Console.WriteLine($"Can play game called\nWith Data:\n{This}");
    //        PipeJSONResponse<CanPlayReturn> Return = new PipeJSONResponse<CanPlayReturn>() { ActionName = This.ActionName, RequestStatus = PipeJSONResponseStatus.Success };
    //        Return.ActionData = new CanPlayReturn()
    //        {
    //            CanPlay = true,
    //            CurrentBalance = 300.00,
    //            NewBalance = 280.00,
    //            NewCheckKey = 3453
    //        };
    //        Console.WriteLine($"Retuning with:\n{Return}");
    //        return Return;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="This">object should be null</param>
    //    /// <returns></returns>
    //    [PipeFunction("GetGames")]
    //    public PipeJSONResponse<GetMachineGamesReturn> GetGames(PipeJSONAction<object> This)
    //    {
    //        Console.WriteLine($"Get Games\nWith Data:\n{This}");
    //        PipeJSONResponse<GetMachineGamesReturn> Return = new PipeJSONResponse<GetMachineGamesReturn>() { ActionData = new GetMachineGamesReturn(), ActionName = This.ActionName, RequestStatus = PipeJSONResponseStatus.Success };
    //        Return.ActionData.Description = "This is the Machines Descriptor.";
    //        Return.ActionData.ID = "0f410d3d-c967-40b3-a222-8010b8040973";
    //        Return.ActionData.IsActive = true;
    //        Return.ActionData.MachineName = "The Swirler";
    //        Return.ActionData.VRStoreID = "1e74528f-1698-4af0-a000-3e731c4c6b0f";
    //        Return.ActionData.IsAttendantRequired = false;
    //        Return.ActionData.VRMachineGames = new List<Games>()
    //        {
    //            new Games()
    //            {
    //                Description = "This is the Games description.",
    //                GUID = "1316e2e1-e19b-4e49-bdf1-ca4a24bf609b",
    //                CostToPlay = 20.00,
    //                Name = "Phantom Breaker: Battle Grounds",
    //                PlayTime = 10,
    //                IsActive = true,
    //                IsDeleted = false,
    //                ImagePath = "insert img path here.",
    //                Path = "I:\\Games\\Steam\\steamapps\\common\\Phantom Breaker Battle Grounds\\bin\\pbbg_win32.exe",
    //                MaxNumberShares = 0,
    //                MinNumberShares =0,
    //                StartOptions = null,
    //            },
    //            new Games()
    //            {
    //                Description = "This is the Games description.",
    //                GUID = "1316e2e1-e19b-4e49-bdf1-ca4a24bf609c",
    //                CostToPlay = 20.00,
    //                Name = "Phantom Breaker: Battle Grounds 3",
    //                PlayTime = 10,
    //                IsActive = true,
    //                IsDeleted = false,
    //                ImagePath = "insert img path here.",
    //                Path = "I:\\Games\\Steam\\steamapps\\common\\Phantom Breaker Battle Grounds\\bin\\pbbg2_win32.exe",
    //                MaxNumberShares = 0,
    //                MinNumberShares =0,
    //                StartOptions = null,
    //            }
    //        };
    //        Console.WriteLine($"Retuning with:\n{Return}");
    //        return Return;
    //    }

    //}
}
