using OOAD_Projekat.Models.ChatModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OOAD_Projekat.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string ChatName { get; set; }
        public ChatType ChatType{ get; set; }
        public ICollection<ChatUser> Users { get; set; }

        [NotMapped]
        public List<Message> Messages { get; set; }

        public Chat()
        {
            Users = new List<ChatUser>();
            Messages = new List<Message>();
        }
    }
}
