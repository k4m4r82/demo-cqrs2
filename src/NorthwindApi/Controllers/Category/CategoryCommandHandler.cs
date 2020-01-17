using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindApi.Model.Entity;
using NorthwindApi.Model.Repository;

namespace NorthwindApi.Controllers
{
    public class CategoryCommandHandler : IRequestHandler<CreateCategoryCommand, IActionResult>,
                                          IRequestHandler<UpdateCategoryCommand, IActionResult>,
                                          IRequestHandler<DeleteCategoryCommand, IActionResult>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator _createValidator;
        private readonly IValidator _updateValidator;
        private readonly ILogger _logger;        

        public CategoryCommandHandler(ICategoryRepository repository, IMapper mapper, 
                IValidator<CreateCategoryCommand> createValidator, IValidator<UpdateCategoryCommand> updateValidator,
                ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _logger = loggerFactory.CreateLogger<CategoryCommandHandler>();
        }

        public Task<IActionResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var validatorResults = _createValidator.Validate(request);

                if (!validatorResults.IsValid)
                {
                    var errors = validatorResults.Errors.Select(f => f.ErrorMessage);
                    var output = ApiResultHelper.GenerateErrorResponse(HttpStatusCode.UnprocessableEntity, errors);

                    response = new UnprocessableEntityObjectResult(output);
                } 
                else
                {
                    var entity = _mapper.Map<Category>(request);
                    var result = _repository.Save(entity).Result;

                    response = new OkObjectResult(ApiResultHelper.GenerateResponse(HttpStatusCode.OK, result));
                }                
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(response);
        }

        public Task<IActionResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var validatorResults = _updateValidator.Validate(request);

                if (!validatorResults.IsValid)
                {
                    var errors = validatorResults.Errors.Select(f => f.ErrorMessage);
                    var output = ApiResultHelper.GenerateErrorResponse(HttpStatusCode.UnprocessableEntity, errors);

                    response = new UnprocessableEntityObjectResult(output);
                }
                else
                {
                    var result = 0;
                    var entity = _repository.GetById(request.CategoryId).Result;

                    if (entity != null)
                    {
                        entity = _mapper.Map<Category>(request);
                        result = _repository.Update(entity).Result;
                    }

                    response = new OkObjectResult(ApiResultHelper.GenerateResponse(HttpStatusCode.OK, result));
                }                
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(response);
        }

        public Task<IActionResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var entity = new Category { CategoryId = request.CategoryId };
                var result = _repository.Delete(entity).Result;

                response = new OkObjectResult(ApiResultHelper.GenerateResponse(HttpStatusCode.OK, result));
            }
            catch (Exception ex)
            {
                if (this._logger != null) _logger.LogError(ex, "Exception");
            }

            return Task.FromResult(response);
        }
    }
}
