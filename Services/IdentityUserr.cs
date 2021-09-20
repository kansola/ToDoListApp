using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListApp.Services
{
    public class IdentityUserr : IdentityDbContext<IdentityUser>
    {
    }
}
