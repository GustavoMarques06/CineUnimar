using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases
{
    public class DeleteChairUseCase
    {
        private readonly IChairRepository _chairRepository;
        public DeleteChairUseCase(IChairRepository chairRepository)
        {
            _chairRepository = chairRepository;
        }
        public async Task RunAsync(Guid id)
        {
            var chair = await _chairRepository.GetByIdAsync(id);

            if (chair is null)
                throw new ArgumentException("Cadeira não encontrada");

            await _chairRepository.DeleteAsync(chair);
        }
    }
}
