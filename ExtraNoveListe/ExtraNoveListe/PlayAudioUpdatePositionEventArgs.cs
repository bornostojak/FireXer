using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{
    public class PlayAudioUpdatePositionEventArgs : EventArgs
    {
        public long Position => AudioReader.Position;
        public double Seconds => AudioReader.CurrentTime.TotalSeconds;
        public double PercentageDouble
        {
            get
            {
                if(AudioReader != null)
                {
                    return (AudioReader.CurrentTime.TotalSeconds * 100) / AudioReader.TotalTime.TotalSeconds;
                }
                return 0;
            }
        }
        public int Percentage => (int)PercentageDouble;
        

        public NAudio.Wave.AudioFileReader AudioReader { get; private set; } = null;
         
        public PlayAudioUpdatePositionEventArgs() : base()
        {

        }

        public PlayAudioUpdatePositionEventArgs(NAudio.Wave.AudioFileReader audio)
        {
            AudioReader = audio;
        }
    }
}
