using System.Collections.Generic;
using Orchard.ContentManagement.Records;
using Orchard.Data.Conventions;

namespace Contrib.Podcasts.Models {
  public class PodcastPartRecord : ContentPartRecord {
    /// <summary>
    /// Podcast description.
    /// </summary>
    [StringLengthMax]
    public virtual string Description { get; set; }

    /// <summary>
    /// Podcast rating.
    /// </summary>
    public virtual SimpleRatingTypes Rating { get; set; }
    
    /// <summary>
    /// Podcast license.
    /// </summary>
    public virtual CreativeCommonsLicenseTypes CreativeCommonsLicense { get; set; }

    /// <summary>
    /// Flag indicating if the show transcripts should be included in the RSS feed.
    /// </summary>
    public virtual bool IncludeTranscriptInFeed { get; set; }

    public PodcastPartRecord() {
      Hosts = new List<PodcastHostRecord>();
    }

    public virtual IList<PodcastHostRecord> Hosts { get; set; }
  }
}