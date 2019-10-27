using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DormyWebService.Entities.TicketEntities;
using DormyWebService.Repositories;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.IssueTicketViewModels.ChangeIssueTicketStatus;
using DormyWebService.ViewModels.IssueTicketViewModels.GetIssueTicket;
using DormyWebService.ViewModels.IssueTicketViewModels.SendIssueTicket;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.GetRoomBooking;
using DormyWebService.ViewModels.TicketViewModels.RoomBooking.SendRoomBooking;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using Sieve.Services;

namespace DormyWebService.Services.TicketServices
{
    public class IssueTicketService : IIssueTicketService
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        private ISieveProcessor _sieveProcessor;

        public IssueTicketService(IRepositoryWrapper repoWrapper, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<IssueTicket> FindById(int id)
        {
            var result = _repoWrapper.IssueTicket.FindByIdAsync(id);

            if (result == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "IssueTicket: IssueTicket is not found.");
            }

            return await result;
        }

        public async Task<SendIssueTicketResponse> SendTicket(SendIssueTicketRequest request)
        {
            //Check if student exists
            var owner = await _repoWrapper.Student.FindByIdAsync(request.OwnerId);
            if (owner == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "IssueTicket: student is not found.");
            }

            if (request.EquipmentId >= 0)
            {
                var equipment = await _repoWrapper.Equipment.FindByIdAsync(request.EquipmentId);
                if (equipment == null)
                {
                    throw new HttpStatusCodeException(HttpStatusCode.NotFound, "IssueTicket: equipment is not found.");
                }
            }

            var type = await _repoWrapper.Param.FindByIdAsync(request.Type);

            if (type == null || type.ParamTypeId != GlobalParams.ParamTypeIssueType)
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "IssueTicket: Issue Ticket Type is invalid.");
            }

            //Create new IssueTicket from request
            var issueTicket = SendIssueTicketRequest.EntityFromRequest(request);

            //Create new in database
            issueTicket = await _repoWrapper.IssueTicket.CreateAsync(issueTicket);

            return new SendIssueTicketResponse()
            {
                IssueTicketId = issueTicket.IssueTicketId
            };
        }

        public async Task<List<GetIssueTicketResponse>> AdvancedGetIssueTicket(string sorts, string filters, int? page, int? pageSize)
        {
            //Build model for SieveProcessor
            var sieveModel = new SieveModel()
            {
                PageSize = pageSize,
                Sorts = sorts,
                Page = page,
                Filters = filters
            };

            //Get all IssueTickets
            var issueTicket = await _repoWrapper.IssueTicket.FindAllAsync();

            if (issueTicket == null || issueTicket.Any() == false)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "IssueTicketService: No IssueTicket is found");
            }

            //Apply filter, sort, pagination
            var result = _sieveProcessor.Apply(sieveModel, issueTicket.AsQueryable()).ToList();

            //Return List of result
            return result.Select( GetIssueTicketResponse.ResponseFromEntity).ToList();
        }

        public async Task<ChangeIssueTicketStatusResponse> ChangeIssueTicketStatus(
            ChangeIssueTicketStatusRequest request)
        {
            //Check if target student Exists
            var student = await _repoWrapper.Student.FindByIdAsync(request.TargetStudentId);
            if (student == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "IssueTicketService: target student not found");
            }

            //Find Issue Ticket
            var issueTicket = await _repoWrapper.IssueTicket.FindByIdAsync(request.IssueTicketId);
            if (issueTicket == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "IssueTicketService: Issue Ticket not found");
            }

            //Update information
            issueTicket = request.UpdateToIssueTicket(issueTicket);

            //Update to database
            issueTicket = await _repoWrapper.IssueTicket.UpdateAsync(issueTicket, issueTicket.IssueTicketId);

            return new ChangeIssueTicketStatusResponse()
            {
                Status = issueTicket.Status
            };
        }
    }
}