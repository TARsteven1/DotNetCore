using GalaSoft.MvvmLight;
using WpfAppExtend.Class.ValidationRuleClass;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Collections.Generic;
using System;
using GalaSoft.MvvmLight.Messaging;

namespace WpfAppExtend.ViewModel
{
    public class MainViewModel : ViewModelBase, IValidationExceptionHandler
    {
        #region 验证器
        //命令一定是共有的
        public RelayCommand SaveCommond { set; get; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        private bool isValid;

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; RaisePropertyChanged(); }
        }
        #endregion
        public MainViewModel()
        {
            SaveCommond = new RelayCommand(() =>
            {
                if (!IsValid)
                {
                    MessageBox.Show("输入错误！");
                    return;
                }
                else
                {
                    //执行后续逻辑
                }
            });

            //导航功能
            modules = new List<Module>();
            modules.Add(new Module() { ItemName = "模块一", ItemColor = "#FF767676" });
            modules.Add(new Module() { ItemName = "模块二", ItemColor = "#FF222676" });
            modules.Add(new Module() { ItemName = "模块三", ItemColor = "#FF767111" });

            OpenCommand = new RelayCommand<string>(t => OpenPage(t));

            Nesting = new Module() { ItemColor = "#FF767111" };
        }

        public void OpenPage(string t)
        {
            Title = t;
            foreach (Module item in modules)
            {
                if (t == item.ItemName)
                {
                    Nesting.ItemColor  = item.ItemColor ;
                }
            }
            //Nesting.ItemColor = "#FF222676";
            switch (t)
            {
                case "模块一":
                    Page = null;
                    MessageBox.Show("XXX");
                    break;
                case "模块二":
                    Page = new NavigationPages.Page1();
                    break;                
                case "模块三":
                    //将数值传给token为GetData的Messenger
                    Messenger.Default.Send(t,"GetData");
                    break;

                default:
                    break;
            }
        }

        public List<Module> modules { get; set; }
        private object page;
        public object Page
        {
            get { return page; }
            set { page = value; RaisePropertyChanged(); }
        }
        public RelayCommand<string> OpenCommand { get; set; }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }
        private Module nesting;

        public Module Nesting
        {
            get { return nesting; }
            set { nesting = value;/* RaisePropertyChanged();*/ }
        }


    }
}