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

using Keys = System.Windows.Forms.Keys;

namespace OpenVoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool running = true;

        public MainWindow()
        {
            InitializeComponent();

            // Start the voice engine
            VoiceEngine.init();

            // Simple Hello World trigger
            Trigger find = new Trigger("Sun");
            find.addSay("Sun Module fund");

            VoiceEngine.addTrigger(find);

            Trigger food = new Trigger("Food");
            food.addSay("food Module fund");

            VoiceEngine.addTrigger(food);

            Trigger iss = new Trigger("iss");
            iss.addSay("iss is over usa");

            VoiceEngine.addTrigger(iss);

            // Complex trigger
            Trigger complex = new Trigger("Say something funny", "You're funny!");
            complex.addKeyPress(Keys.X, 100, 3, false);
            complex.addWait(500);
            complex.addSay("Done!");

            VoiceEngine.addTrigger(complex);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Start listening
            VoiceEngine.startListening();

            // VU Meter
            VoiceEngine.sr.AudioLevelUpdated += (s, args) =>
            {
                VolumeProgressBar.Value = VoiceEngine.sr.AudioLevel;
            };

            // Output to RichTextBox
            VoiceEngine.CommandRecognized += OutputToTextBox;

            // Stopped listening
            VoiceEngine.StoppedListening += stoppedListening;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            VoiceEngine.say("Welcome to PDVoice!");

            (new TriggersWindow()).ShowDialog();
        }

        private void stoppedListening(object sender, EventArgs e)
        {
            StartStopButton.Content = "Start";
        }

        private void OutputToTextBox(object sender, CommandRecognizedEventArgs e)
        {
            DisplayTextBox.FontWeight = FontWeights.Bold;
            DisplayTextBox.AppendText("Command: ");
            DisplayTextBox.FontWeight = FontWeights.Regular;
            DisplayTextBox.AppendText("\"" + e.command + "\"");
            DisplayTextBox.Text += Environment.NewLine;
            DisplayTextBox.ScrollToEnd();
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (running)
            {
                running = false;
                ((Button)sender).Content = "Start";
                VoiceEngine.stopListening();
            }
            else
            {
                running = true;
                ((Button)sender).Content = "Stop";
                VoiceEngine.startListening();
            }
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Modal options dialog
            (new OptionsWindow()).ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
