using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class CONTACT
{
    public long CONTACTID { get; set; }

    public long USERID { get; set; }

    public string CONTACTNAME { get; set; }

    public string CONTACTPHONE { get; set; }

    public bool ISCONNECTED { get; set; }

    public virtual USER USER { get; set; }
}
