using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class NOTIFICATIONSETTING
{
    public long NOTIFICATIONSETTINGID { get; set; }

    public long USERID { get; set; }

    public bool RECEIVEMATCHNOTIFICATIONS { get; set; }

    public bool RECEIVEMESSAGENOTIFICATIONS { get; set; }

    public DateTime CREATEDAT { get; set; }

    public virtual USER USER { get; set; }
}
