using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using Newtonsoft.Json;
using NUnit.Framework;
using SavingsSpa.Controllers;
using SavingsSpa.Models;

namespace SavingsSpa.Tests
{
    [TestFixture]
    public class ValuesControllerTests
    {
        private ValuesController _controller;

        [SetUp]
        public void Given()
        {
            var validator = new InputSetValidator();
            _controller = new ValuesController(validator);
            _controller.Request = new HttpRequestMessage();
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
        }
        
        [Test]
        public void ShouldReturnBadRequestWhenInterestRateIsNotProvided()
        {
            var inputSet = new InputSet();
            inputSet.Principle = 1000;
            inputSet.TermLength = 60;
            inputSet.InterestRate = null;
            inputSet.InstrumentType = InstrumentType.TermDeposit;
            inputSet.PayoutType = PayoutType.Cumulative;
            Assert.AreEqual(HttpStatusCode.BadRequest,_controller.Get(inputSet).StatusCode);

            inputSet.InstrumentType = InstrumentType.RecurringDeposit;
            Assert.AreEqual(HttpStatusCode.BadRequest, _controller.Get(inputSet).StatusCode);

            inputSet.InstrumentType = InstrumentType.SimpleSavings;
            Assert.AreEqual(HttpStatusCode.BadRequest, _controller.Get(inputSet).StatusCode);
        }

        [Test]
        public void ShouldReturnBadRequestWhenTermLengthIsNotProvided()
        {
            var inputSet = new InputSet();
            inputSet.Principle = 1000;
            inputSet.TermLength = null;
            inputSet.InterestRate = 0;
            inputSet.InstrumentType = InstrumentType.TermDeposit;
            inputSet.PayoutType = PayoutType.Cumulative;
            Assert.AreEqual(HttpStatusCode.BadRequest, _controller.Get(inputSet).StatusCode);

            inputSet.InstrumentType = InstrumentType.RecurringDeposit;
            Assert.AreEqual(HttpStatusCode.BadRequest, _controller.Get(inputSet).StatusCode);
        }

        [Test]
        public void ShouldReturnBadRequestWhenPrincipalIsNotProvided()
        {
            var inputSet = new InputSet();
            inputSet.Principle = null;
            inputSet.TermLength = 60;
            inputSet.InterestRate = 0;
            inputSet.InstrumentType = InstrumentType.TermDeposit;
            inputSet.PayoutType = PayoutType.Cumulative;
            Assert.AreEqual(HttpStatusCode.BadRequest, _controller.Get(inputSet).StatusCode);
        }

        [Test]
        public void ShouldReturnBadRequestWhenMonthlySavingIsNotProvided()
        {
            var inputSet = new InputSet();
            inputSet.Principle = 10000;
            inputSet.TermLength = 60;
            inputSet.InterestRate = 0;
            inputSet.InstrumentType = InstrumentType.RecurringDeposit;
            inputSet.PayoutType = PayoutType.Cumulative;
            Assert.AreEqual(HttpStatusCode.BadRequest, _controller.Get(inputSet).StatusCode);
        }

        [Test]
        public void ShouldReturnOkRequest()
        {
            var inputSet = new InputSet();
            inputSet.Principle = 1000;
            inputSet.TermLength = 60;
            inputSet.InterestRate = 0;
            inputSet.InstrumentType = InstrumentType.TermDeposit;
            inputSet.PayoutType = PayoutType.Cumulative;
            inputSet.TermLengthType = TermLengthType.Month;

            Assert.AreEqual(HttpStatusCode.OK, _controller.Get(inputSet).StatusCode);
        }
    }
}