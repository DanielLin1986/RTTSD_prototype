using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Net;

namespace LTP.Common
{
    public class StringClass
    {
        public StringClass()
        {
            //������
        }

        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static Regex RegMobile = new Regex(@"^(13[0-9]|15[0-9]|18[6|8|9])\d{8}$");
        private static Regex RegIdNumber = new Regex(@"\d{17}[\d|X]|\d{15}");

        /// <summary>
        /// ȥ���ո�
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static String StringTrim(String sTestString)
        {
            if (sTestString != null)
            {
                sTestString = System.Text.RegularExpressions.Regex.Replace(sTestString, @"(^\s*)|(\s*$)", "");
                return sTestString;
            }
            else
            {
                return "";
            }
        }

        public static bool CheckMobile(string inputData)
        {
            Match m = RegMobile.Match(inputData);
            return m.Success;
        }

        public static bool CheckPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }

        public static bool CheckIDNumber(string inputData)
        {
            Match m = RegIdNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// ��ȡ�ַ���ǰ��̶������ַ���
        /// </summary>
        /// <param name="s">�ַ���</param>
        /// <param name="l">ǰ l ���ַ�(������Ϊ����Ӣ��)</param>
        /// <returns></returns>
        public static string GetCHString(string s, int l)
        {
            string temp = s.Replace("&nbsp;"," ");
            if (System.Text.RegularExpressions.Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Length <= l)
            {
                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (System.Text.RegularExpressions.Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Length <= l - 3)
                {
                    return temp + "...";
                }
            }
            return "";
        }

        /// <summary>
        /// ��ȡ�ַ�������
        /// </summary>
        /// <param name="str">��Ҫ��ȡ���ַ���</param>
        /// <param name="num">��ȡ�ַ����ĳ���</param>
        /// <returns></returns>
        public static string GetENString(string str, int num)
        {
            return (str.Length > num) ? str.Substring(0, num) + "..." : str;
        }

        public static string StripHTML(string strHtml)
        {
            string[] aryReg ={
							  @"<script[^>]*?>.*?</script>",

							  @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",

							  @"([\r\n])[\s]+",

							  @"&(quot|#34);",

							  @"&(amp|#38);",

							  @"&(lt|#60);",

							  @"&(gt|#62);", 

							  @"&(nbsp|#160);", 

							  @"&(iexcl|#161);",

							  @"&(cent|#162);",

							  @"&(pound|#163);",

							  @"&(copy|#169);",

							  @"&#(\d+);",

							  @"-->",

							  @"<!--.*\n"

						  };



            string[] aryRep = {

							   "",

							   "",

							   "",

							   "\"",

							   "&",

							   "<",

							   ">",

							   " ",

							   "\xa1",//chr(161),

							   "\xa2",//chr(162),

							   "\xa3",//chr(163),

							   "\xa9",//chr(169),

							   "",

							   "\r\n",

							   ""

						   };



            string newReg = aryReg[0];

            string strOutput = strHtml;

            for (int i = 0; i < aryReg.Length; i++)
            {

                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);

                strOutput = regex.Replace(strOutput, aryRep[i]);

            }

            strOutput.Replace("<", "");

            strOutput.Replace(">", "");
            strOutput.Replace("&nbsp", "");

            strOutput.Replace("\r\n", "");

            return strOutput;

        }

        /// <summary>
        /// ���ֱ���ת��,���IE��ַ������
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncodeUrl(string input)
        {
            return System.Web.HttpUtility.UrlEncode(EncodeString(input));
        }

