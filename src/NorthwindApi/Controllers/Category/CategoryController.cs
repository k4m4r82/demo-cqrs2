using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MediatR;
using AutoMapper;
using FluentValidation;

using NorthwindApi.Model.Entity;
using NorthwindApi.Model.Repository;

namespace NorthwindApi.Controllers
{
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CategoryController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // api/categories/?categoryId=xxx
        [HttpGet]
        public async Task<IActionResult> Get(int categoryId)
        {
            return await _mediator.Send(new CategoryQuery { CategoryId = categoryId });
        }

        // api/categories
        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CreateCategoryCommand request)
        {
            if (request == null) return BadRequest(ApiResultHelper.GenerateResponse(HttpStatusCode.BadRequest));

            return await _mediator.Send(request);
        }

        // api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand request)
        {
            if (request == null) return BadRequest(ApiResultHelper.GenerateResponse(HttpStatusCode.BadRequest));

            request.CategoryId = id;
            return await _mediator.Send(request);
        }

        // api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _mediator.Send(new DeleteCategoryCommand { CategoryId = id });
        }
    }
}
