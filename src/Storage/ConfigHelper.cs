using System;
using System.Composition;
using System.IO;
using System.Reflection;
using Kaiheila.OneBot.Cq.Database;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kaiheila.OneBot.Storage
{
    /// <summary>
    /// 提供访问应用配置能力的帮助类型。
    /// </summary>
    [Export]
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
            var databaseFilePath = StorageHelper.GetRootFilePath("database.db");

            if (!File.Exists(ConfigFilePath))
            {
                Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Kaiheila.OneBot"));

                _logger.LogInformation("没有找到配置文件。");

                Stream configFileStream = File.OpenWrite(ConfigFilePath);
                Stream configResourceStream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("Kaiheila.OneBot.Resources.config.json");

                if (configResourceStream is null)
                    throw new ArgumentNullException(nameof(configResourceStream));

                configResourceStream.CopyTo(configFileStream);

                if (!File.Exists(databaseFilePath))
                {
                    using Stream databaseResourceStream = Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("Kaiheila.OneBot.Resources.create_db.sql");

                    if (databaseResourceStream is null)
                        throw new ArgumentNullException(nameof(databaseResourceStream));

                    using StreamReader dbScriptReader = new StreamReader(databaseResourceStream);
                    string createDatabaseScript = dbScriptReader.ReadToEnd();

                    using SqliteConnection connection = new SqliteConnection(CqDatabaseContext.ConnectionString);
                    connection.Open();

                    using var command = connection.CreateCommand();
                    command.CommandText = createDatabaseScript;

                    command.ExecuteNonQuery();
                }

                configFileStream.Close();
                configResourceStream.Close();

                _logger.LogInformation("已经生成了默认的配置文件。修改配置，然后重启应用。");

                Console.ReadKey();
                Environment.Exit(1);
            }

            try
            {
                Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigFilePath)) ?? new Config();
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
}
