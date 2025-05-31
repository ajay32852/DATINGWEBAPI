using System;
using System.Collections.Generic;

namespace DATINGWEBAPI.DAL.Entities;

public partial class SWIPE
{
    public long SWIPERID { get; set; }

    public long SWIPEDID { get; set; }

    public bool LIKED { get; set; }

    public DateTime TIMESTAMP { get; set; }

    public virtual USER SWIPED { get; set; }

    public virtual USER SWIPER { get; set; }
}
