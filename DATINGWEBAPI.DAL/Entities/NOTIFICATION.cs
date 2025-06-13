using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class NOTIFICATION
{
    public long NOTIFICATIONID { get; set; }

    public long USERID { get; set; }

    public string TITLE { get; set; }

    public string MESSAGE { get; set; }

    public bool? ISREAD { get; set; }

    public DateTime? CREATEDAT { get; set; }

    public string TYPE { get; set; }

    public virtual USER USER { get; set; }
}
