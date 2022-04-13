using Superkatten.Katministratie.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Infrastructure.Builders
{
    public class GastgezinDtoBuilder
    {
        private int _id { get; set; }
        private string _name { get; set; } = string.Empty;
        private string _address { get; set; } = string.Empty;
        private string _city { get; set; } = string.Empty;
        private string _phone{ get; set; } = string.Empty;

        private List<SuperkatDto> _superkatten { get; set; } = new List<SuperkatDto>();

        public GastgezinDtoBuilder WithId(int id) { _id = id; return this; }
        public GastgezinDtoBuilder WithName(string name) { _name = name; return this; }
        public GastgezinDtoBuilder WithAddress(string address) { _address = address; return this; }
        public GastgezinDtoBuilder WithCity(string city) { _city = city; return this; }
        public GastgezinDtoBuilder WithPhone(string phone) { _phone = phone; return this; }
        public GastgezinDtoBuilder WithSuperkat(SuperkatDto superkat) { _superkatten.Add(superkat); return this; }

        public GastgezinDto Build()
        {
            var gastgezin = new GastgezinDto
            {
                Id = _id,
                Name = _name,
                Address = _address,
                City = _city,
                Phone = _phone,
            };

            gastgezin.Superkatten.AddRange(_superkatten);

            return gastgezin;
        }
    }
}
