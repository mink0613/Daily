using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daily.Model
{
    class TotalModel
    {
        private int _totalOutgoAmount;

        private int _totalAmountKakao;

        private int _totalAmountSamsung;

        private int _totalAmountHana;

        private int _totalAmountCash;

        private int _totalIncomeAmount;

        public int TotalOutgoAmount
        {
            get
            {
                return _totalOutgoAmount;
            }
            set
            {
                _totalOutgoAmount = value;
            }
        }

        public int TotalAmountKakao
        {
            get
            {
                return _totalAmountKakao;
            }
            set
            {
                _totalAmountKakao = value;
            }
        }

        public int TotalAmountSamsung
        {
            get
            {
                return _totalAmountSamsung;
            }
            set
            {
                _totalAmountSamsung = value;
            }
        }

        public int TotalAmountHana
        {
            get
            {
                return _totalAmountHana;
            }
            set
            {
                _totalAmountHana = value;
            }
        }

        public int TotalAmountCash
        {
            get
            {
                return _totalAmountCash;
            }
            set
            {
                _totalAmountCash = value;
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
    }
}
