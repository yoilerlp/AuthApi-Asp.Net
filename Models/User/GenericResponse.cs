
namespace AuthApi.Models
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public int Error { get; set; }
        public object Data { get; set; }

    }
}