using Stacked.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Stacked.Application.TodoItems.EventHandlers;

public class LogTodoItemCreated : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<LogTodoItemCreated> _logger;

    public LogTodoItemCreated(ILogger<LogTodoItemCreated> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stacked Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
