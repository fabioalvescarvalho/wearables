using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Keys = System.Windows.Forms.Keys;

using InputManager;

namespace OpenVoice
{
    public interface Action
    {
        void run();

        string toString();
    }

    public class KeyPress : Action
    {
        private Keys kCode;
        private int delay;
        private int repetition;
        private bool wait;

        public KeyPress(Keys kCode, int delay, int repetition, bool wait)
        {
            this.kCode = kCode;
            this.delay = delay;
            this.repetition = repetition;
            this.wait = wait;
        }

        public void run()
        {
            Console.WriteLine("Running KeyPress");
            for (int i = repetition; i > 0; i--)
            {
                Console.WriteLine("Pressing keys");
                Keyboard.KeyPress(kCode, delay);
                if (wait) Thread.Sleep(200);
            }
        }

        public string toString()
        {
            return "KeyPress: " + kCode;
        }
    }

    public class Say : Action
    {
        private string text;

        public Say(string text) { this.text = text; }

        public void run()
        {
            Console.WriteLine("Running Say");
            VoiceEngine.say(text);
        }

        public string toString()
        {
            return "Say '" + text + "'";
        }
    }

    public class Wait : Action
    {
        private int time;

        public Wait(int time) { this.time = time; }

        public void run()
        {
            Console.WriteLine("Running Wait");
            Thread.Sleep(time);
        }

        public string toString()
        {
            return "Wait for " + time + "s";
        }
    }

    public class SilentOn : Action
    {
        public void run() { VoiceEngine.stealthModeOn(); }

        public string toString()
        {
            return "Silent mode on";
        }
    }

    public class SilentOff : Action
    {
        public void run() { VoiceEngine.stealthModeOff(); }

        public string toString()
        {
            return "Silent mode off";
        }
    }

    public class stopListening : Action
    {
        public void run() { VoiceEngine.stopListening(); }

        public string toString()
        {
            return "Stop Listening";
        }
    }

    public class Trigger
    {
        public List<string> inputs = new List<string>();
        public List<Action> actions = new List<Action>();

        // Properties
        public string inputsString
        {
            get
            {
                string result = "\"";
                foreach (string input in inputs)
                    result += input + "\"\n";
                return result.Trim();
            }
        }

        public string actionsString
        {
            get
            {
                string result = "";
                foreach (Action action in actions)
                    result += action.toString() + "\n";
                return result.Trim();
            }
        }

        public Trigger(params string[] inputs)
        {
            this.inputs.AddRange(inputs);
        }

        public void addKeyPress(Keys kCode, int delay = 100, int repetition = 1, bool wait = false)
        {
            KeyPress tmp = new KeyPress(kCode, delay, repetition, wait);
            actions.Add(tmp);
        }

        public void addSay(string text) { actions.Add(new Say(text)); }
        public void addWait(int time = 250) { actions.Add(new Wait(time)); }
        public void addSilentOn() { actions.Add(new SilentOn()); }
        public void addSilentOff() { actions.Add(new SilentOff()); }
        public void addStopListening() { actions.Add(new stopListening()); }
    }
}
