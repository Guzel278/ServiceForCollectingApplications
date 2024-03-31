using System;
using ServiceForCollectingApplications.Models;

namespace ServiceForCollectingApplications.Services
{
    public interface IParticipantService
    {
        Task<Participants> CreateParticipantAsync(Participants participant);
        Task<Participants> GetParticipantAsync(int participantId);
    }
}

