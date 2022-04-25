using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using RedacteurPortaal.Grains.GrainInterfaces;
using RedacteurPortaal.DomainModels.Adress;

namespace RedacteurPortaal.Grains.Grains;

public class AddressGrain : Grain, IAddressGrain
{
    private readonly ILogger logger;

    private readonly IPersistentState<AddressModel> address;

    public AddressGrain(ILogger<NewsItemGrain> logger,
    [PersistentState("address", "OrleansStorage")]
    IPersistentState<AddressModel> address)
    {
        this.logger = logger;
        this.address = address;
    }

    public Task<bool> HasState()
    {
        return Task.FromResult(this.address.RecordExists);
    }

    public async Task UpdateAddress(AddressModel address)
    {
        this.address.State = address;
        await this.address.WriteStateAsync();
    }

    public async Task Delete()
    {
        await this.address.ClearStateAsync();
    }

    public Task<AddressModel> Get()
    {
        var state = this.address.State;
        state.Id = this.GetGrainIdentity().PrimaryKey;
        this.logger.LogInformation("foobar");
        return Task.FromResult(this.address.State);
    }
}
