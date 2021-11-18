using FluentValidation;
using PlatformService.Application.Mappings;
using PlatformService.Entities;

namespace PlatformService.Application.Dtos
{
    public class PlatformCreateCommand : IMapTo<Platform>, IMapTo<PlatformReadQuery>
    {
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public string? Cost { get; set; }
    }

    public class PlatformCreateCommandValidator : AbstractValidator<PlatformCreateCommand> 
    {
        public PlatformCreateCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Publisher).NotEmpty();
            RuleFor(x => x.Cost).NotEmpty();
        }
    }
}
