using System;
using System.Collections.Generic;
using System.Text;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class Domains
    {
        private static Dictionary<string, string> allDomains ;
        public static void Load()
        {
            allDomains = new Dictionary<string, string>();
            
            allDomains.Add("100", "語文");
            allDomains.Add("200", "健康與體育");
            allDomains.Add("300", "社會");
            allDomains.Add("400", "藝術與人文");
            allDomains.Add("500", "自然與生活科技");
            allDomains.Add("600", "數學");
            allDomains.Add("700", "綜合活動");
            allDomains.Add("800", "彈性課程");
        }

        public static string GetDomainNameByCode(string domainCode)
        {
            if (allDomains == null)
                Load();

            string result = "";
            if (allDomains.ContainsKey(domainCode))
                result = allDomains[domainCode];

            return result;
        }
    }
}
