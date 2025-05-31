using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class USER_MEDIum
{
    public long MEDIAID { get; set; }

    public long USERID { get; set; }

    public string MEDIA_URL { get; set; }

    public string STORAGE_ID { get; set; }

    public string MEDIA_TYPE { get; set; }

    public bool IS_PROFILE_PIC { get; set; }

    public DateTime CREATED_AT { get; set; }

    public virtual USER USER { get; set; }
}
