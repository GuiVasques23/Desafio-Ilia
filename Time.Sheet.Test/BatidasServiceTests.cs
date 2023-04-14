using Microsoft.AspNetCore.Http;
using Moq;
using System.Net.Http;
using System.Net;
using System.Text;
using Time.Sheet.Application.Services;
using Time.Sheet.Domain.Models;
using Time.Sheet.Domain.Services;
using Time.Sheet.Infraestructure.Repositories;
using Xunit;
using MSTestAssert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Newtonsoft.Json;
using Azure.Core;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Security.Cryptography;
using Time.Sheet.Domain.Utils;
using System.Globalization;

namespace Time.Sheet.Test
{
    public class BatidasServiceTests
    {
        private readonly IBatidasService _batidasService;
        private readonly IRegistrosRepository _registrosRepository;

        public BatidasServiceTests()
        {
            _registrosRepository = new RegistrosRepository();
            _batidasService = new BatidasService(_registrosRepository);
        }

        [Fact]
        public async Task InserirBatidaAsync_DeveRetornarStatus201QuandoInserirBatidaValida()
        {
            // Arrange
            var momento = new Momento
            {
                DataHora = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            // Act
            var response = await _batidasService.InserirBatidaAsync(momento);

            // Assert
            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async Task InserirBatidaAsync_DeveRetornarStatus400QuandoDataHoraForInvalida()
        {
            // Arrange
            var momento = new Momento
            {
                DataHora = "DataHoraInvalida"
            };

            // Act
            var response = await _batidasService.InserirBatidaAsync(momento);

            // Assert
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Data e hora em formato inválido", response.Content);
        }

        [Fact]
        public async Task InserirBatidaAsync_DeveRetornarStatus403QuandoDiaForSabadoOuDomingo()
        {
            // Arrange
            var sabado = new DateTime(2022, 04, 16, 10, 0, 0); // sábado
            var domingo = new DateTime(2022, 04, 17, 10, 0, 0); // domingo

            var momentoSabado = new Momento
            {
                DataHora = sabado.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            var momentoDomingo = new Momento
            {
                DataHora = domingo.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            // Act
            var responseSabado = await _batidasService.InserirBatidaAsync(momentoSabado);
            var responseDomingo = await _batidasService.InserirBatidaAsync(momentoDomingo);

            // Assert
            Assert.Equal(403, responseSabado.StatusCode);
            Assert.Equal("Sábado e domingo não são permitidos como dia de trabalho", responseSabado.Content);

            Assert.Equal(403, responseDomingo.StatusCode);
            Assert.Equal("Sábado e domingo não são permitidos como dia de trabalho", responseDomingo.Content);
        }
    }
}
