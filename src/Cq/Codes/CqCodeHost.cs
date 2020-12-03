// References:
// https://github.com/frank-bots/cqhttp.Cyan/blob/master/cqhttp.Cyan/Globals.cs
// https://github.com/frank-bots/cqhttp.Cyan/blob/master/cqhttp.Cyan/Messages/Serialization.cs

using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Kaiheila.OneBot.Cq.Codes
{
    /// <summary>
    /// CQ码主机。
    /// </summary>
    [Export]
    public class CqCodeHost
    {
        /// <summary>
        /// 初始化CQ码主机。
        /// </summary>
        /// <param name="logger">CQ码主机日志记录器。</param>
        public CqCodeHost(
            ILogger<CqCodeHost> logger)
        {
            _logger = logger;

            foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes()
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqCodeAttribute)) is not null)))
            {
                string cqType = (Attribute.GetCustomAttribute(type, typeof(CqCodeAttribute)) as CqCodeAttribute)?.Type;
                if (cqType == null || _codeTypes.ContainsKey(cqType) || type.FullName == null) continue;

                _codeTypes.Add(cqType, type);
            }

            _logger.LogInformation($"加载了{_codeTypes.Count}个CQ码类型。");
        }

        #region Regex

        public static readonly Regex ExtractRegex
            = new Regex(@"\[CQ:\w+?.*?\]");

        public static readonly Regex ParseTypeRegex
            = new Regex(@"\[CQ:(\w+)");

        public static readonly Regex ParseParamsRegex
            = new Regex(@",([\w\-\.]+?)=([^,\]]+)");

        #endregion

        #region Parser

        public CqCodeBase Parse(string code)
        {
            string cqType;

            try
            {
                cqType = ParseTypeRegex.Match(code).Groups[1].Value;
            }
            catch (Exception e)
            {
                throw new CqCodeException("解析CQ码时出现错误。", e, CqCodePart.Struct, code);
            }

            if (string.IsNullOrEmpty(cqType) || !_codeTypes.ContainsKey(cqType))
                throw new CqCodeException($"不支持的CQ码类型：{cqType}", null, CqCodePart.Type, code);

            CqCodeRaw cqCode = new CqCodeRaw();

            try
            {
                foreach (Match match in ParseParamsRegex.Matches(code))
                    cqCode.Params.Add(match.Groups[1].Value, CqCodeEncoder.Decode(match.Groups[2].Value));
            }
            catch (Exception e)
            {
                throw new CqCodeException("解析CQ码参数时出现错误。", e, CqCodePart.Params, code);
            }

            return Activator.CreateInstance(_codeTypes[cqType], cqCode) as CqCodeBase;
        }

        public CqCodeBase Parse(JToken token)
        {
            try
            {
                if (token["type"] is null || token["type"].Type != JTokenType.String)
                    throw new CqCodeException("解析CQ码时出现错误。", null, CqCodePart.Type, token.ToString());

                string cqType = token["type"].ToObject<string>();

                if (string.IsNullOrEmpty(cqType) || !_codeTypes.ContainsKey(cqType))
                    throw new CqCodeException($"不支持的CQ码类型：{cqType}", null, CqCodePart.Type, token.ToString());

                return Activator.CreateInstance(_codeTypes[cqType],
                    new CqCodeRaw(token["data"]?.ToObject<Dictionary<string, string>>())) as CqCodeBase;
            }
            catch (Exception e)
            {
                throw new CqCodeException("解析CQ码时出现错误。", e, CqCodePart.Struct, token.ToString());
            }
        }

        #endregion

        #region Code Types

        private readonly Dictionary<string, Type> _codeTypes = new Dictionary<string, Type>();

        #endregion

        /// <summary>
        /// CQ码主机日志记录器。
        /// </summary>
        private readonly ILogger<CqCodeHost> _logger;
    }

    [Serializable]
    public class CqCodeException : Exception
    {
        public CqCodeException(
            string message = "",
            Exception inner = null,
            CqCodePart part = CqCodePart.Struct,
            string code = ""
            ) : base(message, inner)
        {
            Part = part;
            Code = code;
        }

        public CqCodePart Part;

        public string Code;
    }

    public enum CqCodePart
    {
        Struct = 0,
        Type = 1,
        Params = 2
    }
}
