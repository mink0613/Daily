using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Daily.Model
{
    public class GraphModel
    {
        private string _date;

        private ObservableCollection<KeyValuePair<ItemType, int>> _itemList = new ObservableCollection<KeyValuePair<ItemType, int>>();

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
                int amount = 0;
                foreach (var item in _itemList)
                {
                    amount += item.Value;
                }

                return amount;
            }
        }

        public string ToolTipStr
        {
            get
            {
                string toolTip = string.Empty;

                var samsung = _itemList.Where(x => x.Key == ItemType.Outgo || x.Key == ItemType.Samsung).ToList();
                if (samsung != null && samsung.Count > 0)
                {
                    toolTip += string.Format("삼성: {0:n0}\n", GetTotalAmount(samsung));
                }

                var kakao = _itemList.Where(x => x.Key == ItemType.Kakao).ToList();
                if (kakao != null && kakao.Count > 0)
                {
                    toolTip += string.Format("카카오: {0:n0}\n", GetTotalAmount(kakao));
                }

                var hana = _itemList.Where(x => x.Key == ItemType.Hana).ToList();
                if (hana != null && hana.Count > 0)
                {
                    toolTip += string.Format("하나: {0:n0}\n", GetTotalAmount(hana));
                }

                var hyundai = _itemList.Where(x => x.Key == ItemType.Hyundai).ToList();
                if (hyundai != null && hyundai.Count > 0)
                {
                    toolTip += string.Format("현대: {0:n0}\n", GetTotalAmount(hyundai));
                }

                var cash = _itemList.Where(x => x.Key == ItemType.Cash).ToList();
                if (cash != null && cash.Count > 0)
                {
                    toolTip += string.Format("현금: {0:n0}\n", GetTotalAmount(cash));
                }

                return toolTip.TrimEnd('\n');
            }
        }

        private int GetTotalAmount(List<KeyValuePair<ItemType, int>> items)
        {
            int amount = 0;
            foreach (KeyValuePair< ItemType, int> item in items)
            {
                amount += item.Value;
            }

            return amount;
        }

        public void AddItem(ItemType type, int amount)
        {
            _itemList.Add(new KeyValuePair<ItemType, int>(type, amount));
        }

        public GraphModel(string date)
        {
            this.Date = date;
        }
    }
}
