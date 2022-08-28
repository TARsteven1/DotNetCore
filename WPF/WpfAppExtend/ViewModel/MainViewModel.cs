using GalaSoft.MvvmLight;
using WpfAppExtend.Class.ValidationRuleClass;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace WpfAppExtend.ViewModel
{
    public class MainViewModel : ViewModelBase,IValidationExceptionHandler
    {
        #region ��֤��
        //����һ���ǹ��е�
        public RelayCommand SaveCommond { set; get; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;RaisePropertyChanged(); }
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
        }
    }
}