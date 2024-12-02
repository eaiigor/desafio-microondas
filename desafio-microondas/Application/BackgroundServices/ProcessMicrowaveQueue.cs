using desafio_microondas.Domain.Entities;
using desafio_microondas.Domain.Enums;
using desafio_microondas.Domain.Models;
using desafio_microondas.Infrastructure.Hubs;
using desafio_microondas.Infrastructure.Services;
using desafio_microondas.Infrastructure.Services.MicrowaveService;
using desafio_microondas.Infrastructure.Services.TaskManagerService;
using Microsoft.AspNetCore.SignalR;

namespace desafio_microondas.Application.BackgroundServices;

public class ProcessMicrowaveQueue : BackgroundService
{
    private readonly IMicrowaveQueueService _microwaveQueueService;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ProcessMicrowaveQueue(IMicrowaveQueueService microwaveQueueService,
        IServiceScopeFactory serviceScopeFactory)
    {
        _microwaveQueueService = microwaveQueueService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceScopeFactory.CreateScope();

            if (_microwaveQueueService.TryDequeueMicrowave(out var microwave))
            {
                await ProcessMicrowaveAsync(microwave);
            }
        }
    }

    private async Task ProcessMicrowaveAsync(Microwave microwave)
    {
        var scope = _serviceScopeFactory.CreateScope();
        var microwaveService = scope.ServiceProvider.GetRequiredService<IMicrowaveService>();
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await timer.WaitForNextTickAsync())
        {
            if (microwave.State == EMicrowaveState.Paused) continue;
            await microwaveService.UpdateTime(microwave, microwave.Time - 1);
            if (microwave.Time % 10 == 0)
            {
                await microwaveService.SaveMicrowave(microwave);
            }

            if (microwave.Time > 0) continue;
            await microwaveService.StopHeating(microwave);

            break;
        }
    }
}