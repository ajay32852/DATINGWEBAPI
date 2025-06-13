using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class APPLICATION_LOG
{
    public int Id { get; set; }

    public string Message { get; set; }

    public string MessageTemplate { get; set; }

    public string Level { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string Exception { get; set; }

    public string Properties { get; set; }

    public string IPADDRESS { get; set; }

    public int? USERID { get; set; }

    public string USEREMAIL { get; set; }

    public DateTimeOffset? EVENTDATETIME { get; set; }
}
