using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetStudeal_Final.Models
{
    public partial class Member
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int IsTutor { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequest { get; set; }
        public virtual ICollection<Tutoring> Tutoring { get; set; }


        public Member()
        {
            MeetingRequest = new HashSet<MeetingRequest>();
            Tutoring = new HashSet<Tutoring>();
        }

        public Member(string firstName, string lastName, string userName, int isTutor)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            IsTutor = isTutor;
        }




    }
}
