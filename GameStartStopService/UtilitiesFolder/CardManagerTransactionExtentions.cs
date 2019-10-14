using GameStartStopService.TheServerClient;
using GameStartStopService.TheServerClient.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RL8000_NFCReader;
using RL8000_NFCReader.NFCCardTypes;

namespace GameStartStopService.UtilitiesFolder
{
    public static class CardManagerTransactionExtentions
    {

        #region Depreciated
        //sigature internal static ResponseInfo<CheckCardReturn, ResponseStatus> CheckCardTransactionWithServer(this ACR122UManager This, ServerClient Client)
        //{
        //    ResponseInfo<CheckCardReturn, ResponseStatus> Response = null;

        //    int CardCheckValue = 0;
        //    string CardNumber = null;

        //    try
        //    {
        //        byte[] Data;
        //        This.ReadBlock(out Data, 8);
        //        CardNumber = Encoding.ASCII.GetString(Data);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new CardTransationException(CardTransationExceptionLocation.CardNumberReadFail, e.Message, e);
        //    }
        //    try
        //    {
        //        Int32 Data;
        //        This.ReadValueFromBlock(out Data, 9);
        //        CardCheckValue = Data;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new CardTransationException(CardTransationExceptionLocation.CardCheckValueReadFail, e.Message, e);
        //    }
        //    try
        //    {
        //        Response = Client.CardCheck(CardNumber, CardCheckValue);
        //        if(Response.Status != ResponseStatus.Success)
        //            throw new CardTransationException(CardTransationExceptionLocation.ServerCardCheckFail, Response.Message, null);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new CardTransationException(CardTransationExceptionLocation.ServerCardCheckFail, e.Message, e);
        //    }
        //    try
        //    {
        //        Int32 Data = (Int32)Response.Data.CheckNum;
        //        This.WriteValueToBlock(Data, 9);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new CardTransationException(CardTransationExceptionLocation.CardCheckValueWriteFail, e.Message, e);
        //    }
        //    return Response;
        //}
        #endregion

        //deprecated sigature internal static ResponseInfo<PlayGameReturn, ResponseStatus> PlayGameTransactionWithServer(this ACR122UManager This, ServerClient Client, string GUID)
        internal static ResponseInfo<CanPlayTransactionReturn, ResponseStatus> CanPlayGameTransactionWithServer(this ISO1443A_MifareClassic_NFCCard This, string GUID)
        {
            ResponseInfo<CanPlayReturn, ResponseStatus> Response = null;

            int CardCheckValue = 0;
            string CardNumber = null;

            try
            {
                #region Depreciated
                //byte[] Data;
                //This.ReadBlock(out Data, 8);
                //CardNumber = Encoding.ASCII.GetString(Data);
                #endregion
                Encoding.ASCII.GetString(This.ReadBlock(8));
            }
            catch (Exception e)
            {
                throw new CardTransationException(CardTransationExceptionLocation.CardNumberReadFail, e.Message, e);
            }
            try
            {
                #region Depreciated
                //Int32 Data;
                //This.ReadValueFromBlock(out Data, 9);
                //CardCheckValue = Data;
                #endregion
                Int32 Data = (Int32)This.ReadValue(9);
            }
            catch (Exception e)
            {
                throw new CardTransationException(CardTransationExceptionLocation.CardCheckValueReadFail, e.Message, e);
            }
            try
            {
                Response = ArcadeGameStartAndStopService.TheServerClient.CanPlayGame(CardNumber, CardCheckValue, GUID);
                if (Response.Status != ResponseStatus.Success)
                    throw new CardTransationException(CardTransationExceptionLocation.ServerCardCheckFail, Response.Message, null);
            }
            catch (Exception e)
            {
                throw new CardTransationException(CardTransationExceptionLocation.ServerCardCheckFail, e.Message, e);
            }
            try
            {
                UInt32 Data = (UInt32)Response.Data.NewCheckKey;
                This.WriteValue(9, Data);
            }
            catch (Exception e)
            {
                throw new CardTransationException(CardTransationExceptionLocation.CardCheckValueWriteFail, e.Message, e);
            }
            return new ResponseInfo<CanPlayTransactionReturn, ResponseStatus> { Status = Response.Status, PageInfo = Response.PageInfo, Message = Response.Message, Data = new CanPlayTransactionReturn() { CardGUID = CardNumber, CanPlay = Response.Data.CanPlay, CurrentBalance = Response.Data.CurrentBalance, NewBalance = Response.Data.NewBalance, NewCheckKey = Response.Data.NewCheckKey } };
        }
    }
}
