using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DataBaseOperation;
using TodoApi.Dto;

namespace TodoApi.DataBaseAccess
{
    public class DataAccess <TypeOfClass> where TypeOfClass : new()
    {
        public DataAccess()
        {
            _dataChanger = new DataChanger<TypeOfClass>();
            _dataProspector = new DataProspector<TypeOfClass>();
            _dataScribe = new DataScribe<TypeOfClass>();
            _dataUtylizer = new DataUtylizer<TypeOfClass>();
        }
        private DataChanger<TypeOfClass> _dataChanger;
        private DataProspector<TypeOfClass> _dataProspector;
        private DataScribe<TypeOfClass> _dataScribe;
        private DataUtylizer<TypeOfClass> _dataUtylizer;

        public void Update(TypeOfClass entityToUpdate)
        {
            _dataChanger.UpdateData(entityToUpdate);
        }
        public List<TypeOfClass> Read()
        {
            return _dataProspector.DownloadData();
        }
        public void Create(TypeOfClass entityToUpdate)
        {
            _dataScribe.SaveDate(entityToUpdate);
        }
        public void Delete(string Id)
        {
            _dataUtylizer.DeleteData(Id);
        }

    }
}
