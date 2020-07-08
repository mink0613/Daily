using Daily.Common;
using Daily.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UILibrary.Base;

namespace Daily.ViewModel
{
    public class AnalyzeViewModel : BaseViewModel
    {
        private ObservableCollection<KeyValuePair<string, int>> _consumeList;

        private DailyAccountDB _database;

        private List<AnalyzeCategoryModel> _categoryList;

        public ObservableCollection<KeyValuePair<string, int>> ConsumeList
        {
            get
            {
                return _consumeList;
            }
            set
            {
                _consumeList = value;
                OnPropertyChanged();
            }
        }

        private void Initialize()
        {
            _database = new DailyAccountDB();
            _consumeList = new ObservableCollection<KeyValuePair<string, int>>();
            _categoryList = new List<AnalyzeCategoryModel>();
        }

        private void InitializeList()
        {
            DateTime today = DateTime.Today;

            DateTime startDayofPeriod = DateHelper.GetStartDayofPeriod(today);
            DateTime endDayofPeriod = DateHelper.GetEndDayofPeriod(today);

            var result = _database.GetPeriodListAccount(DateHelper.ToStringDate(startDayofPeriod), DateHelper.ToStringDate(endDayofPeriod));
            try
            {
                var modelList = JsonConvert.DeserializeObject<List<DailyModel>>(result);
                //modelList.Sort((DailyModel x, DailyModel y) => x.Date.CompareTo(y.Date));

                Dictionary<string, int> analyzedData = new Dictionary<string, int>();

                foreach (DailyModel model in modelList)
                {
                    bool isContain = false;
                    string name = "기타";
                    foreach (AnalyzeCategoryModel category in _categoryList)
                    {
                        foreach (string item in category.Items)
                        {
                            if (model.Name.Contains(item))
                            {
                                name = category.Category;
                                isContain = true;
                                break;
                            }
                        }
                        
                        if (isContain == true)
                        {
                            break;
                        }
                    }

                    if (analyzedData.ContainsKey(name) == false)
                    {
                        analyzedData.Add(name, 0);
                    }

                    analyzedData[name] += model.Amount;
                }

                ConsumeList.Clear();
                foreach (KeyValuePair<string, int> data in analyzedData)
                {
                    ConsumeList.Add(data);
                }

                /*PeriodTotal = DateHelper.ToStringDateMMDD(startDayofPeriod) + " ~ " + DateHelper.ToStringDateMMDD(endDayofPeriod) + " 지출 총액";
                PeriodTotalAmount = model.TotalOutgoAmount + model.TotalAmountSamsung +
                    model.TotalAmountKakao + model.TotalAmountHana + model.TotalAmountCash;

                PeriodTotalAmountToolTip = string.Format("삼성: {0:n0}\n카카오: {1:n0}\n하나: {2:n0}\n현대: {3:n0}\n현금: {4:n0}\n\n수입: {5:n0}",
                    model.TotalAmountSamsung + model.TotalOutgoAmount, model.TotalAmountKakao,
                    model.TotalAmountHana, model.TotalAmountHyundai, model.TotalAmountCash, model.TotalIncomeAmount);

                _lastSearchedStartDayofPeriod = startDayofPeriod;*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void RegisterCommands()
        {

        }

        public AnalyzeViewModel()
        {
            RegisterCommands();
            Initialize();
            InitializeList();
        }
    }
}
