using System;
using System.Collections.Generic;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult.Core.Models
{
    public abstract class Metadata : IMetadata
    {
        /// <summary>
        /// Mensagem descritiva do Metadado
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Data da criação do Metadado
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Dicionário de pequenos pedaços de dados referente ao Metadado
        /// </summary>
        private Dictionary<string, object> Chunks { get; set; }

        /// <summary>
        /// Construtor do Metadado
        /// </summary>
        /// <param name="message">Mensagem descritiva sobre o metadado</param>
        protected Metadata(string message)
        {
            this.Message = message;
            this.CreatedAt = DateTime.UtcNow;
            this.Chunks = new Dictionary<string, object>();
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
                throw new ArgumentNullException(nameof(key), "Property is mandatory!");
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Property is mandatory!");
            }

            ///TODO: Verificar a importância da validação do chunk;
            ///fazer um hash do 'value' já armazenado e comparar com o hash do novo 'value';
            ///Se for o mesmo conteúdo, ignorar ou repetir o dado?
            if (Chunks.ContainsKey(key))
            {
                Chunks.Add(string.Concat(key, "_", DateTime.UtcNow.ToString("u")), value);
            }
            else
            {
                Chunks.Add(key, value);
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
            bool converted = typeof(TIDerivedMeta).IsInstanceOfType(this);

            if (converted)
            {
                derivedMeta = (TIDerivedMeta)((IMetadata)this);
            }

            return converted;
        }
    }
}
