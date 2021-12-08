using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XamlBindingDemo
{
    public class Demo : BaseViewModel
    {
        public Command AddCommand { get; }
        private string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
        public User CurrentUser { get; set; }

        public ObservableCollection<User> AllUser { get; set; }
        public Demo()
        {
            Text = "绑定数据测试";
            AllUser = new ObservableCollection<User>();
            CurrentUser = new User();

            AddCommand = new Command(() =>
            {
                AllUser.Add(new User());
            });
        }
    }

    public class User : BaseViewModel
    {
        public User()
        {
            Age = DateTime.Now.Second;
            Name = $"有吗{DateTime.Now.Month}";
        }
        public string Name { get; set; }

        public int Age { get; set; }
    }

    public class BaseViewModel : INotifyPropertyChanged
    { 
        public Command ReturnCommand { get; }

       


        public BaseViewModel()
        {
            ReturnCommand = new Command(async _ =>
            {
                await Application.Current.MainPage.DisplayAlert("提示", "你点到了我", "确定");
                 
            });// { };
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
