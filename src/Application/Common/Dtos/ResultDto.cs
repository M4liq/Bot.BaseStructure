namespace Infrastructure.Dtos;

public class ResultDto<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
}