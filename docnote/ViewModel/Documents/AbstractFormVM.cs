using docnote.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace docnote.ViewModel.Documents
{
    abstract class AbstractFormVM : ViewModelBase
    {
        protected readonly IDataService _dataService;
        protected readonly string _path;
        protected Document _doc;
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

        protected Hospital Hospital { get; private set; }
        Action _updateDocsDataGrid;

        public ICommand CreateAndSaveWordClickCommand { get; protected set; }
        public ICommand SaveToDBCommand { get; protected set; }

        public AbstractFormVM(Document doc, IDataService dataService, Action reloadDocs, string path)
        {
            _dataService = dataService;
            _updateDocsDataGrid = reloadDocs;
            _path = path;
            CreateAndSaveWordClickCommand = new RelayCommand(CreateAndSaveWord);
            SaveToDBCommand = new RelayCommand(SaveDocumentToDB);
            if (doc.Id != 0)
            {
                _doc = doc;
            }
            else
            {
                LoadHospital();
                Init(doc);
            }
        }

        private void SaveDocumentToDB()
        {
            _dataService.AddUpdateDocument(
                async (isSaved, error) =>
                {
                    MetroWindow window = GetCurrentWindow();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isSaved ? "збережено" : error?.Message);
                        if (result == MessageDialogResult.Affirmative)
                        {
                            _updateDocsDataGrid();
                            window.Close();
                        }
                    }
                }, Document);
        }

        protected abstract MetroWindow GetCurrentWindow();

        private void LoadHospital()
        {
            _dataService.GetHospital(
                (hospital, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Hospital = hospital;
                });
        }

        protected abstract void Init(Document doc);

        private void CreateAndSaveWord()
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Word Documents| *.doc;*.docx";
            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    string fileName = System.IO.Directory.GetCurrentDirectory() + _path;
            //    Resources.WordManager.CreateWordDocument(fileName, saveFileDialog.FileName, _doc);
            //}
        }


    }
}
