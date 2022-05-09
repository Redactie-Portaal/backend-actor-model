using Mapster;
using Microsoft.AspNetCore.Mvc;
using RedacteurPortaal.Api.DTOs;
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

        private readonly ILogger logger;

        public AgendaController(ILogger<AgendaController> logger, IGrainManagementService<IAgendaGrain> grainService)
        {
            this.logger = logger;
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
        public async Task<ActionResult<AgendaDto>> GetAgendaItem([FromRoute] Guid id)
        {
            var grain = await this.grainService.GetGrain(id);
            var response = await grain.Get();
            var dto = response.Adapt<AgendaDto>();

            return this.Ok(response);
        }

        // TODO: Delete after implementing auth - for testing only!
        [HttpGet]
        public async Task<ActionResult<List<AgendaDto>>> GetAllAgendaItems()
        {
            var grain = await this.grainService.GetGrains();

            var response = (await grain.SelectAsync(async x => await
                x.Get())).AsQueryable().ProjectToType<AgendaDto>(null).ToList();
            return this.Ok(response);
        }

        // TODO: Get agenda items by userid
        [HttpGet]
        [Route("s/{startDate}/{endDate}")]
        public async Task<ActionResult<List<AgendaDto>>> SortAgendaItemsByDate(DateTime startDate, DateTime endDate)
        {
            var grain = await this.grainService.GetGrains();

            var list = this.GetAllAgendaItems();


            if (list.Result.Value != null)
            {
                Console.WriteLine("------------> " +
                                  list.Result.Value.Where(x => x.StartDate == new DateTime(2022, 05, 09)).ToList());
                var response = list.Result.Value.Where(x => x.StartDate == new DateTime(2022, 05, 09)).ToList();

                return this.Ok(response);
            }

            return this.NotFound();
        }
    }
}