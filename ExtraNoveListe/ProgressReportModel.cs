using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{
    public class ProgressReportModel
    {
        public long Position { get; set; }
        public double Seconds { get; set; }
        public NAudio.Wave.AudioFileReader AudioReader { get; set; }
        public double PercentageDone
        {
            get
            {
                if (AudioReader != null)
                {
                    return AudioReader.CurrentTime.TotalSeconds / AudioReader.TotalTime.TotalSeconds;
                }
                else return 0;
            }
        }
    }
}
