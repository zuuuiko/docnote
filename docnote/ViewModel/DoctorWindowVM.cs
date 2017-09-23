using docnote.Model;
using docnote.View;
using docnote.View.Documents;
using docnote.ViewModel.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace docnote.ViewModel
{
    class DoctorWindowVM : ViewModelBase
    {
        private readonly IDataService _dataService;

        #region Doctor
        private Doctor _doctor;
        public Doctor Doctor
        {
            get
            {
                return _doctor;
            }
            set
            {
                Set(ref _doctor, value);
            }
        }
        public ICommand DoctorSaveAndCloseClickCommand { get; private set; }
        public ICommand DoctorCloseClickCommand { get; private set; }
        #endregion

        private ICollection<Patient> Patients { get; set; }

        #region Documents
        private Document _selectedDocument;
        private ObservableCollection<Document> _documents;
        public Document SelectedDocument
        {
            get { return _selectedDocument; }
            set { Set(ref _selectedDocument, value); }
        }
        public ObservableCollection<Document> Documents
        {
            get { return _documents; }
            set { Set(ref _documents, value); }
        }

        private Document _selectedDocumentForm;
        private ObservableCollection<Document> _documentFormList;
        public Document SelectedDocumentForm
        {
            get { return _selectedDocumentForm; }
            set { Set(ref _selectedDocumentForm, value); }
        }
        public ObservableCollection<Document> DocumentFormList
        {
            get { return _documentFormList; }
            set { Set(ref _documentFormList, value); }
        }

        public ICommand OpenDocumentsFlyoutCommand { get; private set; }
        public ICommand CreateOpenDocumentCommand { get; private set; }
        public ICommand DeleteDocumentClickCommand { get; private set; }
        #endregion

        public DoctorWindowVM(IDataService dataService, ICollection<Patient> patients)
        {
            _dataService = dataService;
            Patients = patients;
            DoctorSaveAndCloseClickCommand = new RelayCommand(SaveDoctor);
            DoctorCloseClickCommand = new RelayCommand(CloseDoctor);
            LoadDoctor();
            LoadDocuments();
            OpenDocumentsFlyoutCommand = new RelayCommand<Flyout>(OpenDocumentsFlyout);
            DocumentFormList = new ObservableCollection<Document> { //new Form_025_6_o(),
                                                                    //new Form_063_o(),
                                                                    new Form_Prikr() };
            CreateOpenDocumentCommand = new RelayCommand<Document>(CreateOpenDocument);
            DeleteDocumentClickCommand = new RelayCommand<Document>(DeleteDocument);
        }

        private void CloseDoctor()
        {
            Application.Current.Windows.OfType<DoctorWindow>().FirstOrDefault().Close();
        }

        private void SaveDoctor()
        {
            _dataService.UpdateDoctor(
                async (isUpdated, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    var window = Application.Current.Windows.OfType<DoctorWindow>().FirstOrDefault();
                    if (window != null)
                    {
                        var result = await window.ShowMessageAsync(null, isUpdated ? "збережено" : "");//TODO
                        if(result == MessageDialogResult.Affirmative) window.Close();
                    }
                }, Doctor);
        }

        private void LoadDoctor()
        {
            _dataService.GetDoctorAsync(
                (doctor, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Doctor = doctor;
                });
            //this.RaisePropertyChanged(() => this.Patients);
            //Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Employees Loaded."));
        }

        private async void DeleteDocument(Document d)
        {
            if (d == null) return;

            var window = Application.Current.Windows.OfType<DoctorWindow>().FirstOrDefault();
            if (window != null)
            {
                var result = await window.ShowMessageAsync("Видалити?", $"{d.DocumentName} {d.CreationDate}",
                                                                           MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Negative) return;
            }

            _dataService.DeleteDocument(
                async (isDeleted, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    if (window != null && isDeleted)
                        await window.ShowMessageAsync(null, $"{d.DocumentName} {d.CreationDate} видалений");
                }, d);
            LoadDocuments();
        }

        private void CreateOpenDocument(Document doc)
        {
            if (SelectedDocumentForm == null && doc.Id == 0) return;
            //if (Doctor.Id == 0)
            //{
            //    SavePatient();
            //    _updatePatientsDataGrid();
            //}
            doc.Doctor = Doctor;
            MetroWindow fw = null;
            switch (doc.DocumentName)
            {
                case @"Прикріплення":
                    fw = new Form_Prikr_Window();
                    fw.DataContext = new Form_Prikr_VM(doc, Patients.ToList(), null, _dataService, LoadDocuments);
                    break;
                //case @"Форма 063/о":
                //    fw = new Form_063_o_Window();
                //    fw.DataContext = new Form_063_o_VM(doc, _dataService, LoadDocuments);
                //    break;
                default:
                    break;
            }
            fw?.ShowDialog();
            SelectedDocumentForm = null;
            //System.Diagnostics.Debug.WriteLine(
            //    $"DocumentName - {doc.DocumentName}, Patient.LastName ");
            //cew.DataContext = new CardEntryWindowVM(this, Patient.Card, _dataService);
            //cew.ShowDialog();
        }

        private void OpenDocumentsFlyout(Flyout flyout)
        {
            if (flyout == null)
            {
                return;
            }

            flyout.IsOpen = !flyout.IsOpen;
        }

        private void LoadDocuments()
        {
            _dataService.GetDocumentsAsync(
                (documents, error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show(error.StackTrace);
                        return;
                    }
                    Documents = documents;
                }, null);
        }
    }
}
