﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.EquipmentEntities;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.EquipmentViewModels.CreateEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.GetEquipment;
using DormyWebService.ViewModels.EquipmentViewModels.UpdateEquipment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentsController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        /// <summary>
        /// Get equipment in the room of a student, for authorized users
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("GetByStudent/{studentId}")]
        public async Task<ActionResult<List<GetEquipmentResponse>>> GetByStudent(int studentId)
        {
            return await _equipmentService.GetEquipmentOfStudent(studentId);
        }

        /// <summary>
        /// Create new Equipment for Admin
        /// </summary>
        /// <remarks>RoomId can be null</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<CreateEquipmentResponse>> CreateEquipment(CreateEquipmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!EquipmentStatus.IsEquipmentStatus(request.Status))
            {
                
                return BadRequest("Status is not valid. Must be: " + string.Join(", ", EquipmentStatus.ListAllStatuses()));
            }

            return await _equipmentService.CreateEquipment(request);
        }

        /// <summary>
        /// Update Equipment for Admin
        /// </summary>
        /// <remarks>RoomId can be null</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public async Task<ActionResult<UpdateEquipmentResponse>> UpdateEquipment(UpdateEquipmentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!EquipmentStatus.IsEquipmentStatus(request.Status))
            {

                return BadRequest("Status is not valid. Must be: " + string.Join(", ", EquipmentStatus.ListAllStatuses()));
            }

            return await _equipmentService.UpdateEquipment(request);
        }
    }
}