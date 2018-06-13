using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CCG
{
  class GameSearchCommand : ICommand 
  {
    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      
    }

    public event EventHandler CanExecuteChanged;

    private async void RunGameSearch(string searchText)
    {
      TwitchWrapper twitchWrapper = new TwitchWrapper();
      var result = await twitchWrapper.SearchGames(searchText);

      //May need to check that the suggestion page is still active here... would a parameter work? 
    }
  }
}
