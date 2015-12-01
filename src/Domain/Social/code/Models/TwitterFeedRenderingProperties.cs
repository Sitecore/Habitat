using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Social.Models
{
  public class TwitterFeedRenderingProperties
  {
    private int twittsToShow;
    private const int DefaultTwittsToShow = 2;
    public int TwittsToShow
    {
      get
      {
        if (twittsToShow == 0)
        {
          this.twittsToShow = DefaultTwittsToShow;
        }

        return this.twittsToShow;
      }
      set
      {
        this.twittsToShow = value;
      }
    }
  }
}