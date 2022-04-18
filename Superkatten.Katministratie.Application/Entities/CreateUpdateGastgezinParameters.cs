﻿namespace Superkatten.Katministratie.Application.Entities
{
    public class CreateUpdateGastgezinParameters
    {
        public string Address { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}