using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StreamerTool
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    #region Ctor
    public MainWindow()
    {
      m_suppressEvents = true;
      InitializeComponent();

      m_streams = ViewModel.Instance.LoadStreams();
      LoadUpcomingStreams(m_streams);
      m_suppressEvents = false;
    }
    #endregion

    #region Private Members
    private bool m_suppressEvents;

    /// <summary>
    /// Stream state captured at start of edit in order to restore if changes
    /// are thrown out.
    /// </summary>
    private StreamInfo m_restoreStream;
    private List<StreamInfo> m_streams;
    #endregion

    #region UI Events
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrEmpty(AddGameTextBox.Text))
      {
        if(!PlannedGamesListBox.Items.Contains(AddGameTextBox.Text))
        {
          PlannedGamesListBox.Items.Add(AddGameTextBox.Text);
        }
        AddGameTextBox.Text = string.Empty;
      }
    }

    private void RemoveButton_Click(object sender, RoutedEventArgs e)
    {
      PlannedGamesListBox.Items.Remove(PlannedGamesListBox.SelectedItem);
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
      PlannedGamesListBox.Items.Clear();
    }

    private void StreamTitleTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (!m_suppressEvents)
      {
        StreamInfo selectedStream = UpcomingStreamsListBox.SelectedItem as StreamInfo;
        selectedStream.StreamName = StreamTitleTextBox.Text;
        UpcomingStreamsListBox.Items.Refresh();
      }
    }

    private void UpcomingStreamsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (e.AddedItems.Count > 0)
      {
        StreamInfo stream = e.AddedItems[0] as StreamInfo;
        m_restoreStream = new StreamInfo(stream);

        LoadStreamIntoUI(stream);
      }
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
      StreamInfo stream;
      if(UpcomingStreamsListBox.SelectedIndex == -1)
      {
        //This is a new stream
        stream = GetNewStreamFromUI();
      }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      //Throw out current edits by restoring the current restore stream
      if(m_restoreStream != null)
      {
        int index = UpcomingStreamsListBox.SelectedIndex;
        StreamInfo selectedStream = 
          UpcomingStreamsListBox.SelectedItem as StreamInfo;
        m_streams.Insert(index, m_restoreStream);
        m_streams.Remove(selectedStream);
        UpcomingStreamsListBox.SelectedItem = m_restoreStream;
        UpcomingStreamsListBox.Items.Refresh();
        m_restoreStream = null;
      }
    }
    #endregion

    private void LoadUpcomingStreams(List<StreamInfo> streams)
    {
      UpcomingStreamsListBox.ItemsSource = streams;
    }

    private void LoadStreamIntoUI(StreamInfo stream)
    {
      if (stream != null)
      {
        m_suppressEvents = true;
        //Check to see if current stream settings have been committed to the 
        //database - confirm with user to discard if not or continue editing the
        //currently-selected stream

        if (stream.StreamName != null)
        {
          StreamTitleTextBox.Text = stream.StreamName;
        }

        if (stream.StreamDescription != null)
        {
          StreamDescriptionRichTextBox.Document.Blocks.Clear();
          StreamDescriptionRichTextBox.Document.Blocks.Add
            (new Paragraph(new Run(stream.StreamDescription)));
        }

        if (stream.StartDateTime != null)
        {
          StartDateAndTimeDateTimePicker.Value = stream.StartDateTime;
        }

        if (stream.Recurrence != -1)
        {
          RecurrenceCheckBox.IsChecked = true;
          RecurrenceTimeIntegerUpDown.Value = stream.Recurrence;
          if(stream.RecurrenceTimeType != ReccurenceTimeTypes.None)
          {
            foreach(ComboBoxItem item in RecurrenceTimeTypeComboBox.Items)
            {
              string test = stream.RecurrenceTimeType.ToString();
              if (item.Tag.ToString() == stream.RecurrenceTimeType.ToString())
              {
                RecurrenceTimeTypeComboBox.SelectedItem = item;
                break;
              }
            }
          }
        }

        if (stream.PlannedGames != null)
        {
          PlannedGamesListBox.Items.Add(stream.PlannedGames);
        }

        m_suppressEvents = false;
      }
    }

    private StreamInfo GetNewStreamFromUI()
    {
      StreamInfo stream = new StreamInfo();
      stream.StreamName = StreamTitleTextBox.Text;
      stream.StreamDescription = new TextRange
        (StreamDescriptionRichTextBox.Document.ContentStart, 
        StreamDescriptionRichTextBox.Document.ContentEnd).Text;
      stream.PlannedGames = GetPlannedGamesList(); 
    
      return stream;
    }

    private List<string> GetPlannedGamesList()
    {
      List<string> retVal = new List<string>();
      foreach(string item in PlannedGamesListBox.Items)
      {
        retVal.Add(item);
      }

      return retVal;
    }

    private void SetControlsEnabled(bool enable)
    {

    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {

    }

    
  }
}
