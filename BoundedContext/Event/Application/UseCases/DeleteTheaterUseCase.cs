using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases
{
    public class DeleteTheaterUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public DeleteTheaterUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task RunAsync(Guid id)
        {
            await _theaterRepository.DeleteAsync(id);
        }
    }
}
