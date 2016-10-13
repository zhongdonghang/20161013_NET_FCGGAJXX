using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.YS
{
    /// <summary>
    /// ��Ŀ��������
    /// </summary>
    public class ProjectSortAttr : EntityNoNameAttr
    {
        public const string StaGrade = "StaGrade";
    }
    /// <summary>
    /// ��Ŀ����
    /// </summary>
    public class ProjectSort : EntityNoName
    {
        #region ʵ�ֻ����ķ���
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                return uac;
            }
        }
        public new string Name
        {
            get
            {
                return this.GetValStrByKey("Name");
            }
        }
        public int Grade
        {
            get
            {
                return this.No.Length / 2;
            }
        }
        public int StaGrade
        {
            get
            {
                return this.GetValIntByKey(ProjectSortAttr.StaGrade);
            }
            set
            {
                this.SetValByKey(ProjectSortAttr.StaGrade,value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ŀ����
        /// </summary> 
        public ProjectSort()
        {
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        /// <param name="_No"></param>
        public ProjectSort(string _No) : base(_No) { }
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Demo_YS_ProjectSort");
                map.EnDesc = "��Ŀ����"; // "��Ŀ����";
                map.EnType = EnType.Admin;
                map.DepositaryOfMap = Depositary.Application;
                map.DepositaryOfEntity = Depositary.Application;
                map.CodeStruct = "2"; // ��󼶱��� 7 .
                map.IsAutoGenerNo = true;

                map.AddTBStringPK(ProjectSortAttr.No, null, "���", true, true, 2, 2, 2);
                map.AddTBString(ProjectSortAttr.Name, null, "����", true, false, 0, 100, 100);

                //map.AddDDLSysEnum(ProjectSortAttr.StaGrade, 0, "����", true, true, ProjectSortAttr.StaGrade,
                //    "@1=�߲��@2=�в��@3=ִ�и�");


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ��Ŀ����s
    /// </summary>
    public class ProjectSorts : EntitiesNoName
    {
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public ProjectSorts() { }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new ProjectSort();
            }
        }
    }
}