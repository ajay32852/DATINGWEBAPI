using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class INTEREST
{
    public long INTERESTID { get; set; }

    public string NAME { get; set; }

    public string ICONURL { get; set; }

    public virtual ICollection<USERINTEREST> USERINTERESTs { get; set; } = new List<USERINTEREST>();
}
