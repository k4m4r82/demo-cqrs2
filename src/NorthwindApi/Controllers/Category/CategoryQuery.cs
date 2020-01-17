using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using NorthwindApi.Model.Entity;

namespace NorthwindApi.Controllers
{
    public class CategoryQuery : IRequest<IActionResult>
    {
        public int CategoryId { get; set; }
    }
}
