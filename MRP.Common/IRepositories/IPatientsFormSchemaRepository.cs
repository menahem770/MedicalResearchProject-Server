using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.Common.IRepositories
{
    public interface IPatientsFormSchemaRepository
    {
        Task<bool> SaveFirstSchema(string schema);
        Task<string> GetFirstSchema();
    }
}
