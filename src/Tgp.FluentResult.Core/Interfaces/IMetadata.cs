using System;
using System.Collections.Generic;

namespace Tgp.FluentResult.Core.Interfaces
{
    public interface IMetadata
    {
        /// <summary>
        /// Data da criação do Metadado
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Obter Dicionário somente leitura de pedaços de dados do Metadado
        /// </summary>
        IReadOnlyDictionary<string, object> GetChunks { get; }

        /// <summary>
        /// Acrescentar um pequeno pedaço de dado ao metadado
        /// </summary>
        /// <param name="key">Chave única do pedaço</param>
        /// <param name="value">Valor único do pedaço</param>
        /// <returns><see cref="IMetadata"/></returns>
        IMetadata AddChunk(string key, object value);

        /// <summary>
        /// Converter interface base em alguma interface derivada;
        /// </summary>
        /// <typeparam name="TIDerivedMeta">Interface derivada de <see cref="IMetadata"/></typeparam>
        /// <param name="derivedMeta">objeto com a implementação caso sucesso da conversão</param>
        /// <returns>Se a conversão foi um sucesso</returns>
        bool TryConvertMeta<TIDerivedMeta>(out TIDerivedMeta derivedMeta) where TIDerivedMeta : IMetadata;
    }
}