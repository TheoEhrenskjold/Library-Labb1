using System.Net;

namespace Library_Labb1.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessage = new List<string>();
        }
        public bool IsSuccess { get; set; }
        public Object Result { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
