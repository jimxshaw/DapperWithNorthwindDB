using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;

namespace DataLayer.Interfaces
{
    public interface IRegionRepository
    {
        List<Region> GetAll();
        Region Create(Region region);
        Region Retrieve(int id);
        Region Update(Region region);
        void Delete(int id);
    }
}
