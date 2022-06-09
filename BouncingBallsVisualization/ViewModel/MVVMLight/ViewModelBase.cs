
//____________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/TP
//____________________________________________________________________________

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BouncingBalls.ViewModel
{
    /// <summary>
    /// Model widoku.
    /// </summary>
  public class ViewModelBase : INotifyPropertyChanged
  {

    #region INotifyPropertyChanged
    /// <summary>
    /// Subskrypcje dla modelu widoku.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion

    #region API
    /// <summary>
    /// Raises the PropertyChanged event if needed.
    /// </summary>
    /// <param name="propertyName">(optional) The name of the property that changed.
    /// </param>
    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

  }
}
