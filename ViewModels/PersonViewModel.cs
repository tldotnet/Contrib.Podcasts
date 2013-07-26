using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Contrib.Podcasts.Models;


namespace Contrib.Podcasts.ViewModels {
  public class PersonViewModel {
    public int Id { get; set; }

    [Required(ErrorMessage = "Person's name is required.")]    
    public string Name { get; set; }
    [Required(ErrorMessage = "Person's email is required.")]  
    public string Email { get; set; }

    public string Url { get; set; }
    public string TwitterName { get; set; }

    public IEnumerable<PodcastPart> PodcastsHosted { get; set; }
    public IEnumerable<PodcastEpisodePart> EpisodesHosted { get; set; }
    public IEnumerable<PodcastEpisodePart> EpisodesGuested { get; set; }
  }
}
