using TestingModuleWebApp.Data.Enum;

namespace TestingModuleWebApp.Data
{
    public class Dictionaries
    {
        public static string GetGroup(Groups? groups)
        {
            switch (groups)
            {
                case Groups.ISP3418BO: return "ИСП-34-2018БО";
                default: return "ИСП-12-2018БО";
            }
        }
    }
}
