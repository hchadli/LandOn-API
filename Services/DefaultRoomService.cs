using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LandonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LandonAPI.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly HotelApiDbContext _context;
        private readonly IConfigurationProvider _configurationProvider;

        public DefaultRoomService(HotelApiDbContext context, IMapper mapper, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _configurationProvider = configurationProvider;
        }


        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            // What we need to do here is pull all the rooms out of the context
            // and then map each one of them to the room resource,
            // instead of duplicating the code that we have in GetRoomAsync over and over
            // we can use something in auto mapper called IQueryable extensions to project everything all at once

            var query = _context.Rooms
                .ProjectTo<Room>(_configurationProvider);

            return await query.ToArrayAsync();

        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = _context.Rooms
                .SingleOrDefault(r => r.Id == id);

            if (entity == null)
            {
                return null;
            }

            var mapper = _configurationProvider.CreateMapper();
            return mapper.Map<Room>(entity);

        }
    }
}
