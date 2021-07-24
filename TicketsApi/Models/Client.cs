using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsApi.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public int QueueId { get; set; }
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public System.DateTime Creation { get; set; }
        public byte? Status { get; set; }

        public virtual Queue Queue { get; set; }
    }
}
