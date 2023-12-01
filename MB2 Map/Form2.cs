using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static MB2_Map.TownList;

namespace MB2_Map
{
    public partial class Form2 : Form
    {
        TownList _towns;
        public Form2(Form1 creator, TownList towns)
        {
            InitializeComponent();
            // Save our towns list and sub to the form closing event
            _towns = towns;
            FormClosing += creator.Form2_FormClosing;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            updateListBox();
        }

        private void updateListBox()
        {
            var currentItem = listBox1.SelectedItem;
            // Bind and sort our towns list to listBox1
            listBox1.DataSource =
            new List<Town>(_towns.TownsList.Where(town1 =>
                    town1.Name.ToLower().Contains(textBox1.Text.ToLower()))).
                    OrderBy(f => f.ToString()).ToList();
            // Something should be selected for SelectedItem
            listBox1.SelectedItem = currentItem ?? 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateText();
        }

        private void UpdateText()
        {
            // Get our town data and bind them to stuff
            var town = (Town)listBox1.SelectedItem;
            textBox2.Text = town.ToString();
            var location = town.Location;
            numericUpDown1.Value = (decimal)location.X;
            numericUpDown2.Value = (decimal)location.Y;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateListBox();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            // Save our updated town to the list and save it to a file
            var town = (Town)listBox1.SelectedItem;
            PointF location = new((float)numericUpDown1.Value, (float)numericUpDown2.Value);
            town.Name = textBox2.Text;
            town.Location = location;
            _towns.UpdateTown(town);
            WriteToFile(town);
            updateListBox();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            // Delete the town and save that delete to a file
            var town = (Town)listBox1.SelectedItem;
            _towns.DeleteTown(town);
            WriteToFile(town);
            updateListBox();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // Adding a new town
            if (textBox3.Text.Length == 0)
                return;
            // Try to add the town
            // If it returns null, the name is taken
            var town = _towns.AddTown(textBox3.Text, new PointF((float)numericUpDown3.Value, (float)numericUpDown4.Value));
            if (town == null)
            {
                MessageBox.Show("Town is already added!");
                return;
            }
            textBox3.Text = "";
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            updateListBox();
            listBox1.SelectedItem = town;
            // Save our town to a file
            WriteToFile(town);
        }

        private static void WriteToFile(Town town)
        {
            // Get our towns folder
            var wd = @$"{Directory.GetCurrentDirectory()}\Towns";
            using StreamWriter outputFile = new(Path.Combine(wd, @$"{town.Name}.txt"), false);
            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#FFormatString
            // Save to file.
            outputFile.WriteLine(@$"{town.Location.X:F4},{town.Location.Y:F4}");
        }
    }
}
