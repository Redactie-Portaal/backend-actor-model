﻿using RedacteurPortaal.DomainModels.NewsItem;
using RedacteurPortaal.DomainModels.Shared;

namespace RedacteurPortaal.Api.DTOs
{
    public abstract class MediaItemDTO
    {
        protected MediaItemDTO(
            Guid id,
            string title,
            string folder,
            DateTime republishDate,
            string rights,
            string camera,
            string lastWords,
            string proxyFile,
            string presentation,
            Location location,
            string format,
            Uri mediaLocation)
        {
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Folder = folder ?? throw new ArgumentNullException(nameof(folder));
            this.RepublishDate = republishDate;
            this.Rights = rights ?? throw new ArgumentNullException(nameof(rights));
            this.Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            this.LastWords = lastWords ?? throw new ArgumentNullException(nameof(lastWords));
            this.ProxyFile = proxyFile ?? throw new ArgumentNullException(nameof(proxyFile));
            this.Presentation = presentation ?? throw new ArgumentNullException(nameof(presentation));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.Format = format ?? throw new ArgumentNullException(nameof(format));
            this.MediaLocation = mediaLocation ?? throw new ArgumentNullException(nameof(mediaLocation));
        }
        
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Folder { get; set; }

        public DateTime RepublishDate { get; set; }

        public string Rights { get; set; }

        public string Camera { get; set; }

        public string LastWords { get; set; }

        public string ProxyFile { get; set; }

        public string Presentation { get; set; }

        public Location Location { get; set; }

        public string Format { get; set; }

        public Uri MediaLocation { get; set; }
    }
}