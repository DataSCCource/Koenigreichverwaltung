using DorfVerwaltungWPF.Models;
using System.Windows;
using System.Windows.Input;


namespace DorfVerwaltungWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Verwaltung verwaltung;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            verwaltung = new Verwaltung();

            Zwerg gimli = new Zwerg { Name = "Gimli", Alter = 140 };
            gimli.Waffen.Add(new Waffe { Typ = Waffe.AXT, Magie = 12 });
            gimli.Waffen.Add(new Waffe { Typ = Waffe.SCHWERT, Magie = 15 });

            Zwerg zwingli = new Zwerg { Name = "Zwingli", Alter = 70 };
            zwingli.Waffen.Add(new Waffe { Typ = Waffe.ZAUBERSTAB, Magie = 45 });
            zwingli.Waffen.Add(new Waffe { Typ = Waffe.STREITHAMMER, Magie = 15 });

            Stamm altobarden = new Stamm { Name = "Altobarden", ExistenzSeit = 1247, AnfuehrerSeit = 25 };
            altobarden.Zwerge.Add(gimli);
            altobarden.Zwerge.Add(zwingli);
            altobarden.Anfuehrer = gimli;


            Zwerg gumli = new Zwerg { Name = "Gumli", Alter = 163 };
            gumli.Waffen.Add(new Waffe { Typ = Waffe.AXT, Magie = 17 });

            Stamm elbenknechte = new Stamm { Name = "Elbenknechte", ExistenzSeit = 1023 };
            elbenknechte.Zwerge.Add(gumli);
            elbenknechte.Anfuehrer = gumli;


            verwaltung.Staemme.Add(altobarden);
            verwaltung.Staemme.Add(elbenknechte);

            tribeListView.ItemsSource = verwaltung.Staemme;
            taxBlock.Text = verwaltung.Steuern.ToString();
            taxScoreBox.Text = verwaltung.Steuersatz.ToString();
        }


        // Show dwarf-list of the selected tribe and switch on/off buttons
        private void TribeListView_Selected(object sender, RoutedEventArgs e)
        {
            deleteTribeBTN.IsEnabled = tribeListView.SelectedItem != null;
            addDwarfBTN.IsEnabled = tribeListView.SelectedItem != null;
            makeLeaderBTN.IsEnabled = dwarfListView.SelectedItem != null;
            deleteDwarfBTN.IsEnabled = tribeListView.SelectedItem != null;
            dwarfListView.ItemsSource = ((Stamm)tribeListView.SelectedItem)?.Zwerge;
        }

        // Add a tribe
        private void AddTribeBTN_Click(object sender, RoutedEventArgs e)
        {
            verwaltung.Staemme.Add(new Stamm { Name = "Neu..." });
            tribeListView.SelectedIndex = tribeListView.Items.Count - 1;
        }

        // Delete the delected tribe
        private void DeleteTribe_Clicked(object sender, RoutedEventArgs e)
        {
            verwaltung.Staemme.Remove((Stamm)tribeListView.SelectedItem);
            tribeListView.SelectedIndex = -1;
            dwarfListView.SelectedIndex = -1;
            weaponListView.SelectedIndex = -1;
        }


        // Show weapon-list of the selected dwarf and switch on/off buttons
        private void DwarfListView_Selected(object sender, MouseButtonEventArgs e)
        {
            deleteDwarfBTN.IsEnabled = dwarfListView.SelectedItem != null;
            addWeaponBTN.IsEnabled = dwarfListView.SelectedItem != null;
            deleteWeaponBTN.IsEnabled = dwarfListView.SelectedItem != null;
            makeLeaderBTN.IsEnabled = dwarfListView.SelectedItem != null;
            weaponListView.ItemsSource = ((Zwerg)dwarfListView.SelectedItem)?.Waffen;
        }

        // Add a dwarf
        private void AddDwarfBTN_Click(object sender, RoutedEventArgs e)
        {
            ((Stamm)tribeListView.SelectedItem).Zwerge.Add(new Zwerg { Name = "Neu..." });
            dwarfListView.SelectedIndex = dwarfListView.Items.Count - 1;
        }

        //Delete the selected dwarf
        private void DeleteDwarf_Clicked(object sender, RoutedEventArgs e)
        {
            ((Stamm)tribeListView.SelectedItem).Zwerge.Remove((Zwerg)dwarfListView.SelectedItem);
            dwarfListView.SelectedIndex = -1;
            weaponListView.SelectedIndex = -1;
        }


        // When a weapon is de/selected dis/allow deleting it
        private void WeaponListView_Selected(object sender, MouseButtonEventArgs e)
        {
            deleteWeaponBTN.IsEnabled = weaponListView.SelectedItem != null;
        }

        // Add a weapon
        private void AddWeaponBTN_Click(object sender, RoutedEventArgs e)
        {
            ((Zwerg)dwarfListView.SelectedItem).Waffen.Add(new Waffe { Typ = "Neu..." });
            weaponListView.SelectedIndex = weaponListView.Items.Count - 1;
        }

        // Delete the selected weapon
        private void DeleteWeapon_Clicked(object sender, RoutedEventArgs e)
        {
            ((Zwerg)dwarfListView.SelectedItem).Waffen.Remove((Waffe)weaponListView.SelectedItem);
            weaponListView.SelectedIndex = -1;
        }

        // Make the selected dwarf the new tribe leader
        private void MakeDwarfLeaderBTN_Click(object sender, RoutedEventArgs e)
        {
            foreach (var tmpDwarf in dwarfListView.Items)
            {
                if(tmpDwarf == dwarfListView.SelectedItem)
                {
                    ((Stamm)tribeListView.SelectedItem).Anfuehrer = (Zwerg)tmpDwarf;
                    ((Zwerg)tmpDwarf).IstAnfuehrer = "*";

                } else
                {
                    ((Zwerg)tmpDwarf).IstAnfuehrer = "";
                }
            }
            Refresh();
        }

        // Save changes and refresh view
        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        // Update taxscore and calculated tax values
        private void UpdateTax_Clicked(object sender, RoutedEventArgs e)
        {
            verwaltung.Steuersatz = double.Parse(taxScoreBox.Text);
            taxBlock.Text = verwaltung.Steuern.ToString();
        }

        // Refresh view
        private void Refresh()
        {
            var tribeIndex = tribeListView.SelectedIndex;
            var dwarfIndex = dwarfListView.SelectedIndex;
            var weaponIndex = weaponListView.SelectedIndex;

            verwaltung.Refresh();
            UpdateTax_Clicked(null, null);

            tribeListView.SelectedIndex =  tribeIndex;
            dwarfListView.SelectedIndex =  dwarfIndex;
            weaponListView.SelectedIndex = weaponIndex;
        }
    }
}
