using FluentValidation;
using PlatformService.Application.Mappings;
using PlatformService.Entities;

namespace PlatformService.Application.ApiModels
{
    public class PlatformCtrateCommand : IMapTo<Platform>
    {
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public string? Code { get; set; }
    }

    public class PlatformCtrateCommandValidator : AbstractValidator<PlatformCtrateCommand> 
    {
        public PlatformCtrateCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Publisher).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
