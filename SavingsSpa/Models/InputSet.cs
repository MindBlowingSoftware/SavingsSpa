using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SavingsSpa.Models
{
    public class InputSet
    {
        public int? TermLength { get; set; }
        public TermLengthType TermLengthType { get; set; }
        public double? InterestRate { get; set; }
        public int? Principle { get; set; }
        public InstrumentType InstrumentType { get; set; }
        public PayoutType PayoutType { get; set; }
        public CompoundedDuration CompoundedDuration { get; set; }
        public int? MonthlySaving { get; set; }
    }

    public enum CompoundedDuration
    {
        Monthly = 0,
        SemiAnnually = 1,
        Annually = 2
    }

    public enum PayoutType
    {
        Monthly = 0,
        Quaterly = 1,
        Cumulative = 2

    }

    public enum InstrumentType
    {
        TermDeposit = 0,
        RecurringDeposit = 1,
        SimpleSavings = 2
    }

    public enum TermLengthType
    {
        Month = 0,
        Year = 1
    }
}