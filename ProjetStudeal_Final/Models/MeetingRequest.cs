using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetStudeal_Final.Models
{
    public partial class MeetingRequest
    {
        [Key]
        public int RequestId { get; set; }
        public string State { get; set; }
        public int? StudentId { get; set; }
        public int? TimeSlotId { get; set; }

        public virtual Member Student { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }
    }
}
