using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Application.Interfaces;
using PlatformService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformService.Application.AsyncDataServices;
using PlatformService.Application.Dtos;
using PlatformService.Application.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : Controller
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformController(IPlatformRepo platformRepo, IMapper mapper, 
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient
            )
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet(Name = "GetAll")]
        public ActionResult<IEnumerable<PlatformReadQuery>> GetAll()
            => Ok(_mapper.Map<List<PlatformReadQuery>>(_platformRepo.GetAllPlatforms()));

        [HttpGet("{id}",Name = "GetById")]
        public ActionResult<PlatformReadQuery> GetById(int id)
         => Ok(_mapper.Map<PlatformReadQuery>(_platformRepo.GetPlatformById(id)));

        [HttpPost]
        public async Task<ActionResult<PlatformReadQuery>> Post(PlatformCreateCommand command)
        {
            var data = _mapper.Map<Platform>(command);
            _platformRepo.CreatePlatform(data);
            _platformRepo.SaveChanges();

            var platformReadQuery = _mapper.Map<PlatformReadQuery>(command);
            
            // send sync message
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not send sync message to command client {e.Message}");
                throw;
            }
            
            // send async message
            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadQuery);
                platformPublishedDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not send async message to command client {e.Message}");
                throw;
            }
            
            
            
            return CreatedAtRoute(nameof(GetById),new { Id = data.Id },_mapper.Map<PlatformReadQuery>(data));
        }
      
        

    }
}
