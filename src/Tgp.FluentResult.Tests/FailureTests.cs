using NUnit.Framework;
using System;
using System.Linq;
using System.Net;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult.Tests
{
    class FailureTests
    {
        [Test]
        public void Failure1()
        {
            var result = Result.Failure("Resultado com falha");

            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(result.GetMetadata.Values.OfType<IMetaError>().Count(), 1);
        }

        [Test]
        public void Failure2()
        {
            var ex = new NullReferenceException("Objeto nao encontrado");
            var result = Result.Failure("Resultado com falha", ex, HttpStatusCode.BadRequest);

            Assert.IsTrue(result.IsFailed);
            Assert.AreEqual(result.GetMetadata.Values.OfType<IMetaError>().Count(), 1);
        }

        [Test]
        public void Failure3()
        {
            var ex = new NullReferenceException("Objeto nao encontrado");
            var result = Result.Failure("Resultado com falha", ex, HttpStatusCode.BadRequest)
                .AppendMeta(MetaResult.Error("Registra outro erro").AddChunk("TemChave", "ComValor"))
                .AppendMeta(MetaResult.Hit().AddChunk("EsseObjeto", new { Id = Guid.NewGuid() }))
                .AppendMeta(MetaResult.Error("Mas no final, outro erro!", ex));

            Assert.IsTrue(result.IsFailed);

            //Total de metadados no result;
            Assert.AreEqual(result.GetMetadata.Count, 4);

            var errors = result.GetMetadata.Values.OfType<IMetaError>();
            //São 3 metadados de Erro; 1 no Failure, +2 nos Appends;
            Assert.AreEqual(errors.Count(), 3);
            //Um metadado de Erro com 'Chunk';
            Assert.AreEqual(errors.Where(x => x.GetChunks.Count > 0).Count(), 1);
            //São 2 metadados de Erro com Exceções;
            Assert.AreEqual(errors.Where(x => x.Exception != null).Count(), 2);
            
            var hits = result.GetMetadata.Values.OfType<IMetaHit>();
            //Um metadado de acerto
            Assert.AreEqual(hits.Count(), 1);
            //Um metadado com 'chunk';
            Assert.AreEqual(hits.Where(x => x.GetChunks.Count > 0).Count(), 1);
        }
    }
}
