using Superkatten.Katministratie.Infrastructure.Entities;
using System;

namespace Superkatten.Katministratie.Infrastructure.Builders
{
    public class SuperkatDtoBuilder
    {
        private Guid _id { get; set; }
        private int _number { get; set; }
        private DateTimeOffset _catchDate { get; set; } = DateTimeOffset.Now;
        private DateTimeOffset _birthday { get; set; } = DateTimeOffset.Now.AddDays(-21);
        private string _catchLocation { get; set; } = "Rhenoy";
        private bool _reserved { get; set; } = false;
        private string _name { get; set; } = "John Doe";

        public SuperkatDtoBuilder WithId(Guid id) { _id = id; return this; }
        public SuperkatDtoBuilder WithNumber(int number) { _number = number; return this; }
        public SuperkatDtoBuilder WithFoundDate(DateTimeOffset catchDate) { _catchDate = catchDate; return this; }
        public SuperkatDtoBuilder WithBirthday(DateTimeOffset birthday) { _birthday = birthday; return this; }
        public SuperkatDtoBuilder WithCatchLocation(string catchLocation) { _catchLocation = catchLocation; return this; }
        public SuperkatDtoBuilder WithReserved(bool reserved) { _reserved = reserved; return this; }
        public SuperkatDtoBuilder WithName(string name) { _name = name; return this; }

        public SuperkatDto Build()
        {
            return new SuperkatDto
            {
                Id = _id,
                Number = _number,
                Birthday = _birthday,
                CatchLocation = _catchLocation,
                CatchDate = _catchDate,
                Reserved = _reserved,
                Name = _name
            };
        }
    }
}
