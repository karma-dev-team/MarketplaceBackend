namespace KarmaMarketplace.Application.Common.Interactors
{
    public interface BaseUseCase<InputDTO, OutputDTO> 
    {
        public abstract Task<OutputDTO> Execute(InputDTO dto); 
    }
}
