using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Block
    {
        public int Id { get; set; }
        public int Index { get; set; }

        public string DocumentHash { get; set; }
        public string PreviousHash { get; set; }
        public string CurrentHash { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
