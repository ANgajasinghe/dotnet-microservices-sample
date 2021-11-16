using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Application.Interfaces;
using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformService.Application.Dtos;

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

        [HttpGet(Name = "GetAll")]
        public ActionResult<IEnumerable<PlatformReadQuery>> GetAll()
            => Ok(_mapper.Map<List<PlatformReadQuery>>(_platformRepo.GetAllPlatforms()));

        [HttpGet("{id}",Name = "GetById")]
        public ActionResult<PlatformReadQuery> GetById(int id)
         => Ok(_mapper.Map<PlatformReadQuery>(_platformRepo.GetPlatformById(id)));

        [HttpPost]
        public ActionResult<PlatformReadQuery> Post(PlatformCreateCommand command)
        {
            var data = _mapper.Map<Platform>(command);
            _platformRepo.CreatePlatform(data);
            _platformRepo.SaveChanges();
            return CreatedAtRoute(nameof(GetById),new { Id = data.Id },_mapper.Map<PlatformReadQuery>(data));
        }
      

    }
}
