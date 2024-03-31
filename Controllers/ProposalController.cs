using System;
using Microsoft.AspNetCore.Mvc;
using ServiceForCollectingApplications.Models;
using ServiceForCollectingApplications.Services;

namespace ServiceForCollectingApplications.Controllers
{ 
        [ApiController]
        [Route("api/[controller]")]
        [ServiceFilter(typeof(ExceptionHandlingFilter))]
        public class ProposalController : ControllerBase
        {
            private readonly IProposalService _proposalService;

            public ProposalController(IProposalService proposalService)
            {
                _proposalService = proposalService;
            }

            //создание заявки
            [HttpPost("create")]
            public async Task<IActionResult> CreateProposal([FromBody] Proposals request)
            {
                var proposal = await _proposalService.CreateProposalAsync(request.ParticipantId, request.ActivityType, request.Title, request.Description, request.Plan);
                return Ok(proposal);
            }

            //редактирование заявки 
            [HttpPost("edit")]
            public async Task<IActionResult> EditProposal([FromBody] Proposals request)
            {
                var proposal = await _proposalService.EditProposalAsync(request.ProposalId, request.ActivityType, request.Title, request.Description, request.Plan);
                return Ok(proposal);
            }

            //удаление заявки 
            [HttpPost("delete/{proposalId}")]
            public async Task<IActionResult> DeleteProposal(int proposalId)
            {
                var result = await _proposalService.DeleteProposalAsync(proposalId);
                return Ok(result);
            }

            //отправка заявки на рассмотрение
            [HttpPost("submit-for-review/{proposalId}")]
            public async Task<IActionResult> SubmitForReview(int proposalId)
            {
                var result = await _proposalService.SubmitProposalForReviewAsync(proposalId);
                return Ok(result);
            }

            //получение заявок поданных после указанной даты
            [HttpGet("submitted-after/{date}")]
            public async Task<IActionResult> GetProposalsSubmittedAfterDate(DateTime date)
            {
                var proposals = await _proposalService.GetProposalsSubmittedAfterDateAsync(date);
                return Ok(proposals);
            }

            //получение заявок не поданных и старше определенной даты
            [HttpGet("not-submitted-and-older-than/{date}")]
            public async Task<IActionResult> GetProposalsNotSubmittedAndOlderThanDate(DateTime date)
            {
                var proposals = await _proposalService.GetProposalsNotSubmittedAndOlderThanDateAsync(date);
                return Ok(proposals);
            }

            //получение текущей не поданной заявки для указанного пользователя
            [HttpGet("current-draft/{userId}")]
            public async Task<IActionResult> GetCurrentDraftProposalForUser(Guid userId)
            {
                var proposal = await _proposalService.GetCurrentDraftProposalForUserAsync(userId);
                return Ok(proposal);
            }

            //получение заявки по идентификатору
            [HttpGet("{proposalId}")]
            public async Task<IActionResult> GetProposalById(int proposalId)
            {
                var proposal = await _proposalService.GetProposalByIdAsync(proposalId);
                return Ok(proposal);
            }

            //получение списка возможных типов активности
            [HttpGet("possible-activity-types")]
            public async Task<IActionResult> GetPossibleActivityTypes()
            {
                var types = await _proposalService.GetPossibleActivityTypesAsync();
                return Ok(types);
            }
        }
    
}

