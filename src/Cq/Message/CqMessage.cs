using System.Collections.Generic;
using Kaiheila.OneBot.Cq.Codes;

namespace Kaiheila.OneBot.Cq.Message
{
    public class CqMessage
    {
        #region Data

        public List<CqCodeBase> CodeList = new List<CqCodeBase>();

        #endregion

        internal CqMessage()
        {

        }
    }
}
