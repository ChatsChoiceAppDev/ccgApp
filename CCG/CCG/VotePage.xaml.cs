using CCG.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CCG
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class VotePage : ContentPage
  {
    public VotePage()
    {
      InitializeComponent();
    }

    private async void EntryCompleted(object sender, EventArgs e)
    {
      CCGWrapper wrapper = new CCGWrapper();
      string statusText = "Submitted!";
      try
      {
        await wrapper.SubmitSuggestion(0, GameName.Text, Description.Text);
      }
      catch (Exception ex)
      {
        //Server unreachable
        statusText = "Suggestion not submitted, we couldn't reach the CCG servers :(";
      }
      Label.Text = statusText;
    }
  }
}