﻿using Contracts.Base;

namespace PublicApi.v1.DTO.Search
{
    public class OrganizationSearch : BaseSearch
    {
        public string? Name { get; set; }
        public string? RegistrationNumber { get; set; }
        public override string? SortField { get; set; } = nameof(Organization.Name);
    }
}
