namespace DoctorAppointment.Domain.Errors
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess ^ error.IsEmpty)
            {
                throw new ArgumentException("Result must either be successful with no error or be failed with an error!", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; private set; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; private set; }

        public static Result Success() => new(true, Error.None);

        public static Result<T> Success<T>(T value) => new(value, true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<T> Failure<T>(Error error) => new(default, false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            if (isSuccess && error.IsEmpty && value is not null)
            {
                Value = value;
            }
        }

        public T Value
        {
            get => IsSuccess ? _value! : throw new InvalidOperationException("A failure doesn't hold a value.");
            private init => _value = value;
        }
    }
}
