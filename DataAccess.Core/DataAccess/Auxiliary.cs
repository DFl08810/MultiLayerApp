using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Core.DataAccess
{
    public static class Auxiliary
    {
        public static void DetachAllEntities(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                context.Entry(entry.Entity).State = EntityState.Detached;
            }
        }
    }
}
