using System.ComponentModel.DataAnnotations;

namespace MyView.Models
{
    public class NightMarket
    {
        [Display(Name="編號")]
        [Required(ErrorMessage ="必填")]
        [RegularExpression("A[0-9]{2}",ErrorMessage ="編號格式有誤")]
        public string Id { get; set; } = null!;

        [Display(Name = "夜市名稱")]
        [Required(ErrorMessage = "必填")]
        [StringLength(20,ErrorMessage ="最長20個字")]
        public string Name { get; set; } = null!;

        [Display(Name = "地址")]
        [Required(ErrorMessage = "必填")]
        public string Address { get; set; } = null!;

    }
}
