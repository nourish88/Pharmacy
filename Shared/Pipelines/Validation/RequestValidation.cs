using FluentValidation;
using MediatR;
using Pharmacy.Shared.Exceptions.Types;
using ValidationException = Pharmacy.Shared.Exceptions.Types.ValidationException;


namespace Pharmacy.Shared.Pipelines.Validation;

public class RequestValidation
{
    public class RequestValidationBehavior<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ValidationContext<object> context = new(request);

            IEnumerable<ValidationExceptionModel> errors = validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .GroupBy(
                    keySelector: p => p.PropertyName,
                    resultSelector: (propertyName, errors) =>
                        new ValidationExceptionModel { Property = propertyName, Errors = errors.Select(e=>e.ErrorMessage) }
                ).ToList();

            if(errors.Any())
                throw new ValidationException(errors);
            TResponse response = await next();
            return response;
        }
    }
}

