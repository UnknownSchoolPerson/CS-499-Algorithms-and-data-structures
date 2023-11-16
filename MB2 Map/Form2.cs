using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MB2_Map.TownList;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MB2_Map
{
    public partial class Form2 : Form
    {
        Form1 _creator;
        TownList _towns;
        public Form2(Form1 creator, TownList towns)
        {
            InitializeComponent();
            _towns = towns;
            _creator = creator;
            FormClosing += creator.Form2_FormClosing;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            updateListBox();
        }

        private void updateListBox()
        {
            var currentItem = listBox1.SelectedItem;
            listBox1.DataSource =
            new List<TownList.Town>(_towns.TownsList.Where(town1 =>
                    town1.Name.ToLower().Contains(textBox1.Text.ToLower()))).
                    OrderBy(f => f.ToString()).ToList();
            listBox1.SelectedItem = currentItem != null ? currentItem : 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateText();
        }

        private void updateText()
        {
            var town = (TownList.Town)listBox1.SelectedItem;
            textBox2.Text = town.ToString();
            var location = town.Location;
            numericUpDown1.Value = (decimal)location.X;
            numericUpDown2.Value = (decimal)location.Y;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            updateListBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var town = (TownList.Town)listBox1.SelectedItem;
            PointF location = new((float)numericUpDown1.Value, (float)numericUpDown2.Value);
            town.Name = textBox2.Text;
            town.Location = location;
            _towns.UpdateTown(town);
            writeToFile(town);
            updateListBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var town = (Town)listBox1.SelectedItem;
            _towns.DeleteTown(town);
            writeToFile(town);
            updateListBox();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
                return;
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
            writeToFile(town);
        }

        private void writeToFile(Town town)
        {
            var wd = @$"{Directory.GetCurrentDirectory()}\Towns";
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(wd, @$"{town.Name}.txt"), false))
            {
                // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#FFormatString
                outputFile.WriteLine(@$"{town.Location.X:F4},{town.Location.Y:F4}");
            }
        }
    }
}
