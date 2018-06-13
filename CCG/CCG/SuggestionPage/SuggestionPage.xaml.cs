using CCG.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SuggestionPage : ContentPage
  {
    public SuggestionPage()
    {
      InitializeComponent();
      GameSearchBar.SearchCommand = ViewModel.GameSearchCommand;
    }

    //private async void EntryCompleted(object sender, EventArgs e)
    //{
    //  CCGWrapper wrapper = new CCGWrapper();
    //  string statusText = "Submitted!";
    //  try
    //  {
    //    await wrapper.SubmitSuggestion(0, GameName.Text, Description.Text);
    //  }
    //  catch (Exception ex)
    //  {
    //    //Server unreachable
    //    statusText = "Suggestion not submitted, we couldn't reach the CCG servers :(";
    //  }
    //  Label.Text = statusText;
    //}

    public void PopulateSearchList(ObservableCollection<Game> searchResults)
    {
      if(searchResults != null)
      {
        SearchResultsList.ItemsSource = searchResults;
      }
    }

    SuggestionPageViewModel _viewModel;
    public SuggestionPageViewModel ViewModel
    {
      get
      {
        return _viewModel ?? new SuggestionPageViewModel(this);
      }
    }

    private void SearchResultsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      Game selectedGame = e.SelectedItem as Game;

      
      if (selectedGame != null)
      {
        ShowGameDetails(selectedGame);
      }
    }

    private void ShowGameDetails(Game game)
    {
      GameDetails.IsVisible = true;
      GameName.Text = game.Name;
      GameBoxArt.Source = game.Box.Medium;
      ViewModel.SelectedGame = game;
    }
  }
}