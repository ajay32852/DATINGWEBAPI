using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class DEVICE
{
    public long DEVICEID { get; set; }

    public long USERID { get; set; }

    public string DEVICETOKEN { get; set; }

    public string PLATFORM { get; set; }

    public DateTime CREATEDAT { get; set; }

    public virtual USER USER { get; set; }
}
