using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SavingsSpa.Controllers;

namespace SavingsSpa.Models
{
    public class Calculator
    {
        public List<ResultSet> ResultSets(InputSet inputSet)
        {
            var resultSet = new List<ResultSet>();
            var p = (double)inputSet.Principle.Value;
            var r = (double)inputSet.InterestRate;
            if (inputSet.TermLengthType == TermLengthType.Month)
                r = r / 12;
            if (inputSet.InstrumentType == InstrumentType.TermDeposit)
            {
                for (int i = 0; i < inputSet.TermLength.Value; i++)
                {
                    var item = new ResultSet
                    {
                        Interval = 0,
                        Value = p * (1 + (r / 100))
                    };
                    p = item.Value;
                    item.Value = Math.Round(item.Value);
                    resultSet.Add(item);
                }
            }

            if (inputSet.InstrumentType == InstrumentType.RecurringDeposit)
            {
                for (int i = 0; i < inputSet.TermLength.Value; i++)
                {
                    p = p + inputSet.MonthlySaving.Value;
                    var item = new ResultSet
                    {
                        Interval = 0,
                        Value = p * (1 + (r / 100))
                    };
                    p = item.Value;
                    item.Value = Math.Round(item.Value);
                    resultSet.Add(item);
                }
            }

            return resultSet;
        }
    }
}