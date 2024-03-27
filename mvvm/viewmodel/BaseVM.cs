using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfMvvm2703_1125.mvvm.viewmodel
{
    public class BaseVM : INotifyPropertyChanged
    {
        protected void Signal([CallerMemberName]string prop = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));  
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}