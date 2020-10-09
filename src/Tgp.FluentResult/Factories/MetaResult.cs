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
        /// <returns><see cref="IMetaHit"/></returns>
        public static IMetaHit Hit()
            => new MetaHit();

        /// <summary>
        /// Fábrica de metadado para aviso
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o aviso</param>
        /// <returns><see cref="IMetaWarn"/></returns>
        public static IMetaWarn Warn(string message) 
            => new MetaWarn(message);

        /// <summary>
        /// Fábrica de metadado para erro
        /// </summary>
        /// <param name="message">Mensagem amigável para usuário</param>
        /// <param name="exception">Exceção</param>
        /// <returns><see cref="IMetaError"/></returns>
        public static IMetaError Error(string message, Exception exception = null)
            => new MetaError(message, exception);
    }
}