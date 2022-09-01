using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfPrism;

namespace WpfPrism.ClassFiles
{/// <summary>
 /// 新建区域适配器,以便StackPanel能够显示Region(区域)
 /// </summary>
    public class StackPanelRegionAdapter : RegionAdapterBase<StackPanel>
    {
        public StackPanelRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            //检测区域视图的动态变化
            region.Views.CollectionChanged += (s, e) =>
            {
                //检测到视图内有添加控件的动作
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    //动态添加元素控件到目标区域
                    foreach (FrameworkElement item in e.NewItems)
                    {
                        regionTarget.Children.Add(item);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
            //return MainWindow.region;
        }
    }
}
