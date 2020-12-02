using System.Collections.Generic;
using Kaiheila.Cqhttp.Cq.Codes;

namespace Kaiheila.Cqhttp.Cq.Message
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
