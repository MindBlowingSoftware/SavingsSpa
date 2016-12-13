using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Framework;
using SavingsSpa.Controllers;
using SavingsSpa.Models;

namespace SavingsSpa.Tests
{
    [TestFixture]
    public class ResultSetTests
    {
        public InputSet _inputSet;
        public List<ResultSet> _resultSet;
        private ValuesController _controller;

        [SetUp]
        public void Given()
        {
            var validator = new InputSetValidator();
            _controller = new ValuesController(validator);
            _inputSet = new InputSet();
            _inputSet.Principle = 20000;
            _inputSet.TermLength = 60;
            _inputSet.InterestRate = 7;
            _inputSet.InstrumentType = InstrumentType.TermDeposit;
            _inputSet.PayoutType = PayoutType.Cumulative;
            _inputSet.TermLengthType = TermLengthType.Month;
            _inputSet.MonthlySaving = 3000;
            _inputSet.CompoundedDuration = CompoundedDuration.Annually;

            _resultSet = _controller.ResultSets(_inputSet);
        }


        [Test]
        public void ResultSetShouldHaveCorrectNumberoFRows()
        {
            Assert.AreEqual(60,_resultSet.Count);
        }

        [Test]
        public void FinalvalueShouldBeAsexpectedfromAnonlineCalculator1()
        {
            Assert.AreEqual(28353, _resultSet.Select(r => r.Value).Max());
        }

        [Test]
        public void FinalvalueShouldBeAsexpectedfromAnonlineCalculator2()
        {
            _inputSet.Principle = 0;
            _inputSet.InstrumentType = InstrumentType.RecurringDeposit;
            _resultSet = _controller.ResultSets(_inputSet);
            Assert.AreEqual(216032, _resultSet.Select(r => r.Value).Max());
        }

        [Test]
        public void FinalvalueShouldBeAsexpectedfromAnonlineCalculator3()
        {
            _inputSet.Principle = 20000;
            _inputSet.InstrumentType = InstrumentType.RecurringDeposit;
            _resultSet = _controller.ResultSets(_inputSet);
            Assert.AreEqual(244384, _resultSet.Select(r => r.Value).Max());
        }
    }
}
