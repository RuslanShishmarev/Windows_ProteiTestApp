using Prism.Commands;
using Prism.Mvvm;
using ProteiTestApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProteiTestApp.ViewModels
{
    public class StopwatchAppViewModel : BindableBase //, INotifyPropertyChanged
    {
        #region COMMANDS

        public DelegateCommand AddNewCounterCommand { get; private set; }
        public DelegateCommand RemoveCounterCommand { get; private set; }
        public DelegateCommand StartStopCounterCommand { get; private set; }
        public DelegateCommand ResetCounterCommand { get; private set; }
        public DelegateCommand CloseWindowCommand { get; private set; }

        #endregion

        public StopwatchAppViewModel()
        {
            AddNewCounterCommand = new DelegateCommand(AddNewCounter);
            RemoveCounterCommand = new DelegateCommand(RemoveSelectedCounter);
            StartStopCounterCommand = new DelegateCommand(StartStopCounterAsync);
            ResetCounterCommand = new DelegateCommand(ResetCounter);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
            //add first counter
            AddNewCounter();
        }
        #region PROPERTIES

        private string _defaultStopwatchName = "Timer";
        private string _titleName = "Ruslan Shishmarev"; 
        public string TitleName
        {
            get => _titleName;
        }

        private ObservableCollection<StopwatcherTab> _allCounterTabs = new ObservableCollection<StopwatcherTab>();

        public ObservableCollection<StopwatcherTab> AllCounterTabs
        {
            get => _allCounterTabs;
            set
            {
                _allCounterTabs = value;
                RaisePropertyChanged(nameof(AllCounterTabs));
            }
        }

        private StopwatcherTab _selectedCounterTab;
        public StopwatcherTab SelectedCounterTab
        {
            get => _selectedCounterTab;
            set 
            {
                _selectedCounterTab = value;

                if(value != null) SelectedCounter = _selectedCounterTab.Counter;
                RaisePropertyChanged(nameof(SelectedCounterTab));
            }
        }

        private StopwatchCounter _selectedCounter;
        public StopwatchCounter SelectedCounter
        {
            get => _selectedCounter;
            set
            {
                _selectedCounter = value;
                RaisePropertyChanged(nameof(SelectedCounter));
            }
        }
        #endregion
        #region ALL METHODS
        private void AddNewCounter()
        {
            if(AllCounterTabs.Count < 10)
            {
                string newCounterName = String.Format("{0} {1}", _defaultStopwatchName, (AllCounterTabs.Count + 1).ToString());
                var counter = new StopwatchCounter(newCounterName);
                var tab = new StopwatcherTab(counter);
                AllCounterTabs.Add(tab);
            }
        }
        private void RemoveSelectedCounter()
        {
            if (AllCounterTabs.Count > 1 && SelectedCounterTab != null && 
                SelectedCounterTab.Counter.IsWork == false)
            {
                AllCounterTabs.Remove(SelectedCounterTab);
            }
            SelectedCounter = SelectedCounterTab.Counter;
        }
        private bool _isProgramWork = false;
        private async void StartStopCounterAsync()
        {
            if (SelectedCounterTab.Counter.IsWork)
            {
                SelectedCounterTab.Counter.Stop();
            }
            else
            {
                SelectedCounterTab.Counter.Start(2);
                _isProgramWork = true;
                while (_isProgramWork)
                {
                    SelectedCounter = SelectedCounterTab.Counter;
                    await Task.Delay(1);
                }
            }
        }
        private void ResetCounter()
        {
            SelectedCounterTab.Counter.Reset();
        }
        private void CloseWindow()
        {
            _isProgramWork = false;
            foreach (var tab in AllCounterTabs)
                tab.Counter.Stop();
        }
        #endregion
    }
}
