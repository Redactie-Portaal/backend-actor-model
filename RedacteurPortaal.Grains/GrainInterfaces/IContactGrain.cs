﻿using Orleans;
using RedacteurPortaal.DomainModels.NewsItem;

namespace RedacteurPortaal.Grains.GrainInterfaces
{
    public interface IContactGrain : IGrainWithGuidKey
    {
        Contact GetContactDetails(Guid guid);

        Task AddContactDetails(Contact contact);

        Task DeleteContactDetails(Guid guid);

        Task UpdateContactDetails(Contact contact);
    }
}