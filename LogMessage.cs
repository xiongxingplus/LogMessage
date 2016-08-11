using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;

namespace Log
{
    public class LogMessage
    {
        /// <summary>
        /// 主线程ID
        /// </summary>
        private int mainThreadID = -1;

        /// <summary>
        /// 是否可以接收
        /// </summary>
        private static bool isReceived = false;

        /// <summary>
        /// 日志输出
        /// </summary>
        private static ILogOutput m_Logger;

        /// <summary>
        /// 日志消息接收实例
        /// </summary>
        private static LogMessage m_Default;

        /// <summary>
        /// 监听日志消息
        /// </summary>
        public static void Listen()
        {
            isReceived = true;
            if (null == m_Default)
            {
                m_Default = new LogMessage();
                m_Logger = new FileLogOutput();
            }
            m_Logger.Start();
        }

        /// <summary>
        /// 关闭监听日志消息
        /// </summary>
        public static void Close()
        {
            isReceived = false;
            m_Logger.Abort();
        }

        private LogMessage()
        {
            mainThreadID = Thread.CurrentThread.ManagedThreadId;
            Application.logMessageReceived += Application_logMessageReceived;
            Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (mainThreadID == Thread.CurrentThread.ManagedThreadId)
            {
                Output(condition, stackTrace, type);
            }
        }

        private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
        {
            if (mainThreadID != Thread.CurrentThread.ManagedThreadId)
            {
                Output(condition, stackTrace, type);
            }
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="condition">情况</param>
        /// <param name="stackTrace">堆栈信息</param>
        /// <param name="type">日志类型</param>
        private void Output(string condition, string stackTrace, LogType type)
        {
            if (isReceived)
            {
                m_Logger.Log(condition, stackTrace, type);
            }
        }
    }
}
