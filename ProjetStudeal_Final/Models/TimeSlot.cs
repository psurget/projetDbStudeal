using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetStudeal_Final.Models
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            MeetingRequest = new HashSet<MeetingRequest>();
        }
        [Key]
        public int TimeSlotId { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? TutoringId { get; set; }

        public virtual Tutoring Tutoring { get; set; }
        public virtual ICollection<MeetingRequest> MeetingRequest { get; set; }
    }
}
