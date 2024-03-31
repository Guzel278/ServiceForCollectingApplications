using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceForCollectingApplications.Models
{
    public class Participants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ParticipantId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Email")]
        public string? Email { get; set; }     
    }
}


