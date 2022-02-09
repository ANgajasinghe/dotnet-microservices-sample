using PlatformService.Application.Dtos;

namespace PlatformService.Application.AsyncDataServices;

public interface IMessageBusClient
{
    void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
}