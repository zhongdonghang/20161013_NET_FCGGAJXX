using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 namespace NFine.Application
{

    /// <summary>
    /// 页对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageObject<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
        public IList<T> Data { get; set; }

       
    }

    /// <summary>
    /// API返回的页对象
    /// </summary>
    public class ReturnPageResult<T>
    {
        public string ResultCode { get; set; }
        public string Msg { get; set; }
        public PageObject<T> Page{get;set;}
        public string strNo { get; set; }
        public T Data1 { get; set; }
    }


    /*20160725添加*/

    /// <summary>
    /// api操作返回结果（带对象）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnSimpleResult2<T>
    {
        public string ResultCode { get; set; }
        public string Msg { get; set; }
        public T t;
    }

    /// <summary>
    /// api操作返回结果，不带对象
    /// </summary>
    public class ReturnSimpleResult1
    {
        public string ResultCode { get; set; }
        public string Msg { get; set; }
    }
}