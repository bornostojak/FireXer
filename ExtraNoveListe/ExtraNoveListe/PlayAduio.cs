using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Text.RegularExpressions;

namespace ExtraNoveListe
{
    public class PlayAduio
    {
        protected static List<PlayAduio> AllInitiated = new List<PlayAduio>();

        protected static List<PlayAduio> all = new List<PlayAduio>();
        public PlayAduio()
        {
            AllInitiated.Add(this);
        }

        public static void PlayGivenSound()
        {

        }

        public static void StopAll()
        {
            AllInitiated.ForEach((x) =>
            {
                x.directSoundOut?.Stop();
            });
            AllInitiated = new List<PlayAduio>();
        }

        private NAudio.Wave.WaveFileReader wave = null;
        private NAudio.Wave.Mp3FileReader mp3 = null;
        private NAudio.Wave.AudioFileReader audio = null;
        private NAudio.Wave.DirectSoundOut directSoundOut = null;

        private NAudio.Wave.WaveOutEvent directSoundOutEvent = null;

        public void Play(string file)
        {
            DisposeWave();
            if (System.IO.Path.GetExtension(file) == ".mp3" || System.IO.Path.GetExtension(file) == ".wav")
            {
                audio = new NAudio.Wave.AudioFileReader(GetPathFromString(file));
                OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(audio);
                //offsetSampleProvider.SkipOver = TimeSpan.FromSeconds(20f);
                //offsetSampleProvider.Take = TimeSpan.FromSeconds(5f);
                directSoundOut = new DirectSoundOut();
                directSoundOut.Init(offsetSampleProvider);
                directSoundOut.Play();
            }
            
        }

        public void PlayFromEnd(FirePlaySong s, int sec)
        {
            string file = GetPathFromString(s.PathName);
            double t = 0;

            double.TryParse(s.OriginalEndCue.Replace('.', ','), out t);

            DisposeWave();
            if (System.IO.Path.GetExtension(file) == ".mp3" || System.IO.Path.GetExtension(file) == ".wav")
            {
                audio = new NAudio.Wave.AudioFileReader(file);
                OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(audio);
                offsetSampleProvider.SkipOver = TimeSpan.FromSeconds(t-(double)sec < 0f ? 0 : t - (double)sec);
                //offsetSampleProvider.Take = TimeSpan.FromSeconds(5f);
                directSoundOut = new DirectSoundOut();
                directSoundOut.Init(offsetSampleProvider);
                directSoundOut.Play();
            }
            
        }

        private DirectSoundOut PlayFromTheEnd(FirePlaySong s, int sec)
        {
            string file = GetPathFromString(s.PathName);
            double t = 0;

            double.TryParse(s.OriginalEndCue.Replace('.', ','), out t);

            //DisposeWave();
            if (System.IO.Path.GetExtension(file) == ".mp3" || System.IO.Path.GetExtension(file) == ".wav")
            {
                audio = new NAudio.Wave.AudioFileReader(file);
                OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(audio);
                offsetSampleProvider.SkipOver = TimeSpan.FromSeconds(t - (double)sec < 0f ? 0 : t - (double)sec);
                //offsetSampleProvider.Take = TimeSpan.FromSeconds(5f);
                directSoundOut = new DirectSoundOut();
                directSoundOut.Init(offsetSampleProvider);
                directSoundOut.Play();
            }
            return directSoundOut;
        }

        private WaveOutEvent PlayFromTheEnd(NAudio.Wave.AudioFileReader audioReaderInMethod, string end_cue, int sec)
        {
            audio = audioReaderInMethod;
            
            double t = 0;
            double.TryParse(end_cue.Replace('.', ','), out t);
            
            OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(audio);
            offsetSampleProvider.SkipOver = TimeSpan.FromSeconds(t - (double)sec < 0f ? 0 : t - (double)sec);
            directSoundOutEvent = new WaveOutEvent();
            directSoundOutEvent.Init(offsetSampleProvider);
            return directSoundOutEvent;
        }

        private WaveOutEvent PlayFromBegining(NAudio.Wave.AudioFileReader audioReaderInMethod, int sec)
        {
            audio = audioReaderInMethod;
            OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(audio);
            offsetSampleProvider.Take = TimeSpan.FromSeconds((double)sec);
            directSoundOutEvent = new WaveOutEvent();
            directSoundOutEvent.Init(offsetSampleProvider);
            return directSoundOutEvent;
        }

