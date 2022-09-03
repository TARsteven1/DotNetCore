using System;
using System.Collections.Generic;
using System.Text;
using Prism.Regions;
using Prism.Mvvm;
using System.Windows;

namespace WpfPrism.ViewModels
{
    //INavigationAware是Prism框架导航功能的基础接口实现传参离开事件等基础功能.
    //IConfirmNavigationRequest是其子接口
    public class ViewAViewModel : BindableBase, INavigationAware, IConfirmNavigationRequest
    {
        private string txt;

        public string Txt
        {
            get { return txt; }
            set { txt = value; RaisePropertyChanged(); }
        }
        //在导航发生切换前触发,多用于拦截请求
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;
            if (MessageBox.Show("确认导航?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                result = false;
            }
            continuationCallback(result);
        }
        /// <summary>
        /// 每次重新导航的时候,是否重用原来的实例
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 导航离开当前页时触发,用于拦截请求,很少用,多用ConfirmNavigationRequest方法
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            MessageBox.Show("Exit?", "提示", MessageBoxButton.OK);
        }
        /// <summary>
        /// 导航完成前接受用户传递的参数,以及是否允许导航等控制
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("value"))
            {
                Txt = navigationContext.Parameters.GetValue<string>("value");

            }
        }
    }
}
