using System;
using Tgp.FluentResult.Core.Entities;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult
{
    public partial class Result
    {
        /// <summary>
        /// Retorno de falha
        /// </summary>
        /// <param name="exception">Possível exceção que foi gerada no bloco</param>
        /// <returns><see cref="Result"/></returns>
        public static Result Failure(Exception exception = null)
            => new Result(new ErrorMetadata(exception));

        /// <summary>
        /// Retorno de falha com objeto de erro
        /// </summary>
        /// <typeparam name="TError">Entidade do Erro</typeparam>
        /// <param name="error">Objeto erro</param>
        /// <param name="exception">Possível exceção que foi gerada no bloco</param>
        /// <returns><see cref="Result"/></returns>
        public static Result Failure<TError>(TError error, Exception exception = null)
            => new Result(new ErrorMetadata<TError>(error, exception));

        /// <summary>
        /// Retorno de aviso com uma mensagem de aviso para o usuário
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o aviso</param>
        /// <returns><see cref="Result"/></returns>
        public static Result Warning(string message)
            => new Result(new WarnMetadata(message));

        /// <summary>
        /// Retorno de sucesso sem nenhuma mensagem para o usuário
        /// </summary>
        /// <returns><see cref="Result"/></returns>
        public static Result Success()
            => new Result(new SuccessMetadata());

        /// <summary>
        ///  Retorno de sucesso com um objeto de dados para o usuário
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="response">Entidade de resposta</param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Success<TResponse>(TResponse response)
            where TResponse : class
            => Result<TResponse>.Success(response);

        /// <summary>
        /// Result completo. Monte o resultado que for mais conveniente
        /// </summary>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Ctor(IMetadata metadata)
            => new Result(metadata);

        /// <summary>
        /// Result completo com retorno de dado. Monte o resultado que for mais conveniente
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="metadata"><see cref="IMetadata"/></param>
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
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Success(TResponse response)
            => new Result<TResponse>(response, new SuccessMetadata());

        /// <summary>
        /// Result completo. Monte o resultado que for mais conveniente
        /// </summary>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Ctor(TResponse response, IMetadata metadata)
            => new Result<TResponse>(response, metadata);
    }
}