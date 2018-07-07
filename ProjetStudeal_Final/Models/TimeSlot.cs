using System;
using System.Collections.Generic;

namespace ProjetStudeal_Final.Models
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            MeetingRequest = new HashSet<MeetingRequest>();
        }

        public int TimeSlotId { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? TutoringId { get; set; }

        public Tutoring Tutoring { get; set; }
        public ICollection<MeetingRequest> MeetingRequest { get; set; }
    }
}
