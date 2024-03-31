using System;
using Microsoft.EntityFrameworkCore;
using ServiceForCollectingApplications.DataAccess;
using ServiceForCollectingApplications.Models;

namespace ServiceForCollectingApplications.Services
{

    public class ProposalService : IProposalService
    {
        private readonly ConferenceDbContext _dbContext;

        public ProposalService(ConferenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Proposals> CreateProposalAsync(Guid userId, int activityType, string title, string description, string plan)
        {
            var existingDraft = _dbContext.Proposals.FirstOrDefault(p => p.ParticipantId == userId && p.Status == (int)ProposalStatus.Draft);
            if (existingDraft != null)
            {
                throw new InvalidOperationException("У пользователя уже есть черновик заявки.");
            }

            if (activityType == null || string.IsNullOrEmpty(title) )
            {
                throw new ArgumentException("Необходимо указать тип активности, название и план для создания заявки.");
            }

            var proposal = new Proposals
            {
                ParticipantId = userId,
                ActivityType = activityType,
                Title = title,
                Description = description,
                Plan = plan,
                Status = (int)ProposalStatus.Draft
            };

            _dbContext.Proposals.Add(proposal);
            await _dbContext.SaveChangesAsync();

            return proposal;
        }

        public async Task<Proposals> EditProposalAsync(int proposalId, int activityType, string title, string description, string plan)
        {
            var proposal = await _dbContext.Proposals.FindAsync(proposalId);
            if (proposal == null)
            {
                throw new ArgumentException("Заявка не найдена.");
            }

            if (proposal.Status != (int)ProposalStatus.Draft)
            {
                throw new InvalidOperationException("Нельзя редактировать отправленные на рассмотрение заявки.");
            }

            if (activityType == null || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(plan))
            {
                throw new ArgumentException("Необходимо указать тип активности, название и план для редактирования заявки.");
            }

            proposal.ActivityType = (int)activityType;
            proposal.Title = title;
            proposal.Description = description;
            proposal.Plan = plan;

            await _dbContext.SaveChangesAsync();

            return proposal;
        }

        public async Task<bool> DeleteProposalAsync(int proposalId)
        {
            var proposal = await _dbContext.Proposals.FindAsync(proposalId);
            if (proposal == null)
            {
                return false;
            }

            if (proposal.Status != (int)ProposalStatus.Draft)
            {
                throw new InvalidOperationException("Нельзя удалить отправленные на рассмотрение заявки.");
            }

            _dbContext.Proposals.Remove(proposal);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SubmitProposalForReviewAsync(int proposalId)
        {
            var proposal = await _dbContext.Proposals.FindAsync(proposalId);
            if (proposal == null)
            {
                throw new ArgumentException("Заявка не найдена.");
            }

            if (proposal.Status != (int)ProposalStatus.Draft)
            {
                throw new InvalidOperationException("Нельзя отправить на рассмотрение несуществующую или уже отправленную заявку.");
            }

            // Проверка наличия обязательных полей в заявке, но лучше валидацию делать на клиенте то есть на fronte 
            if (string.IsNullOrEmpty(proposal.Title) || string.IsNullOrEmpty(proposal.Plan))
            {
                throw new InvalidOperationException("Нельзя отправить на рассмотрение заявку с незаполненными обязательными полями.");
            }

            proposal.Status = (int)ProposalStatus.Completed;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Proposals>> GetProposalsSubmittedAfterDateAsync(DateTime date)
        {
            return await _dbContext.Proposals.Where(p => p.Status == (int)ProposalStatus.Completed && p.CreatedDate > date).ToListAsync();
        }

        public async Task<IEnumerable<Proposals>> GetProposalsNotSubmittedAndOlderThanDateAsync(DateTime date)
        {
            return await _dbContext.Proposals.Where(p => p.Status == (int)ProposalStatus.Draft && p.CreatedDate < date).ToListAsync();
        }

        public async Task<Proposals> GetCurrentDraftProposalForUserAsync(Guid userId)
        {
            return await _dbContext.Proposals.FirstOrDefaultAsync(p => p.ParticipantId == userId && p.Status == (int)ProposalStatus.Draft);
        }

        public async Task<Proposals> GetProposalByIdAsync(int proposalId)
        {
            return await _dbContext.Proposals.FindAsync(proposalId);
        }

        public async Task<IEnumerable<string>> GetPossibleActivityTypesAsync()
        {
            return Enum.GetNames(typeof(ProposalActivityType));
        }
    }

}

