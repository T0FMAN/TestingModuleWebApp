using System.ComponentModel.DataAnnotations;

namespace TestingModuleWebApp.Data.Enum
{
    public enum Groups
    {
        [Display(Name = "ИСП-12-2018БО")]
        ISP1218BO,
        [Display(Name = "ИСП-34-2018БО")]
        ISP3418BO,
        [Display(Name = "ССА-12-2018БО")]
        SSA1218BO,
        [Display(Name = "ССА-34-2018БО")]
        SSA3418BO
    }
}
