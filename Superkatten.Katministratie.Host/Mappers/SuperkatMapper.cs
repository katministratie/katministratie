using Superkatten.Katministratie.Host.Entities;
using System.ComponentModel;

using contractEntities = Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Mappers
{
    public class SuperkatMapper : ISuperkatMapper
    {
        public CatBehaviour MapToContract(contractEntities.CatBehaviour behaviour)
        {
            return behaviour switch
            {
                contractEntities.CatBehaviour.Social => CatBehaviour.Social,
                contractEntities.CatBehaviour.Shy => CatBehaviour.Shy,
                contractEntities.CatBehaviour.Unknown => CatBehaviour.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(behaviour), (int)behaviour, typeof(CatBehaviour))
            };
        }

        public Gender MapToContract(contractEntities.Gender gender)
        {
            return gender switch
            {
                contractEntities.Gender.Molly => Gender.Molly,
                contractEntities.Gender.Tomcat => Gender.Tomcat,
                contractEntities.Gender.Unknown => Gender.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(gender), (int)gender, typeof(Gender))
            };
        }

        public CatArea MapToContract(contractEntities.CatArea catArea)
        {
            return catArea switch
            {
                contractEntities.CatArea.LargeCage => CatArea.LargeCage,
                contractEntities.CatArea.SmallCage => CatArea.SmallCage,
                contractEntities.CatArea.Unknown => CatArea.Unknown,
                _ => throw new InvalidEnumArgumentException(nameof(catArea), (int)catArea, typeof(CatArea))
            };
        }
    }
}
