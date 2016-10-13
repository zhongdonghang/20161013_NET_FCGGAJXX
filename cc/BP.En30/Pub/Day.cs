using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// 日期
	/// </summary>
	public class Day :SimpleNoNameFix
	{
		#region 实现基本的方方法
		 
		/// <summary>
		/// 物理表
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_Day";
			}
		}
		/// <summary>
		/// 描述
		/// </summary>
		public override string  Desc
		{
			get
			{
                return  "日期";  // "日期";
			}
		}
		#endregion 

		#region 构造方法
		public Day()
        {
        }
        /// <summary>
        /// _No
        /// </summary>
        /// <param name="_No"></param>
		public Day(string _No ): base(_No)
        {
        }
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class Days :SimpleNoNameFixs
	{
		/// <summary>
		/// 日期集合
		/// </summary>
		public Days()
		{
		}
		/// <summary>
		/// 得到它的 Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Day();
			}
		}
        public override int RetrieveAll()
        {
            int num= base.RetrieveAll();

            if (num != 12)
            {
                BP.DA.DBAccess.RunSQL("DELETE FROM Pub_Day ");

                for (int i = 1; i <= 31; i++)
                {
                    BP.Pub.Day yf = new Day();
                    yf.No = i.ToString().PadLeft( 2,'0');
                    yf.Name = i.ToString().PadLeft(2, '0');
                    yf.Insert();
                }

               return base.RetrieveAll();
            }
            return 12;
        }
	}
}
