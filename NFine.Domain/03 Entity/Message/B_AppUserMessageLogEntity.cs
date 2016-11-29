using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.Message
{
    /// <summary>
    /// app消息发送日志（针对用户）
    /// </summary>
    public class B_AppUserMessageLogEntity : IEntity<B_AppUserMessageLogEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
    {
        #region 基础字段属性
        public string F_Id { get; set; }
        public DateTime? F_CreatorTime { get; set; }
        public string F_CreatorUserId { get; set; }
        public DateTime? F_LastModifyTime { get; set; }
        public string F_LastModifyUserId { get; set; }
        public DateTime? F_DeleteTime { get; set; }
        public string F_DeleteUserId { get; set; }
        public bool? F_DeleteMark { get; set; } 
        #endregion

        /// <summary>
        /// 消息类型
        /// </summary>
        public string F_Msg_Type { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime F_SendTime { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string F_Sender { get; set; }

        /// <summary>
        /// 接收人名字
        /// </summary>
        public string F_RealName { get; set; }

        /// <summary>
        /// 接受者账户
        /// </summary>
        public string F_UserName { get; set; }

        /// <summary>
        /// 接受者手机号码
        /// </summary>
        public string F_Phone { get; set; }

        /// <summary>
        /// 接受手机的ClientID
        /// </summary>
        public string F_Cid { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string F_Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string F_Content { get; set; }

        /// <summary>
        /// 阅读状态 Y 已读 N 未读
        /// </summary>
        public string F_ReadState { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? F_ReaderTime { get; set; }

        /// <summary>
        /// 阅读地点
        /// </summary>
        public string F_ReaderLocation { get; set; }

        /// <summary>
        /// 阅读地点纬度
        /// </summary>
        public string F_ReaderLocationX { get; set; }

        /// <summary>
        /// 阅读地点经度
        /// </summary>
        public string F_ReaderLocationY { get; set; }

        /// <summary>
        /// 发送状态  Y发送成功    N发送失败
        /// </summary>
        public string F_State { get; set; }

        /// <summary>
        /// 结果备注
        /// </summary>
        public string F_Result { get; set; }
    }
}
