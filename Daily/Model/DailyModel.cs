using Daily.Common;
using System;

namespace Daily.Model
{
    public enum ItemType
    {
        Outcome,
        Income
    }

    public class DailyModel
    {
        private ItemType _type;

        private string _name;

        private string _date;

        private int _amount;

        private DailyAccountDB _db = new DailyAccountDB();

        public ItemType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        public int Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        public void PostData()
        {
            _db.PostDailyAccount(_type, _date, _name, _amount);
        }
    }
}
