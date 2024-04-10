﻿using Contracts.Base;

namespace DAL.DTO.Search
{
    public class PersonSearch : BaseSearch
    {
        public string? Name { get; set; }
        public string? PersonalCode { get; set; }
        public override string? SortField { get; set; } = nameof(Person.FirstName);
    }
}