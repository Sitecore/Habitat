using System;

namespace Habitat.Framework.Emotions.Enums
{
    [Flags]
    public enum Emotions
    {
        None = 0,
        Anger = 1,
        Contempt = 2,
        Disgust = 4,
        Fear = 16,
        Happiness = 32,
        Neutral = 64,
        Sadness = 128,
        Surprise = 256
    }
}