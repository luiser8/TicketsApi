using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsApi.Models
{
    public class Queue
    {
        public Queue()
        {
            Client = new HashSet<Client>();
        }
        [Key]
        public int QueueId { get; set; }
        public string Name { get; set; }
        public byte Duration { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Creation { get; set; }
        public byte? Status { get; set; }
        public virtual ICollection<Client> Client { get; set; }
    }
}
