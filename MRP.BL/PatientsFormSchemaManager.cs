using MRP.Common.IRepositories;
using MRP.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.BL
{
    public class PatientsFormSchemaManager
    {
        private IPatientsFormSchemaRepository _rep;

        public PatientsFormSchemaManager()
        {
            _rep = new PatientsFormSchemaRepository();
        }

        public Task<bool> SaveFirstSchema(string schema)
        {
            return _rep.SaveFirstSchema(schema);
        }

        public Task<string> GetFirstSchema()
        {
            return _rep.GetFirstSchema();
        }
    }
}
