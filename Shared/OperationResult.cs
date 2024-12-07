namespace LaundryAPI.Shared
{
    public class OperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; } = string.Empty;
        public OperationResult() { }

        public OperationResult(bool isSuccess, T data, string errorMessage = "")
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
        }
    }
}
