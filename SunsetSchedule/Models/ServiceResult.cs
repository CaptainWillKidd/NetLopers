namespace SunsetSchedule.Models;

public class ServiceResult<T>
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public T? Data { get; set; }

    // Helper properties for convenience
    public bool IsSuccess => Success;
    public string Message => Error ?? string.Empty;

    // Factory methods
    public static ServiceResult<T> SuccessResult(T data)
    {
        return new ServiceResult<T> { Success = true, Data = data };
    }

    public static ServiceResult<T> FailureResult(string error)
    {
        return new ServiceResult<T> { Success = false, Error = error };
    }
}