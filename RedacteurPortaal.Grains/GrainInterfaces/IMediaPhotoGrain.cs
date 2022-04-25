﻿using Orleans;
using RedacteurPortaal.DomainModels.Media;

namespace RedacteurPortaal.Grains.GrainInterfaces;

public interface IMediaPhotoGrain : IManageableGrain<MediaPhotoItem>
{
    Task Update(MediaPhotoItem mediaItem);
}