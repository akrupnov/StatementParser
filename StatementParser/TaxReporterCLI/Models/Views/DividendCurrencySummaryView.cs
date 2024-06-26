using System;
using System.Collections.Generic;
using System.Linq;
using StatementParser.Attributes;
using StatementParser.Models;

namespace TaxReporterCLI.Models.Views
{
	public class DividendCurrencySummaryView : IView
	{
		[Description("Exchanged to currency")]
		public Currency ExchangedToCurrency { get; }

		public Currency Currency { get; }

		[Description("Total Income")]
		public decimal TotalIncome { get; }

		[Description("Total Tax")]
		public decimal TotalTax { get; }

		[Description("Total Income in {ExchangedToCurrency} per Day")]
		public decimal ExchangedPerDayTotalIncome { get; }

		[Description("Total Income in {ExchangedToCurrency} per Year")]
		public decimal ExchangedPerYearTotalIncome { get; }

		[Description("Total Tax in {ExchangedToCurrency} per Day")]
		public decimal ExchangedPerDayTotalTax { get; }

		[Description("Total Tax in {ExchangedToCurrency} per Year")]
		public decimal ExchangedPerYearTotalTax { get; }

		public DividendCurrencySummaryView(IList<DividendTransactionView> transactions, Currency currency)
		{
			this.Currency = currency;

			var brokerTransactions = transactions.Where(i => i.Transaction.Currency == currency);

			foreach (var transaction in brokerTransactions)
			{
				ExchangedToCurrency = brokerTransactions.First().ExchangedToCurrency;
				var dividendTransaction = (transaction.Transaction as DividendTransaction);

				TotalIncome +=  dividendTransaction.Income;
				TotalTax += dividendTransaction.Tax;

				if (transaction.ExchangedPerDayIncome.HasValue)
				{
					ExchangedPerDayTotalIncome += transaction.ExchangedPerDayIncome.Value;
				}

				if (transaction.ExchangedPerYearIncome.HasValue)
				{
					ExchangedPerYearTotalIncome += transaction.ExchangedPerYearIncome.Value;
				}

				if (transaction.ExchangedPerDayTax.HasValue)
				{
					ExchangedPerDayTotalTax += transaction.ExchangedPerDayTax.Value;
				}

				if (transaction.ExchangedPerYearTax.HasValue)
				{
					ExchangedPerYearTotalTax += transaction.ExchangedPerYearTax.Value;
				}
			}
		}

		public override string ToString()
		{
			return $"{nameof(ExchangedToCurrency)}: {ExchangedToCurrency} {nameof(Currency)}: {Currency} {nameof(TotalIncome)}: {TotalIncome} {nameof(TotalTax)}: {TotalTax} {nameof(ExchangedPerDayTotalIncome)}: {ExchangedPerDayTotalIncome} {nameof(ExchangedPerYearTotalIncome)}: {ExchangedPerYearTotalIncome} {nameof(ExchangedPerDayTotalTax)}: {ExchangedPerDayTotalTax} {nameof(ExchangedPerYearTotalTax)}: {ExchangedPerYearTotalTax}";
		}

        public int CompareTo(IView other)
        {
            return 0;
        }
    }
}