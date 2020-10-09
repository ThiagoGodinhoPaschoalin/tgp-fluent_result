namespace Tgp.FluentResult.Core.Interfaces
{
    /// <summary>
    /// Interface de Metadado para Aviso
    /// </summary>
    public interface IMetaWarn : IMetadata
    {
        /// <summary>
        /// Mensagem descritiva do Metadado do Aviso
        /// </summary>
        string Message { get; }
    }
}