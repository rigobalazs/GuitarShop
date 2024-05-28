namespace Domain.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
