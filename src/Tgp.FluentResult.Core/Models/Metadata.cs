using System;
using System.Collections.Generic;
using Tgp.FluentResult.Core.Exceptions;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult.Core.Models
{
    public abstract class Metadata : IMetadata
    {
        /// <summary>
        /// Dicionário de pequenos pedaços de dados referente ao Metadado
        /// </summary>
        private readonly Dictionary<string, object> chunks;

        /// <summary>
        /// Data da criação do Metadado
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Obter Dicionário somente leitura de pedaços de dados do Metadado
        /// </summary>
        public IReadOnlyDictionary<string, object> GetChunks => chunks;

        /// <summary>
        /// Construtor do Metadado
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o metadado</param>
        protected Metadata()
        {
            this.CreatedAt = DateTime.UtcNow;
            this.chunks = new Dictionary<string, object>();
        }

        /// <summary>
        /// Acrescentar um pequeno pedaço de dado ao metadado
        /// </summary>
        /// <param name="key">Chave única do pedaço</param>
        /// <param name="value">Valor único do pedaço</param>
        /// <returns><see cref="IMetadata"/></returns>
        public IMetadata AddChunk(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new FluentResultException(nameof(Metadata), nameof(AddChunk), nameof(key), "Property is mandatory!");
            }

            if (value is null)
            {
                throw new FluentResultException(nameof(Metadata), nameof(AddChunk), nameof(value), "Property is mandatory!");
            }

            if (chunks.ContainsKey(key))
            {
                throw new FluentResultException(nameof(Metadata), nameof(AddChunk), nameof(key), $"the '{key}' key of the Chunk already exists.");
            }
            else
            {
                chunks.Add(key, value);
            }

            return this;
        }

        /// <summary>
        /// Converter interface base em alguma interface derivada;
        /// </summary>
        /// <typeparam name="TIDerivedMeta">Interface derivada de <see cref="IMetadata"/></typeparam>
        /// <param name="derivedMeta">objeto com a implementação caso sucesso da conversão</param>
        /// <returns>Se a conversão foi um sucesso</returns>
        public bool TryConvertMeta<TIDerivedMeta>(out TIDerivedMeta derivedMeta) where TIDerivedMeta : IMetadata
        {
            derivedMeta = default;
            IMetadata thisMeta = this;
            bool converted = thisMeta is TIDerivedMeta;

            if (converted)
            {
                derivedMeta = (TIDerivedMeta) thisMeta;
            }

            return converted;
        }
    }
}