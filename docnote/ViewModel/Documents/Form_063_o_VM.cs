using docnote.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls;

namespace docnote.ViewModel.Documents
{
    class Form_063_o_VM : AbstractFormVM
    {
        public Form_063_o_VM(Document doc, IDataService dataService, Action reloadDocs)
            :base(doc, null, null, dataService, reloadDocs, @"\Form_063_o.docx")
        {
        }

        protected override MetroWindow GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<View.Documents.Form_063_o_Window>().FirstOrDefault();
        }

        protected override void Init(Document doc, ICollection<Patient> patients, ICollection<CardEntry> cardEntries)
        {
            //_patientVM = patientVM;
            //if (doc.Id != 0)
            //{
            //    _doc = doc;
            //    return;
            //}
            var p = doc.Patient;
            _document = new Form_063_o
            {
                //LastName = patientVM.Patient.LastName
            };
        }
    }
}
