using Daily.Common;
using Daily.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UILibrary.Base;

namespace Daily.ViewModel
{
    public class DailyViewModel : BaseViewModel
    {
        private ObservableCollection<DailyModel> _itemCollection;

        private DailyModel _selectedItem;

        private List<ItemType> _typeList;

        private ItemType _selectedType;

        private bool _isAddMode;

        private ICommand _addUpdateClick;

        private ICommand _clearClick;

        private string _addUpdateText;

        private string _date;

        private string _name;

        private string _amount;

        private int _totalAmount;

        public ObservableCollection<DailyModel> ItemCollection
        {
            get
            {
                return _itemCollection;
            }
            set
            {
                _itemCollection = value;
                OnPropertyChanged();
            }
        }

        public DailyModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _isAddMode = false;
                _selectedItem = value;
                UpdateText();
                OnPropertyChanged();
            }
        }

        public List<ItemType> TypeList
        {
            get
            {
                return _typeList;
            }
            set
            {
                _typeList = value;
                OnPropertyChanged();
            }
        }

        public ItemType SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddUpdateClick
        {
            get
            {
                return _addUpdateClick;
            }
        }

        public ICommand ClearClick
        {
            get
            {
                return _clearClick;
            }
        }

        public string AddUpdateText
        {
            get
            {
                return _addUpdateText;
            }
            set
            {
                _addUpdateText = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public int TotalAmount
        {
            get
            {
                return _totalAmount;
            }
            set
            {
                _totalAmount = value;
                OnPropertyChanged();
            }
        }

        private DateTime GetMondayOfWeek(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Monday)
            {
                return GetMondayOfWeek(date.AddDays(-1));
            }

            return date;
        }

        private DateTime GetSundayOfWeek(DateTime date)
        {
            if (date.DayOfWeek != DayOfWeek.Sunday)
            {
                return GetSundayOfWeek(date.AddDays(1));
            }

            return date;
        }

        private string ToStringDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        private void Initialize()
        {
            _isAddMode = true;

            TypeList = new List<ItemType>();
            TypeList.Add(ItemType.Outcome);
            TypeList.Add(ItemType.Income);

            SelectedType = TypeList[0];

            ItemCollection = new ObservableCollection<DailyModel>();
            TotalAmount = 0;

            InitializeList();
            TextBoxInitialize();
            UpdateText();

            _addUpdateClick = new RelayCommand((param) => AddUpdate(), true);
            _clearClick = new RelayCommand((param) => Clear(), true);
        }

        private void InitializeList()
        {
            DailyAccountDB db = new DailyAccountDB();
            string monday = ToStringDate(GetMondayOfWeek(DateTime.Now));
            string sunday = ToStringDate(GetSundayOfWeek(DateTime.Now));

            string result = db.GetWeeklyAccount(monday, sunday);

            try
            {
                var models = JsonConvert.DeserializeObject<List<DailyModel>>(result);
                foreach(DailyModel model in models)
                {
                    ItemCollection.Add(model);
                }

                UpdateListView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AddUpdate()
        {
            if (_date.Length == 0 || _name.Length == 0 || _amount.Length == 0)
            {
                return;
            }

            if (_isAddMode == false)
            {
                ItemCollection.Remove(_selectedItem);
            }

            DailyModel newItem = new DailyModel();
            newItem.Type = _selectedType;
            newItem.Date = _date;
            newItem.Name = _name;
            newItem.Amount = int.Parse(_amount);

            ItemCollection.Add(newItem);

            newItem.PostData();

            UpdateListView();
            TextBoxInitialize();
            UpdateText();
        }

        private void UpdateListView()
        {
            ObservableCollection<DailyModel> temp = new ObservableCollection<DailyModel>(ItemCollection.OrderByDescending(x => x.Date));
            ItemCollection.Clear();
            TotalAmount = 0;

            for (int i = 0; i < temp.Count; i++)
            {
                ItemCollection.Add(temp[i]);
                if (temp[i].Type == ItemType.Outcome)
                {
                    TotalAmount -= temp[i].Amount;
                }
                else
                {
                    TotalAmount += temp[i].Amount;
                }
            }
        }

        private void Clear()
        {
            _isAddMode = true;
            _selectedItem = null;
            TextBoxInitialize();
            UpdateText();
        }

        private void TextBoxInitialize()
        {
            _isAddMode = true;

            SelectedType = ItemType.Outcome;
            Date = ToStringDate(DateTime.Now);
            Name = "";
            Amount = "";
        }

        private void UpdateText()
        {
            if (_isAddMode == true)
            {
                AddUpdateText = "추가";
            }
            else
            {
                AddUpdateText = "업데이트";
            }

            if (_selectedItem != null)
            {
                Date = _selectedItem.Date;
                Name = _selectedItem.Name;
                Amount = _selectedItem.Amount.ToString();
            }
        }

        public DailyViewModel()
        {
            Initialize();
        }
    }
}
