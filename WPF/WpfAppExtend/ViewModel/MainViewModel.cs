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
        #region ��֤��
        //����һ���ǹ��е�
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
                    MessageBox.Show("�������");
                    return;
                }
                else
                {
                    //ִ�к����߼�
                }
            });

            //��������
            modules = new List<Module>();
            modules.Add(new Module() { ItemName = "ģ��һ", ItemColor = "#FF767676" });
            modules.Add(new Module() { ItemName = "ģ���", ItemColor = "#FF222676" });
            modules.Add(new Module() { ItemName = "ģ����", ItemColor = "#FF767111" });

            OpenCommand = new RelayCommand<string>(t => OpenPage(t));
            //tempdataobject
            Nesting = new Module() { ItemColor = "#FF767111" };
        }
        //��ߵ�����ť�߼�
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
                case "ģ��һ":
                    Page = null;
                    MessageBox.Show("XXX");
                    break;
                case "ģ���":
                    Page = new NavigationPages.Page1();
                    break;                
                case "ģ����":
                    //����ֵ����tokenΪGetData��Messenger
                    Messenger.Default.Send(t,"GetData");
                    break;

                default:
                    break;
            }
        }
        public RelayCommand<string> OpenCommand { get; set; }

        public List<Module> modules { get; set; }
        //��һ�������ݰ󶨣��������û��ؼ�����ֵ��Page���Ӷ�Ƕ�׵���������ʾ
        private object page;
        public object Page
        {
            get { return page; }
            set { page = value; RaisePropertyChanged(); }
        }
        //��һֵ���ݰ�
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }
        //Ƕ�����ݰ�
        private Module nesting;

        public Module Nesting
        {
            get { return nesting; }
            set { nesting = value;/* RaisePropertyChanged();*/ }
        }


    }
}