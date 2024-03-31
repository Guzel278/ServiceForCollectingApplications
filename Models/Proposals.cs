using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceForCollectingApplications.Models
{
    public class Proposals
    {
        [Key]
        public int ProposalId { get; set; }
        public Guid ParticipantId { get; set; }
        public int ActivityType { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; } 
        public string? Plan { get; set; } 
        public int Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}

