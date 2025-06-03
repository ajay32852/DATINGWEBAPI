using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class USER
{
    public long USERID { get; set; }

    public string PHONENUMBER { get; set; }

    public string FIRSTNAME { get; set; }

    public string LASTNAME { get; set; }

    public string GENDER { get; set; }

    public DateTime? BIRTHDAY { get; set; }

    public int? AGE { get; set; }

    public string BIO { get; set; }

    public string LOCATION { get; set; }

    public string PROFILEIMAGEURL { get; set; }

    public string ROLE { get; set; }

    public bool ISDELETED { get; set; }

    public DateTime? DELETEDAT { get; set; }

    public DateTime CREATEDAT { get; set; }

    public DateTime? UPDATEDAT { get; set; }

    public DateTime LASTLOGIN { get; set; }

    public bool ISBLOCKED { get; set; }

    public bool? ALLOWCONTACTACCESS { get; set; }

    public bool? ENABLENOTIFICATIONS { get; set; }

    public double? LATITUDE { get; set; }

    public double? LONGITUDE { get; set; }

    public bool ISPROFILECOMPLETE { get; set; }

    public virtual ICollection<CONTACT> CONTACTs { get; set; } = new List<CONTACT>();

    public virtual ICollection<DEVICE> DEVICEs { get; set; } = new List<DEVICE>();

    public virtual ICollection<MATCH> MATCHUSER1s { get; set; } = new List<MATCH>();

    public virtual ICollection<MATCH> MATCHUSER2s { get; set; } = new List<MATCH>();

    public virtual ICollection<NOTIFICATIONSETTING> NOTIFICATIONSETTINGs { get; set; } = new List<NOTIFICATIONSETTING>();

    public virtual ICollection<SWIPE> SWIPESWIPEDs { get; set; } = new List<SWIPE>();

    public virtual ICollection<SWIPE> SWIPESWIPERs { get; set; } = new List<SWIPE>();

    public virtual ICollection<USERINTEREST> USERINTERESTs { get; set; } = new List<USERINTEREST>();

    public virtual ICollection<USER_MEDIum> USER_MEDIa { get; set; } = new List<USER_MEDIum>();

    public virtual ICollection<USER_STORy> USER_STORies { get; set; } = new List<USER_STORy>();

    public virtual ICollection<VERIFICATIONCODE> VERIFICATIONCODEs { get; set; } = new List<VERIFICATIONCODE>();
}
