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
    public class AreaService : IAreaService
    {
        private IAreaRepository repository;

        public AreaService(IAreaRepository repository)
        {
            this.repository = repository;
        }

        public void Create(IEnumerable<AreaDTO> areaDTOs)
        {
            var list = new List<Area>();

            foreach (var item in areaDTOs)
            {
                if (!IsDescriptionUniqueByLayoutId(item))
                {
                    throw new NotUniqueDescriptionException();
                }

                list.Add(new Area { LayoutId=item.LayoutId, Description = item.Description, CoordX=item.CoordX, CoordY=item.CoordY });
            }

            repository.Create(list);
        }

        public void Create(AreaDTO obj)
        {
            if (!IsDescriptionUniqueByLayoutId(obj))
            {
                throw new NotUniqueDescriptionException();
            }

            repository.Create(new Area { LayoutId = obj.LayoutId, Description = obj.Description, CoordX = obj.CoordX, CoordY = obj.CoordY });
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<AreaDTO> GetAll()
        {
            var list = new List<AreaDTO>();

            foreach (var item in repository.GetAll())
            {
                list.Add(new AreaDTO { Id = item.Id, LayoutId = item.LayoutId, Description = item.Description, CoordX = item.CoordX, CoordY = item.CoordY });
            }

            return list;
        }

        public AreaDTO GetById(int id)
        {
            var area = repository.GetById(id);

            if (area == null)
            {
                return null;
            }

            return new AreaDTO { Id = area.Id, LayoutId = area.LayoutId, Description = area.Description, CoordX = area.CoordX, CoordY = area.CoordY };
        }

        public void Update(AreaDTO obj)
        {
            if (!IsDescriptionUniqueByLayoutId(obj))
            {
                throw new NotUniqueDescriptionException();
            }

            repository.Update(new Area { Id = obj.Id, LayoutId = obj.LayoutId, Description = obj.Description, CoordX = obj.CoordX, CoordY = obj.CoordY });
        }

        public bool IsDescriptionUniqueByLayoutId(AreaDTO obj)
        {
            var list = repository.GetAll().ToList().FindAll(x => x.LayoutId == obj.LayoutId);
            var areaWithTheSameDescription = list.FirstOrDefault(x => x.Description == obj.Description);

            if (list.Count == 0 || 
                areaWithTheSameDescription != null && areaWithTheSameDescription.Id == obj.Id && 
                (areaWithTheSameDescription.CoordX != obj.CoordX || areaWithTheSameDescription.CoordY != obj.CoordY))
            {
                return true;
            }

            return !repository.GetAll().ToList().FindAll(x => x.LayoutId == obj.LayoutId).Exists(x => x.Description == obj.Description);
        }
    }
}
