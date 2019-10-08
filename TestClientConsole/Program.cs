using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameStartAndStopPipeClientNS;
using System.Reflection;
using System.IO.Pipes;
using System.IO;
using System.Security.Principal;
using RL8000_NFCReader;
using RL8000_NFCReader.MifareClassicEvents;
using RL8000_NFCReader.NFCCardTypes;
using RL8000_NFCReader.MifareClassicControlEnums;

namespace TestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region MCPipesTesting
            //string cmd = "{{ \"ActionData\": { },\"ActionName\":\"GetGames\"}}";

            //NamedPipeClientStream PipeClient = new NamedPipeClientStream(".", "GameStartAndStopServicePipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None);
            //StreamReader Rx = new StreamReader(PipeClient);
            //StreamWriter Tx = new StreamWriter(PipeClient);

            //PipeClient.Connect();
            //PipeClient.ReadMode = PipeTransmissionMode.Message;
            //Tx.WriteLine(cmd);
            //Tx.Flush();

            //while (Rx.Peek() == 0)
            //{

            //}

            //foreach (Assembly Asm in AppDomain.CurrentDomain.GetAssemblies())
            //{
            //    Console.WriteLine(Asm.FullName);
            //}
            //GameStartAndStopPipeClient Client = new GameStartAndStopPipeClient();
            //{
            //    PipeJSONResponse<GetMachineGamesReturn> Return = Client.GetGames();
            //    Console.WriteLine(Return.ToString().Replace("}", "}\n").Replace(",", ",\n").Replace("}\n,", "},"));
            //}
            //{
            //    PipeJSONResponse<CanPlayReturn> Return = Client.CanPlayGame("bb0bde2e-2296-4fbe-9ed8-0b34baf10a2e");
            //    Console.WriteLine(Return.ToString().Replace("}", "}\n").Replace(",", ",\n").Replace("}\n,", "},"));
            //}
            //{
            //    PipeJSONResponse<PlayGameReturn> Return = Client.PlayGame("bb0bde2e-2296-4fbe-9ed8-0b34baf10a2e");
            //    Console.WriteLine(Return.ToString().Replace("}", "}\n").Replace(",", ",\n").Replace("}\n,", "},"));
            //}
            //Console.ReadKey();
            #endregion
            #region GameMenuTesting
            //GameMenu gameMenu = new GameMenu(new GameStartAndStopDummyServerController());

            //bool Run = true;

            //gameMenu.StartMenu();

            //Console.WriteLine("Press T to test a card tap and e to exit.");

            //do
            //{
            //    ConsoleKey Key = Console.ReadKey().Key;
            //    switch (Key)
            //    {
            //        case ConsoleKey.T:
            //            PipeJSONResponse<object> Response = gameMenu.NotifyOfCardTap();
            //            Console.WriteLine($"Response back. {Response}");
            //            break;
            //        case ConsoleKey.E:
            //            Run = false;
            //            break;
            //    }
            //} while (Run);

            //gameMenu.KillMenu();

            //Console.ReadKey();
            #endregion

            RL8000_NFC CardReader = new RL8000_NFC();
            CardReader.MifareClassicISO1443ACardDetectedEvent += CardDetected;


            Console.ReadKey();
        }

        static void CardDetected(object sender, MifareClassicISO1443ACardDetectedEventArg e)
        {
            Console.WriteLine("Card Detected");
            Console.WriteLine($"Card Info:\n\tAir Protocol:{e.CardInfo.AirProtocalID}\n\tAntennaID:{e.CardInfo.AntennaID}\n\tDSFID:{e.CardInfo.DSFID}\n\tTagID:{e.CardInfo.TagID}\n\tUID:{BitConverter.ToString(e.CardInfo.UID, 0, e.CardInfo.UIDlen)}");
            ISO1443A_MifareClassic_NFCCard Card = e.Reader.ConnectAs_ISO14443A_MifareClassic_NFC(e.CardInfo);
            Card.Athenthicate(0, new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, KeyTypes.KeyA);
            Console.WriteLine("Successfuly athenticated block 0.");
            Console.WriteLine($"{BitConverter.ToString(Card.ReadBlock(0))}");
            Card.Athenthicate(5, new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF }, KeyTypes.KeyA);
            Card.WriteValue(5, 500);
            Console.WriteLine($"{BitConverter.ToString(Card.ReadBlock(5))}");
            Console.WriteLine($"{Card.ReadValue(5)}");
        }
    }
}
