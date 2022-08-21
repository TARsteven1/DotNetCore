using GalaSoft.MvvmLight;
using WpfApp1.DB;
using WpfApp1.Models;
using WpfApp1.View;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Windows;

namespace WpfApp1.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            localDb = new LocalDB();
            //Query();
            QueryCommand = new RelayCommand(Query);
            ResetCommand = new RelayCommand(() => { Search = string.Empty; this.Query(); });
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand<int>(t => Edit(t));
            DelCommand = new RelayCommand<int>(t => Del(t));
        }



        LocalDB localDb;
        private string search = string.Empty;

        public string Search
        {
            get { return search /*= string.Empty*/; }
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
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand<int> EditCommand { get; set; }
        public RelayCommand<int> DelCommand { get; set; }
        #endregion
        public void Query()
        {
            //var models = localDb.GetData();
            var models = localDb.FindDataByName(Search);
            GirdModelList = new ObservableCollection<Student>();
            if (models != null)
            {
                models.ForEach(arg => { GirdModelList.Add(arg); });

            }
        }
        public void Add()
        {
            Student student = new Student();
            AddUser addUser = new AddUser(student);
            var r = addUser.ShowDialog();
            if (r.Value)
            {
                student.ID = GirdModelList.Max(t => t.ID) + 1;
                localDb.AddData(student);
                this.Query();
            }
            }
        public void Edit(int id)
        {
           // string model = localDb.FindDataByID(id).Name;
            var model = localDb.FindDataByID(id);
            if (model != null)
            {
                AddUser addUser = new AddUser(model);
                var r = addUser.ShowDialog();
                    //var tempmodel = GirdModelList.FirstOrDefault(t => t.ID == id);
                if (r.Value)
                {
                     var tempmodel = GirdModelList.FirstOrDefault(t => t.ID == id );
                    if (tempmodel != null)
                    {
                            tempmodel.Name = model.Name;
                           // tempmodel.Name = model;
                    }
                }
                //实例化窗口  打开窗口
                //AddUser addUser = new AddUser(model);
                //var r = addUser.ShowDialog();
                //if (r.Value)
                //{
                //    var tempmodel = GirdModelList.FirstOrDefault(t => t.ID == id);
                //  //  var tempmodel = GirdModelList.FirstOrDefault(t => t.ID == model.ID );
                //    if (tempmodel != null)
                //    {
                //        tempmodel.Name = student.Name;
                //       // tempmodel.Name = model;
                //    }
                //}
                //else
                //{
                //    var tempmodel = GirdModelList.FirstOrDefault(t => t.ID == id);
                //    tempmodel.Name = model.Name;
                //}
                this.Query();
            }
        }
        public void Del(int id)
        {
            var model = localDb.FindDataByID(id);
            if (model != null)
            {
                var r = MessageBox.Show($"确认删除当前用户数据:{model.Name}？", "操作提示", MessageBoxButton.OK, MessageBoxImage.Question);
                if (r == MessageBoxResult.OK)
                {
                    localDb.DelData(model.ID);
                }
                this.Query();
            }
        }
    }
}