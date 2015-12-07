﻿namespace Sitecore.Feature.Social.Models
{
  public class TwitterFeedRenderingProperties
  {
    private int tweetsToShow;
    private const int DefaultTweetsToShow = 2;
    public int TweetsToShow
    {
      get
      {
        if (tweetsToShow == 0)
        {
          this.tweetsToShow = DefaultTweetsToShow;
        }

        return this.tweetsToShow;
      }
      set
      {
        this.tweetsToShow = value;
      }
    }
  }
}