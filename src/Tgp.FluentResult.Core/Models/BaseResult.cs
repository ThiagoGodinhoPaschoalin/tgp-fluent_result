using System.Collections.Generic;
using System.Linq;
using Tgp.FluentResult.Core.Exceptions;
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
        /// Registro de metadados;
        /// </summary>
        private readonly Dictionary<byte, IMetadata> metadata;

        /// <summary>
        /// valor inicial da chave do dicionário dos metadados
        /// </summary>
        private const byte _initialValueOfMetaKey = 1;

        /// <summary>
        /// Registrar se o objeto armazenado é uma falha ou não;
        /// </summary>
        public bool IsFailed { get; private set; }

        /// <summary>
        /// Metadado que é registrado na inicialização do Result;
        /// </summary>
        /// <returns></returns>
        public IMetadata GetFirstMetadata => metadata
            .Where(x => x.Key == _initialValueOfMetaKey)
            .Select(x => x.Value)
            .First();

        /// <summary>
        /// Obter dicionário somente leitura dos metadados
        /// </summary>
        public IReadOnlyDictionary<byte, IMetadata> GetMetadata => metadata;

        /// <summary>
        /// Construtor de Resultado Fluente
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="statusCode"></param>
        protected BaseResult(IMetadata metadata)
        {
            if (metadata is null)
            {
                throw new FluentResultException(nameof(BaseResult<TResult>), nameof(BaseResult<TResult>), nameof(metadata), "Entity is mandatory!");
            }

            this.metadata = new Dictionary<byte, IMetadata>() { [_initialValueOfMetaKey] = metadata };
            this.IsFailed = metadata is IErrorMetadata;
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
                throw new FluentResultException(nameof(BaseResult<TResult>), nameof(AppendMeta), nameof(metadata), "Entity is mandatory!");
            }

            int sum = this.metadata.Count + 1;

            if (sum > byte.MaxValue)
            {
                throw new FluentResultException(nameof(BaseResult<TResult>)
                    , nameof(AppendMeta)
                    , nameof(metadata)
                    , $"Each Result object can contain a maximum of {byte.MaxValue} Metadata's.");
            }

            this.metadata.Add( (byte) sum, metadata);

            return (TResult) this;
        }
    }
}