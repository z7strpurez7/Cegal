using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetClinic.Dto;
using PetClinic.Model;
using PetClinic.Model.Repository.Interfaces;
using PetClinic.Validation;

namespace PetClinic.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PetValidator _petValidator; 
        private readonly IPetRepository _repository;

        public PetController(IMapper mapper, PetValidator petValidator, IPetRepository petRepository)
        {
            _mapper = mapper;
            _petValidator = petValidator;
            _repository = petRepository;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterPet(PostPetDto petDto)
        {
            var errors = _petValidator.ValidatePostPet(petDto);
            var petExists = await _repository.IsPetRegisteredAsync(petDto.Name, petDto.ClientId);
            if (petExists)
            {
                errors.Add("A pet with the same name already exists for this owner.");
            }
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            var pet = _mapper.Map<Pet>(petDto);
            _repository.InsertPet(pet);
            await _repository.SaveAsync();

            return Ok(pet.Id);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditPetDetails(int id, BasePetDto petDto)
        {
            var pet = await _repository.GetPetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            var errors = _petValidator.ValidatePutPet(petDto);
            var petExists = await _repository.IsDuplicatePetNameAsync(petDto.Name, petDto.ClientId, pet.Id);
            if (petExists)
            {
                errors.Add("A pet with the same name already exists for this owner.");
            }
            if (errors.Any())
            {
                return BadRequest(errors);
            }
           

            _mapper.Map(petDto, pet);
            _repository.UpdatePet(pet);
            await _repository.SaveAsync();

            return Ok(pet.Id);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePet(int id)
        {
            var pet = await _repository.GetPetWithAppointmentsAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            if (pet.Appointments.Any(a => a.StartTime > DateTime.Now))
            {
                return BadRequest("Cannot delete the pet as it has active appointments.");
            }

            _repository.DeletePet(pet);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetPetDto>> GetPetById(int id)
        {
            var pet = await _repository.GetPetWithAppointmentsAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            var petDto = _mapper.Map<GetPetDto>(pet);
            return Ok(petDto);
        }
        [HttpGet("client/{clientId:int}/pets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetPetDto>>> GetPetsByClientId(int clientId)
        {
            var pets = await _repository.GetPetsWithChildrenFromClientIdAsync(clientId);
            if (pets == null || !pets.Any())
            {
                return NotFound();
            }

            var petDtos = _mapper.Map<List<GetPetDto>>(pets);
            return Ok(petDtos);
        }
    }
}
