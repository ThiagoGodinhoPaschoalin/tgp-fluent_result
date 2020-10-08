using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult.Core.Models
{
    /// <summary>
    /// Abstração de Resultado fluente
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public abstract class BaseResult<TResult> : IBaseResult<TResult> where TResult : BaseResult<TResult>
    {
        /// <summary>
        /// Código de retorno da requisição;
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Registrar se o objeto armazenado é uma falha ou não;
        /// </summary>
        public bool IsFailed { get; private set; }

        /// <summary>
        /// Metadado que é registrado na inicialização do Result;
        /// </summary>
        /// <returns></returns>
        public IMetadata GetFirstMetadata => Metadatas
            .Where(x => x.Key == _initialValueOfMetaKey)
            .Select(x => x.Value)
            .First();

        /// <summary>
        /// Registro de metadados;
        /// </summary>
        private Dictionary<byte, IMetadata> Metadatas { get; set; }

        /// <summary>
        /// valor inicial da chave do dicionário dos metadados
        /// </summary>
        private const byte _initialValueOfMetaKey = 1;

        /// <summary>
        /// Construtor de Resultado Fluente
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="statusCode"></param>
        protected BaseResult(IMetadata metadata, HttpStatusCode statusCode)
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata), "Entity is mandatory!");
            }

            this.Metadatas = new Dictionary<byte, IMetadata>() { [_initialValueOfMetaKey] = metadata };
            this.IsFailed = typeof(IMetaError).IsInstanceOfType(metadata);
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Enfileirar Metadados ao Resultado
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns><see cref="BaseResult{TResult}"/></returns>
        public TResult AppendMeta(IMetadata metadata)
        {
            if (metadata is null)
            {
                throw new ArgumentNullException(nameof(metadata), "Entity is mandatory!");
            }

            int sum = Metadatas.Count + 1;

            if (sum > byte.MaxValue)
            {
                throw new IndexOutOfRangeException($"Each Result object can contain a maximum of {byte.MaxValue} Metadata's.");
            }

            Metadatas.Add( (byte) sum, metadata);

            return (TResult) this;
        }
    }
}
