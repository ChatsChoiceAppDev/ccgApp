using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace CCG
{
  public class SuggestionPageViewModel
  {
    public SuggestionPageViewModel(SuggestionPage view)
    {
      m_view = view;
    }

    private SuggestionPage m_view;

    private async void RunGameSearch(string searchText)
    {
      TwitchWrapper twitchWrapper = new TwitchWrapper();
      var result = await twitchWrapper.SearchGames(searchText);

      //May need to check that this page is active
      ObservableCollection<Game> results = new ObservableCollection<Game>(result.Games);
      m_view.PopulateSearchList(results);
    }

    private ObservableCollection<Game> GameSearchResults
    {
      get; set;
    }

    private ICommand _gameSearchCommand;
    public ICommand GameSearchCommand
    {
      get
      {
        return _gameSearchCommand ?? (_gameSearchCommand = new Command<string>((text) =>
        {
          RunGameSearch(text);
        }));
      }
    }

    public Game SelectedGame
    {
      get; set;
    }
  }
}
