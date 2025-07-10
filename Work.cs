using System;
using System.Collections;
using System.Collections.Generic;


namespace Custom_Class_Library
{
   public class  Work : IEnumerable
    {
        public string WorkCode { get; set; }

        public string WorkDate { get; set; }
        public int WorkHours { get; set; }
        public Work(string workCode, string workDate, int workHours)
        {
            WorkCode = workCode;
            WorkDate = workDate;
            WorkHours = workHours;
        }
        public Work()
        {
        }
        public override string ToString()
        {
            return $"Work Code: {WorkCode},\n WorkDate: {WorkDate},\n WorkHours: {WorkHours}";
        }
        public Dictionary<string, Work> listWork = new Dictionary<string, Work>();
        public Work this[string name]
        {
            get => (Work)listWork[name];
            set => listWork[name] = value;

        }
        public void ClearPeople()
        {
            listWork.Clear();
        }
        public int Count => listWork.Count;
        IEnumerator IEnumerable.GetEnumerator() => listWork.GetEnumerator();


    }
}
