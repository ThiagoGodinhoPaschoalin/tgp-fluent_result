using System;
using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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
        public static Result Failure(string message, Exception exception = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new Result(new MetaError(message, exception), statusCode);


        /// <summary>
        /// Retorno de aviso com uma mensagem de aviso para o usuário
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o aviso</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Warning(string message, HttpStatusCode statusCode = HttpStatusCode.Conflict)
            => new Result(new MetaWarn(message), statusCode);


        /// <summary>
        /// Retorno de sucesso sem nenhuma mensagem para o usuário
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK)
            => new Result(new MetaHit(), statusCode);

        /// <summary>
        ///  Retorno de sucesso com um objeto de dados para o usuário
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Success<TResponse>(TResponse response, HttpStatusCode statusCode = HttpStatusCode.OK)
            where TResponse : class
            => Result<TResponse>.Success(response, statusCode);

        /// <summary>
        /// Abstração de execução de uma consulta
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="queryFunction">Função que será executada de forma assíncrona</param>
        /// <param name="errorMsg"></param>
        /// <returns>Task <see cref="Result{TResponse}"/></returns>
        public static Task<Result<TResponse>> Query<TResponse>(Task<TResponse> queryFunction, string errorMsg)
            where TResponse : class
            => Result<TResponse>.Query(queryFunction, errorMsg);


        /// <summary>
        /// Result completo. Monte o resultado que for mais conveniente
        /// </summary>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result"/></returns>
        public static Result Ctor(IMetadata metadata, HttpStatusCode statusCode)
            => new Result(metadata, statusCode);

        /// <summary>
        /// Result completo com retorno de dado. Monte o resultado que for mais conveniente
        /// </summary>
        /// <typeparam name="TResponse">Objeto de valor complexo</typeparam>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Ctor<TResponse>(TResponse response, IMetadata metadata, HttpStatusCode statusCode)
            where TResponse : class
            => Result<TResponse>.Ctor(response, metadata, statusCode);
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
        public static Result<TResponse> Success(TResponse response, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new Result<TResponse>(response, new MetaHit(), statusCode);

        /// <summary>
        /// Abstração de execução de uma consulta
        /// </summary>
        /// <param name="queryFunction"></param>
        /// <param name="errorMsg"></param>
        /// <returns>Task <see cref="Result{TResponse}"/></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Register error in Result object.")]
        public static async Task<Result<TResponse>> Query(Task<TResponse> queryFunction, string errorMsg, [CallerMemberName] string @callerMethodName = null)
        {
            try
            {
                ///TODO: Método ainda em desenvolvimento;

                var result = await queryFunction;

                if (result is null || result is default(TResponse) || (result is IEnumerable enumerable && !enumerable.GetEnumerator().MoveNext()))
                {
                    return new Result<TResponse>(result, new MetaWarn("Retorno sem resultado."), HttpStatusCode.NotFound);
                }

                return new Result<TResponse>(result, new MetaHit(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new Result(new MetaError(errorMsg, ex).AddChunk("CallerMemberName", @callerMethodName), HttpStatusCode.BadRequest);
            }
        }


        /// <summary>
        /// Result completo. Monte o resultado que for mais conveniente
        /// </summary>
        /// <param name="response">Entidade de resposta</param>
        /// <param name="metadata"><see cref="IMetadata"/></param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/></param>
        /// <returns><see cref="Result{TResponse}"/></returns>
        public static Result<TResponse> Ctor(TResponse response, IMetadata metadata, HttpStatusCode statusCode)
            => new Result<TResponse>(response, metadata, statusCode);
    }
}