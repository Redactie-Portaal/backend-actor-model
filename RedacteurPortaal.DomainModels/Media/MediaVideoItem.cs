namespace RedacteurPortaal.DomainModels.Media
{
    public class MediaVideoItem : MediaItem
    {
        string Reporter { get; set; }
        string Sound { get; set; }
        string Editor { get; set; }
        string LastPicture { get; set; }
        List<string> Keywords { get; set; }
        string VoiceOver { get; set; }
        string Description { get; set; }
        DateTime ProgramDate { get; set; }
        string ItemName { get; set; }
        string EPG { get; set; }
        string Presentation { get; set; }
        TimeSpan Duration { get; set; }
        string ArchiveMaterial { get; set; }
        Weather Weather { get; set; }
        string Producer { get; set; }
        string Director { get; set; }
        List<string> Guests { get; set; }
        string FirstPicture { get; set; }
        string LastWords { get; set; }
        string ProgramName { get; set; }
        string FirstWords { get; set; }
    }
}
