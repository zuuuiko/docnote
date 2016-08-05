using docnote.Model;
using docnote.Model.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
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
            Init(patientVM, doc);
            
        }

        private void Init(PatientWindowVM patientVM, Document doc)
        {
            _patientVM = patientVM;
            CreateAndSaveWordClickCommand = new RelayCommand(CreateAndSaveWord);
            if (doc.Id != 0)
            {
                _doc = doc;
                return;
            }
            _doc = new Form_063_o
            {
                LastName = patientVM.Patient.LastName
            };
            
        }

        private void CreateAndSaveWord()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents| *.doc;*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = System.IO.Directory.GetCurrentDirectory() + @"\Form_063_o.docx";
                Resources.WordManager.CreateWordDocument(fileName, saveFileDialog.FileName, _doc);
            }
        }
    }
}
