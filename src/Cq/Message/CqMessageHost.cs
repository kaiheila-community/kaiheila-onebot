using System.Text.RegularExpressions;
using Kaiheila.Cqhttp.Cq.Code;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kaiheila.Cqhttp.Cq.Message
{
    /// <summary>
    /// CQ消息主机。
    /// </summary>
    public class CqMessageHost
    {
        /// <summary>
        /// 初始化CQ消息主机。
        /// </summary>
        /// <param name="cqCodeHost">CQ码主机。</param>
        public CqMessageHost(
            CqCodeHost cqCodeHost)
        {
            _cqCodeHost = cqCodeHost;
        }

        #region Parser

        public CqMessage Parse(string raw)
        {
            CqMessage message = new CqMessage();

            Match match = CqCodeHost.ExtractRegex.Match(raw);
            while (match.Success)
            {
                if (match.Index > 0) message.CodeList.Add(new CqCodeText(raw.Substring(0, match.Index)));

                message.CodeList.Add(_cqCodeHost.Parse(match.Value));
                raw = raw.Substring(match.Index + match.Length);
                match = CqCodeHost.ExtractRegex.Match(raw);
            }

            if (raw.Length != 0) message.CodeList.Add(new CqCodeText(raw));

            return message;
        }

        public CqMessage Parse(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.String:
                    return Parse(token.ToObject<string>());
                case JTokenType.Array:
                    CqMessage message = new CqMessage();
                    foreach (JToken cqCodeToken in token) message.CodeList.Add(_cqCodeHost.Parse(cqCodeToken));
                    return message;
                default:
                    throw new JsonReaderException("不支持的CQ消息。消息必须是字符串或数组格式。");
            }
        }

        #endregion

        private CqCodeHost _cqCodeHost;
    }
}
