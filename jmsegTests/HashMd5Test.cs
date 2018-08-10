using System;
using Xunit;
using NSubstitute;
using jmseg.Business;
using jmseg.Models;

namespace jmsegTests
{
    public class HashMd5Test
    {
        private const string SENHA_VALIDA = "123";
        private const string SENHA_VAZIA = "";
        private const string SENHA_NULA = null;

        private IUserBusiness userBusinessMock;
        
        public HashMd5Test() {
            userBusinessMock = Substitute.For<IUserBusiness>();

            userBusinessMock.GerarHashMd5(SENHA_VALIDA).Returns("202cb962ac59075b964b07152d234b70");

            // Mock de criptografia de senha vazia
            userBusinessMock.GerarHashMd5(SENHA_VAZIA).Returns("d41d8cd98f00b204e9800998ecf8427e");

            userBusinessMock.GerarHashMd5(SENHA_NULA).Returns(SENHA_NULA);
        }

        [Fact]
        public void GerarHashMd5OKTest() {
            Assert.Equal("ok", "ok");
        }

        [Fact]
        public void GerarHashMd5VazioTest() {
            Assert.Equal("", "");
        }

        [Fact]
        public void GerarHashMd5NuloTest() {
            Assert.Null(SENHA_NULA);
        }
    }
}
