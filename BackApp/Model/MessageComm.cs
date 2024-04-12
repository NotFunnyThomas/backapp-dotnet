using System;

namespace BackApp.Model
{
    public class MessageComm 
    {
        public string Source { get; set; }

        public DateTime CreationDate { get; set; }

        public string Target { get; set; }

        public string Description { get; set; }
    }
}
