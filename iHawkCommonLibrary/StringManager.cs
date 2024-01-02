using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iHawkCommonLibrary
{
    /// <summary>
    /// 字符串管理器
    /// </summary>
    public static class StringManager
    {
        /// <summary>
        /// 按照固定长度分割字符串
        /// </summary>
        /// <param name="str">待分割字符串</param>
        /// <param name="maxLength">分割长度</param>
        /// <returns>分割后的字符串列表</returns>
        public static IEnumerable<string> SplitByLength(string str, int maxLength)
        {
            for (var index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }

        /// <summary>
        /// 解析命令行参数
        /// </summary>
        /// <param name="command">命令行</param>
        /// <returns>解析后的参数列表</returns>
        public static List<string> ParseArgs(string command, List<string> specialStartTags, List<string> specialEndTags)
        {
            var ss = command.Split(' ').ToList();
            var cmdArgs = new List<string>();
            var b = false;
            while (ss.Count > 0)
            {
                var s = ss[0];
                var startTag = s.Substring(0, 1);
                var endTag = s.Substring(s.Length - 1);
                if (b)
                {
                    if (specialEndTags.Contains(endTag))
                    {
                        s = s.Substring(0, s.Length - 1);
                        b = false;
                    }
                    cmdArgs[cmdArgs.Count - 1] += $" {s}";
                }
                else
                {
                    if (specialStartTags.Contains(startTag))
                    {
                        s = s.Substring(1);
                        if (specialEndTags.Contains(endTag))
                        {
                            s = s.Substring(0, s.Length - 1);
                        }
                        else
                        {
                            b = true;
                        }
                    }
                    cmdArgs.Add(s);
                }
                ss.RemoveAt(0);
            }
            return cmdArgs;
        }
    }
}
