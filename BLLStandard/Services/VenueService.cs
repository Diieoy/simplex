using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.ServicesInterfaces;
using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System.Collections.Generic;

namespace BLLStandard.Services
{
    public class VenueService : IVenueService
    {
        private IVenueRepository repository;

        public VenueService(IVenueRepository repository)
        {
            this.repository = repository;
        }

        public void Create(IEnumerable<VenueDTO> venueDTOs)
        {
            var list = new List<Venue>();

            foreach (var item in venueDTOs)
            {
                if (!IsNameUnique(item))
                {
                    throw new NotUniqueNameException();
                }

                list.Add(new Venue { Name = item.Name, Description = item.Description, Address = item.Address, Phone = item.Phone });
            }

            repository.Create(list);
        }

        public void Create(VenueDTO obj)
        {
            if (!IsNameUnique(obj))
            {
                throw new NotUniqueNameException();
            }

            repository.Create(new Venue { Name = obj.Name, Description = obj.Description, Address = obj.Address, Phone = obj.Phone });
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public VenueDTO GetById(int id)
        {
            var venue = repository.GetById(id);

            if(venue == null)
            {
                return null;
            }

            return new VenueDTO { Id = venue.Id, Name = venue.Name, Description = venue.Description, Address = venue.Address, Phone = venue.Phone };
        }

        public VenueDTO GetByName(string name)
        {
            var venue = repository.GetByName(name);

            if (venue == null)
            {
                return null;
            }

            return new VenueDTO { Id = venue.Id, Name = venue.Name, Description = venue.Description, Address = venue.Address, Phone = venue.Phone };
        }

        public void Update(VenueDTO obj)
        {
            if (!IsNameUnique(obj))
            {
                throw new NotUniqueNameException();
            }

            repository.Update(new Venue { Id = obj.Id, Name = obj.Name, Description = obj.Description, Address = obj.Address, Phone = obj.Phone });
        }

        public IEnumerable<VenueDTO> GetAll()
        {
            var list = new List<VenueDTO>();

            foreach (var item in repository.GetAll())
            {
                list.Add(new VenueDTO { Id = item.Id, Name = item.Name, Description = item.Description, Address = item.Address, Phone = item.Phone });
            }

            return list;
        }

        public bool IsNameUnique(VenueDTO obj)
        {
            var venueFromDb = repository.GetByName(obj.Name);

            if (venueFromDb != null)
            {
                if ((!venueFromDb.Description.Equals(obj.Description) || !venueFromDb.Address.Equals(obj.Address) || !venueFromDb.Phone.Equals(obj.Phone)) ||
                    (venueFromDb.Description.Equals(obj.Description) && venueFromDb.Address.Equals(obj.Address) && venueFromDb.Phone.Equals(obj.Phone)))
                {
                    return false;
                }              
            }

            return true;
        }
    }
}
