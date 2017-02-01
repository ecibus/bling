using System;
using System.Collections.Generic;

namespace Bling.Presenter.LOS
{
    public interface IHMDA
    {
        DateTime Now { get; }
        List<string> AvailableYear { set; }
    }
}
