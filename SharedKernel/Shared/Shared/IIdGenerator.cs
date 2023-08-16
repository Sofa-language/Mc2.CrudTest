using IdGen;

namespace Mc2.CrudTest.Presentation.Shared.Shared
{
    public interface IIdGenerator
    {
        long GetNewId();
    }
    public class SnowflakeIdGenerator : IIdGenerator
    {
        private readonly IdGenerator _idGenerator;

        public SnowflakeIdGenerator(IdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        public long GetNewId()
        {
            return _idGenerator.CreateId();
        }
    }
}
