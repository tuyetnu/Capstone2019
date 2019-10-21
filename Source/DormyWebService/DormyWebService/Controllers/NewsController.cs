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
        /// Get list of news headlines for authorized users
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Headlines")]
        public async Task<ActionResult<List<GetNewsHeadlinesResponse>>> GetNewsHeadlines()
        {
            try
            {
                return await _newsServices.GetNewsHeadLines();
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
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
            try
            {
                return await _newsServices.GetNewsDetail(id);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
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

            try
            {
                return await _newsServices.CreateNews(request);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
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
                return BadRequest("News status is not valid");
            }

            //Call Service
            try
            {
                return await _newsServices.UpdateNews(request);
            }
            catch (HttpStatusCodeException e)
            {
                return StatusCode(e.StatusCode, e.Message);
            }
        }

    }
}