using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpearPay.Naming;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace SpearPay.Gateway
{
    /// <inheritdoc />
    /// <summary> 支付网关数据 </summary>
    public class RequestData : SortedDictionary<string, object>
    {
        public RequestData() : base(StringComparer.Ordinal) { }
        public RequestData(IComparer<string> comparer) : base(comparer) { }
        /// <summary> 原始值 </summary>
        public string Raw { get; set; }

        private void Add(IEnumerable<MemberInfo> info, object obj, NamingCase namingCase)
        {
            foreach (var item in info)
            {
                var notAddattributes = item.GetCustomAttributes(typeof(IgnoreAttribute), true);
                if (notAddattributes.Length > 0)
                {
                    continue;
                }

                string key;
                object value;
                var renameAttribute = item.GetCustomAttributes(typeof(NamingAttribute), true);
                if (renameAttribute.Length > 0)
                {
                    key = ((NamingAttribute)renameAttribute[0]).Name;
                }
                else
                {
                    switch (namingCase)
                    {
                        case NamingCase.Camel:
                            key = item.Name.ToCamelCase();
                            break;
                        case NamingCase.Snake:
                            key = item.Name.ToSnakeCase();
                            break;
                        default:
                            key = item.Name;
                            break;
                    }
                }

                switch (item.MemberType)
                {
                    case MemberTypes.Field:
                        value = ((FieldInfo)item).GetValue(obj);
                        break;
                    case MemberTypes.Property:
                        value = ((PropertyInfo)item).GetValue(obj);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if (value is null || string.IsNullOrEmpty(value.ToString()))
                {
                    continue;
                }

                if (ContainsKey(key))
                {
                    this[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="namingCase">字符串策略</param>
        /// <returns></returns>
        public bool Add(object obj, NamingCase? namingCase = null)
        {
            obj.Validate();
            Raw = null;
            var type = obj.GetType();
            var attr = type.GetCustomAttribute<NamingAttribute>();
            if (attr != null)
            {
                namingCase = attr.NamingCase;
            }
            var properties = type.GetProperties();
            var fields = type.GetFields();

            namingCase = namingCase ?? NamingCase.None;
            Add(properties, obj, namingCase.Value);
            Add(fields, obj, namingCase.Value);

            return true;
        }

        /// <summary>
        /// 将网关数据转成Xml格式数据
        /// </summary>
        /// <returns></returns>
        public string ToXml(string root = "xml")
        {
            if (Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.Append($"<{root}>");
            foreach (var item in this)
            {
                sb.AppendFormat(item.Value is string ? "<{0}><![CDATA[{1}]]></{0}>" : "<{0}>{1}</{0}>", item.Key,
                    item.Value);
            }

            sb.Append($"</{root}>");

            return sb.ToString();
        }

        /// <summary>
        /// 将Xml格式数据转换为网关数据
        /// </summary>
        /// <param name="xml">Xml数据</param>
        /// <returns></returns>
        public void FromXml(string xml)
        {
            try
            {
                Clear();
                if (string.IsNullOrEmpty(xml))
                    return;
                var xmlDoc = new XmlDocument
                {
                    XmlResolver = null
                };
                xmlDoc.LoadXml(xml);
                var xmlElement = xmlDoc.DocumentElement;
                if (xmlElement == null) return;
                var nodes = xmlElement.ChildNodes;
                foreach (var item in nodes)
                {
                    var xe = (XmlElement)item;
                    Add(xe.Name, xe.InnerText);
                }
            }
            finally
            {
                Raw = xml;
            }
        }

        /// <summary>
        /// 将网关数据转换为Url格式数据
        /// </summary>
        /// <param name="isUrlEncode">是否需要url编码</param>
        /// <param name="filterEmpty"></param>
        /// <returns></returns>
        public string ToUrl(bool isUrlEncode = true, bool filterEmpty = false)
        {
            IEnumerable<string> arr;
            if (filterEmpty)
            {
                arr = this.Where(t => t.Value != null && !string.IsNullOrWhiteSpace(t.Value.ToString())).Select(a =>
                    $"{a.Key}={(isUrlEncode ? HttpUtility.UrlEncode(a.Value.ToString(), Encoding.UTF8) : a.Value.ToString())}");
            }
            else
            {
                arr = this.Select(a =>
                    $"{a.Key}={(isUrlEncode ? HttpUtility.UrlEncode(a.Value.ToString(), Encoding.UTF8) : a.Value.ToString())}");
            }

            return string.Join("&", arr);
        }

        /// <summary>
        /// 将Url格式数据转换为网关数据
        /// </summary>
        /// <param name="queryString">url数据</param>
        /// <param name="isUrlDecode">是否需要url解码</param>
        /// <returns></returns>
        public void FromQueryString(string queryString, bool isUrlDecode = true)
        {
            try
            {
                Clear();
                if (string.IsNullOrEmpty(queryString))
                    return;
                var index = queryString.IndexOf('?');
                if (index == 0)
                {
                    queryString = queryString.Substring(index + 1);
                }

                var regex = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
                var mc = regex.Matches(queryString);

                foreach (Match item in mc)
                {
                    var value = item.Result("$3");
                    Add(item.Result("$2"), isUrlDecode ? HttpUtility.UrlDecode(value, Encoding.UTF8) : value);
                }
            }
            finally
            {
                Raw = queryString;
            }
        }

        /// <summary>
        /// 将表单数据转换为网关数据
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        public void FromForm(IFormCollection form)
        {
            try
            {
                Clear();
                var allKeys = form.Keys;

                foreach (var item in allKeys)
                {
                    Add(item, form[item]);
                }
            }
            catch { }
        }

        /// <summary>
        /// 将键值对转换为网关数据
        /// </summary>
        /// <param name="nvc">键值对</param>
        /// <returns></returns>
        public void FromNameValueCollection(NameValueCollection nvc)
        {
            foreach (var item in nvc.AllKeys)
            {
                Add(item, nvc[item]);
            }
        }

        /// <summary>
        /// 将网关数据转换为表单数据
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public string ToForm(string url)
        {
            var html = new StringBuilder();
            html.AppendLine("<body>");
            html.AppendLine($"<form name='gateway' method='post' action ='{url}'>");
            foreach (var item in this)
            {
                html.AppendLine(
                    $"<input type='hidden' name='{item.Key}' value='{item.Value}'>");
            }

            html.AppendLine("</form>");
            html.AppendLine("<script language='javascript' type='text/javascript'>");
            html.AppendLine("document.gateway.submit();");
            html.AppendLine("</script>");
            html.AppendLine("</body>");

            return html.ToString();
        }

        /// <summary>
        /// 将网关数据转成Json格式数据
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 将Json格式数据转成网关数据
        /// </summary>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public void FromJson(string json)
        {
            try
            {
                Clear();
                if (string.IsNullOrEmpty(json)) return;
                var jObject = JObject.Parse(json);
                foreach (var item in jObject)
                {
                    Add(item.Key,
                        item.Value.Type == JTokenType.Object
                            ? item.Value.ToString(Newtonsoft.Json.Formatting.None)
                            : item.Value.ToString());
                }
            }
            finally
            {
                Raw = json;
            }
        }

        /// <summary>
        /// 将网关参数转为类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="namingCase">字符串策略</param>
        /// <returns></returns>
        public T ToObject<T>(NamingCase namingCase)
        {
            var type = typeof(T);
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties();

            foreach (var item in properties)
            {
                var renameAttribute = item.GetCustomAttributes(typeof(NamingAttribute), true);

                string key;
                if (renameAttribute.Length > 0)
                {
                    key = ((NamingAttribute)renameAttribute[0]).Name;
                }
                else
                {
                    switch (namingCase)
                    {
                        case NamingCase.Camel:
                            key = item.Name.ToCamelCase();
                            break;
                        case NamingCase.Snake:
                            key = item.Name.ToSnakeCase();
                            break;
                        default:
                            key = item.Name;
                            break;
                    }
                }

                if (TryGetValue(key, out var value))
                {
                    item.SetValue(obj, Convert.ChangeType(value, item.PropertyType));
                }
            }

            return (T)obj;
        }

        /// <summary>
        /// 异步将网关参数转为类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="namingCase">字符串策略</param>
        /// <returns></returns>
        public async Task<T> ToObjectAsync<T>(NamingCase namingCase)
        {
            return await Task.Run(() => ToObject<T>(namingCase));
        }
    }
}
