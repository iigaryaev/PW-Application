using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Database
{
    [Table("Payment")]
    public class Payment
    {
        public Payment()
        {
            this.CreatedUtc = DateTime.UtcNow;
            this.StateId = 1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SenderUserId { get; set; }

        public int CorrespondentUserId { get; set; }

        public int Ammount { get; set; }

        public int StateId { get; set; }

        public DateTime? ProcessedUtc { get; set; }

        public DateTime CreatedUtc { get; set; }

        [MaxLength(1000)]
        public string ProcessingComment { get; set; }

        [ForeignKey("SenderUserId")]
        public virtual Account SenderUser { get; set; }

        [ForeignKey("CorrespondentUserId")]
        public virtual Account CorrespondentUser { get; set; }

        [ForeignKey("StateId")]
        public virtual PaymentState State { get; set; }
    }
}
