﻿using MediatR;

namespace FeaturesManagementDashboard.Application.Commands
{
    public interface ICommandHandler<in TCommand> : INotificationHandler<TCommand>
        where TCommand : class, ICommand
    {
    }
}