namespace Tgp.FluentResult.Core.Interfaces
{
    /// <summary>
    /// Interface de Metadado para Aviso
    /// </summary>
    public interface IWarnMetadata : IMetadata
    {
        /// <summary>
        /// Mensagem descritiva do Metadado do Aviso
        /// </summary>
        string Message { get; }
    }
}