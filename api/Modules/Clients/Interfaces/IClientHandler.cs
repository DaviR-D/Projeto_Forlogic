namespace Api.Modules.Clients.Interfaces
{
    public interface IClientHandler<Output, Input>
    {
        Output Handle(Input input);
    }
}