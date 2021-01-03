using System;

namespace Tgp.FluentResult.Core.Interfaces
{
    public interface IErrorMetadata : IMetadata
    {
        /// <summary>
        /// Exceção do Metadado de erro
        /// </summary>
        Exception Exception { get; }
    }

    /// <summary>
    /// Interface de Metadado para Erro com dados
    /// </summary>
    public interface IErrorMetadata<out TData> : IErrorMetadata
    {
        /// <summary>
        /// Dados do Metadado de erro
        /// </summary>
        TData Data { get; }   
    }
}