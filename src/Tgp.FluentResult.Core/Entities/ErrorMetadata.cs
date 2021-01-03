using System;
using Tgp.FluentResult.Core.Exceptions;
using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Core.Models;

namespace Tgp.FluentResult.Core.Entities
{
    /// <summary>
    /// Metadado padrão para Erro
    /// </summary>
    public class ErrorMetadata : Metadata, IErrorMetadata
    {
        public Exception Exception { get; private set; }

        public ErrorMetadata(Exception exception)
        {
            Exception = exception;
        }
    }

    /// <summary>
    /// Metadado padrão para Erro com dados
    /// </summary>
    public class ErrorMetadata<TError> : ErrorMetadata, IErrorMetadata<TError>
    {
        /// <summary>
        /// Erro!
        /// </summary>
        public TError Data { get; private set; }

        /// <summary>
        /// Metadado para Erro
        /// </summary>
        /// <param name="message">Mensagem </param>
        /// <param name="exception"></param>
        public ErrorMetadata(TError data, Exception exception)
            : base(exception)
        {
            if (data == null)
            {
                throw new FluentResultException(nameof(ErrorMetadata<TError>), nameof(ErrorMetadata<TError>), nameof(data), "Property is Mandatory!");
            }

            this.Data = data;
        }
    }
}