using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using WpfPrism.ClassFiles;
using ModuleA.ClassFiles;
using ModuleA.UserControls;
using WpfPrism.UserControls;
using WpfPrism.Views;
using WpfPrism.ViewModels;

namespace WpfPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            //使用官方提供的容器拿到主窗口并返回
            return Container.Resolve<MainWindow>();
        }
        //使用容器的类型注册器来注册我们要使用的页面，依赖或者服务
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //只有注册的view才能在导航中使用
            containerRegistry.RegisterForNavigation<ModuleViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            //手动绑定上下文,少用xaml的自动查找上下文设置(prism:ViewModelLocator.AutoWireViewModel="true")
            //containerRegistry.RegisterForNavigation<ModuleViewA, ModuleAProfile>();
            //也可以重命名view为ViewC
            containerRegistry.RegisterForNavigation<RegionControl>("ViewC");
            //注册弹窗并绑定上下文
            containerRegistry.RegisterDialog<ViewDialog, DialogViewModel>();

        }

        //注册新的自定义区域适配器
        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);

            regionAdapterMappings.RegisterMapping(typeof(StackPanel),Container.Resolve<StackPanelRegionAdapter>());
        }

        #region 模块加载
        /// <summary>
        /// 加载模块的方式有很多种：AppConfig配置，代码加载，文件路径（Directory），LoadManual，Xaml等
        /// </summary>
        /// <param name="moduleCatalog"></param>
        //代码的方式引用模块
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleAProfile>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
        //配置文件路径 的方式引用模块,生成后的文件目录下的Modules文件夹下放入模块的dll即可
        //protected override IModuleCatalog CreateModuleCatalog()
        //{
        //    //return base.CreateModuleCatalog();
        //    return new DirectoryModuleCatalog() { ModulePath=@".\Modules"} ;
        //}
        #endregion
    }
}
