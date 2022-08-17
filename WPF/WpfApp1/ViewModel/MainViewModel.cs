using GalaSoft.MvvmLight;
using WpfApp1.DB;
using WpfApp1.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfApp1.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            localDb = new LocalDB();
            Query();
            QueryCommand = new RelayCommand(Query);
        }

        LocalDB localDb;
        private string search = string.Empty;

        public string Search
        {
            get { return search = string.Empty; }
            set { search = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Student> girdModelList;

        public ObservableCollection<Student> GirdModelList
        {
            get { return girdModelList; }
            set { girdModelList = value; RaisePropertyChanged(); }
        }
        #region Command
        public RelayCommand QueryCommand { get; set; }
        #endregion
        public void Query()
        {
            var models = localDb.FindDataByName(Search);
            GirdModelList = new ObservableCollection<Student>();
            if (models != null)
            {
                models.ForEach(arg => { GirdModelList.Add(arg); });

            }
        }
    }
}