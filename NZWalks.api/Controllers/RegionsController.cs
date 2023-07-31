using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.api.Data;
using NZWalks.api.Models.Domain;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repository;
using System.Text.Json;



namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    
    public class RegionsController : ControllerBase
    {
        public readonly NZWalksDBContext dBContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDBContext dBContext, IRegionRepository regionRepository, IMapper mapper, 
            ILogger<RegionsController> logger)
        {
                this.dBContext = dBContext;
                this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll() 
        {
            logger.LogInformation("Get All region method invoked");

            var regions= await regionRepository.GetAllRegionsAsync();

            logger.LogInformation($"Get all region method completed successfully: {JsonSerializer.Serialize(regions)}");

            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name= "Auckland Region",
            //        Code= "AKL",
            //        RegionImageurl = "https://images.pexels.com/photos/10466914/pexels-photo-10466914.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            //    },
            //     new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name= "Wellington Region",
            //        Code= "WLG",
            //        RegionImageurl = "https://images.pexels.com/photos/14213781/pexels-photo-14213781.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            //    },
            //};

            //DTO mapping
            //var regionsDTO= new List<RegionDTO>();
            //foreach (var region in regions)
            //{
            //    regionsDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageUrl = region.RegionImageurl
            //    });
            //}
            return Ok(mapper.Map<List<RegionDTO>>(regions));

            //return Ok(regions);
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
       // [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetRegionByIdAsync(id);
            //var region = dBContext.Regions.FirstOrDefault(x => x.Id == Id);
            if (region == null) 
            {
                return NotFound();
            }
            return Ok(region);
        }

        //POST to Create a New region
        [HttpPost]
       // [Authorize(Roles ="Writer")]
        public IActionResult CreateRegion([FromBody] AddRegionDTO addRegionDTO)
        {
            if (ModelState.IsValid)
            {
                var RegionDomainModel = new Region
                {
                    Code = addRegionDTO.Code,
                    Name = addRegionDTO.Name,
                    RegionImageurl = addRegionDTO.RegionImageUrl
                };

                dBContext.Regions.Add(RegionDomainModel);
                dBContext.SaveChanges();

                var regionDTO = new RegionDTO
                {
                    Code = RegionDomainModel.Code,
                    Name = RegionDomainModel.Name,
                    RegionImageUrl = RegionDomainModel.RegionImageurl
                };

                return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO )
        {
            var regionDomainModel = await dBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel == null) {  return NotFound(); };

            regionDomainModel.Code=updateRegionDTO.Code;
            regionDomainModel.Name=updateRegionDTO.Name;
            regionDomainModel.RegionImageurl=updateRegionDTO.RegionImageUrl;

            await dBContext.SaveChangesAsync();

            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageurl
            };

            return Ok(regionDTO);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
       // [Authorize(Roles ="Writer")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteRegionAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }

    }
}
