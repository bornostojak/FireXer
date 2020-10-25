using System;
using NAudio.Wave;

namespace ExtraNoveListe
{
    public interface IPlayAudio : IDisposable
    {
        AudioFileReader AudioReader { get; }
        bool IsPaused { get; }
        bool IsPlaying { get; }
        bool IsStopped { get; }
        string Path { get; }

        event EventHandler PathChanged;
        event EventHandler PlayingStarted;
        event EventHandler PlayingStopped;
        event PlayAudio.UpdatePositionEventHandler UpdatePositionEvent;
        event EventHandler UpdaterEvent;

        void Dispose();
        long GetPositionInBytes();
        double GetPositionInSeconds();
        void Play();
        void PlayPause();
        void ReloadAudio(string path);
        void SetPosition(double value);
        void SetPositionFromBytes(long value);
        void Stop();
    }
}