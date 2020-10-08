using System;

namespace Tgp.FluentResult.Core.Interfaces
{
    /// <summary>
    /// Interface de Metadado para Erro
    /// </summary>
    public interface IMetaError : IMetadata
    {
        Exception Exception { get; }
    }
}