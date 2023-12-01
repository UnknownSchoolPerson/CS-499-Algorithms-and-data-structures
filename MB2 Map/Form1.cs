using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Diagnostics;

namespace MB2_Map
{
    public partial class Form1 : Form
    {
        private TownList _towns;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _towns = new TownList(listBox1, numericUpDown1);
            listBox1.Items.Clear();
            var loadFilesThread = new Thread(Load_Files_Into_ListBox);
            loadFilesThread.Start();
        }

        private void Load_Files_Into_ListBox()
        {
            // Open Towns folder
            var wd = @$"{Directory.GetCurrentDirectory()}\Towns";
            var di = new DirectoryInfo(wd);
            try
            {
                if (!di.Exists)
                {
                    // If the folder is missing.
                    MessageBox.Show("Towns folder missing!", @"An Error has occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.ExitThread();
                    Invoke(new MethodInvoker(Application.Exit));
                    Application.ExitThread();
                }
                try {
                    // Load this first before any other files to avoid double towns.
                    LoadMasterList($@"{wd}\MasterList.txt");
                }
                // Ignore if MasterList doesn't exist
                catch (Exception) { }
                // Only get the text files
                var files = di.GetFiles("*.txt");
                // Regex to only get numbers
                var rx = new Regex(@"\d+\.?\d+");
                // Look in every town file
                foreach (var file in files)
                {
                    if (file.DirectoryName == null)
                        continue;
                    if (file.Name == "MasterList.txt")
                    {
                        continue;
                    }
                    using var sr = new StreamReader(file.FullName);
                    var line = sr.ReadLine();
                    if (line == null)
                        continue;
                    // Match the regex
                    var splitLine = rx.Matches(line);
                    // Remove the extension
                    var extRemoved = file.Name.Remove(file.Name.Length - file.Extension.Length);
                    // If there isn't two numbers in the file, it must be a deleted masterlist town
                    if (splitLine.Count != 2)
                    {
                        _towns.DeleteTown(extRemoved);
                        continue;
                    }
                    // Add town on a new thread.
                    Invoke(new MethodInvoker(delegate
                    {
                        _towns.UpdateTown(extRemoved, float.Parse(splitLine[0].Value),
                            float.Parse(splitLine[1].Value));
                    }));
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"An Error has occurred!", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.ExitThread();
                Invoke(new MethodInvoker(Application.Exit));
                Application.ExitThread();
            }
            // Bind our townlist to the listbox in a new thread for sorting
            Invoke(new MethodInvoker(delegate
            {
                //listBox1.Sorted = true;
                //listBox2.Sorted = true;
                //Debugger.Break();
                listBox1.DataSource = new List<TownList.Town>(_towns.TownsList);
                listBox1.SelectedIndex = 0;
                //Debugger.Break();
                listBox2.DataSource = new List<TownList.Town>(_towns.TownsList.Where(town1 =>
                        town1.Name.ToLower().Contains(textBox2.Text.ToLower()))).
                    OrderBy(f => f.CurrentDistance).ToList();
                Enabled = true;
            }));
            //Debugger.Break();
        }

        private void LoadMasterList(string fullPath)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-7.0
            using var sr = new StreamReader(fullPath);
            string line;
            // Regex to get String, Float, Float
            var rx = new Regex(@"^""(.+)"",(\d+\.?\d+),(\d+\.?\d+)$");
            while ((line = sr.ReadLine()) != null)
            {
                var match = rx.Match(line);
                if (!match.Success || match.Groups.Count != 4)
                    continue;
                // Add town on new thread
                Invoke(new MethodInvoker(delegate
                {
                    // Don't care about return
                    _ = _towns.AddTown(match.Groups[1].Value, float.Parse(match.Groups[2].Value),
                        float.Parse(match.Groups[3].Value), true);
                }));
            }
        }

        /*
private void LoadTrueList()
{
   var wd = @$"{Directory.GetCurrentDirectory()}\TrueDistance.txt";
   var rx = new Regex(@"\s*(\w+ *\w+)\s*,\s*(\w+ ?\w+)\s*, (\d+\.?\d+)");
   using var sr2 = new StreamReader(wd);
   while (!sr2.EndOfStream)
   {
       var line = sr2.ReadLine();
       if (line == null)
           continue;
       var splitLine = rx.Matches(line);
       //var splitLine = line.Split(",");
       if (splitLine.Count != 1 && splitLine[0].Groups.Count != 4)
           continue;
       Invoke(new MethodInvoker(delegate
       {
           _towns.AddTrueLocation(splitLine[0].Groups[1].Value, splitLine[0].Groups[2].Value, float.Parse(splitLine[0].Groups[3].Value));
       }));
   }
}
*/

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Don't update listbox2 if nothing is selected,
            // this will cause a crash if the listbox2 is updated with nothing in listbox1.
            if (listBox1.SelectedIndex == -1)
                return;
            UpdateListBox2();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox1();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateListBox2();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateListBox2();
        }

        private void UpdateListBox1()
        {
            // Bind and sort our towns
            var currentItem = listBox1.SelectedItem;
            listBox1.DataSource =
                new List<TownList.Town>(_towns.TownsList.Where(town1 =>
                    town1.Name.ToLower().Contains(textBox1.Text.ToLower()))).
                    OrderBy(f => f.ToString()).ToList();
            // Something has to be selected in listbox1
            // If currentItem is null, than set selected item to 0 instead
            listBox1.SelectedItem = currentItem ?? 0;
        }

        private void UpdateListBox2()
        {
            // Bind and sort our towns
            var currentItem = listBox2.SelectedItem;
            listBox2.DataSource =
                new List<TownList.Town>(_towns.TownsList.Where(town1 =>
                        town1.Name.ToLower().Contains(textBox2.Text.ToLower()))).
                    OrderBy(f => f.CurrentDistance).ToList();
            // It doesn't matter if listBox2 has nothing selected
            listBox2.SelectedItem = currentItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Disable form1
            Enabled = false;
            listBox1.SelectedIndex = -1;
            // Create form2 and pass our towns object
            Form2 townEditor = new(this, _towns);
            townEditor.ShowDialog();
        }
        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form.formclosing?view=windowsdesktop-7.0#system-windows-forms-form-formclosing
        // Event for when Form2 closes
        public void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            UpdateListBox1();
            Enabled = true;
        }
    }
}