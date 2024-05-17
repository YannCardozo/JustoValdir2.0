
namespace WebApi.Controller.MethodsCommom
{
    public class ApiResponse<T>
    {


        //método genérico para retorno para qualquer solicitação de api
        public T Data { get; set; }
    }

    public class SuccessResponse
    {
        public string Message { get; set; }

        public static implicit operator SuccessResponse(string v)
        {
            throw new NotImplementedException();
        }
    }

    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }

        public static implicit operator ErrorResponse(string v)
        {
            throw new NotImplementedException();
        }
    }

    public class InfoResponse
    {
        public string InfoMessage { get; set; }
    }
}
