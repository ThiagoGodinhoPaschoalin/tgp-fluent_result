using System;
using Tgp.FluentResult.Core.Entities;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult
{
    /// <summary>
    /// Fábrica de Resultados para metadados
    /// </summary>
    public static class MetaResult
    {
        /// <summary>
        /// Fábrica de metadado para acerto
        /// </summary>
        /// <param name="message">Mensagem amigável para usuário</param>
        /// <returns><see cref="IHitMetadata"/></returns>
        public static IHitMetadata Hit()
            => new HitMetadata();

        /// <summary>
        /// Fábrica de metadado para aviso
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o aviso</param>
        /// <returns><see cref="IWarnMetadata"/></returns>
        public static IWarnMetadata Warn(string message) 
            => new WarnMetadata(message);

        /// <summary>
        /// Fábrica de metadado para erro
        /// </summary>
        /// <typeparam name="TError">Entidade</typeparam>
        /// <param name="error">Objeto de erro</param>
        /// <param name="exception"><see cref="Exception"/></param>
        /// <returns><see cref="IErrorMetadata{TData}"/></returns>
        public static IErrorMetadata<TError> Error<TError>(TError error, Exception exception = null)
            => new ErrorMetadata<TError>(error, exception);
    }
}