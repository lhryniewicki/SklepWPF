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
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public ICollection<User> Receivers { get; set; }
        public ICollection<User> Senders { get; set; }
        public bool ClientSeen { get; set; }

        public bool AdminSeen { get; set; }

        public override string ToString()
        {
            if(Content.Length >= 30)
            {
                return Content.Substring(0, 30) + "...";
            }
            else
            {
                return Content + "...";
            }
        }
    }
}
