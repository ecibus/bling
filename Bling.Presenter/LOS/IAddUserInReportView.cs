using System;
using System.Collections.Generic;
using Bling.Domain;

namespace Bling.Presenter.LOS
{
    public interface IAddUserInReportView
    {
        List<ReportUser> CurrentReportUser { set; }
        List<UserInfo> AvailableUser { set; }
    }
}
