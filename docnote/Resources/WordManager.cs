using docnote.Model;
using docnote.View.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace docnote.Resources
{
    public static class WordManager
    {
        private static void FindAndReplace(Word.Application wordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace, ref matchKashida,
                        ref matchDiactitics, ref matchAlefHamza,
                        ref matchControl);
        }

        //Methode Create the document :
        public static void CreateWordDocument(object filename, object savaAs, Document doc)
        {
            List<int> processesbeforegen = getRunningProcesses();
            object missing = Missing.Value;

            Word.Application wordApp = new Word.Application();

            Word.Document aDoc = null;

            try
            {
                if (File.Exists((string)filename))
                {
                    object readOnly = false; //default
                    object isVisible = false;

                    wordApp.Visible = false;

                    aDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing, ref missing);

                    aDoc.Activate();
                    //Find and replace:
                    if (doc is Form_Prikr)
                    {
                        Form_Prikr document = doc as Form_Prikr;
                        object start = 0;
                        object end = 0;
                        Word.Range myRange = aDoc.Range(ref start, ref end);
                        Word.Table myTable = aDoc.Tables[1];
                        int rowCount = 3;

                        //add a row for each item in a collection.
                        foreach (var item in document.PatientsDatas)
                        {
                            myTable.Rows.Add(ref missing);

                            myTable.Rows[rowCount].Cells[1].Range.Text = item.RowId?.ToString();
                            myTable.Rows[rowCount].Cells[2].Range.Text = item.FLMName;
                            myTable.Rows[rowCount].Cells[3].Range.Text = item.BirthDate?.ToShortDateString();
                            myTable.Rows[rowCount].Cells[4].Range.Text = item.IdentificationDocumentDetails;
                            myTable.Rows[rowCount].Cells[5].Range.Text = item.City;
                            myTable.Rows[rowCount].Cells[6].Range.Text = item.Street;
                            myTable.Rows[rowCount].Cells[7].Range.Text = item.Building;
                            myTable.Rows[rowCount].Cells[8].Range.Text = item.Apartment;
                            myTable.Rows[rowCount].Cells[10].Range.Text = item.SignDate?.ToShortDateString();
                            myTable.Rows[rowCount].Cells[11].Range.Text = item.UnboundReasonCode?.ToString();
                            myTable.Rows[rowCount].Cells[12].Range.Text = item.UnboundDate?.ToShortDateString();

                            rowCount++;
                        }
                        myTable.Rows.Last.Delete();
                    }
                    else
                    {
                        Type type = doc.GetType();
                        PropertyInfo[] properties = type.GetProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            if (property.PropertyType.GUID == typeof(Patient).GUID) continue;

                            var value = property.GetValue(doc);

                            if (property.GetCustomAttributes<RomanAttribute>().Count() > 0)
                                value = ConvertByteToRome(value);

                            if (value is Boolean)
                                value = (bool)value ? 1 : 2;

                            FindAndReplace(wordApp, $"<<{property.Name}>>", value);
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Файл відсутній: {filename}");
                    return;
                }

                //Save as: filename
                aDoc.SaveAs(ref savaAs, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                List<int> processesaftergen = getRunningProcesses();
                killProcesses(processesbeforegen, processesaftergen);
            }
            //Close Document:
            //aDoc.Close(ref missing, ref missing, ref missing);

        }

        private static object ConvertByteToRome(object value)
        {
            switch (value?.ToString())
            {
                case "1": return "I";
                case "2": return "II";
                case "3": return "III";
                default:
                    return null;
            }
        }

        private static List<int> getRunningProcesses()
        {
            List<int> ProcessIDs = new List<int>();
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                    continue;
                if (clsProcess.ProcessName.Contains("WINWORD"))
                {
                    ProcessIDs.Add(clsProcess.Id);
                }
            }
            return ProcessIDs;
        }

        private static void killProcesses(List<int> processesbeforegen, List<int> processesaftergen)
        {
            foreach (int pidafter in processesaftergen)
            {
                bool processfound = false;
                foreach (int pidbefore in processesbeforegen)
                {
                    if (pidafter == pidbefore)
                    {
                        processfound = true;
                    }
                }

                if (processfound == false)
                {
                    Process clsProcess = Process.GetProcessById(pidafter);
                    clsProcess.Kill();
                }
            }
        }
    }
}
