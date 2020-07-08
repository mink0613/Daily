using System.Collections.Generic;

namespace Daily.Model
{
    public class AnalyzeCategoryModel
    {
        private string _category;

        private List<string> _items;

        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public List<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }
    }
}
