using Daily.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UILibrary.Base;
using System.Linq;

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

        private string _addUpdateText;

        private string _date;

        private string _name;

        private string _amount;

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
                _isAddMode = true;
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

        private void Initialize()
        {
            _isAddMode = true;

            TypeList = new List<ItemType>();
            TypeList.Add(ItemType.Outcome);
            TypeList.Add(ItemType.Income);

            SelectedType = TypeList[0];

            TextBoxInitialize();
            UpdateText();

            _addUpdateClick = new RelayCommand((param) => AddUpdate(), true);
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

            ObservableCollection<DailyModel> temp = new ObservableCollection<DailyModel>();
            for (int i = 0; i < ItemCollection.Count; i++)
            {
                temp.Add(ItemCollection[i]);
            }

            ItemCollection.Clear();
            ItemCollection = null;

            ItemCollection = new ObservableCollection<DailyModel>(temp.OrderByDescending(x => x.Date));

            TextBoxInitialize();
            UpdateText();
        }

        private void TextBoxInitialize()
        {
            _isAddMode = false;

            SelectedType = ItemType.Outcome;
            Date = DateTime.Now.ToString("yyyyMMdd");
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
        }

        public DailyViewModel()
        {
            Initialize();
        }
    }
}
