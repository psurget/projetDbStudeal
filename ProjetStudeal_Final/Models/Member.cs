using System;
using System.Collections.Generic;

namespace ProjetStudeal_Final.Models
{
    public partial class Member
    {
        public Member()
        {
            MeetingRequest = new HashSet<MeetingRequest>();
            Tutoring = new HashSet<Tutoring>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IsTutor { get; set; }

        public ICollection<MeetingRequest> MeetingRequest { get; set; }
        public ICollection<Tutoring> Tutoring { get; set; }
    }
}
