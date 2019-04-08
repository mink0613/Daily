using Daily.Common;
using Daily.Model;
using Daily.View;
using LiveCharts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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

        private DailyAccountDB _database;

        private ObservableCollection<DailyModel> _itemCollection;

        private ObservableCollection<KeyValuePair<string, int>> _graphItemCollection;

        private SeriesCollection _graphDataCollection;

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

        private ICommand _printClick;

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

        public SeriesCollection GraphDataCollection
        {
            get
            {
                return _graphDataCollection;
            }
            set
            {
                _graphDataCollection = value;
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

        public ICommand PrintClick
        {
            get
            {
                return _printClick;
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

            _database = new DailyAccountDB();

            TypeList = new List<ItemType>();
            TypeList.Add(ItemType.Outcome);
            TypeList.Add(ItemType.Income);

            SelectedType = TypeList[0];

            ItemCollection = new ObservableCollection<DailyModel>();
            GraphItemCollection = new ObservableCollection<KeyValuePair<string, int>>();
            GraphDataCollection = new SeriesCollection();
            
            TotalIncome = 0;
            TotalOutcome = 0;
            TotalAmount = 0;
            _week = 0;
            _selectedDate = DateTime.Today.AddDays(_week * 7);

            IsShowGraph = false;

            MondayDate = ToStringDate(GetMondayOfWeek(_selectedDate));
            SundayDate = ToStringDate(GetSundayOfWeek(_selectedDate));

            Refresh();

            _refreshClick = new RelayCommand((param) => Refresh(), true);
            _prevWeekClick = new RelayCommand((param) => PrevWeek(), true);
            _nextWeekClick = new RelayCommand((param) => NextWeek(), true);

            _deleteClick = new RelayCommand((param) => Delete(), true);
            _addUpdateClick = new RelayCommand((param) => AddUpdate(), true);
            _printClick = new RelayCommand((param) => Print(), true);
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
            GraphDataCollection.Clear();

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
            string monday = ToStringDate(GetMondayOfWeek(_selectedDate));
            string sunday = ToStringDate(GetSundayOfWeek(_selectedDate));

            string result = _database.GetWeeklyAccount(monday, sunday);

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
                result = _database.GetPeriodTotalAccount(ToStringDate(startDayofPeriod), ToStringDate(endDayofPeriod));
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
                result = _database.GetMonthlyTotalAccount(_selectedDate.Year, _selectedDate.Month);
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

        private void Print()
        {
            CalendarView calendar = new CalendarView();
            calendar.ViewModel.OKClicked += (s, e) =>
            {
                var data = e.EventData;

                var startDate = data.Item1;
                var endDate = data.Item2;

                if (startDate > endDate)
                {
                    var temp = startDate;
                    startDate = endDate;
                    endDate = temp;
                }

                var result = _database.GetPeriodListAccount(ToStringDate(startDate), ToStringDate(endDate));
                try
                {
                    var modelList = JsonConvert.DeserializeObject<List<DailyModel>>(result);
                    modelList.Sort((DailyModel x, DailyModel y) => x.Date.CompareTo(y.Date));

                    if (modelList != null && modelList.Count > 0)
                    {
                        Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                        Microsoft.Office.Interop.Word.Document document = new Microsoft.Office.Interop.Word.Document();

                        Object oMissing = System.Reflection.Missing.Value;
                        Object oFalse = false;

                        Microsoft.Office.Interop.Word.Paragraph paragraph = document.Content.Paragraphs.Add(ref oMissing);
                        paragraph.Range.Font.Size = 15;

                        object start = 0;
                        object end = 0;
                        object oEndOfDoc = "\\endofdoc";
                        Microsoft.Office.Interop.Word.Range tableLocation = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
                        var table = document.Content.Tables.Add(tableLocation, modelList.Count, 4);
                        table.Range.Font.Size = 15;
                        table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                        table.AllowAutoFit = true;

                        int row = 1;
                        foreach (var model in modelList)
                        {
                            table.Cell(row, 1).Range.Text = model.Date;
                            table.Cell(row, 2).Range.Text = model.Type.ToString();
                            table.Cell(row, 3).Range.Text = model.Name;

                            if (model.Type == ItemType.Outcome)
                            {
                                table.Cell(row, 4).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;
                            }
                            else
                            {
                                table.Cell(row, 4).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlue;
                            }

                            table.Cell(row, 4).Range.Text = "\\ " + string.Format("{0:###,###,###,###,###,###,###}", model.Amount);

                            row++;
                        }

                        table.Columns[1].AutoFit();
                        Single width1 = table.Columns[1].Width;
                        table.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent); // fill page width
                        table.Columns[1].SetWidth(width1, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustFirstColumn);

                        table.Columns[2].AutoFit();
                        Single width2 = table.Columns[2].Width;
                        table.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent); // fill page width
                        table.Columns[2].SetWidth(width2, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustFirstColumn);

                        table.Columns[3].AutoFit();
                        Single width3 = table.Columns[3].Width;
                        table.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent); // fill page width
                        table.Columns[3].SetWidth(width3, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustFirstColumn);

                        table.Columns[4].AutoFit();
                        Single width4 = table.Columns[4].Width;
                        table.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitContent); // fill page width
                        table.Columns[4].SetWidth(width4, Microsoft.Office.Interop.Word.WdRulerStyle.wdAdjustFirstColumn);

                        table.Rows.Alignment = Microsoft.Office.Interop.Word.WdRowAlignment.wdAlignRowCenter;

                        document.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;

                        Object fileName = Directory.GetCurrentDirectory() + "\\" + "Report_" + ToStringDate(startDate) + "_" + ToStringDate(endDate) + ".doc";

                        try
                        {
                            document.SaveAs(ref fileName);

                            System.Diagnostics.Process.Start(fileName.ToString());

                            if (word.ActivePrinter != null || word.ActivePrinter != "")
                            {
                                document.PrintOut(ref oFalse, ref oMissing, ref oMissing, ref oMissing,
                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oFalse,
                                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                            }

                            document.Close();
                            word.Quit();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("오류가 발생했습니다. 다시 시도 해 주세요.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            };
            calendar.ViewModel.Closing += (s, e) =>
            {
                calendar.Close();
            };
            calendar.ViewModel.SelectedStartDate = GetMondayOfWeek(_selectedDate);
            calendar.ViewModel.SelectedEndDate = GetSundayOfWeek(_selectedDate);
            calendar.ShowDialog();
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
                SelectedType = _selectedItem.Type;
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
