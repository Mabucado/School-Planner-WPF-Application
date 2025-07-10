using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_Class_Library
{
   public class Module : IEnumerable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int NoCredits { get; set; }
        public int Hours { get; set; }
        public Module(string code, string name, int noCredits, int hours)
        {
            Code = code;
            Name = name;
            NoCredits = noCredits;
            Hours = hours;
        }

        public Module()
        {
        }

        public override string ToString()
        {
            return $"Code: {Code},\n Name: {Name},\n NoCredits: {NoCredits},\n Hours: {Hours}";
        }

        public Dictionary<string, Module> listModule = new Dictionary<string, Module>();
        public Module this[string name]
        {
            get => (Module)listModule[name];
            set => listModule[name] = value;

        }
        public void ClearPeople()
        {
            listModule.Clear();
        }
        public int Count => listModule.Count;
        IEnumerator IEnumerable.GetEnumerator() => listModule.GetEnumerator();

    }
}
