using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tgp.FluentResult.Core.Entities;
using Tgp.FluentResult.Core.Exceptions;

namespace Tgp.FluentResult.Tests.Ideas
{
    public static class ResultFactoryQueryIdea
    {
        /// <summary>
        /// Abstração de execução assíncrona de uma consulta
        /// </summary>
        /// <param name="queryAsyncFunc"></param>
        /// <param name="errorMsg"></param>
        /// <returns>Task <see cref="Result{TResponse}"/></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Register error in Result object.")]
        public static async Task<Result<TResponse>> QueryAsync<TResponse>(Task<TResponse> queryAsyncFunc, string errorMsg, [CallerMemberName] string @callerMethodName = null)
            where TResponse : class
        {
            try
            {
                ///Eu devo colocar MetaWarn quando o retorno do método for nulo/vazio?
                ///Se sim, como que eu facilmente identificaria isso e qual é a utilidade?
                ///Qual a diferença entre MetaHit e MetaWarn?

                var result = await queryAsyncFunc;

                if ((result is IEnumerable enumerable && !enumerable.GetEnumerator().MoveNext()))
                {
                    return new Result<TResponse>(result, new MetaWarn("Retorno vazio."));
                }

                return new Result<TResponse>(result, new MetaHit());
            }
            catch (Exception ex)
            {
                var meta = new MetaError(errorMsg, new FluentResultException(errorMsg, ex))
                    .AddChunk("CallerMemberName", @callerMethodName);
                return new Result(meta);
            }
        }
    }
}