using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class MATCH
{
    public long USER1ID { get; set; }

    public long USER2ID { get; set; }

    public DateTime MATCHEDAT { get; set; }

    public virtual USER USER1 { get; set; }

    public virtual USER USER2 { get; set; }
}
