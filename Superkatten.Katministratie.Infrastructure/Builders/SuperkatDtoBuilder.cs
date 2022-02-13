using Superkatten.Katministratie.Infrastructure.Entities;
using System;

namespace Superkatten.Katministratie.Infrastructure.Builders
{
    public class SuperkatDtoBuilder
    {
        private int _id { get; set; }
        private int _number { get; set; }
        private DateTimeOffset _foundDate { get; set; } = DateTimeOffset.Now;
        private DateTimeOffset _birthday { get; set; } = DateTimeOffset.Now.AddDays(-21);
        private string _catchLocation { get; set; } = "Rhenoy";
        private string _kleur { get; set; } = "Red";
        private string _name { get; set; } = "John Doe";

        public SuperkatDtoBuilder WithId(int id) { _id = id; return this; }
        public SuperkatDtoBuilder WithNumber(int number) { _number = number; return this; }
        public SuperkatDtoBuilder WithFoundDate(DateTimeOffset founddate) { _foundDate = founddate; return this; }
        public SuperkatDtoBuilder WithBirthday(DateTimeOffset birthday) { _birthday = birthday; return this; }
        public SuperkatDtoBuilder WithCatchLocation(string catchLocation) { _catchLocation = catchLocation; return this; }
        public SuperkatDtoBuilder WithKLeur(string kleur) { _kleur = kleur; return this; }
        public SuperkatDtoBuilder WithName(string name) { _name = name; return this; }

        public SuperkatDto Build()
        {
            return new SuperkatDto
            {
                Id = _id,
                Number = _number,
                Birthday = _birthday,
                CatchLocation = _catchLocation,
                FoundDate = _foundDate,
                Kleur = _kleur,
                Name = _name
            };
        }
    }
}
