﻿using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace LoggerHelper
{
    /// <summary>
    /// 日志管理系统
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 追踪
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logId">日志Id</param>
        /// <param name="param">入参</param>
        /// <param name="userId">用户Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Trace(string message, string logId = "", string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Trace, message, null, logId, requestUrl, param, userId, func);
        }

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="logId">日志Id</param>
        /// <param name="param">入参</param>
        /// <param name="userId">阿里Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Info(string message, string logId = "", string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Info, message, null, logId, requestUrl, param, userId, func);
        }

        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logId">日志Id</param>
        /// <param name="param">入参</param>
        /// <param name="userId">阿里Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Debug(string message, string logId = "", string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Debug, message, null, logId, requestUrl, param, userId, func);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logId">日志Id</param>
        /// <param name="param">入参</param>
        /// <param name="userId">用户Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Warn(string message, string logId = "", string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Warn, message, null, logId, requestUrl, param, userId, func);
        }

        /// <summary>
        /// 异常错误
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">Exception</param>
        /// <param name="logId">日志Id</param>
        /// <param name="errCode">错误码</param>
        /// <param name="param">入参</param>
        /// <param name="userId">阿里Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Error(string message, Exception ex, string logId = "", string errCode = "",string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Error, message, ex, logId, requestUrl, param, userId, func, errCode);
        }

        /// <summary>
        /// 致命
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logId">日志Id</param>
        /// <param name="param">入参</param>
        /// <param name="userId">用户Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Fatal(string message, string logId = "", string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Fatal, message, null, logId, requestUrl, param, userId, func);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="logId">日志Id</param>
        /// <param name="param">入参</param>
        /// <param name="userId">用户Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="requestUrl">请求地址</param>
        /// <returns></returns>
        public static string Off(string message, string logId = "", string param = "", string userId = "", string func = "", string requestUrl = "")
        {
            return WriteLog(LogLevel.Off, message, null, logId, requestUrl, param, userId, func);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志信息</param>
        /// <param name="ex">Exception</param>
        /// <param name="logId">日志Id</param>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="param">入参</param>
        /// <param name="userId">用户Id</param>
        /// <param name="func">功能名称</param>
        /// <param name="errCode">错误码</param>
        /// <returns></returns>
        public static string WriteLog(LogLevel level, string message, Exception ex = null, string logId = "", string requestUrl = "", string param = "", string userId = "", string func = "", string errCode = "")
        {
            return WriteLog(new LoggerEntity()
            {
                Level = level,
                Message = message,
                LogId = logId,
                Params = param,
                UserId = userId,
                RequestUrl = requestUrl,
                Ex = ex,
                Func = func,
                ErrorCode = errCode
            });
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="model">日志模型</param>
        /// <returns></returns>
        public static string WriteLog(LoggerEntity model)
        {
            if (string.IsNullOrEmpty(model.LogId))
            {
                model.LogId = Guid.NewGuid().ToString("N");
            }
            var log = LogManager.GetCurrentClassLogger();
            var logInfo = new LogEventInfo(model.Level, "", "") { Message = model.Message, Exception = model.Ex };
            logInfo.Properties["LogId"] = model.LogId;
            logInfo.Properties["RequestUrl"] = model.RequestUrl;
            logInfo.Properties["Params"] = model.Params;
            logInfo.Properties["UserId"] = model.UserId;
            logInfo.Properties["Func"] = model.Func;
            logInfo.Properties["ErrorCode"] = model.ErrorCode;
            log.Log(logInfo);
            return model.LogId;
        }
    }
}
