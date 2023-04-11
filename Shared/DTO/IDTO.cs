using System.Linq.Expressions;

namespace DOOR.Shared.DTO
{
    public interface IDTO<Raw, PK> where Raw : class
    {
        abstract public PK primaryKey();
        abstract public Raw ToRecord();
    }
}
