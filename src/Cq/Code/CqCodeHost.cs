// References:
// https://github.com/frank-bots/cqhttp.Cyan/blob/master/cqhttp.Cyan/Globals.cs
// https://github.com/frank-bots/cqhttp.Cyan/blob/master/cqhttp.Cyan/Messages/Serialization.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Kaiheila.Cqhttp.Cq.Code
{
    /// <summary>
    /// CQ码主机。
    /// </summary>
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
                    .Where(type => Attribute.GetCustomAttribute(type, typeof(CqCodeTypeAttribute)) is not null)))
            {
                string cqType = (Attribute.GetCustomAttribute(type, typeof(CqCodeTypeAttribute)) as CqCodeTypeAttribute)?.Type;
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

        #region Encoder

        private static string Decode(string enc) => 
            enc
                .Replace("&#91;", "[")
                .Replace("&#93;", "]")
                .Replace("&#44;", ",")
                .Replace("&amp;", "&");

        #endregion

        #region Parser

        public CqCode Parse(string code)
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

            CqCode cqCode = new CqCode();

            foreach (Match match in ParseParamsRegex.Matches(code))
                cqCode.Params.Add(match.Groups[1].Value, Decode(match.Groups[2].Value));

            return Assembly.GetExecutingAssembly()
                    .CreateInstance(
                        _codeTypes[cqType].FullName!,
                        false,
                        BindingFlags.Default,
                        null,
                        new object[] {cqCode},
                        null,
                        null)
                as CqCode;
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
