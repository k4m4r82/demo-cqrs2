using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindApi.Model.Entity;
using NorthwindApi.Model.Repository;

namespace NorthwindApi.Controllers
{
    public class CategoryQueryHandler : IRequestHandler<CategoryQuery, IActionResult>
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger _logger;

        public CategoryQueryHandler(ICategoryRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<CategoryQueryHandler>();
        }

        public Task<IActionResult> Handle(CategoryQuery request, CancellationToken cancellationToken)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var list = new List<Category>();

                if (request.CategoryId == 0)
                    list = _repository.GetAll().Result;
                else
                {
                    var obj = _repository.GetById(request.CategoryId).Result;
                    if (obj != null) list.Add(obj);
                }

                response = new OkObjectResult(ApiResultHelper.GenerateResponse(HttpStatusCode.OK, list, 1));
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(response);
        }
    }
}
