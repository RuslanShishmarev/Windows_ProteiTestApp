﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProteiTestApp.Models
{
    public class StopwatchCounter
    {
        private bool _isWork = false;
        public bool IsWork
        {
            get => _isWork;
            set => _isWork = value;
        }
        public bool IsInProcess
        {
            get
            {
                if (AllMilliseconds > 0) return true;
                return false;
            }
        }
        public bool CanBeReset
        {
            get
            {
                if (IsInProcess && IsWork == false) return true;
                return false;
            }
        }
        public DateTime CreationTime
        {
            get; private set;
        }
        private string _name;
        public string Name 
        {
            get => String.Format("{0} {1}", _name, CreationTime.ToString("T"));
        }
        public StopwatchCounter(string name)
        {
            CreationTime = DateTime.Now;
            _name = name;
        }
        public StopwatchCounter(StopwatchCounter stopwatch)
        {
            CreationTime = stopwatch.CreationTime;
            _name = stopwatch.Name;
            AllMilliseconds = stopwatch.AllMilliseconds;
            IsWork = stopwatch.IsWork;
        }

        private int _allMilliseconds = 0;
        public int AllMilliseconds
        {
            get => _allMilliseconds;
            set => _allMilliseconds = value;
        }
        public int Milliseconds
        {
            get => AllMilliseconds % 1000;
        }
        public int Seconds
        {
            get => AllMilliseconds / 1000;
        }

        public int Minutes
        {
            get => AllMilliseconds / 60000;
        }
        public override string ToString()
        {
            string forMill = (Milliseconds / 10).ToString();
            if (Milliseconds-100 < 0) forMill = "0" + forMill;

            string forSec = Seconds.ToString();
            if (Seconds-10 < 0) forSec = "0" + forSec;

            string forMin = Minutes.ToString();
            if (Minutes-10 < 0) forMin = "0" + forMin;

            return String.Format("{0}:{1},{2}", forMin, forSec, forMill);
        }
        public static StopwatchCounter operator ++(StopwatchCounter counter)
        {
            counter.AllMilliseconds++;
            return counter;
        }

        public async void StartOrStopWork()
        {
            IsWork = !IsWork;
            if (IsWork)
            {
                await Task.Run(() =>
                {
                    while (IsWork)
                    {
                        AllMilliseconds+=2;
                        Thread.Sleep(1);
                    }
                });
            }
        }
        public void Reset()
        {
            IsWork = false;
            AllMilliseconds = 0;
        }
    }
}