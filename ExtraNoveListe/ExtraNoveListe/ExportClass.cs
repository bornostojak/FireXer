using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraNoveListe
{
    public class ExportClass
    {
        private FirePlayList EXPORT_LIST = new FirePlayList("temp");
        public FirePlayList ExportList { get { return EXPORT_LIST; } }

        private FirePlayList EXPORT_LIST_DEFAULTED = new FirePlayList("temp");
        public FirePlayList ExportListDefaulted { get { return EXPORT_LIST_DEFAULTED; } }

        public string Location { get; set; }

        public ExportClass(FirePlayList fp_list, string path)
        {
            this.Location = path;
            Setup(fp_list);

            ExportWindow win = new ExportWindow(this);
        }

        public ExportClass(FirePlayList fp_list)
        {
            EXPORT_LIST = fp_list.Clone();
            EXPORT_LIST = fp_list.Clone();
            Setup(fp_list);

            ExportWindow win;
            Task.Run(() => win = new ExportWindow(this));
            
        }



        public async void SetupParallelAsync(FirePlayList l)
        {
            List<Task<FirePlayList>> Tasks = new List<Task<FirePlayList>>();
            Tasks.Add(Task.Run(() => this.EXPORT_LIST = l.Clone()));
            Tasks.Add(Task.Run(() =>  this.EXPORT_LIST_DEFAULTED = l.Clone()));

            await Task.WhenAll(Tasks);
            await Task.Run(() => EXPORT_LIST_DEFAULTED.DefaultTheList());
        }

        public async void Setup(FirePlayList l)
        {
            this.EXPORT_LIST = l.Clone();
            this.EXPORT_LIST_DEFAULTED = l.Clone();
            EXPORT_LIST_DEFAULTED.DefaultTheList();
        }
    }
}
