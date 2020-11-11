using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kaiheila.Cqhttp.Storage
{
    /// <summary>
    /// 提供访问应用配置能力的帮助类型。
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 初始化应用配置。
        /// </summary>
        /// <param name="logger">配置日志记录器。</param>
        public ConfigHelper(ILogger<ConfigHelper> logger)
        {
            _logger = logger;
            _logger.LogInformation("开始加载配置。");

            ReloadConfig();
        }

        /// <summary>
        /// 重载应用配置。
        /// </summary>
        public void ReloadConfig()
        {
            ConfigFilePath = StorageHelper.GetRootFilePath("config.json");

            if (!File.Exists(ConfigFilePath))
            {
                _logger.LogCritical("无法找到配置文件。");

                // TODO: 使用预先配置完毕的配置文件替换自动生成的配置
                File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(new Config(), Formatting.Indented));

                _logger.LogInformation("已经生成了默认的配置文件。修改配置，然后重启应用。");

                Console.ReadKey();
                Environment.Exit(1);
            }

            try
            {
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigFilePath));
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "加载配置文件时发生了错误。");

                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// 配置日志记录器。
        /// </summary>
        private readonly ILogger<ConfigHelper> _logger;

        /// <summary>
        /// 配置文件的完整路径。
        /// </summary>
        public string ConfigFilePath;

        /// <summary>
        /// 应用配置。
        /// </summary>
        public Config Config;
    }

    /// <summary>
    /// 应用配置。
    /// </summary>
    public class Config
    {
    }
}
