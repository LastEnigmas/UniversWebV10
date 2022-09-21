using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Star
    {
        [Key]
        public int StarId { get; set; }
        [Required]
        public int Point { get; set; }
        public int UserID { get; set; }

        #region Relation

        [ForeignKey("UserID")]
        public virtual User User { get; set; }


        #endregion
    }
}
