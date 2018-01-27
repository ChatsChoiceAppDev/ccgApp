using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCG
{
  public interface IWebViewSilent
  {
    void AddNavigationHandler(Action<string> navHandler);
  }
}
