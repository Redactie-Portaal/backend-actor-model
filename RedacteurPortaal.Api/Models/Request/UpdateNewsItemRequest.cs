﻿using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api.Models.Request;

public class UpdateNewsItemRequest
{
    public string? Title { get; set; }

    public string? Status { get; set; }

    public string? Author { get; set; }

    public FeedSource? Source { get; set; }

    public string? BodyDescription { get; set; }

    public string? ContactDetails { get; set; }

    public string? LocationDetails { get; set; }

    public DateTime EndDate { get; set; }

    public string? Category { get; set; }

    public string? Region { get; set; }

    public MediaVideoItem? Video { get; private set; }

    public MediaAudioItem? Audio { get; private set; }

    public MediaPhotoItem[]? Photos { get; private set; }
}