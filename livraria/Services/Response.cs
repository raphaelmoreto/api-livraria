namespace Services
{
    public class Response<T>
    {
        public T? Dados {  get; private set; }

        public List<string> Notificacao { get; private set; } = new();

        public bool StatusResponse { get; private set; } = false;

        public Response<T> Sucesso(T? dados, string? notificacao = null)
        {
            var response = new Response<T>
            {
                Dados = dados,
                StatusResponse = true
            };

            if (!string.IsNullOrEmpty(notificacao))
                response.Notificacao.Add(notificacao);

            return response;
        }

        public Response<T> Erro(params string[] notificacao)
        {
            var response = new Response<T>();
            response.Notificacao.AddRange(notificacao);
            return response;
        }
    }
}
