using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;
using Prism.Ioc;
using Prism.Events;
using MyToDo.Extensions;
using Prism.Mvvm;

namespace MyToDo.ViewModels
{
    public class NavigationViewModel :BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        public readonly IEventAggregator aggregator;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
            aggregator = containerProvider.Resolve<IEventAggregator>();
        }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        public void UpdateLoading( bool isopen)
        {
            aggregator.UpdateLoading(new Common.Event.UpdateModel() { IsOpen = isopen }) ;
        }
    }
}
