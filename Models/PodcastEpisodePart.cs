using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.Core.Title.Models;
using Orchard.Security;

namespace Contrib.Podcasts.Models {
  public class PodcastEpisodePart : ContentPart<PodcastEpisodePartRecord> {

    [Required]
    public int Podcast_id {
      get { return Record.Podcast_id; }
      set { Record.Podcast_id = value; }
    }

    [Required]
    public int EpisodeNumber {
      get { return Record.EpisodeNumber; }
      set { Record.EpisodeNumber = value; }
    }

    [Required]
    public string Title {
      get { return this.As<TitlePart>().Title; }
      set { this.As<TitlePart>().Title = value; }
    }

    public DateTime RecordingDate {
      get { return Record.RecordingDate; }
      set { Record.RecordingDate = value; }
    }

    public string EnclosureUrl {
      get { return Record.EnclosureUrl; }
      set { Record.EnclosureUrl = value; }
    }

    public string Duration {
      get { return Record.Duration; }
      set { Record.Duration = value; }
    }

    public int EnclosureFilesize {
      get { return Record.EnclosureFilesize; }
      set { Record.EnclosureFilesize = value; }
    }

    public string Notes {
      get { return Record.Notes; }
      set { Record.Notes = value; }
    }

    public string Transcription {
      get { return this.As<BodyPart>().Text; }
      set { this.As<BodyPart>().Text = value; }
    }

    [Required]
    public SimpleRatingTypes Rating {
      get { return Record.Rating; }
      set { Record.Rating = value; }
    }

    public IEnumerable<PersonRecord> Hosts {
      get { return Record.PodcastPeople.ToList().Where(p => p.IsHost == true).Select(p => p.PersonRecord); }
    }

    public IEnumerable<PersonRecord> Guests {
      get { return Record.PodcastPeople.ToList().Where(p => p.IsHost == false).Select(p => p.PersonRecord); }
    }

    public PodcastPart PodcastPart {
      get { return this.As<ICommonPart>().Container.As<PodcastPart>(); }
      set { this.As<ICommonPart>().Container = value; }
    }

    public IUser Creator {
      get { return this.As<ICommonPart>().Owner; }
      set { this.As<ICommonPart>().Owner = value; }
    }

    public bool IsPublished {
      get { return ContentItem.VersionRecord != null && ContentItem.VersionRecord.Published; }
    }

    public bool HasDraft {
      get {
        return (
                   (ContentItem.VersionRecord != null) && (
                       (ContentItem.VersionRecord.Published == false) ||
                       (ContentItem.VersionRecord.Published && ContentItem.VersionRecord.Latest == false)));
      }
    }

    public bool HasPublished {
      get {
        return IsPublished || ContentItem.ContentManager.Get(Id, VersionOptions.Published) != null;
      }
    }

    public DateTime? PublishedUtc {
      get { return this.As<ICommonPart>().PublishedUtc; }
    }
  }
}