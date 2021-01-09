using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Core.Models;

namespace Tgp.FluentResult.Core.Entities
{
    /// <summary>
    /// Metadado padrão para acerto
    /// </summary>
    public class SuccessMetadata : Metadata, ISuccessMetadata
    {
        /// <summary>
        /// Metadado para acerto
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o acerto</param>
        public SuccessMetadata()
            : base()
        { }
    }
}