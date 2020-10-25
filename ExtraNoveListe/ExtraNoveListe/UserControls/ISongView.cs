using System;
using System.Windows.Forms;
using System.Drawing;

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace ExtraNoveListe
{
    public interface ISongView : IComponent
    {
        bool Checked { get; }
        string GetName { get; set; }
        int Index { get; set; }
        IFirePlaySong Song { get; set; }
        string TimeInfo { get; set; }
        UserControl UserControl { get; set; }

        event EventHandler SongViewChecked;

        void ChangeSelectState();
        SongView GetClone();
        void Remove();
        void Select();
        void Unselect();
        void UUU(object s, PlayAudioUpdatePositionEventArgs e);
        CheckBox checkbox_selected { get; }
        void Hide();
        void Show();
        void Back_Color(Color c);
        Color Back_Color();
        IPlayAudio play { get; }
        Color DefaultColor { get; }

        Control Control { get; }
        Control Parent { get; }
    }
}