using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Core.Models;

namespace Tgp.FluentResult.Core.Entities
{
    /// <summary>
    /// Metadado padrão para acerto
    /// </summary>
    public class HitMetadata : Metadata, IHitMetadata
    {
        /// <summary>
        /// Metadado para acerto
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o acerto</param>
        public HitMetadata()
            : base()
        { }
    }
}