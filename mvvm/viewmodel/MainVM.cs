using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfMvvm2703_1125.mvvm.view;

namespace WpfMvvm2703_1125.mvvm.viewmodel
{
    public class MainVM : BaseVM
    {
        private Page currentPage;

        public Page CurrentPage { 
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        public VmCommand Search { get; set; }

        public MainVM()
        {
            Search = new VmCommand(() => 
            {
                OpenSearch();
            });

            OpenSearch();
        }

        private void OpenSearch()
        {
            CurrentPage = new ListDrinks(this);
        }
    }
}
