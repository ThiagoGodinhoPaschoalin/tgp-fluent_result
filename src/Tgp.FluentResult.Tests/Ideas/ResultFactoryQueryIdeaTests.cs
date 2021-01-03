using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tgp.FluentResult.Core.Exceptions;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult.Tests.Ideas
{
    internal class MockModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Deve causar erro, pois a exceção será lançada fora do bloco de execução segura do Result.Query;
        ///     É preciso explicitamente avisar que o método que está sendo passado é assíncrono, 
        /// para isso basta adicionar o modificador 'async' na assinatura do seu método.
        /// </summary>
        /// <returns></returns>
        public static Task<IEnumerable<MockModel>> MethodWithThrow()
            => throw new NotImplementedException("Método não implementado");

        /// <summary>
        /// Aceito corretamente, 
        /// pois o modificador 'async' na assinatira do método explicita que este método é assíncrono!
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<MockModel>> MethodWithThrowAsync()
        {
            await Task.CompletedTask;
            throw new NotImplementedException("Método não implementado");
        }

        public static async Task<IEnumerable<MockModel>> GetEmptyAsync()
        {
            await Task.CompletedTask;
            return Enumerable.Empty<MockModel>();
        }

        public static async Task<MockModel> GetOneAsync()
            => await Task.FromResult(new MockModel { Id = Guid.NewGuid() });

        public static async Task<IEnumerable<MockModel>> GetListAsync()
            => await Task.FromResult(new[] { new MockModel { Id = Guid.NewGuid() } });
    }



    class ResultFactoryQueryIdeaTests
    {
        [Test]
        public void SuccessFailedWhenExecuteBeforeEnterInQueryAsync()
        {
            Assert.ThrowsAsync<NotImplementedException>(async delegate ()
            {
                _ = await ResultFactoryQueryIdea.QueryAsync(MockModel.MethodWithThrow(), "Falha na execução.");
            },
            "O método não foi executado dentro da Fabrica!");
        }

        [Test]
        public async Task SuccessFailedWhenThrowInsideQueryAsync()
        {
            var queryResult = await ResultFactoryQueryIdea.QueryAsync(MockModel.MethodWithThrowAsync(), "Falha na execução.");

            Assert.IsTrue(queryResult.IsFailed);
            Assert.IsTrue(queryResult.GetFirstMetadata.TryConvertMeta(out IErrorMetadata<string> metaError));
            Assert.IsTrue(metaError.Exception is FluentResultException);
        }

        [Test]
        public async Task SuccessWhenReturnAsEmpty()
        {
            var queryResult = await ResultFactoryQueryIdea.QueryAsync(MockModel.GetEmptyAsync(), "Falha na execução.");

            Assert.IsFalse(queryResult.IsFailed);
            Assert.IsTrue(queryResult.IsValidData);

            ///É atenção, pois a saída é vazia! não tem um objeto preenchido
            Assert.IsTrue(queryResult.GetFirstMetadata.TryConvertMeta<IWarnMetadata>(out _));
        }

        [Test]
        public async Task SuccessWhenReturnAsOne()
        {
            var queryResult = await ResultFactoryQueryIdea.QueryAsync(MockModel.GetOneAsync(), "Falha na execução.");

            Assert.IsFalse(queryResult.IsFailed);
            Assert.IsTrue(queryResult.IsValidData);

            Assert.IsTrue(queryResult.GetFirstMetadata.TryConvertMeta<IHitMetadata>(out _));
        }

        [Test]
        public async Task SuccessWhenReturnAsList()
        {
            var queryResult = await ResultFactoryQueryIdea.QueryAsync(MockModel.GetListAsync(), "Falha na execução.");

            Assert.IsFalse(queryResult.IsFailed);
            Assert.IsTrue(queryResult.IsValidData);

            Assert.IsTrue(queryResult.GetFirstMetadata.TryConvertMeta<IHitMetadata>(out _));

            Assert.AreEqual(1, queryResult.Data.Count());
        }
    }
}