using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            var result = Result.Success();

            Assert.IsFalse(result.IsFailed);
            Assert.AreEqual(result.GetMetadata.Values.OfType<IMetaHit>().Count(), 1);
        }

        [Test]
        public void Success2()
        {
            var result = Result.Success<Guid[]>(new[] { Guid.NewGuid(), Guid.NewGuid() });

            Assert.IsFalse(result.IsFailed);
            Assert.IsTrue(result.IsValidData);
            Assert.AreEqual(result.GetMetadata.Values.OfType<IMetaHit>().Count(), 1);
        }

        [Test]
        public void Success3()
        {
            var result = Task.Run<Result<Guid[]>>(() => Result.Success()).Result;

            Assert.IsFalse(result.IsFailed);
            //Com o implicit operator, criei um Result sem Data! mesmo ele sendo requisitado.
            Assert.IsFalse(result.IsValidData);
            Assert.AreEqual(result.GetMetadata.Values.OfType<IMetaHit>().Count(), 1);
        }
    }
}