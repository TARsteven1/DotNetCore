using System;
using System.Collections.Generic;
using System.Text;
using ModuleA.UserControls;
using Prism.Ioc;
using Prism.Modularity;

namespace ModuleA.ClassFiles
{
    //下载Prism.DryIoc包后， 模块要继承IModule
  public  class ModuleAProfile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ModuleViewA>();
        }
    }
}
