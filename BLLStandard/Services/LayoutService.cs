using BLLStandard.DTO;
using BLLStandard.Exceptions;
using BLLStandard.ServicesInterfaces;
using DALStandard.Models;
using DALStandard.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLLStandard.Services
{
    public class LayoutService : ILayoutService
    {
        private ILayoutRepository repository;

        public LayoutService(ILayoutRepository repository)
        {
            this.repository = repository;
        }

        public void Create(IEnumerable<LayoutDTO> layoutDTOs)
        {
            var list = new List<Layout>();

            foreach (var item in layoutDTOs)
            {
                if (!IsLayoutNameUniqueByVenueId(item))
                {
                    throw new NotUniqueNameException();
                }

                list.Add(new Layout { Name = item.Name, VenueId = item.VenueId, Description = item.Description });
            }

            repository.Create(list);
        }

        public void Create(LayoutDTO obj)
        {
            if (!IsLayoutNameUniqueByVenueId(obj))
            {
                throw new NotUniqueNameException();
            }

            repository.Create(new Layout { Name = obj.Name, VenueId = obj.VenueId, Description = obj.Description });
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<LayoutDTO> GetAll()
        {
            var list = new List<LayoutDTO>();

            foreach (var item in repository.GetAll())
            {
                list.Add(new LayoutDTO { Id = item.Id, Name = item.Name, VenueId = item.VenueId, Description = item.Description });
            }

            return list;
        }

        public LayoutDTO GetById(int id)
        {
            var layout = repository.GetById(id);

            if (layout == null)
            {
                return null;
            }

            return new LayoutDTO { Id = layout.Id, Name = layout.Name, VenueId = layout.VenueId, Description = layout.Description };
        }

        public LayoutDTO GetByName(string name)
        {
            var layout = repository.GetByName(name);

            if (layout == null)
            {
                return null;
            }

            return new LayoutDTO { Id = layout.Id, Name = layout.Name, VenueId = layout.VenueId, Description = layout.Description };
        }

        public void Update(LayoutDTO obj)
        {
            if (!IsLayoutNameUniqueByVenueId(obj))
            {
                throw new NotUniqueNameException();
            }

            repository.Update(new Layout { Id = obj.Id, Name = obj.Name, VenueId = obj.VenueId, Description = obj.Description });
        }

        public bool IsLayoutNameUniqueByVenueId(LayoutDTO obj)
        {
            var list = repository.GetAll().ToList().FindAll(x => x.VenueId == obj.VenueId);
            var layoutWithTheSameName = list.FirstOrDefault(x => x.Name == obj.Name);

            if(layoutWithTheSameName != null && layoutWithTheSameName.Id == obj.Id && !String.Equals(layoutWithTheSameName.Description, obj.Description))
            {
                return true;
            }

            return !repository.GetAll().ToList().FindAll(x => x.VenueId == obj.VenueId).Exists(x => x.Name == obj.Name);
        }
    }
}
