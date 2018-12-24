using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily.Model
{
    class MonthTotalModel
    {
        private int _totalOutcomeAmount;

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
