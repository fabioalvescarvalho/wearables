using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace OpenVoice
{
    public class CommandRecognizedEventArgs : EventArgs
    {
        public string command { get; set; }

        public CommandRecognizedEventArgs(string text) { this.command = text; }
    }

    static class VoiceEngine
    {
        private static Thread sayThread;

        private static List<Trigger> Triggers = new List<Trigger>();

        static public SpeechRecognitionEngine sr;
        static public SpeechSynthesizer ss;

        static public event EventHandler<CommandRecognizedEventArgs> CommandRecognized;
        static public event EventHandler StoppedListening;

        public static void init()
        {
            // Create new SpeechRecognizer/SpeechSynthesizer instances
            sr = new SpeechRecognitionEngine();
            ss = new SpeechSynthesizer();

            // Recognition
            sr = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            sr.SetInputToDefaultAudioDevice();

            // Event listener
            sr.SpeechRecognized += speechRecognized;
        }

        public static void startListening()
        {
            // Reload grammar
            VoiceEngine.loadGrammar(Triggers);

            sr.RecognizeAsync(RecognizeMode.Multiple);
        }

        public static void stopListening()
        {
            sr.RecognizeAsyncStop();
            StoppedListening(null, EventArgs.Empty);
        }

        public static void stealthModeOn()
        {
            sr.UnloadAllGrammars();
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append("Listen up");

            sr.LoadGrammar(new Grammar(grammarBuilder));
        }

        public static void stealthModeOff()
        {
            sr.UnloadAllGrammars();
            loadGrammar(Triggers);
        }

        public static void addTrigger(Trigger t)
        {
            Triggers.Add(t);
        }

        public static void loadGrammar(List<Trigger> triggers)
        {
            Choices commands = new Choices();

            triggers.ForEach(x => x.inputs.ForEach(y => { commands.Add(y); }));

            Console.WriteLine("Loaded " + triggers.Count + " commands");

            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(commands);

            sr.LoadGrammar(new Grammar(grammarBuilder));
        }

        public static void speechRecognized(object sender, SpeechRecognizedEventArgs args)
        {
            float confidence = 0, confidenceCount = 0;
            string line = "";

            foreach (RecognizedWordUnit word in args.Result.Words)
            {
                Console.WriteLine(word.Text + " " + word.Confidence);
                confidence += word.Confidence; confidenceCount++;
                if (word.Confidence > 0.85f)
                    line += word.Text + " ";
            }

            confidence = confidence / confidenceCount;
            line = line.Trim();

            Trigger command = Triggers.FirstOrDefault(x => x.inputs.Contains(line));

            if (command == null)
            {
                VoiceEngine.say("Sorry?");
            }
            else
            {
                foreach (Action ac in command.actions)
                {
                    ac.run();
                }
            }

            // Trigger event
            CommandRecognized(null, new CommandRecognizedEventArgs(line + " (" + confidence + ")"));
        }

        public static void exit()
        {
            sayThread.Join();
            sr.Dispose();
            ss.Dispose();
        }

        public static string currentVoice()
        {
            return ss.Voice.Name;
        }

        public static int setVoice(string name)
        {
            try
            {
                ss.SelectVoice(name);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static void sayFunc(string input)
        {
            ss.Speak(input);
        }

        public static void say(string input)
        {
            sayThread = new Thread(() => sayFunc(input));
            sayThread.Start();
        }

        public static List<Trigger> getTriggers()
        {
            return Triggers;
        }
    }
}
