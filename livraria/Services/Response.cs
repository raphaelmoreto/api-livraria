using System.Reflection.Metadata.Ecma335;

namespace Services
{
    public class Response<T>
    {
        public T? Dados {  get; private set; }

        public List<string> Notificacao { get; private set; } = new();

        public bool StatusResponse { get; private set; } = false;

        public Response<T> Sucesso(T? dados, string? notificacao = null)
        {
            Dados = dados;
            StatusResponse = true;

            if (!string.IsNullOrEmpty(notificacao))
                Notificacao.Add(notificacao);

            return this; //USAR O "this" PARA QUE NÃO RETORNE UMA "response" ATUALIZADA
        }

        public Response<T> Erro(params string[] notificacao)
        {
            Notificacao.AddRange(notificacao);
            return this;
        }

        public bool TemNotificacao() => Notificacao.Count != 0;
    }
}
