using System;
using System.Collections.Generic;

namespace ToDoList.Database.Models;

public partial class TblTaskCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<TblToDoList> TblToDoLists { get; set; } = new List<TblToDoList>();
}
