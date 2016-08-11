using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Log
{
    public class FileLogHandler : ILogHandler
    {
        private FileStream m_FileStream;
        private StreamWriter m_StreamWriter;
        private ILogHandler m_DefaultLogHandler = Debug.logger.logHandler;
        private const string logDir = "/Log/";

        public FileLogHandler()
        {
            InitLogFile();

            // Replace the default debug log handler
            Debug.logger.logHandler = this;
        }

        /// <summary>
        /// 初始化日志文件
        /// </summary>
        private void InitLogFile()
        {
            DateTime now = DateTime.Now;
            string logName = string.Format("{0}{1}{2}.log", now.Year, now.Month, now.Day);
            string dir = Application.streamingAssetsPath + logDir;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string filePath = dir + logName;

            m_FileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            m_StreamWriter = new StreamWriter(m_FileStream);
        }

        public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                m_DefaultLogHandler.LogFormat(logType, context, format, args);
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                m_StreamWriter.WriteLine(string.Format(format, args));
                m_StreamWriter.Flush();
            }
        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                m_DefaultLogHandler.LogException(exception, context);
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                m_StreamWriter.WriteLine(exception.Message);
                m_StreamWriter.Flush();
            }
        }

    }
}
