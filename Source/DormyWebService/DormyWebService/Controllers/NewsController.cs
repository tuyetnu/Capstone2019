using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DormyWebService.Entities.AccountEntities;
using DormyWebService.Entities.NewsEntities;
using DormyWebService.Services.NewsServices;
using DormyWebService.Utilities;
using DormyWebService.ViewModels.NewsViewModels;
using DormyWebService.ViewModels.NewsViewModels.CreateNews;
using DormyWebService.ViewModels.NewsViewModels.GetNewsDetail;
using DormyWebService.ViewModels.NewsViewModels.GetNewsHeadlines;
using DormyWebService.ViewModels.NewsViewModels.UpdateNews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DormyWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsServices _newsServices;

        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        /// <summary>
        /// Get list of active news headlines for authorized users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Headlines")]
        public async Task<ActionResult<List<GetNewsHeadlinesResponse>>> GetActiveNewsHeadlines()
        {
            return await _newsServices.GetActiveNewsHeadLines();
        }

        /// <summary>
        /// Get list of news headlines with conditions, for authorized user
        /// </summary>
        /// <remarks></remarks>
        /// <param name="sorts">See GET /api/Rooms for examples</param>
        /// <param name="filters">See GET /api/Rooms for examples</param>
        /// <param name="page">See GET /api/Rooms for examples</param>
        /// <param name="pageSize">See GET /api/Rooms for examples</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("AdvancedGet")]
        public async Task<ActionResult<List<GetNewsHeadlinesResponse>>> AdvancedGetNewsHeadlines(string sorts,
            string filters, int? page, int? pageSize)
        {
            return await _newsServices.AdvancedGetNewsHeadLines(sorts, filters, page, pageSize);
        }

        /// <summary>
        /// Get list of news headlines for Staff and Admin
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin + "," + Role.Staff)]
        [HttpGet]
        public async Task<ActionResult<List<GetNewsHeadlinesResponse>>> GetNewsHeadlines()
        {
            return await _newsServices.GetActiveNewsHeadLines();
        }

        /// <summary>
        /// Get Detail of news,  for authorized users
        /// </summary>
        /// <param name="id">news id</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<GetNewsDetailResponse>> GetNewsDetail(int id)
        {
            return await _newsServices.GetNewsDetail(id);
        }

        /// <summary>
        /// Create new news, done by admin
        /// </summary>
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<CreateNewsResponse>> CreateNews(CreateNewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _newsServices.CreateNews(request);
        }

        /// <summary>
        /// Update News for admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public async Task<ActionResult<UpdateNewsResponse>> UpdateNews(UpdateNewsRequest request)
        {
            //Check Model State
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check News Status
            if (!NewsStatus.IsNewsStatus(request.Status))
            {
                return BadRequest("News status is not valid, must be: " + string.Join(", ", NewsStatus.NewsStatusList));
            }

            //Call Service
            return await _newsServices.UpdateNews(request);
        }

        /// <summary>
        /// Change News Status for Admin
        /// </summary>
        /// <param name="id">NewsId</param>
        /// <param name="status">Status you want to change to</param>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<UpdateNewsResponse>> ChangeNewsStatus(int id, string status)
        {
            if (!NewsStatus.IsNewsStatus(status))
            {
                return BadRequest("News status is not valid");
            }

            //Call Service
            return await _newsServices.ChangeNewsStatus(id, status);
        }
    }
}