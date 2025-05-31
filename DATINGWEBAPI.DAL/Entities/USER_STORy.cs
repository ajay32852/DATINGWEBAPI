using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class USER_STORy
{
    public long STORYID { get; set; }

    public long USERID { get; set; }

    public string MEDIA_URL { get; set; }

    public string STORAGE_ID { get; set; }

    public string MEDIA_TYPE { get; set; }

    public string CAPTION { get; set; }

    public bool IS_ACTIVE { get; set; }

    public DateTime? EXPIRES_AT { get; set; }

    public DateTime CREATED_AT { get; set; }

    public DateTime UPDATED_AT { get; set; }

    public virtual USER USER { get; set; }
}
