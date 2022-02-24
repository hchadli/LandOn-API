using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace LandonAPI
{
    public class HotelApiDbContext : DbContext
    {
        public HotelApiDbContext(DbContextOptions options)
        :base(options)
        {
            
        }

        public DbSet<RoomEntity> Rooms { get; set; }    
    }
}
