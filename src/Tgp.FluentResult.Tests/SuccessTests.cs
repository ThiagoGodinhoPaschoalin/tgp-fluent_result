using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tgp.FluentResult.Core.Interfaces;

namespace Tgp.FluentResult.Tests
{
    class SuccessTests
    {
        [Test]
        public void Success1()
        {
            Result<string> result = Result.Success();

            Assert.IsFalse(result.IsFailed);
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<ISuccessMetadata>().Count());
        }

        [Test]
        public void Success2()
        {
            Result<Guid[]> result = Result.Success<Guid[]>(new[] { Guid.NewGuid(), Guid.NewGuid() });

            Assert.IsFalse(result.IsFailed);
            Assert.IsTrue(result.IsValidData);
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<ISuccessMetadata>().Count());
        }

        [Test]
        public void Success3()
        {
            Result<Guid[]> result = Task.Run<Result<Guid[]>>(() => Result.Success()).Result;

            Assert.IsFalse(result.IsFailed);
            //Com o implicit operator, criei um Result sem Data! mesmo ele sendo requisitado.
            Assert.IsFalse(result.IsValidData);
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<ISuccessMetadata>().Count());
        }

        [Test]
        public void Success4()
        {
            Result<string> result = Result.Success().AppendMeta(MetaResult.Warn("Atenção!"));

            Assert.IsFalse(result.IsFailed);
            Assert.AreEqual(2, result.GetMetadata.Count);
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<ISuccessMetadata>().Count());
            Assert.AreEqual(1, result.GetMetadata.Values.OfType<IWarnMetadata>().Count());
        }
    }
}