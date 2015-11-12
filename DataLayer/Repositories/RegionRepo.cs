using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataLayer.Config;
using DataLayer.Interfaces;
using ModelLayer;

namespace DataLayer.Repositories
{
    public class RegionRepo : IRegionRepository
    {
        private SqlConnection _cn = new SqlConnection(Settings.ConnectionString);

        public List<Region> GetAll()
        {
            return _cn.Query<Region>("SELECT * FROM Region").ToList();
        }

        public Region Create(Region region)
        {
            var sqlQuery = "INSERT INTO Region (RegionDescription) VALUES(@RegionDescription); " +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";
            var regionID = _cn.Query<int>(sqlQuery, region).Single();
            region.RegionID = regionID;

            return region;
        }

        public Region Retrieve(int id)
        {
            var p = new DynamicParameters();
            p.Add("RegionID", id);

            return _cn.Query<Region>("SELECT * FROM Region WHERE RegionID = @RegionID", p).SingleOrDefault();
        }

        public Region Update(Region region)
        {
            var p = new DynamicParameters();
            p.Add("RegionID", region.RegionID);
            p.Add("RegionDescription", region.RegionDescription);

            var sqlQuery = "UPDATE Region " +
                           "SET RegionDescription = @RegionDescription " +
                           "WHERE RegionID = @RegionID";

            _cn.Execute(sqlQuery, p);

            return region;
        }

        public void Delete(int id)
        {
            var p = new DynamicParameters();
            p.Add("RegionID", id);

            var sqlQuery = "DELETE FROM Region WHERE RegionID = @RegionID";

            _cn.Execute(sqlQuery, p);
        }
    }
}
