using Mapster;
using Microsoft.AspNetCore.Mvc;
using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.Api.Models.Request;
using RedacteurPortaal.DomainModels.Agenda;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.Grains.GrainServices;
using RedacteurPortaal.Helpers;

namespace RedacteurPortaal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : Controller
    {
        private readonly IGrainManagementService<IAgendaGrain> grainService;

        public AgendaController(ILogger<AgendaController> logger, IGrainManagementService<IAgendaGrain> grainService)
        {
            this.grainService = grainService;
        }

        [HttpPost]
        public async Task<ActionResult<AgendaDto>> SaveAgendaItem([FromBody] AgendaDto agendaItem)
        {
            Guid newGuid = Guid.NewGuid();

            TypeAdapterConfig<AgendaDto, AgendaModel>
                .NewConfig()
                .Map(dest => dest.Id,
                    src => newGuid);

            var grain = await this.grainService.CreateGrain(newGuid);
            var update = agendaItem.Adapt<AgendaModel>();
            var createdGrain = await grain.UpdateAgenda(update);
            var response = createdGrain.Adapt<AgendaModel>();
            return this.CreatedAtRoute(nameof(this.GetAgendaItem), new {id = newGuid}, response);
        }

        [HttpGet]
        [Route("{id:Guid}", Name = nameof(GetAgendaItem))]
        public async Task<ActionResult<AgendaReadDto>> GetAgendaItem([FromRoute] Guid id)
        {
            var grain = await this.grainService.GetGrain(id);
            Console.WriteLine(id);
            Console.WriteLine(grain.Get());
            var response = await grain.Get();

            return this.Ok(response);
        }

        // TODO: Delete after implementing auth - for testing only!
        [HttpGet]
        public async Task<ActionResult<List<AgendaReadDto>>> GetAllAgendaItems()
        {
            var grain = await this.grainService.GetGrains();

            var response = (await grain.SelectAsync(async x =>
                await x.Get())).AsQueryable().ProjectToType<AgendaReadDto>(null).ToList();
            return this.Ok(response);
        }

        // TODO: Get agenda items by userid
        // Can be used for most date sorting use-cases
        [HttpGet]
        [Route("s/")]
        public async Task<ActionResult<List<AgendaReadDto>>> SortAgendaItemsByDate([FromQuery] AgendaFilterParameters query)
        {
            var grain = await this.grainService.GetGrains();

            var list = (await grain.SelectAsync(async x => await
                x.Get())).AsQueryable().ProjectToType<AgendaReadDto>(null).ToList();

            var response = list.Where(x => x.StartDate >= query.StartDate && x.EndDate <= query.EndDate)
                .OrderBy(dto => dto.StartDate).ToList();

            return this.Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<AgendaDto>> DeleteAgendaItem(Guid id)
        {
            await this.grainService.DeleteGrain(id);
            return this.NoContent();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<ActionResult<AgendaReadDto>> UpdateAgendaItem(Guid id, [FromBody] UpdateAgendaRequest request)
        {
            TypeAdapterConfig<UpdateAgendaRequest, AgendaModel>
                .NewConfig()
                .Map(dest => dest.Id,
                    src => id);

            var grain = await this.grainService.GetGrain(id);
            var update = request.Adapt<AgendaModel>();
            var updatedGrain = await grain.UpdateAgenda(update);
            var response = updatedGrain.Adapt<AgendaReadDto>();
            return this.Ok(response);
        }
    }
}