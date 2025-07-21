namespace Api.Shared.Interfaces
{
    public interface IRequestHandler<Output, Input>
    {
        Output Handle(Input input);
    }
}