        /// <summary>
        /// �޸������ַ����ո���⣩
        /// </summary>
        /// <param name="str">Ҫ�滻���ַ���</param>
        /// <returns></returns>
        public static string EncodeStr(string str)
        {
            return str.Replace("<br />\r\n", "\r\n").Replace("'", "&apos;").Replace(@"""", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" where ", " wh&#101;re ").
                Replace(" select ", " sel&#101;ct ").Replace(" insert ", " ins&#101;rt ").Replace(" create ", " cr&#101;ate ").Replace(" drop ", " dro&#112 ").
                Replace(" alter ", " alt&#101;r ").Replace(" delete ", " del&#101;te ").Replace(" update ", " up&#100;ate ").Replace(" or ", " o&#114; ").Replace("\"", @"&#34;").Replace(",", "��");
        }

        /// <summary>
        /// �޸������ַ�
        /// </summary>
        /// <param name="str">Ҫ�滻���ַ���</param>
        /// <returns></returns>
        public static string EncodeString(string str)
        {
            return str.Replace("<br />\r\n", "\r\n").Replace("'", "&apos;").Replace(@"""", "&quot;").Replace(" ", "&nbsp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(" where ", " wh&#101;re ").
                Replace(" select ", " sel&#101;ct ").Replace(" insert ", " ins&#101;rt ").Replace(" create ", " cr&#101;ate ").Replace(" drop ", " dro&#112 ").
                Replace(" alter ", " alt&#101;r ").Replace(" delete ", " del&#101;te ").Replace(" update ", " up&#100;ate ").Replace(" or ", " o&#114; ").Replace("\"", @"&#34;").
                Replace("\r\n", "<br />\r\n");
        }

        /// <summary>
        /// �޸�SQL�ַ�
        /// </summary>
        /// <param name="str">Ҫ�滻���ַ���</param>
        /// <returns></returns>
        public static string RemoveSQL(string str)
        {
            return str.Replace(" where ", " wh&#101;re ").Replace(" select ", " sel&#101;ct ").Replace(" insert ", " ins&#101;rt ").Replace(" create ", " cr&#101;ate ").Replace(" drop ", " dro&#112 ").
                Replace(" alter ", " alt&#101;r ").Replace(" delete ", " del&#101;te ").Replace(" update ", " up&#100;ate ").Replace(" or ", " o&#114; ").Replace("'", "&apos;"); ;
        }

        /// <summary>
        /// �ָ������ַ�
        /// </summary>
        /// <param name="str">Ҫ�滻���ַ���</param>
        /// <returns></returns>
        public static string UncodeString(string str)
        {
            return str.Replace("&apos;", "'").Replace("&quot;", @"""").Replace("&nbsp;", " ").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", " ").Replace(" wh&#101;re ", " where ").
                Replace(" sel&#101;ct ", " select ").Replace(" ins&#101;rt ", " insert ").Replace(" cr&#101;ate ", " create ").Replace(" dro&#112 ", " drop ").
                Replace(" alt&#101;r ", " alter ").Replace(" del&#101;te ", " delete ").Replace(" up&#100;ate ", " update ").Replace(" o&#114; ", " or ").Replace(@"&#34;", "\"");
        }

        /// <summary>
        /// �滻���д�
        /// </summary>
        /// <param name="str">Ҫ�滻�����д�</param>
        /// <returns></returns>
        public static string ReplaceSensitiveStr(string str)
        {
            return str.Replace("����", "***").Replace("�����������", "***").Replace("�����й�", "***").Replace("���й�", "***").Replace("̫�ӵ�", "***").Replace("�Ϻ���", "***").Replace("��з���", "***").Replace("������", "***").
                Replace("һ��ר��", "***").Replace("һ��ר��", "***").Replace("ר����Ȩ", "***").Replace("�й���Ȩ", "***").Replace("�ٱ���", "***").Replace("89����", "***").Replace("֧�ֲض�", "***").Replace("������", "***").Replace("����һ��", "***").
                Replace("�ٷ�һ��", "***").Replace("���Ԫ", "***").Replace("����ר��", "***").Replace("��������", "***").Replace("��", "***").Replace("а��", "***").Replace("����", "***").Replace("�й���", "***").Replace("̨����", "***").Replace("����", "***").Replace("װb", "***").
                Replace("��sb", "***").Replace("ɵ��", "***").Replace("ɵb", "***").Replace("ɷ��", "***").Replace("�������", "***").Replace("�����", "***").Replace("������", "***").Replace("�Ҳ�", "***").Replace("�Ҳ�", "***").Replace("�����", "***").Replace("ܳ��", "***").
                Replace("������", "***").Replace("������", "***").Replace("������", "***").Replace("������", "***").Replace("������", "***").Replace("������", "***").Replace("������", "***").Replace("������", "***").Replace("��������", "***").Replace("����ȫ��", "***").Replace("�����ү", "***").
                Replace("������", "***").Replace("������", "***").Replace("��ȫ��", "***").Replace("ȫ������", "***").Replace("ȫ�Ҳ��ú���", "***").Replace("ȫ������", "***").Replace("����", "***").Replace("fuck", "***").Replace("Fuck", "***").Replace("FUCK", "***").Replace("Fuck-you", "***").
                Replace("��b", "***").Replace("aƬ", "***").Replace("��Ƭ", "***").Replace("�ַ���", "***").Replace("��ȥ����", "***").Replace("��x��", "***").Replace("�󷨵���", "***").Replace("�˵�", "***").Replace("����", "***").Replace("����", "***").Replace("����", "***").Replace("���˵�Ӱ", "***").
                Replace("������̳", "***").Replace("����ɫ��", "***").Replace("����С˵", "***").Replace("������ѧ", "***").Replace("18��", "***").Replace("������ѧ", "***").Replace("�ն��Ӱ���", "***").Replace("�����¼�", "***").Replace("����ǹ֧", "***").Replace("��У����", "***").Replace("������Ȩ", "***").
                Replace("��������", "***").Replace("����С��", "***").Replace("��ū", "***").Replace("����", "***");
        }

        /// <summary>
        /// ת�崫��Pad�˵��ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodePadStr(string str)
        {
            return UncodeString(str).Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public static string GetSpell(string cn)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "?";
            }
            else return cn;
        }

        /// <summary>
        /// ��ȡ���ֵ�һ��ƴ��
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string GetSpells(string input)
        {
            int len = input.Length;
            string reVal = "";
            for (int i = 0; i < len; i++)
            {
                reVal += GetSpell(input.Substring(i, 1));
            }
            return reVal;
        }

        /// <summary>
        /// ���תȫ��
        /// </summary>
        /// <param name="BJstr"></param>
        /// <returns></returns>
        public static string GetQuanJiao(string BJstr)
        {
            char[] c = BJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }

            string strNew = new string(c);
            return strNew;
        }

        /// <summary>
        /// ȫ��ת���
        /// </summary>
        /// <param name="QJstr"></param>
        /// <returns></returns>
        public static string GetBanJiao(string QJstr)
        {
            char[] c = QJstr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        c[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                }
            }
            string strNew = new string(c);
            return strNew;
        }

        public enum PasswordFormat { SHA1, MD5_32, MD5_16 }

        /// <summary>
        /// �ַ�������
        /// </summary>
        /// <param name="PasswordString">Ҫ���ܵ��ַ���</param>
        /// <param name="PasswordFormat">Ҫ���ܵ����</param>
        /// <returns></returns>
        public static string EncryptPassword(string PasswordString, PasswordFormat paFormat)
        {
            string passWord;
            switch (paFormat)
            {
                case PasswordFormat.SHA1:
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
                        break;
                    }
                case PasswordFormat.MD5_32:
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
                        break;
                    }
                case PasswordFormat.MD5_16:
                    {
                        passWord = FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
                        passWord = passWord.Substring(8, 16);
                        break;
                    }
                default:
                    {
                        passWord = string.Empty;
                        break;
                    }
            }
            return passWord;
        }

        public static string UbbReplace(string str)
        {
            return str.Replace("\n", "<br>").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace(" ", "&nbsp;");
        }

        public static string GetPageInfo(String url)
        {
            WebResponse wr_result = null;
            StringBuilder txthtml = new StringBuilder();
            try
            {
                WebRequest wr_req = WebRequest.Create(url);
                wr_result = wr_req.GetResponse();
                Stream ReceiveStream = wr_result.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
                StreamReader sr = new StreamReader(ReceiveStream, encode);
                if (true)
                {
                    Char[] read = new Char[256];
                    int count = sr.Read(read, 0, 256);
                    while (count > 0)
                    {
                        String str = new String(read, 0, count);
                        txthtml.Append(str);
                        count = sr.Read(read, 0, 256);
                    }
                }
            }
            catch (Exception)
            {
                txthtml.Append("err");
            }
            finally
            {
                if (wr_result != null)
                {
                    wr_result.Close();
                }
            }
            return txthtml.ToString();
        }


        public static string KeywordShow(string result, string keyword)
        {
            if (keyword != "")
            {
                keyword = keyword.Replace("&nbsp;", "+");
                for (int i = 0; i < keyword.Split('+').Length; i++)
                {
                    result = result.Replace(keyword.Split('+')[i], "<font style='color:orange;'>" + keyword.Split('+')[i] + "</font>");
                }
            }
            return result;
        }

        public static string GetOrderID()
        {
            Random ra = new Random();
            return DateTime.Now.ToString("yyyyMMddHHmmss") + ra.Next(10, 99).ToString();
        }
    }
}
