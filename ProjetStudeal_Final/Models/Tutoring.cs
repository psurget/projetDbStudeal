﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetStudeal_Final.Models
{
    public partial class Tutoring
    {
        public Tutoring()
        {
            TimeSlot = new HashSet<TimeSlot>();
        }
        [Key]
        public int TutoringId { get; set; }
        [Required]
        public string Subject { get; set; }
        public int? TutorId { get; set; }
        public DateTime? CreationDate { get; set; }

        public virtual Member Tutor { get; set; }
        public virtual ICollection<TimeSlot> TimeSlot { get; set; }
    }
}
