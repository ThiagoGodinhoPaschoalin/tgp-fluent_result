using System;
using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Core.Models;

namespace Tgp.FluentResult.Core.Entities
{
    /// <summary>
    /// Metadado padrão para Aviso
    /// </summary>
    public class MetaWarn : Metadata, IMetaWarn
    {
        /// <summary>
        /// Mensagem descritiva do Metadado de Aviso
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Metadado para Aviso
        /// </summary>
        /// <param name="message">Mensagem</param>
        public MetaWarn(string message)
            : base()
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Property is Mandatory!");
            }

            this.Message = message;
        }
    }
}