namespace Money.Domain.Helpers
{
    public class Result<T>
    {
        #region Attrs and Constructors

        public T Value { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int Code { get; set; }

        public Result() { }

        public Result(T value, string message, bool isSuccess, int code)
        {
            Value = value;
            Message = message;
            IsSuccess = isSuccess;
            Code = code;
        }

        public Result(Result<T> result)
        {
            Value = result.Value;
            Message = result.Message;
            Code = result.Code;
            IsSuccess = result.IsSuccess;
        }

        #endregion

        #region Methods

        public Result<T> Success(string message, T value, int code)
        {
            return new Result<T>(value, message, true, code);
        }

        public Result<T> Success()
        {
            return new Result<T>();
        }

        public Result<T> Success(Result<T> result)
        {
            return new Result<T>(result);
        }

        public Result<T> Failure(string message, int code)
        {
            return new Result<T>(default(T), message, false, code);
        }

        public Result<T> Failure()
        {
            return new Result<T>();
        }

        public Result<T> Failure(Result<T> result)
        {
            return new Result<T>(result);
        }


        #endregion

    }
}
