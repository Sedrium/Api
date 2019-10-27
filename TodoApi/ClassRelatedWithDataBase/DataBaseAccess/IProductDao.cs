using System.Collections.Generic;

namespace TodoApi.DataBaseAccess
{
    public interface IProductDao<TypeOfClass> where TypeOfClass : new()
    {
        void Update(TypeOfClass entityToUpdate);
        List<TypeOfClass> Read();
        void Create(TypeOfClass entityToUpdate);
        void Delete(string Id);
    }
}