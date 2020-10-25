using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{
    public class FileTypeException : Exception
    {
        public FileTypeException() : base() { }
        public FileTypeException(string message) : base(message) { }
    }
}
