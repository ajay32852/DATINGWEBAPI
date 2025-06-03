using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class VERIFICATIONCODE
{
    public long ID { get; set; }

    public string PHONENUMBER { get; set; }

    public string CODE { get; set; }

    public DateTime EXPIRESAT { get; set; }

    public bool ISUSED { get; set; }

    public DateTime CREATEDAT { get; set; }

    public long? USERID { get; set; }

    public virtual USER USER { get; set; }
}
