﻿using RedacteurPortaal.Api.DTOs;
using RedacteurPortaal.DomainModels.Media;
using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedacteurPortaal.Api.Models.Request;

public class UpdateNewsItemRequest
{
    public string Title { get; set; }

    public Status Status { get; set; }

    public ApprovalState? ApprovalState { get; set; }

    public string? Author { get; set; }

    public FeedSourceDto Source { get; set; }

    public string Body { get; set; }

    public List<UpdateContactRequest> ContactDetails { get; set; }

    public UpdateLocationRequest LocationDetails { get; set; }

    public DateTime ProductionDate { get; set; }

    public DateTime EndDate { get; set; }

    public Category Category { get; set; }

    public Region Region { get; set; }

    public UpdateMediaVideoItemRequest[] Videos { get; set; }

    public UpdateMediaAudioItemRequest[] Audio { get; set; }

    public UpdateMediaPhotoItemRequest[] Photos { get; set; }
}
