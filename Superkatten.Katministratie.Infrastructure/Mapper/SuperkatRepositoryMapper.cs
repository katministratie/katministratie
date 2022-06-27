using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Entities;

namespace Superkatten.Katministratie.Infrastructure.Mapper;

public class SuperkatRepositoryMapper : ISuperkatRepositoryMapper
{
    public SuperkatDto MapDomainToRepository(Superkat superkat)
    {
        return new SuperkatDto
        {
            Id = superkat.Id,
            Number = superkat.Number,
            Name = superkat.Name,
            CatchDate = superkat.CatchDate,
            CatchLocation = superkat.CatchLocation,
            Birthday = superkat.Birthday,
            Area = EnumConverter<CatArea>.ToInt(superkat.CatArea),
            CageNumber = superkat.CageNumber,
            Retour = superkat.Retour,
            Reserved = superkat.Reserved,
            Behaviour = EnumConverter<CatBehaviour>.ToInt(superkat.Behaviour),
            IsKitten = superkat.IsKitten,
            Gender = EnumConverter<Gender>.ToInt(superkat.Gender),
            LitterType = EnumConverter<LitterGranuleType>.ToInt(superkat.LitterType),
            WetFoodAllowed = superkat.WetFoodAllowed,
            FoodType = EnumConverter<FoodType>.ToInt(superkat.FoodType),
            Color = superkat.Color
};
    }

    public Superkat MapRepositoryToDomain(SuperkatDto superkatDto)
    {
        var superkat = new Superkat(
            superkatDto.Number,
            superkatDto.CatchDate,
            superkatDto.CatchLocation)
        {
            Id = superkatDto.Id
        };
        superkat.SetName(superkatDto.Name ?? string.Empty);
        superkat.SetReserved(superkatDto.Reserved);
        superkat.SetArea(EnumConverter<CatArea>.FromInt(superkatDto.Area));
        superkat.SetCageNumber(superkatDto.CageNumber);
        superkat.SetRetour(superkatDto.Retour);
        superkat.SetBehaviour(EnumConverter<CatBehaviour>.FromInt(superkatDto.Behaviour));
        superkat.SetBirthday(superkatDto.Birthday);
        superkat.SetIsKitten(superkatDto.IsKitten);
        superkat.SetGender(EnumConverter<Gender>.FromInt(superkatDto.Gender));
        superkat.SetLitterType(EnumConverter<LitterGranuleType>.FromInt(superkatDto.LitterType));
        superkat.SetWetFoodAllowed(superkatDto.WetFoodAllowed);
        superkat.SetFoodType(EnumConverter<FoodType>.FromInt(superkatDto.FoodType));
        superkat.SetColor(superkatDto.Color ?? string.Empty);

        return superkat;
    }
}
