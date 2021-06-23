namespace WebApplication.Dtos
{
    public class ResponseDto<T>
    {
        public string Error { get; set; }
        public T Content { get; set; }
        public bool IsSuccess { get; set; }
    }
    
    public class ResponseDto
    {
        public string Error { get; set; }
        public bool IsSuccess { get; set; }
    }
}