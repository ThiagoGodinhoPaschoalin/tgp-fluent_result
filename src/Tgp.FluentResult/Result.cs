﻿using System.Net;
using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Core.Models;

namespace Tgp.FluentResult
{
    /// <summary>
    /// Resultado Fluente
    /// </summary>
    public sealed partial class Result : BaseResult<Result>
    {
        public Result(IMetadata metadata, HttpStatusCode statusCode)
            : base(metadata, statusCode)
        { }
    }

    /// <summary>
    /// Resultado Fluente com Objeto de Valor
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public sealed partial class Result<TResponse> : BaseResult<Result<TResponse>> where TResponse : class
    {
        /// <summary>
        /// Objeto de saída de valor
        /// </summary>
        public TResponse Data { get; private set; }

        /// <summary>
        /// Existe um Objeto de saída de valor?
        /// </summary>
        public bool IsValidData { get; private set; }

        public Result(TResponse data, IMetadata metadata, HttpStatusCode statusCode)
            : base(metadata, statusCode)
        {
            this.Data = data;
            this.IsValidData = data != null;
        }

        /// <summary>
        /// (Result<TResponse> == Result) TRUE;
        /// </summary>
        /// <param name="result"></param>
        public static implicit operator Result<TResponse>(Result result) => new Result<TResponse>(result);

        public Result(Result result)
            : this(null, result.GetFirstMetadata, result.StatusCode)
        { }
    }
}