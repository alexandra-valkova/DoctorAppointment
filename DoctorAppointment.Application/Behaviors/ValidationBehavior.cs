using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DoctorAppointment.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (_validators.Any())
            {
                ValidationContext<TRequest> context = new(request);

                ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

                IEnumerable<ValidationFailure> validationFailures = validationResults.Where(validation => !validation.IsValid)
                                                                                     .SelectMany(validation => validation.Errors);

                if (validationFailures.Any())
                {
                    throw new ValidationException(validationFailures);
                }
            }

            return await next();
        }
    }
}
