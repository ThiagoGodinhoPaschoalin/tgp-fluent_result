﻿using System.Net;

namespace Tgp.FluentResult.Core.Interfaces
{
    /// <summary>
    /// Interface da abstração do Result
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface IBaseResult<TResult>
    {
        /// <summary>
        /// Código de retorno da requisição;
        /// </summary>
        HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Registrar se o objeto armazenado é uma falha ou não;
        /// </summary>
        bool IsFailed { get; }

        /// <summary>
        /// Metadado que é registrado na inicialização do Result;
        /// </summary>
        /// <returns></returns>
        IMetadata GetFirstMetadata { get; }

        /// <summary>
        /// Enfileirar Metadados ao Resultado
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns><see cref="TResult"/></returns>
        TResult AppendMeta(IMetadata metadata);
    }
}