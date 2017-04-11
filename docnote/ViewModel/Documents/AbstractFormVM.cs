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
using System.Windows;
using System.Windows.Input;

namespace docnote.ViewModel.Documents
{
    abstract class AbstractFormVM : ViewModelBase
    {
        protected readonly IDataService _dataService;
        protected readonly string _path;
        protected Document _document;
        public Document Document
        {
            get
            {
                return _document;
            }
            set
            {
                Set(ref _document, value);
            }
        }

        protected Hospital Hospital { get; private set; }
        Action _updateDocsDataGrid;

        public ICommand CreateAndSaveWordClickCommand { get; protected set; }
        public ICommand SaveToDBCommand { get; protected set; }
        public ICommand CloseDocumentClickCommand { get; private set; }

        public AbstractFormVM(Document document, ICollection<Patient> patients, ICollection<CardEntry> cardEntries, IDataService dataService, Action reloadDocs, string path)
        {
            _dataService = dataService;
            _updateDocsDataGrid = reloadDocs;
            _path = path;
            CreateAndSaveWordClickCommand = new RelayCommand(CreateAndSaveWord);
            SaveToDBCommand = new RelayCommand(SaveDocumentToDB);
            CloseDocumentClickCommand = new RelayCommand(CloseDocumentWindow);

            // Open exist document
            if (document.Id != 0)
            {
                _document = document;
                //TODO: kostil
                if (document is Form_Prikr)
                {
                    Init(_document, null, null);
                }
            }
            //Create new document
            else
            {
                LoadHospital();
                Init(document, patients, cardEntries);
            }
        }

        private void CloseDocumentWindow()
        {
            GetCurrentWindow().Close();
        }

        private void SaveDocumentToDB()
        {
            //if(Document is Form_Prikr)
            //{
            //    StringBuilder patientsDatas = new StringBuilder();
            //    (this as Form_Prikr_VM).PatientItems.OrderBy(d => d.RowId).ToList().ForEach(pPData => patientsDatas.AppendLine(pPData.ToString()));

            //    (Document as Form_Prikr).PatientsDatas = patientsDatas.ToString();
            //}
            _dataService.AddUpdateDocument(
                async (isSaved, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    MetroWindow window = GetCurrentWindow();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isSaved ? "збережено" : error?.Message);//TODO: No
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
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Hospital = hospital;
                });
        }

        protected abstract void Init(Document document, ICollection<Patient> patients, ICollection<CardEntry> cardEntries);

        private async void CreateAndSaveWord()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word Documents| *.doc;*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = System.IO.Directory.GetCurrentDirectory() + _path;
                try
                {
                    Resources.WordManager.CreateWordDocument(fileName, saveFileDialog.FileName, _document);
                    MetroWindow window = GetCurrentWindow();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, $"Файл: ${saveFileDialog.FileName} збережено.");//TODO: No
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }


    }
}
