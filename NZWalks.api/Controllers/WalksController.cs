using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repository;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalkasync([FromBody] AddWalkDTO addWalkDTO)
        {
            var walkDomainModel = _mapper.Map<Walk>(addWalkDTO);
            await _walkRepository.CreateWalkAsync(walkDomainModel);
            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetWalkAsync([FromQuery] string? filteron, [FromQuery] string? filterquery, [FromQuery] string? sortby, [FromQuery] bool isAscending )
        {
            var walkDomainModel = await _walkRepository.GetWalkAsync(filteron,filterquery,sortby,isAscending);
            return Ok(_mapper.Map<List<WalkDTO>>(walkDomainModel));
        }

        [HttpGet]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> GetWalkByIdAsync([FromRoute] Guid Id)
        {
            var walksDomainModel= await _walkRepository.GetWalkByIdAsync(Id);
            if(walksDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(walksDomainModel));
        }

        [HttpPost]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid Id, [FromBody] UpdateWalkDTO updateWalkDTO)
        {
            var walkDomainModel = _mapper.Map<Walk>(updateWalkDTO);
            walkDomainModel= await _walkRepository.UpdateWalkAsync(Id, walkDomainModel);

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync([FromRoute] Guid Id)
        {
            var DeleteWalkId = await _walkRepository.DeleteWalkAsync(Id);

            if (DeleteWalkId == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<WalkDTO>(DeleteWalkId));
        }
    }
}
