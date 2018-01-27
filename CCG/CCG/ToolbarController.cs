using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CCG
{
  public static class ToolbarController
  {
    private static List<ToolbarItem> m_toolbarCache = new List<ToolbarItem>();
    private static Page m_toolbarPage = null;
    public static Page ToolbarPage
    {
      set
      {
        m_toolbarPage = value;
        foreach(ToolbarItem item in m_toolbarCache)
        {
          AddToolbarItem(item);
        }
        m_toolbarCache.Clear();
      }
    }

    public static void AddToolbarItem(ToolbarItem item)
    {
      if(m_toolbarPage != null)
      {
        m_toolbarPage.ToolbarItems.Add(item);
      }
      else
      {
        m_toolbarCache.Add(item);
      }
    }

    public static ToolbarItem GetToolbarItem(string name)
    {
      if(m_toolbarPage != null)
      {
        foreach(ToolbarItem toolbarItem in m_toolbarPage.ToolbarItems)
        {
          if(toolbarItem.Text == name)
          {
            return toolbarItem;
          }
        }
      }
      return null;
    }

    private static void DummyAction()
    {

    }
  }
}
