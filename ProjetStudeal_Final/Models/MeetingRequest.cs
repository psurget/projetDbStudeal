using System;
using System.Collections.Generic;

namespace ProjetStudeal_Final.Models
{
    public partial class MeetingRequest
    {
        public int RequestId { get; set; }
        public string State { get; set; }
        public int? StudentId { get; set; }
        public int? TimeSlotId { get; set; }

        public Member Student { get; set; }
        public TimeSlot TimeSlot { get; set; }
    }
}
