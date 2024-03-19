namespace NameApp.Application.Common.Interactors
{
    public interface BaseInteractor<InputDTO, OutputDTO> 
    {
        public abstract Task<OutputDTO> Execute(InputDTO dto); 
    }
}
