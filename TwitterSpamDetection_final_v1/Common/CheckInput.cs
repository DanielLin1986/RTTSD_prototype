using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LTP.Common
{
    public class CheckInput
    {
        private static Regex RegPhone = new Regex("^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7,8}$|^0\\d{3}[- ]?\\d{7,8}$");
        private static Regex RegMobile = new Regex(@"^(13[0-9]|15[0-9]|18[0-9])\d{8}$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //�ȼ���^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w Ӣ����ĸ�����ֵ��ַ������� [a-zA-Z0-9] �﷨һ�� 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static Regex RegExpert = new Regex(@"^[a-zA-Z]{2,5}\d+$");
        private static Regex RegIdNumber = new Regex(@"^\d{17}[\d|X|x]$|^\d{15}$");

        public CheckInput()
        {
            //������
        }

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

        /// <summary>
        /// ��֤�绰����
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool CheckPhone(string inputData)
        {
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// ��֤�ֻ�����
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool CheckMobile(string inputData)
        {
            Match m = RegMobile.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// ��֤��¼�ʻ�������ר�ҳ�ͻ
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool CheckExpert(string inputData)
        {
            Match m = RegExpert.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// ��֤���֤��
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool CheckIDNumber(string inputData)
        {
            Match m = RegIdNumber.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// �û���
        /// ��ĸ��ͷ����ĸ�����֡��»������
        /// ����6��18λ
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckUserName(String sTestString)
        {
            if (StringTrim(sTestString) == String.Empty)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]\w{4,17}$");
            if (RegexNum.IsMatch(sTestString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// ����
        /// ��ĸ�����֡��»������
        /// ����6��18
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckPassword(String sTestString)
        {
            if (StringTrim(sTestString) == String.Empty)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"\w{5,17}$");
            if (RegexNum.IsMatch(sTestString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ƥ������
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckDigit(String sTestString)
        {
            if (StringTrim(sTestString) == String.Empty)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"^-?\d+$");
            if (RegexNum.IsMatch(sTestString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ƥ�����֣�������С��
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        //public static bool CheckNumber(String sTestString)
        //{
        //    if (StringTrim(sTestString) == String.Empty)
        //    {
        //        return false;
        //    }
        //    System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"^(-?\d+)(\.\d+)?$");
        //    if (RegexNum.IsMatch(sTestString))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public static bool CheckNumber(string sTestString)
        {
            bool isPass = false;
            try
            {
                System.Convert.ToDecimal(sTestString);
                isPass = true;
            }
            catch
            {
                isPass = false;
            }
            return isPass;
        }

        public static bool CheckMoney(string sTestString)
        {
            bool isPass = false;
            if (CheckNumber(sTestString) && System.Convert.ToDecimal(sTestString) >= 0)
            {
                isPass = true;
            }
            return isPass;
        }

        /// <summary>
        /// ��֤bool��������
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckBool(string sTestString)
        {
            bool isPass = false;
            if (sTestString.ToLower() == "false" || sTestString.ToLower() == "true" || sTestString.ToLower() == "0" || sTestString.ToLower() == "1")
            {
                isPass = true;
            }
            return isPass;
        }

        /// <summary>
        /// ��֤�ַ�����ֻ�������֣���ĸ���»������
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckEnString(String sTestString)
        {
            if (StringTrim(sTestString) == String.Empty)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9]|[_])*$");
            if (RegexNum.IsMatch(sTestString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��֤�ַ��������֣���ĸ���»��ߣ��㣬�������
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckString(String sTestString)
        {
            if (StringTrim(sTestString) == String.Empty)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9\u4e00-\u9fa5]|[_]|[\.])*$");
            if (RegexNum.IsMatch(sTestString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// EMAIL��֤
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckEmail(String sTestString)
        {
            if (StringTrim(sTestString) == String.Empty)
            {
                return false;
            }
            System.Text.RegularExpressions.Regex RegexNum = new System.Text.RegularExpressions.Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            if (RegexNum.IsMatch(sTestString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��֤���ڣ�ʱ��
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckDateTime(String sTestString)
        {
            int flag = 0;
            try
            {
                System.Convert.ToDateTime(sTestString);
                flag = 1;
            }
            catch
            {
                flag = 0;
            }
            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��֤GUID
        /// </summary>
        /// <param name="sTestString"></param>
        /// <returns></returns>
        public static bool CheckGUID(String sTestString)
        {
            int flag = 0;
            try
            {
                Guid tempID = new Guid(sTestString);
                flag = 1;
            }
            catch
            {
                flag = 0;
            }
            if (flag == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
