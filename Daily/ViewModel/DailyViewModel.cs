﻿using Daily.Common;
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
        private DailyAccountDB _database;

        private ObservableCollection<DailyModel> _itemCollection;

        private ObservableCollection<GraphModel> _graphItemCollection;

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

        private int _totalOutgo;

        private string _totalOutgoToolTip;

        private int _totalAmount;

        private string _totalAmountToolTip;

        private int _periodTotalAmount;

        private string _periodTotalAmountToolTip;

        private int _monthTotalAmount;

        private string _monthTotalAmountToolTip;

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

        public ObservableCollection<GraphModel> GraphItemCollection
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

        public int TotalOutgo
        {
            get
            {
                return _totalOutgo;
            }
            set
            {
                _totalOutgo = value;
                OnPropertyChanged();
            }
        }

        public string TotalOutgoToolTip
        {
            get
            {
                return _totalOutgoToolTip;
            }
            set
            {
                _totalOutgoToolTip = value;
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

        public string TotalAmountToolTip
        {
            get
            {
                return _totalAmountToolTip;
            }
            set
            {
                _totalAmountToolTip = value;
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

        public string PeriodTotalAmountToolTip
        {
            get
            {
                return _periodTotalAmountToolTip;
            }
            set
            {
                _periodTotalAmountToolTip = value;
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

        public string MonthTotalAmountToolTip
        {
            get
            {
                return _monthTotalAmountToolTip;
            }
            set
            {
                _monthTotalAmountToolTip = value;
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

        private void Initialize()
        {
            _isAddMode = true;

            _database = new DailyAccountDB();

            TypeList = new List<ItemType>();
            TypeList.Add(ItemType.Kakao);
            TypeList.Add(ItemType.Samsung);
            TypeList.Add(ItemType.Hana);
            TypeList.Add(ItemType.Hyundai);
            TypeList.Add(ItemType.Cash);
            TypeList.Add(ItemType.Income);

            SelectedType = TypeList[0];

            ItemCollection = new ObservableCollection<DailyModel>();
            GraphItemCollection = new ObservableCollection<GraphModel>();
            GraphDataCollection = new SeriesCollection();
            
            TotalIncome = 0;
            TotalOutgo = 0;
            TotalAmount = 0;

            TotalOutgoToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}", 0, 0, 0, 0, 0);
            TotalAmountToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}\n\n수입: {5:n0}", 0, 0, 0, 0, 0, 0);

            _week = 0;
            _selectedDate = DateTime.Today.AddDays(_week * 7);

            IsShowGraph = false;

            MondayDate = DateHelper.ToStringDate(DateHelper.GetMondayOfWeek(_selectedDate));
            SundayDate = DateHelper.ToStringDate(DateHelper.GetSundayOfWeek(_selectedDate));

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

            List<GraphModel> tempCollection = new List<GraphModel>();
            GraphDataCollection.Clear();

            // Initialize collection
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Monday))));
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Tuesday))));
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Wednesday))));
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Thursday))));
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Friday))));
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Saturday))));
            tempCollection.Add(new GraphModel(DateHelper.ToStringDate(DateHelper.GetDayOfWeek(_selectedDate, DayOfWeek.Sunday))));

            for (int i = 0; i < tempCollection.Count; i++)
            {
                var itemList = ItemCollection.Where(x => x.Date.Equals(tempCollection[i].Date));
                if (itemList.Count() == 0)
                {
                    continue;
                }

                foreach (var model in itemList)
                {
                    if (model.Type == ItemType.Income)
                    {
                        continue;
                    }

                    tempCollection[i].AddItem(model.Type, model.Amount);
                }
            }

            // Save to GraphItemCollection
            GraphItemCollection.Clear();
            var sortedCollection = tempCollection.OrderBy(x => x.Date);
            for (int i = 0; i < sortedCollection.Count(); i++)
            {
                GraphItemCollection.Add(sortedCollection.ElementAt(i));
            }
        }

        private void InitializeList()
        {
            // Calculate weekly total account info
            string monday = DateHelper.ToStringDate(DateHelper.GetMondayOfWeek(_selectedDate));
            string sunday = DateHelper.ToStringDate(DateHelper.GetSundayOfWeek(_selectedDate));

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
            DateTime startDayofPeriod = DateHelper.GetStartDayofPeriod(_selectedDate);
            if (_lastSearchedStartDayofPeriod != startDayofPeriod)
            {
                DateTime endDayofPeriod = DateHelper.GetEndDayofPeriod(_selectedDate);
                result = _database.GetPeriodTotalAccount(DateHelper.ToStringDate(startDayofPeriod), DateHelper.ToStringDate(endDayofPeriod));
                try
                {
                    var model = JsonConvert.DeserializeObject<TotalModel>(result);
                    PeriodTotal = DateHelper.ToStringDateMMDD(startDayofPeriod) + " ~ " + DateHelper.ToStringDateMMDD(endDayofPeriod) + " 지출 총액";
                    PeriodTotalAmount = model.TotalOutgoAmount + model.TotalAmountSamsung + 
                        model.TotalAmountKakao + model.TotalAmountHana + model.TotalAmountCash;

                    PeriodTotalAmountToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}\n\n수입: {5:n0}",
                        model.TotalAmountSamsung + model.TotalOutgoAmount, model.TotalAmountKakao, 
                        model.TotalAmountHana, model.TotalAmountHyundai, model.TotalAmountCash, model.TotalIncomeAmount);

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
                    MonthTotalAmount = model.TotalOutgoAmount + model.TotalAmountSamsung +
                        model.TotalAmountKakao + model.TotalAmountHana + model.TotalAmountCash;

                    MonthTotalAmountToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}\n\n수입: {5:n0}",
                        model.TotalAmountSamsung + model.TotalOutgoAmount, model.TotalAmountKakao,
                        model.TotalAmountHana, model.TotalAmountHyundai, model.TotalAmountCash, model.TotalIncomeAmount);

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
            MondayDate = DateHelper.ToStringDate(DateHelper.GetMondayOfWeek(_selectedDate));
            SundayDate = DateHelper.ToStringDate(DateHelper.GetSundayOfWeek(_selectedDate));
            Refresh();
        }

        private void NextWeek()
        {
            _week++;
            _selectedDate = DateTime.Today.AddDays(_week * 7);
            MondayDate = DateHelper.ToStringDate(DateHelper.GetMondayOfWeek(_selectedDate));
            SundayDate = DateHelper.ToStringDate(DateHelper.GetSundayOfWeek(_selectedDate));
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
            TotalOutgo = 0;
            TotalIncome = 0;

            int kakao = 0;
            int samsung = 0;
            int hana = 0;
            int hyundai = 0;
            int cash = 0;

            for (int i = 0; i < temp.Count; i++)
            {
                ItemCollection.Add(temp[i]);
                if (temp[i].Type == ItemType.Outgo || temp[i].Type == ItemType.Kakao 
                    || temp[i].Type == ItemType.Samsung || temp[i].Type == ItemType.Hana
                    || temp[i].Type == ItemType.Hyundai || temp[i].Type == ItemType.Cash)
                {
                    TotalOutgo += temp[i].Amount;
                    TotalAmount -= temp[i].Amount;

                    if (temp[i].Type == ItemType.Outgo || temp[i].Type == ItemType.Samsung)
                    {
                        samsung += temp[i].Amount;
                    }
                    else if (temp[i].Type == ItemType.Kakao)
                    {
                        kakao += temp[i].Amount;
                    }
                    else if (temp[i].Type == ItemType.Hana)
                    {
                        hana += temp[i].Amount;
                    }
                    else if (temp[i].Type == ItemType.Hyundai)
                    {
                        hyundai += temp[i].Amount;
                    }
                    else if (temp[i].Type == ItemType.Cash)
                    {
                        cash += temp[i].Amount;
                    }
                }
                else
                {
                    TotalIncome += temp[i].Amount;
                    TotalAmount += temp[i].Amount;
                }
            }

            TotalOutgoToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}", samsung, kakao, hana, hyundai, cash);
            TotalAmountToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}\n\n수입: {5:n0}", samsung, kakao, hana, hyundai, cash, TotalIncome);
        }

        private void Print()
        {
            AnalyzeView analyzeView = new AnalyzeView();
            analyzeView.Show();

            return;
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

                var result = _database.GetPeriodListAccount(DateHelper.ToStringDate(startDate), DateHelper.ToStringDate(endDate));
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

                            if (model.Type == ItemType.Outgo || model.Type == ItemType.Kakao
                            || model.Type == ItemType.Samsung || model.Type == ItemType.Hana
                            || model.Type == ItemType.Hyundai || model.Type == ItemType.Cash)
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

                        Object fileName = Directory.GetCurrentDirectory() + "\\" + "Report_" + DateHelper.ToStringDate(startDate) + "_" + DateHelper.ToStringDate(endDate) + ".doc";

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
            calendar.ViewModel.SelectedStartDate = DateHelper.GetMondayOfWeek(_selectedDate);
            calendar.ViewModel.SelectedEndDate = DateHelper.GetSundayOfWeek(_selectedDate);
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

            SelectedType = TypeList[0];
            Date = DateHelper.ToStringDate(_selectedDate);
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
                Date = DateHelper.ToStringDate(_selectedDate);
            }
        }

        public DailyViewModel()
        {
            Initialize();
        }
    }
}
