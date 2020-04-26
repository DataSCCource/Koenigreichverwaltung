using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DorfVerwaltungWPF.Models
{
    /// <summary>
    /// Model class of dwarfs
    /// </summary>
    public class Zwerg
    {
        public string Name { get; set; }
        public int Alter { get; set; }
        public int Machtfaktor => Waffen.Select(w => w.Magie).Sum();
        public string IstAnfuehrer { get; set; }
        public ObservableCollection<Waffe> Waffen { get; }

        public Zwerg()
        {
            Waffen = new ObservableCollection<Waffe>();
        }
    }
}