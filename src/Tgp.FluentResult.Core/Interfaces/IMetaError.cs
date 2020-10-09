using System;

namespace Tgp.FluentResult.Core.Interfaces
{
    /// <summary>
    /// Interface de Metadado para Erro
    /// </summary>
    public interface IMetaError : IMetadata
    {
        /// <summary>
        /// Mensagem descritiva do Metadado de erro
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Exceção do Metadado de erro
        /// </summary>
        Exception Exception { get; }
    }
}