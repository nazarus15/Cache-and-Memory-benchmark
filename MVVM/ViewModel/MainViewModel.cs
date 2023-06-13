using Cache_Memory_Benchmark.Core;
using Cache_Memory_Benchmark.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cache_Memory_Benchmark.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RellayComand HomeViewCommand { get; set; }

        public RellayComand BenchmarkViewCommand { get; set; }

        public RellayComand SettingsViewCommand { get; set; }

        public RellayComand UsageViewCommand { get; set; }

        public RellayComand HistoryViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }

        public BenchmarkViewModel BenchmarkVM { get; set; }

        public SettingsViewModel SettingsVM { get; set; }   

        public UsageViewModel UsageVM { get; set; }

        public HistoryViewModel1 HistoryVM { get; set; }

        private object _currenView;      

        public object CurrentView
        {
            get { return _currenView; }
            set 
            { 
                _currenView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel() 
        { 
            HomeVM = new HomeViewModel();
            BenchmarkVM = new BenchmarkViewModel();
            SettingsVM = new SettingsViewModel();   
            UsageVM = new UsageViewModel();
            HistoryVM = new HistoryViewModel1();
            CurrentView = HomeVM;

            HomeViewCommand = new RellayComand(o => 
            {
                CurrentView = HomeVM;
            });

            BenchmarkViewCommand = new RellayComand(o =>
            {
                CurrentView = BenchmarkVM;
            });

            SettingsViewCommand = new RellayComand(o =>
            {
                CurrentView = SettingsVM;
            });

            UsageViewCommand = new RellayComand(o =>
            {
                CurrentView = UsageVM;
            });

            HistoryViewCommand = new RellayComand(o =>
            {
                CurrentView = HistoryVM;
            });
        }
    }
}
