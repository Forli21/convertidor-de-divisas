using System.Net;

namespace API
{
    public class APIResponse<T> // Cambiado a una clase genérica
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public T ?Data { get; set; } // Ahora es genérico


        public APIResponse()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Success = true;
            this.Messages = new List<string>();
        }
    }
}
