using System.Net;

namespace DesafioEscola.Crosscutting.Helpers;

public class ExceptionAPI : Exception
{
    private HttpStatusCode _httpStatusCode = HttpStatusCode.BadRequest;

    public string Mensagem { get; private set; }
    public HttpStatusCode HttpStatusCode => _httpStatusCode;
    public ExceptionAPI() : base("")
    {
        Mensagem = string.Empty;
    }

    public ExceptionAPI(string mensagem) : base(mensagem)
    {
        Mensagem = mensagem;
    }

    public ExceptionAPI(string mensagem, HttpStatusCode statusCode) : base(mensagem)
    {
        Mensagem = mensagem;
        _httpStatusCode = statusCode;
    }


}
