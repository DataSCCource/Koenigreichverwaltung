using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DorfVerwaltungWPF.Models
{
    /// <summary>
    /// Modelclass of weapons
    /// </summary>
    public class Waffe
    {
        public static readonly string AXT = "Axt";
        public static readonly string SCHWERT = "Schwert";
        public static readonly string STREITHAMMER = "Streithammer";
        public static readonly string ZAUBERSTAB = "Zauberstab";

        public string Typ { get; set; }
        public int Magie { get; set; }
    }
}