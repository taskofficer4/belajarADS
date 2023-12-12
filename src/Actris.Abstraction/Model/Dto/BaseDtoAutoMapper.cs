using AutoMapper;

namespace Actris.Abstraction.Model.Dto
{


    /// <summary>
    /// Must implement Ctor() & Ctor(T)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseDtoAutoMapper<TEntity>
    {

        public BaseDtoAutoMapper()
        {

        }
        public BaseDtoAutoMapper(TEntity entity)
        {
            Mapper.Map(entity, this);
        }
        public virtual TEntity ToEntity()
        {
            return Mapper.Map<TEntity>(this);
        }
        
    }
}
