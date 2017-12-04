using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Chat
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string ContextID { get; set; }
        public List<string> Suggestions { get; set; }
        public string ContentType { get; set; }
        public string LinkedChatID { get; set; }
        public string AuthToken { get; set; }
    }

    public enum ContentTypes
    {
        text,
        image,
        date,
        hyperlink,
        html
    }
}