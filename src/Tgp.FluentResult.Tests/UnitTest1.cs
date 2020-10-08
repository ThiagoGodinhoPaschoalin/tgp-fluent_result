using NUnit.Framework;

namespace Tgp.FluentResult.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var result = Result.Failure("Não deu certo");
            Assert.IsTrue(result.IsFailed);
        }

        [Test]
        public void Test2()
        {
            var result = Result.Failure("Não deu certo").AppendMeta(MetaResult.Hit("Sucesso!"));
            Assert.IsTrue(result.IsFailed);
        }

        [Test]
        public void Test3()
        {
            var result = Result.Success("Sucesso!!!").AppendMeta(MetaResult.Error("Deu Ruim!"));
            Assert.IsFalse(result.IsFailed);
        }
    }
}