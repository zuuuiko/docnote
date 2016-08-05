using docnote.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace docnote.ViewModel.Documents
{
    class Form_063_o_VM : ViewModelBase
    {
        private readonly IDataService _dataService;
        PatientWindowVM _patientVM;
        Document _doc;
        public Document Document
        {
            get
            {
                return _doc;
            }
            set
            {
                Set(ref _doc, value);
            }
        }

        public ICommand CreateAndSaveWordClickCommand { get; private set; }

        public Form_063_o_VM(PatientWindowVM patientVM, Document doc, IDataService dataService)
        {
            _dataService = dataService;
            _doc = doc;
            Init(patientVM);
        }

        private void Init(PatientWindowVM patientVM)
        {
            _patientVM = patientVM;
        }
    }
}
