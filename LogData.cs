using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Log
{
    /// <summary>
    /// 日志数据类
    /// </summary>
    public class LogData
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Log { get; set; }

        /// <summary>
        /// 追踪
        /// </summary>
        public string Track { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType type { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_condition"></param>
        /// <param name="_stackTrace"></param>
        /// <param name="_type"></param>
        public LogData(string _condition, string _stackTrace, LogType _type)
        {
            Log = _condition;
            Track = _stackTrace;
            type = _type;
        }
    }
}
