﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Stats
{
    public class Stats
    {
        public Stats(Stat[] s)
        {
            stats = new Dictionary<string, Stat>();
            modifiers = new Dictionary<int, Modifier>();
            foreach(var stat in s)
                stats.Add(stat.Name, stat);
            this.Print();
        }

        public void AddModifier(int id, Modifier m)
        {
            modifiers.Add(id, m);
            Debug.WriteLine("Add modifier {0} {1} {2}", modifiers[id].stat, modifiers[id].type, modifiers[id].value);
            stats[m.stat].Apply(m);
            this.Print();
        }

        public void RemoveModifier(int id)
        {
            Debug.WriteLine("Remove modifier {0} {1} {2}", modifiers[id].stat, modifiers[id].type, modifiers[id].value);
            stats[modifiers[id].stat].Remove(modifiers[id]);
            modifiers.Remove(id);
            this.Print();
        }

        private void Add(Stat s)
        {
            stats.Add(s.Name, s);
        }

        public void Print()
        {
            var lines = stats.Select(kvp => kvp.Key + ":" + kvp.Value.Value.ToString());
            Debug.WriteLine(string.Join(Environment.NewLine, lines));
            Debug.WriteLine(Environment.NewLine);
        }

        public void ClearModifiers()
        {
            var keys = modifiers.Keys.ToArray();
            foreach(int key in keys)
                RemoveModifier(key);
            modifiers.Clear();
        }

        Dictionary<int, Modifier> modifiers;
        Dictionary<string, Stat> stats;

    }
}