using System.Collections.Generic;
using Kaiheila.Cqhttp.Cq.Code;

namespace Kaiheila.Cqhttp.Cq.Message
{
    public class CqMessage
    {
        #region Data

        public List<CqCode> CodeList = new List<CqCode>();

        #endregion

        internal CqMessage()
        {

        }
    }
}
