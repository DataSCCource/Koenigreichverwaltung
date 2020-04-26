using DorfVerwaltungWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DorfVerwaltungWPF
{
    /// <summary>
    /// Base management class for the tax calculations
    /// </summary>
    public class Verwaltung
    {
        public double Steuersatz { get; set; }
        public ObservableCollection<Stamm> Staemme { get; }
        public double Steuern => Staemme.Select(s => s.Machtfaktor).Sum() * Steuersatz;

        public Verwaltung()
        {
            Staemme = new ObservableCollection<Stamm>();
            Steuersatz =  2.125;
        }

        /// <summary>
        /// Get taxes for an individual Dwarf
        /// </summary>
        /// <param name="zwerg">Dwarf</param>
        /// <returns></returns>
        public double GetSteuernFuerZwerg(Zwerg zwerg) => zwerg.Machtfaktor * Steuersatz;


        /// <summary>
        /// To refresh the observableCollections
        /// I have not figured out, how to send a PropertyChanged event, when an Attribute of an Element within a List is changed.
        /// Rebuilding does the trick, but is of course just a dirty quickfix...
        /// </summary>
        public void Refresh()
        {
            var tmpTribes = Staemme.ToList();
            Staemme.Clear();

            foreach (var tmpTribe in tmpTribes)
            {
                Staemme.Add(tmpTribe);
                var tmpDwarfs = tmpTribe.Zwerge.ToList();
                tmpTribe.Zwerge.Clear();

                foreach (var tmpDwarf in tmpDwarfs)
                {
                    tmpTribe.Zwerge.Add(tmpDwarf);
                    var tmpWeapons = tmpDwarf.Waffen.ToList();
                    tmpDwarf.Waffen.Clear();

                    foreach(var tmpWeapon in tmpWeapons)
                    {
                        tmpDwarf.Waffen.Add(tmpWeapon);
                    }
                }
            }
        }
    }
}
