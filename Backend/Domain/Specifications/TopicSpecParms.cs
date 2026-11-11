using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public  class TopicSpecParms
    {

        private int MaxPageSize { get; } = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = Math.Min(value, MaxPageSize); }
        }


   
        public string Sort { get; set; } = string.Empty;

        private string _search = string.Empty;

        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }


    }
}
