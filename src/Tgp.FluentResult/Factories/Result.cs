using System;
using System.Net;
using Tgp.FluentResult.Core.Entities;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult
{
    public partial class Result
    {
        /// <summary>
        /// Retorno de falha com uma mensagem de falha para o usuário
        /// </summary>
        /// <param name="message">Mensagem amigável para o usuário</param>
        /// <param name="exception">Possível exceção que foi gerada no bloco</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Failure(string message, Exception exception = null)
            => new Result(new MetaError(message, exception));


        /// <summary>
        /// Retorno de aviso com uma mensagem de aviso para o usuário
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o aviso</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Warning(string message)
            => new Result(new MetaWarn(message));


        /// <summary>
        /// Retorno de sucesso sem nenhuma mensagem para o usuário
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Success()
            => new Result(new MetaHit());

        /// <summary>
        ///  Retorno de sucesso com um objeto de dados para o usuário
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Success<TResponse>(TResponse response)
            where TResponse : class
            => Result<TResponse>.Success(response);

        /// <summary>
        /// Result completo. Monte o resultado que for mais conveniente
        /// </summary>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Ctor(IMetadata metadata)
            => new Result(metadata);

        /// <summary>
        /// Result completo com retorno de dado. Monte o resultado que for mais conveniente
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Ctor<TResponse>(TResponse response, IMetadata metadata)
            where TResponse : class
            => Result<TResponse>.Ctor(response, metadata);
    }

    public partial class Result<TResponse>
    {
        /// <summary>
        /// Retorno de sucesso com um objeto de dados e uma mensagem para o usuário
        /// </summary>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="message">Mensagem amigável para o usuário</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Success(TResponse response)
            => new Result<TResponse>(response, new MetaHit());

        /// <summary>
        /// Result completo. Monte o resultado que for mais conveniente
        /// </summary>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Ctor(TResponse response, IMetadata metadata)
            => new Result<TResponse>(response, metadata);
    }
}