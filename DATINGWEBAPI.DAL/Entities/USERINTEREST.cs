using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class USERINTEREST
{
    public long USERINTERESTID { get; set; }

    public long USERID { get; set; }

    public long INTERESTID { get; set; }

    public virtual INTEREST INTEREST { get; set; }

    public virtual USER USER { get; set; }
}
