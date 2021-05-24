using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VeiebryggeApplication
{
    /// <summary>
    /// Interaction logic for helpPage.xaml
    /// </summary>
    public partial class helpPage : Page
    {
        public helpPage()
        {
            InitializeComponent();
        }

        private void Button_Click_runTest(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "Bruksveiledning for siden kjør test\n\nDenne siden brukes til å initialere tester på plattformen.\n\nDiagnostisk test -Diagnostisk test er en funksjonalitet som er implementert for sikkerhet og forsikring av god ytelse.Når du klikker på denne knappen, kjøres en diagnostisk test på system komponentene, og en kontroll startes for å sikre at sensorer og annen hardware fungerer som forventet, og om det er noen feil tilstede\n\nKjør test - For å kjøre en test må tekstfeltet med registreringsnummer fylles ut.Hvis brukeren skriver et registreringsnummer som eksisterer i databasen, vil feltet fylles ut automatisk med kjøretøyet som har det registreringsnummeret.Om brukeren skriver inn ett registreringsnummer som ikke eksisterer fra før, dvs.at det er et nytt kjøretøy som skal testes, vil det åpnes et nytt vindu hvor brukeren kan fylle inn informasjon for dette kjøretøyet. Når dette er gjort, må brukeren også fyll inn i utstyrsfeltet om kjøretøyet er rustet med ekstra utstyr.Når dette er gjort, kan testen startes.";
        }

        private void Button_Click_prevTest(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "Brukerveiledning for siden tidligere tester\n\nPå denne siden kommer alle de tidligere kjørte testene opp. \n\nValg av test - For å endre valgt test trykker du på en av linjene I databasen slik at den blir til en lysere farge.\n\nGenerer rapport -Ved å trykke på denne knappen blir den samme rapporten som kommer opp etter kjørt test, generert på nytt for den valgte testen.Denne kan da leses, lagres og / eller skrives ut.Rapporten vil åpne seg i en eksterne standard pdf - leser.Vi anbefaler å bruke Microsoft Edge til dette.\n\nExporter til Excel - Denne knappen eksporterer den valgte testen til en Excel fil.Brukeren vil først bli spurt om hvor filen skal lagres og hva den skal lagres som.Dersom et nytt navn ikke blir gitt, vil filen bli lagret som test_(ID).En lagret fil vil være lesbar i Microsoft Excel eller andre programmer som forstår xlsx formatet.Åpner man filen vil man kunne se all dataen for den kjørte testen.Disse filene kan dermed lagres lokalt og importeres inn til programmet senere som blir beskrevet under.\n\nImporter fra excel - Brukeren vil bli møtt med et vindu der han / hun kan velge en xlsx fil fra en valgfri lokasjon på datamaskinen.Hvis den valgte filen er en xlsx fil som allerede inneholder en test, så vil denne testen bli importert inn til databasen dersom den ikke er der fra før av.\n\nSlett test - Denne knappen sletter den valgte testen fra databasen.Dersom du ikke eksporterer testen først vil testen bli slettet permanent og det er ikke lenger mulig å se testen eller generere en rapport fra den.\n\nSøk - Bruker kan søke etter hvilken som helst nøkkeldata i tekstfeltet, og raden som inneholder den nevnte nøkkeldataen vil dukke opp";
        }

        private void Button_Click_vehicles(object sender, RoutedEventArgs e)
        {
            txtContent.Text = "Bruksveiledning for siden kjøretøy\n\nDenne siden viser alle kjøretøy som er registrert i databasen og tillater brukeren å legge til nye kjøretøy\n\nSett inn -Ved å trykke på denne knappen, vil brukeren kunne legge til et nytt kjøretøy i datbasen. Det er viktig at dataen i tekstfeltene er på riktig format, ellers vil det forekomme feilmeldinger.\n\nEndre - For å endre opplysninger på et kjøretøy, må brukeren først velge kjøretøyet som skal endres.Deretter kan hvilken som helst detalj ved kjøretøyet endres.Etter endringen er gjort, og knappen er trykket på, vil verdiene i databasen oppdateres og overskrive de eksisterende opplysningene\n\nSlett - For å slette et kjøretøy, må brukeren først velge kjøretøyet som skal slettes.Ved sletting, vil dette kjøretøyet bli fjernet fra databasen permanent\n\nSøk - Brukeren kan søke etter nøkkeldata i søkefeltet og den samsvarende dataen vil dukke opp.For å gjøre søket enkelt, er det også en rullegardinmeny med filter der søkeordet kan begrenses til data i testen, for eksempel med registreringsnummer";
        }
    }
}