        private DirectSoundOut PlayFromBegining(FirePlaySong s, int sec)
        {
            string file = GetPathFromString(s.PathName);
            double t = 0;

            double.TryParse(s.OriginalEndCue.Replace('.', ','), out t);

            //DisposeWave();
            if (System.IO.Path.GetExtension(file) == ".mp3" || System.IO.Path.GetExtension(file) == ".wav")
            {
                audio = new NAudio.Wave.AudioFileReader(file);
                OffsetSampleProvider offsetSampleProvider = new OffsetSampleProvider(audio);
                offsetSampleProvider.Take = TimeSpan.FromSeconds((double)sec);
                //offsetSampleProvider.Take = TimeSpan.FromSeconds(5f);
                directSoundOut = new DirectSoundOut();
                directSoundOut.Init(offsetSampleProvider);
                directSoundOut.Play();
            }
            return directSoundOut;
        }

        public void PlayPair(FirePlaySong a, FirePlaySong b, int sec)
        {
            string file_1 = GetPathFromString(a.PathName);
            string file_2 = GetPathFromString(b.PathName);



            if ((System.IO.Path.GetExtension(file_1) == ".mp3" || System.IO.Path.GetExtension(file_1) == ".wav") && (System.IO.Path.GetExtension(file_1) == ".mp3" || System.IO.Path.GetExtension(file_1) == ".wav"))
            {
                var audio_1 = new AudioFileReader(file_1);
                var audio_2 = new AudioFileReader(file_2);

                WaveOutEvent ds_a = PlayFromTheEnd(audio_1, a.EndCue ,sec);
                WaveOutEvent ds_b = PlayFromBegining(audio_2, sec);
                ds_a.Play();
                ds_a.PlaybackStopped += (sender, args) =>
                {
                    ds_b.Play();
                    ds_b.PlaybackStopped += (o, eventArgs) =>
                    {
                        ds_a.Dispose();
                        ds_b.Dispose();
                    };
                };

            }


            
        }


        public void PlayPair(FirePlaySong a, FirePlaySong b, int sec, int sec_2)
        {
            string file_1 = GetPathFromString(a.PathName);
            string file_2 = GetPathFromString(b.PathName);



            if ((System.IO.Path.GetExtension(file_1) == ".mp3" || System.IO.Path.GetExtension(file_1) == ".wav") && (System.IO.Path.GetExtension(file_1) == ".mp3" || System.IO.Path.GetExtension(file_1) == ".wav"))
            {
                var audio_1 = new AudioFileReader(file_1);
                var audio_2 = new AudioFileReader(file_2);

                WaveOutEvent ds_a = PlayFromTheEnd(audio_1, a.EndCue, sec);
                WaveOutEvent ds_b = PlayFromBegining(audio_2, sec_2);
                ds_a.Play();
                ds_a.PlaybackStopped += (sender, args) =>
                {
                    ds_b.Play();
                    ds_b.PlaybackStopped += (o, eventArgs) =>
                    {
                        ds_a.Dispose();
                        ds_b.Dispose();
                    };
                };

            }

        }
            


        /*
        public void PlayPair(FirePlaySong a, FirePlaySong b, int sec, int sec_2)
        {
            var ds_a = PlayFromTheEnd(a, sec);
            ds_a.PlaybackStopped += (sender, args) =>
            {
                var ds_b = PlayFromBegining(b, sec_2);
                ds_b.PlaybackStopped += (o, eventArgs) =>
                {
                    ds_a.Dispose();
                    ds_b.Dispose();
                };
            };
        }*/

        private void DisposeWave()
        {
            if (directSoundOut != null)
            {
                if (directSoundOut.PlaybackState == NAudio.Wave.PlaybackState.Playing) directSoundOut.Stop();
                directSoundOut.Dispose();
                directSoundOut = null;
            }

            if (wave != null)
            {
                wave.Dispose();
                wave = null;
            }

            if (mp3 != null)
            {
                mp3.Dispose();
                mp3 = null;
            }

            if (audio != null)
            {
                audio.Dispose();
                audio = null;
            }
        }



        public static string GetPathFromString(string s)
        {
            string path = String.Empty;
            string or_path = s;
            while (or_path != string.Empty)
            {
                var m = Regex.Match(or_path, @"(\\*[^\\]*)\\*(.*)");
                path += path == String.Empty ? m.Groups[1].Value : @"\" + m.Groups[1].Value;
                or_path = m.Groups[2].Value;
            }

            if (!System.IO.File.Exists(path))
            {
                if (!(path.EndsWith(".mp3") || path.EndsWith(".wav")))
                {
                    if (System.IO.File.Exists($"{path}.mp3"))
                    {
                        path = $"{path}.mp3";
                    }
                    else if (System.IO.File.Exists($"{path}.wav"))
                    {
                        path = $"{path}.wav";
                    }
                    else
                    {
                        throw new Exception($"The path of the song leads to nowhere. Path: {path}");
                    }
                }
                else
                {
                    throw new Exception($"Unknow error occured in path : {path}");
                }
            }
            
            return path;
        }

        public static void DisposeAll()
        {
            all.ForEach((x) => x.DisposeWave());
        }
    }

}
