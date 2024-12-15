using FluentValidation;
using GeoSearch.BusinessLogicLayer.DTO;

namespace GeoSearch.BusinessLogicLayer.Validators;

public class LocationSearchRequestValidator : AbstractValidator<LocationSearchRequest>
{
    public LocationSearchRequestValidator()
    {
        RuleFor(req => req.Latitude)
            .NotEmpty().WithMessage("Latitude is required")
            .ExclusiveBetween(-90, 90).WithMessage("Latitude must be between 90 and 90 degrees");
        
        RuleFor(req => req.Longitude)
            .NotEmpty().WithMessage("Longitude is required")
            .ExclusiveBetween(-180, 180).WithMessage("Longitude must be between 180 and 180 degrees");
        
        RuleFor(req => req.Radius)
            .InclusiveBetween(0, 100000).WithMessage("Radius must be between 0 and 100000");
    }
}