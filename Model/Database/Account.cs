using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database
{
    [Table("Account")]
    public class Account
    {
        public Account()
        {
            this.CreatedUtc = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(255)]
        [Required]
        public string UserLogin { get; set; }

        [MaxLength(32)]
        [Required]
        public byte[] PasswordMD5 { get; set; }

        [Required]
        public int Balance { get; set; }

        public DateTime CreatedUtc { get; set; }
        
    }
}
