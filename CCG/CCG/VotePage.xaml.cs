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

    private void EntryCompleted(object sender, EventArgs e)
    {
      CCGWrapper wrapper = new CCGWrapper();
      wrapper.SubmitSuggestion(0, GameName.Text, Description.Text);
      Label.Text = "Submitted";
    }
  }
}