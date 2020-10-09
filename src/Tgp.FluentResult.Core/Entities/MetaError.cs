using System;
using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Core.Models;

namespace Tgp.FluentResult.Core.Entities
{
    /// <summary>
    /// Metadado padrão para Erro
    /// </summary>
    public sealed class MetaError : Metadata, IMetaError
    {
        /// <summary>
        /// Mensagem descritiva do Metadado de erro
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Exceção do Metadado de Erro
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Metadado para Erro
        /// </summary>
        /// <param name="message">Mensagem </param>
        /// <param name="exception"></param>
        public MetaError(string message, Exception exception)
            : base()
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message), "Property is Mandatory!");
            }

            this.Message = message;
            this.Exception = exception;
        }
    }
}