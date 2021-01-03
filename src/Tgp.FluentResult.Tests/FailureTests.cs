using NUnit.Framework;
using System;
using System.Linq;
using Tgp.FluentResult.Core.Interfaces;
using Tgp.FluentResult.Tests.DTOs;

namespace Tgp.FluentResult.Tests
{
    class FailureTests
    {
        [Test]
        public void Failure1()
        {
            var result = Result.Failure("Resultado com falha");

            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<IErrorMetadata>().Count());
        }

        [Test]
        public void Failure2()
        {
            var ex = new NullReferenceException("Objeto nao encontrado");
            var result = Result.Failure("Resultado com falha", ex);

            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<IErrorMetadata>().Count());
        }

        [Test]
        public void Failure3()
        {
            var ex = new NullReferenceException("Objeto nao encontrado");
            var result = Result.Failure("Resultado com falha", ex)
                .AppendMeta(MetaResult.Error("Registra outro erro").AddChunk("TemChave", "ComValor"))
                .AppendMeta(MetaResult.Hit().AddChunk("EsseObjeto", new { Id = Guid.NewGuid() }))
                .AppendMeta(MetaResult.Error("Mas no final, outro erro!", ex));

            Assert.IsTrue(result.IsFailed);

            ///Total de metadados no result;
            Assert.AreEqual(4, result.GetMetadata.Count);

            var errors = result.GetMetadata.Values.OfType<IErrorMetadata<string>>();
            ///São 3 metadados de Erro; 1 no Failure, +2 nos Appends;
            Assert.AreEqual(3, errors.Count());
            ///Um metadado de Erro com 'Chunk';
            Assert.AreEqual(1, errors.Count(x => x.GetChunks.Count > 0));
            ///São 2 metadados de Erro com Exceções;
            Assert.AreEqual(2, errors.Count(x => x.Exception != null));
            
            var hits = result.GetMetadata.Values.OfType<IHitMetadata>();
            ///Um metadado de acerto vindo do Append;
            Assert.AreEqual(1, hits.Count());
            ///Um metadado com 'chunk';
            Assert.AreEqual(1, hits.Count(x => x.GetChunks.Count > 0));
        }

        [Test]
        public void Failure4()
        {
            ErrorResponseExampleDTO dto = new ErrorResponseExampleDTO
            {
                Code = 1,
                Message = "xpto"
            };

            var result = Result.Failure(dto);

            Assert.IsTrue(result.IsFailed);

            Assert.AreEqual(1, result.GetMetadata.Values.OfType<IErrorMetadata>().Count());
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<IErrorMetadata<ErrorResponseExampleDTO>>().Count());

            var obj = result.GetMetadata.Values.OfType<IErrorMetadata<ErrorResponseExampleDTO>>().First();
            Assert.AreEqual(dto, obj.Data);
        }
    }
}
