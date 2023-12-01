using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MB2_Map
{
    public class TownList
    {
        public class Town
        {
            private readonly TownList _mainClass;
            public string Name { get; set; }
            public PointF Location { get; set; }
            public bool MasterListTown { get; set; }
            public float CurrentDistance => _mainClass.GetTownsDistance(this, _mainClass._referTo.SelectedItem as Town);

            public Town(TownList mainClass, string name, PointF location)
            {
                _mainClass = mainClass;
                Name = name;
                Location = location;
            }

            public Town(TownList mainClass, string name, float locationX, float locationY)
            {
                _mainClass = mainClass;
                Name = name;
                Location = new PointF(locationX, locationY);
            }

            public override string ToString()
            {
                // Refer to if listBox1 has something selected
                // If it does return just the town name
                // If not return the town name with the distance to what is selected in listBox1
                return _mainClass._referTo.SelectedItem == null
                    ? Name
                    : $"{Name} - {_mainClass.GetTownsDistance(this, _mainClass._referTo.SelectedItem as Town)} - {Math.Ceiling(this.CurrentDistance / decimal.ToSingle(_mainClass._numericupdown.Value)).ToString(CultureInfo.CurrentCulture)} Seconds?";
            }
            // Always return the town name
            public string ToString(bool getName)
            {
                return getName ? Name : ToString();
            }
        }

        //private readonly Dictionary<string, Town> _townDictionary = new();
        private readonly Dictionary<(string, string), float> _trueDistance = new();
        private readonly ListBox _referTo;
        private readonly NumericUpDown _numericupdown;

        private readonly Func<Town, Town, float> _distForm = (town1, town2) =>
            (float) Math.Sqrt(Math.Pow(town2.Location.X - town1.Location.X, 2) +
                              Math.Pow(town2.Location.Y - town1.Location.Y, 2));

        public List<Town> TownsList { get; } = new();

        // ReSharper disable once IdentifierTypo
        public TownList(ListBox listBox, NumericUpDown numericupdown)
        {
            _numericupdown = numericupdown;
            _referTo = listBox;
        }

        public Town AddTown(string name, PointF loc, bool masterListTown = false)
        {
            // _trueDistance must contain the distance between a town's own town.
            // So if we can't find the town referring to itself, it doesn't exist in our list yet.
            var townTuple = (name, name);
            if (_trueDistance.ContainsKey(townTuple))
                return null;
            // Create a new town and give it our masterListTown
            var newTown = new Town(this, name, loc)
            {
                MasterListTown = masterListTown
            };
            TownsList.Add(newTown);
            // For each town, calculate the distance and add it to _trueDistance
            foreach (var town in TownsList)
            {
                string[] townArray = { name, town.ToString() };
                Array.Sort(townArray);
                townTuple = (townArray[0], townArray[1]);
                if (!_trueDistance.ContainsKey(townTuple))
                    _trueDistance.Add(townTuple, _distForm(town, newTown));
            }
            return newTown;
        }

        public Town AddTown(string name, float locationX, float locationY, bool masterListTown = false)
        {
            // _trueDistance must contain the distance between a town's own town.
            // So if we can't find the town referring to itself, it doesn't exist in our list yet.
            var townTuple = (name, name);
            if (_trueDistance.ContainsKey(townTuple))
                return null;
            var newTown = new Town(this, name, locationX, locationY)
            {
                MasterListTown = masterListTown
            };
            TownsList.Add(newTown);
            // For each town, calculate the distance and add it to _trueDistance
            foreach (var town in TownsList)
            {
                string[] townArray = {name, town.ToString()};
                Array.Sort(townArray);
                townTuple = (townArray[0], townArray[1]);
                if (!_trueDistance.ContainsKey(townTuple))
                    _trueDistance.Add(townTuple, _distForm(town, newTown));
            }
            return newTown;
        }
        /*
        public void AddTrueLocation(string town1, string town2, float distance)
        {
            string[] townArray = {town1, town2};
            Array.Sort(townArray);
            var townTuple = (townArray[0], townArray[1]);
            if (!_trueDistance.ContainsKey(townTuple))
                _trueDistance.Add((townArray[0], townArray[1]), distance);
        } */
        public void DeleteTown(string name)
        {
            Town townToRemove = null;
            // Find the town
            foreach (var town in TownsList)
            {
                if (town.ToString(true) == name)
                {
                    townToRemove = town;
                    break;
                }
            }
            // If we didn't find the town, leave the func.
            if (townToRemove == null)
                return;
            // Remove every distance referring to the town we removed
            foreach (var town in TownsList)
            {
                string[] townArray = { town.ToString(true), townToRemove.ToString(true) };
                Array.Sort(townArray);
                var townTuple = (townArray[0], townArray[1]);
                _trueDistance.Remove(townTuple);
            }
            _ = TownsList.Remove(townToRemove);
        }
        public void DeleteTown(Town townToRemove)
        {
            // We can't remove something that doesn't exist
            if (townToRemove == null)
                return;
            // Remove every distance referring to the town we removed
            foreach (var town in TownsList)
            {
                string[] townArray = { town.ToString(true), townToRemove.ToString(true) };
                Array.Sort(townArray);
                var townTuple = (townArray[0], townArray[1]);
                _trueDistance.Remove(townTuple);
            }
            _ = TownsList.Remove(townToRemove);
        }
        public void UpdateTown(Town townToUpdate)
        {
            // Update every distance referring to the town we updated
            // If the town doesn't exist we will add it
            foreach (var town in TownsList)
            {
                string[] townArray = { town.ToString(true), townToUpdate.ToString(true) };
                Array.Sort(townArray);
                var townTuple = (townArray[0], townArray[1]);
                if (!_trueDistance.ContainsKey(townTuple))
                    _trueDistance.Add(townTuple, _distForm(town, townToUpdate));
                else
                    _trueDistance[townTuple] = _distForm(town, townToUpdate);
            }
        }
        public void UpdateTown(string townNameToUpdate, float x, float y)
        {
            // Find the town
            Town townToUpdate = null;
            foreach (var town in TownsList)
            {
                if (town.ToString(true) == townNameToUpdate)
                {
                    townToUpdate = town;
                    break;
                }
            }
            // If the town doesn't exist we will add it
            if (townToUpdate == null)
            {
                townToUpdate = new Town(this, townNameToUpdate, new PointF(x, y));
                TownsList.Add(townToUpdate);
            } 
            else
            {
                townToUpdate.Location = new PointF(x, y);
            }
            // Update every distance referring to the town we updated
            foreach (var town in TownsList)
            {
                string[] townArray = { town.ToString(true), townToUpdate.ToString(true) };
                Array.Sort(townArray);
                var townTuple = (townArray[0], townArray[1]);
                if (!_trueDistance.ContainsKey(townTuple))
                    _trueDistance.Add(townTuple, _distForm(town, townToUpdate));
                else
                    _trueDistance[townTuple] = _distForm(town, townToUpdate);
            }
        }

        public float GetTownsDistance(Town town1, Town town2)
        {
            //Debugger.Break();
            //if (town1 == null || town2 == null)
            //    return 0.0f;
            string[] townArray = {town1.ToString(true), town2.ToString(true)};
            Array.Sort(townArray);
            return _trueDistance[(townArray[0], townArray[1])];
        }
    }
}