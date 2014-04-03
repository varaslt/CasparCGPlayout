using System;

namespace CasparCGPlayout.Utils
{
    class CurrentPlayingItem
    {
        public Int32 inFrames { get; set; }
        public Int32 outFrames { get; set; }
        public Int32 position { get; set; }
        public Int32 length { get; set; }
        public Int32 framerate { get; set; }
        public int index { get; set; }
        public int lastPlayedIndex { get; set; }
        
    }
}
