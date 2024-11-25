using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SmtMAUI
{
    class ObservableObject : INotifyPropertyChanged{

         public event PropertyChangedEventHandler PropertyChanged;

          protected void onPropertyChanged([CallerMemberName] string name = null)
        {
            onPropertyChanged();
        }
    }
   


    

}