using Daily.Common;
using Daily.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UILibrary.Base;

namespace Daily.ViewModel
{
    public enum GraphType
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }

    public class DailyViewModel : BaseViewModel
    {
        private readonly int _periodStartDate = 16;

        private ObservableCollection<DailyModel> _itemCollection;

        private ObservableCollection<KeyValuePair<string, int>> _graphItemCollection;

        private GraphType _selectedGraphType;

        private DailyModel _selectedItem;

        private List<ItemType> _typeList;

        private ItemType _selectedType;

        private TotalAmountType _totalType;

        private bool _isAddMode;

        private int _monthSearched;

        private ICommand _refreshClick;

        private ICommand _prevWeekClick;

        private ICommand _nextWeekClick;

        private ICommand _deleteClick;

        private ICommand _addUpdateClick;

        private ICommand _clearClick;

        private ICommand _graphToggleClick;

        private string _mondayDate;

        private string _sundayDate;

        private string _addUpdateText;

        private string _date;

        private string _name;

        private string _amount;

        private string _periodTotal;

        private string _monthTotal;

        private int _totalIncome;

        private int _totalOutcome;

        private int _totalAmount;

        private int _periodTotalAmount;

        private int _monthTotalAmount;

        private int _week;

        private DateTime _selectedDate;

        private DateTime _lastSearchedStartDayofPeriod;

        private bool _isShowGraph;

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

        public ObservableCollection<KeyValuePair<string, int>> GraphItemCollection
        {
            get
            {
                return _graphItemCollection;
            }
            set
            {
                _graphItemCollection = value;
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

        public TotalAmountType TotalType
        {
            get
            {
                return _totalType;
            }
            set
            {
                _totalType = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshClick
        {
            get
            {
                return _refreshClick;
            }
        }

        public ICommand PrevWeekClick
        {
            get
            {
                return _prevWeekClick;
            }
        }

        public ICommand NextWeekClick
        {
            get
            {
                return _nextWeekClick;
            }
        }

        public ICommand DeleteClick
        {
            get
            {
                return _deleteClick;
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

        public ICommand GraphToggleClick
        {
            get
            {
                return _graphToggleClick;
            }
        }

        public string MondayDate
        {
            get
            {
                return _mondayDate;
            }
            set
            {
                _mondayDate = value;
                OnPropertyChanged();
            }
        }

        public string SundayDate
        {
            get
            {
                return _sundayDate;
            }
            set
            {
                _sundayDate = value;
                OnPropertyChanged();
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

        public string PeriodTotal
        {
            get
            {
                return _periodTotal;
            }
            set
            {
                _periodTotal = value;
                OnPropertyChanged();
            }
        }

        public string MonthTotal
        {
            get
            {
                return _monthTotal;
            }
            set
            {
                _monthTotal = value;
                OnPropertyChanged();
            }
        }

        public int TotalIncome
        {
            get
            {
                return _totalIncome;
            }
            set
            {
                _totalIncome = value;
                OnPropertyChanged();
            }
        }

        public int TotalOutcome
        {
            get
            {
                return _totalOutcome;
            }
            set
            {
                _totalOutcome = value;
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
                if (_totalAmount < 0)
                {
                    TotalType = TotalAmountType.Minus;
                }
                else
                {
                    TotalType = TotalAmountType.Plus;
                }

                OnPropertyChanged();
            }
        }

        public int PeriodTotalAmount
        {
            get
            {
                return _periodTotalAmount;
            }
            set
            {
                _periodTotalAmount = value;
                OnPropertyChanged();
            }
        }

        public int MonthTotalAmount
        {
            get
            {
                return _monthTotalAmount;
            }
            set
            {
                _monthTotalAmount = value;
                OnPropertyChanged();
            }
        }

        public bool IsShowGraph
        {
            get
            {
                return _isShowGraph;
            }
            set
            {
                _isShowGraph = value;
                OnPropertyChanged();
            }
        }

        private DateTime GetDayOfWeek(DateTime date, DayOfWeek day)
        {
            if (date.DayOfWeek < day)
            {
                return GetDayOfWeek(date.AddDays(1), day);
            }
            else if (date.DayOfWeek > day)
            {
                // Let Sunday be the last day of week
                if (day == DayOfWeek.Sunday)
                {
                    return GetDayOfWeek(date.AddDays(1), day);
                }
                else
                {
                    return GetDayOfWeek(date.AddDays(-1), day);
                }
            }
            return date;
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

        private DateTime GetStartDayofPeriod(DateTime date)
        {
            if (date.Day != _periodStartDate)
            {
                return GetStartDayofPeriod(date.AddDays(-1));
            }

            return date;
        }

        private DateTime GetEndDayofPeriod(DateTime date)
        {
            if (date.Day != _periodStartDate - 1)
            {
                return GetEndDayofPeriod(date.AddDays(1));
            }

            return date;
        }

        private string ToStringDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        private string ToStringDateMMDD(DateTime date)
        {
            return date.ToString("MM-dd");
        }

        private void Initialize()
        {
            _isAddMode = true;

            TypeList = new List<ItemType>();
            TypeList.Add(ItemType.Outcome);
            TypeList.Add(ItemType.Income);

            SelectedType = TypeList[0];

            ItemCollection = new ObservableCollection<DailyModel>();
            GraphItemCollection = new ObservableCollection<KeyValuePair<string, int>>();

            TotalIncome = 0;
            TotalOutcome = 0;
            TotalAmount = 0;
            _week = 0;
            _selectedDate = DateTime.Today.AddDays(_week * 7);

            _selectedGraphType = GraphType.Daily;
            IsShowGraph = false;

            MondayDate = ToStringDate(GetMondayOfWeek(_selectedDate));
            SundayDate = ToStringDate(GetSundayOfWeek(_selectedDate));

            Refresh();

            _refreshClick = new RelayCommand((param) => Refresh(), true);
            _prevWeekClick = new RelayCommand((param) => PrevWeek(), true);
            _nextWeekClick = new RelayCommand((param) => NextWeek(), true);

            _deleteClick = new RelayCommand((param) => Delete(), true);
            _addUpdateClick = new RelayCommand((param) => AddUpdate(), true);
            _clearClick = new RelayCommand((param) => Clear(), true);
            _graphToggleClick = new RelayCommand((param) => GraphToggle(), true);
        }

        private void UpdateGraphButtonImage()
        {
            IsShowGraph = !IsShowGraph;
            UpdateGraph();
        }

        private void UpdateGraph()
        {
            if (IsShowGraph == false)
            {
                return;
            }

            ObservableCollection<KeyValuePair<string, int>> tempCollection = new ObservableCollection<KeyValuePair<string, int>>();

            // Initialize collection
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Monday)), 0));
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Tuesday)), 0));
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Wednesday)), 0));
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Thursday)), 0));
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Friday)), 0));
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Saturday)), 0));
            tempCollection.Add(new KeyValuePair<string, int>(ToStringDate(GetDayOfWeek(_selectedDate, DayOfWeek.Sunday)), 0));

            for (int i = ItemCollection.Count - 1; i >= 0; i--)
            {
                if (ItemCollection[i].Type == ItemType.Income)
                {
                    continue;
                }

                var itemList = tempCollection.Where(x => x.Key.Equals(ItemCollection[i].Date));
                if (itemList.Count() == 0)
                {
                    tempCollection.Add(new KeyValuePair<string, int>(ItemCollection[i].Date, ItemCollection[i].Amount));
                }
                else
                {
                    var item = itemList.First();
                    KeyValuePair<string, int> newItem = new KeyValuePair<string, int>(item.Key, item.Value + ItemCollection[i].Amount);

                    tempCollection.Remove(item);
                    tempCollection.Add(newItem);
                }
            }

            // Save to GraphItemCollection
            GraphItemCollection.Clear();
            var sortedCollection = tempCollection.OrderBy(x => x.Key);
            for (int i = 0; i < sortedCollection.Count(); i++)
            {
                GraphItemCollection.Add(sortedCollection.ElementAt(i));
            }
        }

        private void InitializeList()
        {
            // Calculate weekly total account info
            DailyAccountDB db = new DailyAccountDB();
            string monday = ToStringDate(GetMondayOfWeek(_selectedDate));
            string sunday = ToStringDate(GetSundayOfWeek(_selectedDate));

            string result = db.GetWeeklyAccount(monday, sunday);

            try
            {
                ItemCollection.Clear();
                var models = JsonConvert.DeserializeObject<List<DailyModel>>(result);
                foreach (DailyModel model in models)
                {
                    ItemCollection.Add(model);
                }

                UpdateListView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            // Calculate period total account info
            DateTime startDayofPeriod = GetStartDayofPeriod(_selectedDate);
            if (_lastSearchedStartDayofPeriod != startDayofPeriod)
            {
                DateTime endDayofPeriod = GetEndDayofPeriod(_selectedDate);
                result = db.GetPeriodTotalAccount(ToStringDate(startDayofPeriod), ToStringDate(endDayofPeriod));
                try
                {
                    var model = JsonConvert.DeserializeObject<TotalModel>(result);
                    PeriodTotal = ToStringDateMMDD(startDayofPeriod) + " ~ " + ToStringDateMMDD(endDayofPeriod) + " 지출 총액";
                    PeriodTotalAmount = model.TotalOutcomeAmount;

                    _lastSearchedStartDayofPeriod = startDayofPeriod;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            // Calculate monthly total account info
            int month = _selectedDate.Month;
            if (month != _monthSearched)
            {
                result = db.GetMonthlyTotalAccount(_selectedDate.Year, _selectedDate.Month);
                try
                {
                    var model = JsonConvert.DeserializeObject<TotalModel>(result);
                    MonthTotal = month + " 월 지출 총액";
                    MonthTotalAmount = model.TotalOutcomeAmount;

                    _monthSearched = month;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void Refresh()
        {
            InitializeList();
            TextBoxInitialize();
            UpdateText();
            UpdateGraph();
        }

        private void PrevWeek()
        {
            _week--;
            _selectedDate = DateTime.Today.AddDays(_week * 7);
            MondayDate = ToStringDate(GetMondayOfWeek(_selectedDate));
            SundayDate = ToStringDate(GetSundayOfWeek(_selectedDate));
            Refresh();
        }

        private void NextWeek()
        {
            _week++;
            _selectedDate = DateTime.Today.AddDays(_week * 7);
            MondayDate = ToStringDate(GetMondayOfWeek(_selectedDate));
            SundayDate = ToStringDate(GetSundayOfWeek(_selectedDate));
            Refresh();
        }

        private void Delete()
        {
            if (_isAddMode == false && _selectedItem != null)
            {
                if (MessageBox.Show("정말로 지우겠습니까?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _selectedItem.DeleteData();
                    ItemCollection.Remove(_selectedItem);

                    UpdateListView();
                    TextBoxInitialize();
                    UpdateText();
                }
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
                _selectedItem.DeleteData();
                ItemCollection.Remove(_selectedItem);
            }

            DailyModel newItem = new DailyModel();
            newItem.Type = _selectedType;
            newItem.Date = _date;
            newItem.Name = _name;
            newItem.Amount = int.Parse(_amount.Replace(",", ""));

            ItemCollection.Add(newItem);

            newItem.AddData();

            _lastSearchedStartDayofPeriod = DateTime.MinValue; // In order to update period total amount
            _monthSearched = 0; // In order to update monthly total amount
            InitializeList(); // In order to update all the IDs
            TextBoxInitialize();
            UpdateText();
        }

        private void UpdateListView()
        {
            ObservableCollection<DailyModel> temp = new ObservableCollection<DailyModel>(ItemCollection.OrderByDescending(x => x.Date));
            ItemCollection.Clear();
            TotalAmount = 0;
            TotalOutcome = 0;
            TotalIncome = 0;

            for (int i = 0; i < temp.Count; i++)
            {
                ItemCollection.Add(temp[i]);
                if (temp[i].Type == ItemType.Outcome)
                {
                    TotalOutcome += temp[i].Amount;
                    TotalAmount -= temp[i].Amount;
                }
                else
                {
                    TotalIncome += temp[i].Amount;
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

        private void GraphToggle()
        {
            UpdateGraphButtonImage();
        }

        private void TextBoxInitialize()
        {
            _isAddMode = true;

            SelectedType = ItemType.Outcome;
            Date = ToStringDate(_selectedDate);
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

        public void CheckDateFormat()
        {
            string dateDigitOnly = Date.Replace("-", "");
            int digits = 0;
            if (int.TryParse(dateDigitOnly, out digits) == false || dateDigitOnly.Length != 8)
            {
                Date = ToStringDate(_selectedDate);
            }
        }

        public DailyViewModel()
        {
            Initialize();
        }
    }
}
