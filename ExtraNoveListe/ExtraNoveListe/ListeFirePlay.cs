using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{
    class ListeFirePlay
    {
        public class Lista
        {
            enum Upitno { Lista, Folder }; 
            
            private Lista(string path, Upitno x)
            {

            }
            static Lista CreateListaFromLista(string path)
            {
                return new ExtraNoveListe.ListeFirePlay.Lista(path, Upitno.Lista);
            }
            static Lista CreateListaFromFolder(string path)
            {
                return new ExtraNoveListe.ListeFirePlay.Lista(path, Upitno.Folder);
            }
        }
    }
}
