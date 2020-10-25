using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{

    public enum ChangeMade { Removed, Added, Defauld, None };


    public class SongChangeEventArgs : EventArgs
    {
        private ChangeMade _change;
        public ChangeMade Data { get { return _change; } }

        public SongChangeEventArgs(ChangeMade change)
        {
            _change = change;
        }

            public SongChangeEventArgs()
        {
            _change = ChangeMade.Defauld;
        }
    }
}
