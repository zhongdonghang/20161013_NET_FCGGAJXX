using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BP.WF;
using BP.DA;
using BP.En;
using BP.Port;
using BP.Sys;

namespace BP.Web.Port
{
    /// <summary>
    /// Port ��ժҪ˵����
    /// </summary>
    public partial class Port : System.Web.UI.Page
    {
        #region ���봫�ݲ���
        public string DoWhat
        {
            get
            {
                return this.Request.QueryString["DoWhat"];
            }
        }
        public string UserNo
        {
            get
            {
                return this.Request.QueryString["UserNo"];
            }
        }
        public string SID
        {
            get
            {
                return this.Request.QueryString["SID"];
            }
        }
        #endregion

        #region  ��ѡ��Ĳ���
        public string FK_Flow
        {
            get
            {
                return this.Request.QueryString["FK_Flow"];
            }
        }
        public string FK_Node
        {
            get
            {
                return this.Request.QueryString["FK_Node"];
            }
        }
        public string WorkID
        {
            get
            {
                return this.Request.QueryString["WorkID"];
            }
        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e)
        {
          

            Response.AddHeader("P3P", "CP=CAO PSA OUR");
            if (this.UserNo != null && this.SID != null)
            {
                try
                {
                    string uNo = "";
                    if (this.UserNo == "admin")
                        uNo = "zhoupeng";
                    else
                        uNo = this.UserNo;

                   
                    if (  BP.WF.Dev2Interface.Port_CheckUserLogin(uNo,this.SID)==false)
                    {
                        this.Response.Write("�Ƿ��ķ��ʣ��������Ա��ϵ��sid=" + this.SID);
                        return;
                    }
                    else
                    {
                        Emp emL = new Emp(this.UserNo);
                        WebUser.Token = this.Session.SessionID;
                        WebUser.SignInOfGenerLang(emL, SystemConfig.SysLanguage);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("@�п�����û�����ú�ccflow�İ�ȫ��֤����:" + ex.Message);
                }
            }
            else
            {
                if (BP.Web.WebUser.No != "admin")
                    throw new Exception("�Ƿ��ĵ�¼�û�.");
            }

            Emp em = new Emp(this.UserNo);
            WebUser.Token = this.Session.SessionID;
            WebUser.SignInOfGenerLang(em, SystemConfig.SysLanguage);

            string paras = "";
            foreach (string str in this.Request.QueryString)
            {
                string val = this.Request.QueryString[str];
                if (val.IndexOf('@') != -1)
                    throw new Exception("��û���ܲ���: [ " + str + " ," + val + " ] ��ֵ ��URL �����ܱ�ִ�С�");

                switch (str)
                {
                    case DoWhatList.DoNode:
                    case DoWhatList.Emps:
                    case DoWhatList.EmpWorks:
                    case DoWhatList.FlowSearch:
                    case DoWhatList.Login:
                    case DoWhatList.MyFlow:
                    case DoWhatList.MyWork:
                    case DoWhatList.Start:
                    case DoWhatList.Start5:
                    case DoWhatList.JiSu:
                    case DoWhatList.StartSmall:
                    case DoWhatList.FlowFX:
                    case DoWhatList.DealWork:
                    case DoWhatList.DealWorkInSmall:
                    //   case DoWhatList.CallMyFlow:
                    case "FK_Flow":
                    case "WorkID":
                    case "FK_Node":
                    case "SID":
                        break;
                    default:
                        paras += "&" + str + "=" + val;
                        break;
                }
            }

            if (this.IsPostBack == false)
            {
                if (this.IsCanLogin() == false)
                {
                    this.ShowMsg("<fieldset><legend>��ȫ��֤����</legend> ϵͳ�޷�ִ���������󣬿��������ĵ�½ʱ��̫���������µ�½��<br>�����Ҫȡ����ȫ��֤���޸�web.config ��IsDebug �е�ֵ���ó�1��</fieldset>");
                    return;
                }

                BP.Port.Emp emp = new BP.Port.Emp(this.UserNo);
                BP.Web.WebUser.SignInOfGener(emp); //��ʼִ�е�½��

                if (this.Request.QueryString["IsMobile"] != null)
                    BP.Web.WebUser.UserWorkDev = UserWorkDev.Mobile;

                string nodeID = int.Parse(this.FK_Flow + "01").ToString();
                switch (this.DoWhat)
                {
                    case DoWhatList.OneWork: // ��������������.
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@���� FK_Flow ���� WorkID ΪNull ��");
                        this.Response.Redirect("/WF/WFRpt.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    case DoWhatList.JiSu: // ����ģʽ�ķ�ʽ������
                        if (this.FK_Flow == null)
                            this.Response.Redirect("./App/Simple/Default.aspx", true);
                        else
                            this.Response.Redirect("./App/Simple/Default.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.Start5: // ������
                        if (this.FK_Flow == null)
                            this.Response.Redirect("./App/Classic/Default.aspx", true);
                        else
                            this.Response.Redirect("./App/Classic/Default.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.StartLigerUI:
                        if (this.FK_Flow == null)
                            this.Response.Redirect("/WF/App/EasyUI/Default.aspx", true);
                        else
                            this.Response.Redirect("/WF/App/EasyUI/Default.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case "Amaze":
                           if (this.FK_Flow == null)
                               this.Response.Redirect("./App/Amaz/Default.aspx", true);
                        else
                               this.Response.Redirect("./App/Amaz/Default.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.Start: // ������
                        if (this.FK_Flow == null)
                            this.Response.Redirect("Start.aspx", true);
                        else
                            this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.StartSmall: // ��������С����
                        if (this.FK_Flow == null)
                            this.Response.Redirect("Start.aspx?FK_Flow=" + this.FK_Flow + paras, true);
                        else
                            this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + paras, true);
                        break;
                    case DoWhatList.StartSmallSingle: //����������С����
                        if (this.FK_Flow == null)
                            this.Response.Redirect("Start.aspx?FK_Flow=" + this.FK_Flow + paras + "&IsSingle=1&FK_Node=" + nodeID, true);
                        else
                            this.Response.Redirect("MyFlowSmallSingle.aspx?FK_Flow=" + this.FK_Flow + paras + "&FK_Node=" + nodeID, true);
                        break;
                    case DoWhatList.Runing: // ��;�й���
                        this.Response.Redirect("Runing.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Tools: // ������Ŀ��
                        this.Response.Redirect("Tools.aspx", true);
                        break;
                    case DoWhatList.ToolsSmall: // С������Ŀ.
                        this.Response.Redirect("Tools.aspx?RefNo=" + this.Request["RefNo"], true);
                        break;
                    case DoWhatList.EmpWorks: // �ҵĹ���С����.
                        if (this.FK_Flow == null || this.FK_Flow == "")
                            this.Response.Redirect("EmpWorks.aspx", true);
                        else
                            this.Response.Redirect("EmpWorks.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Login:
                        if (this.FK_Flow == null)
                            this.Response.Redirect("EmpWorks.aspx", true);
                        else
                            this.Response.Redirect("EmpWorks.aspx?FK_Flow=" + this.FK_Flow, true);
                        break;
                    case DoWhatList.Emps: // ͨѶ¼��
                        this.Response.Redirect("Emps.aspx", true);
                        break;
                    case DoWhatList.FlowSearch: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearch.aspx", true);
                        else
                            this.Response.Redirect("/Rpt/Search.aspx?Endse=s&FK_Flow=001&EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowSearchSmall: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearch.aspx", true);
                        else
                            this.Response.Redirect("./Comm/Search.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowSearchSmallSingle: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            this.Response.Redirect("FlowSearchSmallSingle.aspx", true);
                        else
                            this.Response.Redirect("./Comm/Search.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.FlowFX: // ���̲�ѯ��
                        if (this.FK_Flow == null)
                            throw new Exception("@û�в������̱�š�");

                        this.Response.Redirect("./Comm/Group.aspx?EnsName=ND" + int.Parse(this.FK_Flow) + "Rpt" + paras, true);
                        break;
                    case DoWhatList.DealWork:
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@���� FK_Flow ���� WorkID ΪNull ��");
                        this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    case DoWhatList.DealWorkInSmall:
                        if (this.FK_Flow == null || this.WorkID == null)
                            throw new Exception("@���� FK_Flow ���� WorkID ΪNull ��");
                        this.Response.Redirect("MyFlow.aspx?FK_Flow=" + this.FK_Flow + "&WorkID=" + this.WorkID + "&o2=1" + paras, true);
                        break;
                    default:
                        this.ToErrorPage("û��Լ���ı��:DoWhat=" + this.DoWhat);
                        break;
                }
            }
        }
        public void ShowMsg(string msg)
        {
            this.Response.Write(msg);
        }
        /// <summary>
        /// ��֤��½�û��Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        public bool IsCanLogin()
        {
            if (BP.Sys.SystemConfig.AppSettings["IsAuth"] == "1")
            {
                if (this.SID != this.GetKey())
                {
                    if (SystemConfig.IsDebug)
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }
        public string GetKey()
        {
            return BP.DA.DBAccess.RunSQLReturnString("SELECT SID From Port_Emp WHERE no='" + this.UserNo + "'");
        }

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        public void ToMsgPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "Port/InfoPage.aspx", true);
            return;
        }
        /// <summary>
        /// �л�����ϢҲ�档
        /// </summary>
        /// <param name="mess"></param>
        public void ToErrorPage(string mess)
        {
            System.Web.HttpContext.Current.Session["info"] = mess;
            System.Web.HttpContext.Current.Response.Redirect("/WF/Comm/Port/InfoPage.aspx");
            return;
        }
    }
}