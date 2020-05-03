using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.ViewModels
{
    public class Interval
    {
        public double a { get; set; }
        public double b { get; set; }

        public string IntervalString
        {
            get { return a.ToString() + " : " + b.ToString(); }
            private set { }
        }

        public override bool Equals(object obj)
        {
            var o = obj as Interval;
            
            if(o is Interval)
            {
                return o.a == a && o.b == b;
            }

            return base.Equals(obj);
            
        }
    }

    class TrapezeViewModel: Screen
    {
        private BindableCollection<Interval> _intervalList;
        private Interval _selectedInterval;
        private string _intervalA;
        private string _intervalB;
        private bool _isUserInterval;


        public BindableCollection<Interval> IntervalList 
        { 
            get => _intervalList;
            set
            {
                _intervalList = value;
                NotifyOfPropertyChange(() => IntervalList);
            }
        }
        public Interval SelectedInterval 
        {
            get { return _selectedInterval; }
            set
            {
                _selectedInterval = value;
                NotifyOfPropertyChange(() => SelectedInterval);
            }
        }
        public string IntervalA { get => _intervalA; set { _intervalA = value; NotifyOfPropertyChange(() => IntervalA); } }
        public string IntervalB { get => _intervalB; set { _intervalB = value; NotifyOfPropertyChange(() => IntervalB); } }
        //public bool IsUserInterval { get => _isUserInterval; set { _isUserInterval = value; NotifyOfPropertyChange(() => IsUserInterval); } }

        public TrapezeViewModel()
        {
            IntervalList = new BindableCollection<Interval>();

            IntervalList.Add(new Interval { a = 0, b = 10 });
            IntervalList.Add(new Interval { a = 20, b = 30 });

        }

        public void AddInterval()
        {
            var interval = new Interval();

            double a, b;

            try
            {
                a = double.Parse(IntervalA);
                b = double.Parse(IntervalB);
            }
            catch
            {
                return;
            }

            interval.a = a;
            interval.b = b;

            if (IntervalList.Contains(interval))
            {
                return;
            }

            IntervalList.Add(interval);
        }

        public void RemoveInterval()
        {
            if(IntervalList.Contains(SelectedInterval))
            {
                IntervalList.Remove(SelectedInterval);
            }
        }
    }
}
