using Base.Core;
using Base.UI.MessageBox;
using System;
using System.IO;

/// Taken from Assorted Adjustments

namespace SkipIntro.Helpers
{
    public class FileLogger
    {
        private static string _logPath;
        private static int _debugLevel;
        private static string _modName;
        private static bool _awake;

        public const int ALWAYS = 0;
        public const int ERROR = 1;
        public const int DEBUG = 2;
        public const int INFO = 3;

        public static void Initialize(string logPath, int debugLevel, string modName)
        {
            _logPath = logPath;
            _debugLevel = debugLevel;
            _modName = modName;
            _awake = true;

            Cleanup();
            AlwaysDivider();
            Always($"Logger.Initialize(logPath: {logPath}, debugLevel: {debugLevel})");
            AlwaysDivider();
        }

        public static void Sleep()
        {
            _awake = false;
        }

        public static void Wake()
        {
            _awake = true;
        }

        public static void Cleanup()
        {
            using (StreamWriter writer = new StreamWriter(_logPath, false))
            {
                writer.WriteLine("----------------------------------------------------------------------------------------------------", false);
                writer.WriteLine($"[{_modName} @ {DateTime.Now}] CLEANED UP");
                writer.WriteLine("----------------------------------------------------------------------------------------------------", false);
            }
        }

        public static void Error(Exception ex)
        {
            if (_awake && _debugLevel >= ERROR)
            {
                using (StreamWriter writer = new StreamWriter(_logPath, true))
                {
                    writer.WriteLine("----------------------------------------------------------------------------------------------------", false);
                    writer.WriteLine($"[{_modName} @ {DateTime.Now}] EXCEPTION:");
                    writer.WriteLine("Message: " + ex.Message + "<br/>" + Environment.NewLine + "StackTrace: " + ex.StackTrace);
                    writer.WriteLine("----------------------------------------------------------------------------------------------------", false);
                }
                GameUtl.GetMessageBox().ShowSimplePrompt($"<b>An error has occurred in the BetterClasses mod!</b>\nPlease check {_logPath} for further information.\n\n<b>CAUTION:</b>\nContinuing this run may result in unstable behavior or even cause the game to crash.", MessageBoxIcon.Warning, MessageBoxButtons.OK, null);
            }
        }

        public static void Debug(string line, bool showPrefix = true)
        {
            if (_awake && _debugLevel >= DEBUG)
            {
                using (StreamWriter writer = new StreamWriter(_logPath, true))
                {
                    string prefix = showPrefix ? $"[{_modName} @ {DateTime.Now}] " : "";
                    writer.WriteLine(prefix + line);
                }
            }
        }

        public static void Info(string line, bool showPrefix = true)
        {
            if (_awake && _debugLevel >= INFO)
            {
                Debug(line, showPrefix);
            }
        }

        public static void Always(string line, bool showPrefix = true)
        {
            using (StreamWriter writer = new StreamWriter(_logPath, true))
            {
                string prefix = showPrefix ? $"[{_modName} @ {DateTime.Now}] " : "";
                writer.WriteLine(prefix + line);
            }
        }

        public static int GetDebugLevel()
        {
            return _debugLevel;
        }

        public static void AlwaysDivider()
        {
            Always("----------------------------------------------------------------------------------------------------", false);
        }

        public static void DebugDivider()
        {
            Debug("---debug--------------------------------------------------------------------------------------------", false);
        }

        public static void InfoDivider()
        {
            Info("---info---------------------------------------------------------------------------------------------", false);
        }
    }
}