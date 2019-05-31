using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SklepWPF.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? AuthorId { get; set; }
        public User Author { get; set; }
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
