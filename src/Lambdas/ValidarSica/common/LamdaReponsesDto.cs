namespace Sica.Handler.common
{
    public class LambdaResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static LambdaResponseDto<T> Ok(T data, string? message = null)
        {
            return new LambdaResponseDto<T>
            {
                Success = true,
                Data = data,
                Message = message ?? "Request completed successfully"
            };
        }

        public static LambdaResponseDto<T> Fail(string message)
        {
            return new LambdaResponseDto<T>
            {
                Success = false,
                Message = message
            };
        }
    }
    
}

