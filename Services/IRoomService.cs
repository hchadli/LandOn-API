using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonAPI.Models;

namespace LandonAPI.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsAsync();    
        Task<Room> GetRoomAsync(Guid id);
    }
}
