using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TicketsApi.Models
{
    public class Attended
    {
        [Key]
        public int AttendedId { get; set; }
        public int ClientId { get; set; }
        public int QueueId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.DateTime Creation { get; set; }
        public byte? Status { get; set; }

        public virtual Queue Queue { get; set; }
        public virtual Client Client { get; set; }
    }
}
