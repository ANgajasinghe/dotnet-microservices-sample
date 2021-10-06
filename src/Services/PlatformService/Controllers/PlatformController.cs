using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Application.ApiModels;
using PlatformService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : Controller
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public PlatformController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadQuery>> Get()
            => Ok(_mapper.Map<List<PlatformReadQuery>>(_platformRepo.GetAllPlatforms()));


    }
}
