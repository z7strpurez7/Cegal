using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Dto;
using PetClinic.Model.Repository.Interfaces;
using PetClinic.Model;
using PetClinic.Validation;

namespace PetClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ClinicValidator _clinicValidator;
        private readonly IClinicRepository _clinicRepository;

        public ClinicController(IMapper mapper, ClinicValidator clinicValidator, IClinicRepository clinicRepository)
        {
            _mapper = mapper;
            _clinicValidator = clinicValidator;
            _clinicRepository = clinicRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<ActionResult> UpsertClinic([FromBody] PostClinicDto clinicDto)
        {
            var errors = _clinicValidator.ValidatePostClinic(clinicDto);

            bool NameExists = await _clinicRepository.ClinicNameExistsAsync(clinicDto.Name);
            if(NameExists){
                errors.Add("Name already exists");
            }
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            var clinic = _mapper.Map<Clinic>(clinicDto);
            await _clinicRepository.InsertClinicAsync(clinic);

            return Ok(clinic.Id);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutClinic(int id, [FromBody] PostClinicDto clinicDto)
        {
            if (!await _clinicRepository.ClinicExistsAsync(id))
            {
                return NotFound($"Clinic with ID {id} not found.");
            }

            var errors = _clinicValidator.ValidatePutClinic(clinicDto);
            if (errors.Any())
            {
                return BadRequest(errors);
            }

            var clinicToUpdate = await _clinicRepository.GetClinicByIdAsync(id);
            _mapper.Map(clinicDto, clinicToUpdate); 

            await _clinicRepository.UpdateClinicAsync(clinicToUpdate);
            return NoContent(); 
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(GetClinicDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetClinicDto>> GetClinic(int id)
        {
            var clinic = await _clinicRepository.GetClinicWithChildrenIdAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetClinicDto>(clinic));

        }


       /* [HttpGet("ByIds")]

          public async Task<ActionResult<IEnumerable<GetClinicDto>>> GetClinicsByIds([FromQuery] List<int> ids)
          {
              var clinics = await _clinicRepository.GetClinicsByIdsAsync(ids);

              return Ok(_mapper.Map<IEnumerable<GetClinicDto>>(clinics));
          }
        /*
          [HttpGet("BySpecialty/{specialty}")]
          public async Task<ActionResult<IEnumerable<GetClinicDto>>> GetClinicBySpeciality(string specialty)
          {
              var clinics = await _clinicRepository.GetClinicsBySpecialtyAsync(specialty);

              if (clinics == null || !clinics.Any())
              {
                  return NotFound($"No clinics found specializing in {specialty}.");
              }

              return Ok(_mapper.Map<IEnumerable<GetClinicDto>>(clinics));
          }  */
    }
}
