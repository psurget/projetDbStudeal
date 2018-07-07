using System;
using System.Collections.Generic;

namespace ProjetStudeal_Final.Models
{
    public partial class Tutoring
    {
        public Tutoring()
        {
            TimeSlot = new HashSet<TimeSlot>();
        }

        public int TutoringId { get; set; }
        public string Subject { get; set; }
        public int? TutorId { get; set; }
        public DateTime? CreationDate { get; set; }

        public Member Tutor { get; set; }
        public ICollection<TimeSlot> TimeSlot { get; set; }
    }
}
