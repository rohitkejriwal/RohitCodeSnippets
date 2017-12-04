using IOCInfrastructure;
using Sgi.Core.DBService;
using Sgi.LPA.Indexer.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Indexer.Service
{
    public class IndexerService : Sgi.LPA.Indexer.Service.IIndexerService
    {
        private ILogHelper _logHelper;
        private IDBRepository _dbRepository;
        private const string collection = "dataindex";
        public IndexerService(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _dbRepository = serviceResolver.GetInstance<IDBRepository>();
        }

        public void AddUpdateIndex(IndexerModel model)
        {
            _dbRepository.Update<IndexerModel>(collection, model._id, model);
        }
    }
}
