using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily.Model
{
    class TotalModel
    {
        private int _totalOutcomeAmount;

        private int _totalOutcomeAmountSamsung;

        private int _totalOutcomeAmountHana;

        private int _totalOutcomeAmountCash;

        private int _totalIncomeAmount;

        private int _maxOutcomeAmount;

        public int TotalOutcomeAmount
        {
            get
            {
                return _totalOutcomeAmount;
            }
            set
            {
                _totalOutcomeAmount = value;
            }
        }

        public int TotalOutcomeAmountSamsung
        {
            get
            {
                return _totalOutcomeAmountSamsung;
            }
            set
            {
                _totalOutcomeAmountSamsung = value;
            }
        }

        public int TotalOutcomeAmountHana
        {
            get
            {
                return _totalOutcomeAmountHana;
            }
            set
            {
                _totalOutcomeAmountHana = value;
            }
        }

        public int TotalOutcomeAmountCash
        {
            get
            {
                return _totalOutcomeAmountCash;
            }
            set
            {
                _totalOutcomeAmountCash = value;
            }
        }

        public int TotalIncomeAmount
        {
            get
            {
                return _totalIncomeAmount;
            }
            set
            {
                _totalIncomeAmount = value;
            }
        }

        public int MaxOutcomeAmount
        {
            get
            {
                return _maxOutcomeAmount;
            }
            set
            {
                _maxOutcomeAmount = value;
            }
        }
    }
}
