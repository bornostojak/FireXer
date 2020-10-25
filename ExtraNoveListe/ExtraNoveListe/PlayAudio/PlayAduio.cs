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
    public class PlayAudio : IPlayAudio
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////    VARS    //////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        private string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            private set
            {
                _path = value;
                OnPathChange();
            }
        }
        
        private NAudio.Wave.AudioFileReader audio = null;
        public NAudio.Wave.AudioFileReader AudioReader => audio;

        public bool IsPlaying => output.PlaybackState == PlaybackState.Playing ? true : false;
        public bool IsPaused => output.PlaybackState == PlaybackState.Paused ? true : false;
        public bool IsStopped => output.PlaybackState == PlaybackState.Stopped ? true : false;

        private NAudio.Wave.IWavePlayer output = new WaveOutEvent();
        //public virtual NAudio.Wave.IWavePlayer Output => output;

        
        //public Progress<>
        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    STATIC VARS    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////
        private static List<IPlayAudio> _all = new List<IPlayAudio>();
        private static List<IPlayAudio> _playing = new List<IPlayAudio>();

        System.Timers.Timer timer = new System.Timers.Timer();
        

        private static NAudio.Wave.IWavePlayer static_output;
        /////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////    CONSTRUCTOR    ///////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////
        public int ii = 0;
        public PlayAudio()
        {
            ii = _all.Count;
            _all.Add(this);
            Timer_Setup();
            output.PlaybackStopped += (s, o) => Stopped();
        }
        public PlayAudio(string path)
        {
            ii = _all.Count;
            Path = GetPathFromString(path);
            LoadAudio();
            _all.Add(this);
            Timer_Setup();
            output.PlaybackStopped += (s, o) => Stopped();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      METHOD      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////

        public void Play()
        {
            SetPosition(0);

            timer.Start();
            output.Play();
            UpdatePlaying();
            OnPlayingStarted();
        }
        public void PlayPause()
        {
            if (output != null && output.PlaybackState == PlaybackState.Playing)
            {
                timer.Stop();
                output.Pause(); 
            }
            else if (output != null && (output.PlaybackState == PlaybackState.Paused || output.PlaybackState == PlaybackState.Stopped))
            {
                timer.Start();
                var previousPlaybackState = output.PlaybackState;
                output.Play();
                if (previousPlaybackState == PlaybackState.Stopped)
                {
                    OnPlayingStarted();
                }
            }
            UpdatePlaying();
        }
        public void Stop()
        {
            timer.Stop();
            output.Stop();
            SetPosition(0);
            UpdatePlaying();
        }

        public void SetPosition(double value)
        {
            if (audio != null)
            {
                audio.CurrentTime = TimeSpan.FromSeconds(value);
            }
        }
        public void SetPositionFromBytes(long value)
        {
            if (audio != null)
            {
                audio.Position = value;
            }
        }

        public double GetPositionInSeconds()
        {
            if (audio != null)
            {
                return audio.CurrentTime.TotalSeconds;
            }
            else return 0;

        }
        public long GetPositionInBytes()
        {
            if (audio != null)
            {
                return audio.Position;
            }
            return 0;
        }


        protected virtual void LoadAudio()
        {
            if ((Path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase)) || (Path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase)))
            {
                if (audio != null)
                {
                    audio.Dispose();
                }
                audio = new NAudio.Wave.AudioFileReader(Path);
                output.Init(audio);

            }
            else
            {
                throw new FileTypeException("File type must be .mp3 or .wav");
            }


        }
        public virtual void ReloadAudio(string path)
        {
            Path = GetPathFromString(path);
            LoadAudio();
            //if ((Path.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase)) || (Path.EndsWith(".wav", StringComparison.OrdinalIgnoreCase)))
            //{
            //    if (audio != null)
            //    {
            //        audio.Dispose();
            //    }
            //    audio = new NAudio.Wave.AudioFileReader(Path);
            //    output.Init(audio);

            //}
            //else
            //{
            //    throw new FileTypeException("File type must be .mp3 or .wav");
            //}
        }

        protected void UpdatePlaying()
        {
            if (output.PlaybackState == PlaybackState.Playing)
            {
                if (_playing.Contains(this))
                {
                }

                else
                {
                    _playing.Add(this);
                }
            }

            else if (output.PlaybackState == PlaybackState.Stopped)
            {
                if (_playing.Contains(this))
                {
                    _playing.Remove(this);
                }

                else
                {
                }
            }
        }

        protected void Stopped()
        {
            SetPosition(0);
            OnPlayingStopped();
        }


        protected void Timer_Setup()
        {
            timer.AutoReset = true;
            timer.Interval = 25;
            timer.Elapsed += (s, e) =>
            {
                OnUpdatePositionEvent();
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool boolean)
        {
            if (boolean)
            {
                this.AudioReader.Dispose();
                audio = null;
                Path = null;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      STATIC      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public static string GetPathFromString(string s)
        {
            string path = String.Empty;
            string or_path = s;
            while (or_path != string.Empty)
            {
                Match m = Regex.Match(or_path, @"(\\*[^\\]*)\\*(.*)");
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

        public static void StopPlayingAll()
        {
            if (_playing.Count > 0)
            {
                _playing.ForEach((x) => 
                {
                    //x.output.Stop();
                    //x.audio.Position = 0;

                    x.Stop();
                    x.SetPosition(0);
                });
                _playing = new List<IPlayAudio>();
            }
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////      EVENTS      ///////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        public event EventHandler UpdaterEvent;
        protected virtual void OnUpdaterEvent()
        {
            UpdaterEvent?.Invoke(this, new EventArgs());
        }

        public delegate void UpdatePositionEventHandler(object sender, PlayAudioUpdatePositionEventArgs e);
        public event UpdatePositionEventHandler UpdatePositionEvent;
        protected virtual void OnUpdatePositionEvent()
        {
            UpdatePositionEvent?.Invoke(this, new PlayAudioUpdatePositionEventArgs(this.AudioReader));
        }

        public event EventHandler PlayingStarted;
        protected virtual void OnPlayingStarted()
        {
            PlayingStarted?.Invoke(this, new PlayAudioUpdatePositionEventArgs(this.AudioReader));
        }

        public event EventHandler PlayingStopped;
        protected virtual void OnPlayingStopped()
        {
            PlayingStopped?.Invoke(this, new PlayAudioUpdatePositionEventArgs(this.AudioReader));
        }

        public event EventHandler PathChanged;
        private void OnPathChange()
        {
            PathChanged?.Invoke(this, new EventArgs());
        }
        
    }
    
    
}
