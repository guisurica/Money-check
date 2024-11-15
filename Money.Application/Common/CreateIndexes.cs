using Money.Domain.Contracts.Bases;

namespace Money.Application.Common
{
    public static class CreateIndexes<TEntity> where TEntity : Entity
    {
        public static List<string> Create(TEntity entity)
        {
            var names = entity.Name.Split(' ');
            var indexes = new List<string>();

            foreach (var name in names) 
            { 
                indexes.Add(TextCleaner.RemoveWhiteSpaces(name.ToLower()));
            }

            return indexes;
        }
    }
}
