using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUCSA.DATA
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PictureID { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual AcitivityCategory Category { get; set; }

        [Required]
        public string PictureName { get; set; }
        public string Description { get; set; }
        [Required]
        public byte[] Picture { get; set; }

        [Required]
        public byte[] Thumbnails { get; set; }

        [Required]
        public bool IsTop { get; set; }
    }
}
