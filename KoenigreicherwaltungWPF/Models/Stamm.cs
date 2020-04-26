using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorfVerwaltungWPF.Models
{
    /// <summary>
    /// Model class of the tribes
    /// </summary>
    public class Stamm
    {
        public string Name { get; set; }
        public int ExistenzSeit { get; set; }
        public ObservableCollection<Zwerg> Zwerge { get; set; }

        public int Machtfaktor => Zwerge.Select(z => z.Machtfaktor).Sum(); 
        public int AnfuehrerSeit { get; set; }

        private Zwerg anfuehrer;
        public Zwerg Anfuehrer
        {
            get => anfuehrer;
            set
            {
                anfuehrer = value;
                AnfuehrerSeit = 0;

                foreach (var tmpDwarf in Zwerge)
                {
                    tmpDwarf.IstAnfuehrer = (tmpDwarf == value) ? "*" : "";
                }
            }
        }

        public Stamm()
        {
            Zwerge = new ObservableCollection<Zwerg>();
        }

    }
}
