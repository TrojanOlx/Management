using GalaSoft.MvvmLight;
using Management.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Management.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            MenuModels = new ObservableCollection<MenuModel>();
            menuModels.Add(new MenuModel() { IconFont = "\xe635", Title = "我的一天", BackColor = "#218868", });
            menuModels.Add(new MenuModel() { IconFont = "\xe6b6", Title = "重要", BackColor = "#EE3B3B", });
            menuModels.Add(new MenuModel() { IconFont = "\xe6e1", Title = "已计划日程", BackColor = "#218868", });
            menuModels.Add(new MenuModel() { IconFont = "\xe614", Title = "已分配给我", BackColor = "#EE3B3B", });
            menuModels.Add(new MenuModel() { IconFont = "\xe755", Title = "任务", BackColor = "#218868", });
            menuModels.Add(new MenuModel() { IconFont = "\xe63b", Title = "购物清单", BackColor = "#009ACD", });
            menuModels.Add(new MenuModel() { IconFont = "\xe63b", Title = "杂货清单", BackColor = "#009ACD", });
            menuModels.Add(new MenuModel() { IconFont = "\xe63b", Title = "待办事项", BackColor = "#009ACD", });
        }

        private ObservableCollection<MenuModel> menuModels;

        public ObservableCollection<MenuModel> MenuModels
        {
            get => menuModels;
            set { menuModels = value; RaisePropertyChanged(); }
        }

    }
}
