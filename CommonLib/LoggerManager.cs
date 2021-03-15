﻿using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace CommonLib
{
    public class LoggerManager
    {

        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private static void Logger(LogLevel level, string msg)
        {
            var frame = new StackFrame(2, false);//获取上一层调用信息
            //LogEventInfo theEvent = new LogEventInfo(LogLevel.Fatal, frame.GetMethod().DeclaringType.FullName, msg);
            var theEvent = new LogEventInfo(level, frame.GetMethod().DeclaringType.FullName, msg);
            theEvent.Properties["Sys.HostName"] = Dns.GetHostName();
            log.Log(theEvent);
        }

        public static void DBLogger(string logName, string msg, IEnumerable<KeyValuePair<string, string>> logdatas)
        {
            var log2 = LogManager.GetLogger("DB" + logName);
            var theEvent = new LogEventInfo(LogLevel.Fatal, "DB" + logName, msg);
            foreach (var d in logdatas)
            {
                theEvent.Properties.Add(d.Key, d.Value);
            }

            log2.Log(theEvent);
        }

        /// <summary>
        /// Trace 仅记入控制台<![CDATA[Trace<Debug<Info<Warn<Error<Fatal]]>
        /// </summary>
        /// <param name="msg"></param>
        public static void Trace(string msg)
        {
            Logger(LogLevel.Trace, msg);
            System.Diagnostics.Trace.Write(msg);
        }

        /// <summary>
        /// 默认记录进文件 <![CDATA[Trace<Debug<Info<Warn<Error<Fatal]]>
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            Logger(LogLevel.Debug, msg);
            System.Diagnostics.Trace.Write(msg);
        }

        /// <summary>
        /// 默认记录进文件  <![CDATA[Trace<Debug<Info<Warn<Error<Fatal]]>
        /// </summary>
        /// <param name="msg"></param>
        public static void Info(string msg)
        {
            Logger(LogLevel.Info, msg);
            System.Diagnostics.Trace.TraceInformation(msg);
        }

        /// <summary>
        /// 默认记录进文件  <![CDATA[Trace<Debug<Info<Warn<Error<Fatal]]>
        /// </summary>
        /// <param name="msg"></param>
        public static void Warn(string msg)
        {
            Logger(LogLevel.Warn, msg);
            System.Diagnostics.Trace.TraceWarning(msg);
        }

        /// <summary>
        /// 默认记录进文件  <![CDATA[Trace<Debug<Info<Warn<Error<Fatal]]>
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Logger(LogLevel.Error, msg);
            System.Diagnostics.Trace.TraceError(msg);
        }

        /// <summary>
        /// 默认记录进文件  <![CDATA[Trace<Debug<Info<Warn<Error<Fatal]]>
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            Logger(LogLevel.Fatal, msg);
            System.Diagnostics.Trace.TraceInformation(msg);
        }

    }
}
