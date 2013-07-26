using System.Collections.Generic;
using Contrib.Podcasts.Models;

namespace Contrib.Podcasts.ViewModels {
  public class PeopleViewModel {
    public IEnumerable<PersonRecord> People { get; set; }
    public dynamic Pager { get; set; }

  }
}