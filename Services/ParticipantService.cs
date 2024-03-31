using System;
using ServiceForCollectingApplications.DataAccess;
using ServiceForCollectingApplications.Models;

namespace ServiceForCollectingApplications.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly ConferenceDbContext _dbContext;

        public ParticipantService(ConferenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Participants> CreateParticipantAsync(Participants participant)
        {
            _dbContext.Participants.Add(participant);
            await _dbContext.SaveChangesAsync();
            return participant;
        }

        public async Task<Participants> GetParticipantAsync(int participantId)
        {
            return await _dbContext.Participants.FindAsync(participantId);
        }
    }
}

