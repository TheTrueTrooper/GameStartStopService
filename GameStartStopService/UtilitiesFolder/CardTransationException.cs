using GameStartStopService.TheServerClient.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.UtilitiesFolder
{
    public class CardTransationException : Exception
    {
        public CardTransationExceptionLocation CardTransationExceptionLocation { private set; get; }
        public CardTransationException(CardTransationExceptionLocation CardTransationExceptionLocation, string Message, Exception InnerException) : base(Message, InnerException)
        {
            this.CardTransationExceptionLocation = CardTransationExceptionLocation;
        }
    }
}
