using EventsWebApp.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Application.Commands.EventCategory.DeleteEventCategoryCommand
{
    public class DeleteEventCategoryCommandHandler : IRequestHandler<DeleteEventCategoryCommand, Unit>
    {
        private readonly IEventCategoryRepository _eventCategoryRepository;

        public DeleteEventCategoryCommandHandler(IEventCategoryRepository eventCategoryRepository)
        {
            _eventCategoryRepository = eventCategoryRepository;
        }

        public async Task<Unit> Handle(DeleteEventCategoryCommand request, CancellationToken cancellationToken)
        {
            await _eventCategoryRepository.DeleteAsync(request.id);

            return Unit.Value;
        }
    }
}
