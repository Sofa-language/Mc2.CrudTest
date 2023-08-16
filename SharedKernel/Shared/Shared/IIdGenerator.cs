using IdGen;

namespace Mc2.CrudTest.Presentation.Shared.Shared
{
    public interface IIdGenerator
    {
        long GetNewId();
    }
    public class SnowflakeIdGenerator : IIdGenerator
    {
        public long GetNewId()
        {
            var idGenerator = new IdGenerator(123);
            return idGenerator.CreateId();
        }
    }
}
