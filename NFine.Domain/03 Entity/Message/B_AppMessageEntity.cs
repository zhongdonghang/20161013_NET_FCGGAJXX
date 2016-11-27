using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._03_Entity.Message
{
    /// <summary>
    /// app消息记录
    /// </summary>
    public class B_AppMessageEntity : IEntity<B_AppMessageEntity>, ICreationAudited, IDeleteAudited, IModificationAudited
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
        /// 接受者
        /// </summary>
        public string F_Accepter { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string F_Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string F_Content { get; set; }
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
