using System;
using ServiceForCollectingApplications.Models;

namespace ServiceForCollectingApplications.Services
{
    public interface IProposalService
    {
        Task<Proposals> CreateProposalAsync(Guid userId, int activityType, string title, string description, string plan);
        Task<Proposals> EditProposalAsync(int proposalId, int activityType, string title, string description, string plan);
        Task<bool> DeleteProposalAsync(int proposalId);
        Task<bool> SubmitProposalForReviewAsync(int proposalId);
        Task<IEnumerable<Proposals>> GetProposalsSubmittedAfterDateAsync(DateTime date);
        Task<IEnumerable<Proposals>> GetProposalsNotSubmittedAndOlderThanDateAsync(DateTime date);
        Task<Proposals> GetCurrentDraftProposalForUserAsync(Guid userId);
        Task<Proposals> GetProposalByIdAsync(int proposalId);
        Task<IEnumerable<string>> GetPossibleActivityTypesAsync();
    }
}

