using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsApi.Models
{
    public class Queue
    {
        [Key]
        public int QueueId { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Creation { get; set; }
        public byte? Status { get; set; }
    }
}
