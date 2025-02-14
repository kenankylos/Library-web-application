using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kumbuthane.Models
{
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Kitap Türü Adı girmelisiniz!")]
        [MaxLength(50)]
        [MinLength(2)]
        [DisplayName("Kitap Türü Adı")]
        public string Name { get; set; }


    }
}
