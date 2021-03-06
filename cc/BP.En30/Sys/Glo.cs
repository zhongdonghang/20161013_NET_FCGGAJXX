﻿using System;
using System.Collections.Generic;
using System.Text;
using BP.Sys;
using BP.DA;

namespace BP.Sys
{
    /// <summary>
    /// 公用的静态方法.
    /// </summary>
    public class Glo
    {
        #region 写入系统日志(写入的文件:\DataUser\Log\*.*)
        /// <summary>
        /// 写入一条消息
        /// </summary>
        /// <param name="msg">消息</param>
        public static void WriteLineInfo(string msg)
        {
            BP.DA.Log.DefaultLogWriteLineInfo(msg);
        }
        /// <summary>
        /// 写入一条警告
        /// </summary>
        /// <param name="msg">消息</param>
        public static void WriteLineWarning(string msg)
        {
            BP.DA.Log.DefaultLogWriteLineWarning(msg);
        }
        /// <summary>
        /// 写入一条错误
        /// </summary>
        /// <param name="msg">消息</param>
        public static void WriteLineError(string msg)
        {
            BP.DA.Log.DefaultLogWriteLineError(msg);
        }
        #endregion 写入系统日志 

        #region 写入用户日志(写入的用户表:Sys_UserLog).
        /// <summary>
        /// 写入用户日志
        /// </summary>
        /// <param name="logType">类型</param>
        /// <param name="empNo">操作员编号</param>
        /// <param name="msg">信息</param>
        /// <param name="ip">IP</param>
        public static void WriteUserLog(string logType, string empNo, string msg, string ip)
        {
            UserLog ul = new UserLog();
            ul.MyPK = DBAccess.GenerGUID();
            ul.FK_Emp = empNo;
            ul.LogFlag = logType;
            ul.Docs = msg;
            ul.IP = ip;
            ul.RDT = DataType.CurrentDataTime;
            ul.Insert();
        }
        /// <summary>
        /// 写入用户日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="empNo">操作员编号</param>
        /// <param name="msg">消息</param>
        public static void WriteUserLog(string logType, string empNo, string msg)
        {
            UserLog ul = new UserLog();
            ul.MyPK = DBAccess.GenerGUID();
            ul.FK_Emp = empNo;
            ul.LogFlag = logType;
            ul.Docs = msg;
            ul.RDT = DataType.CurrentDataTime;
            try
            {
                if (BP.Sys.SystemConfig.IsBSsystem)
                    ul.IP = BP.Sys.Glo.Request.UserHostAddress;
            }
            catch
            {
            }
            ul.Insert();
        }
        #endregion 写入用户日志.

        /// <summary>
        /// 获得对象.
        /// </summary>
        public static System.Web.HttpRequest Request
        {
            get
            {
                return System.Web.HttpContext.Current.Request; 
            }
        }

    }
}
