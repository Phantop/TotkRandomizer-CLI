namespace TotkRandomizer {
    public class Form {}
    public static class backgroundWorker1 {
        public static string Text;
        public static void RunWorkerAsync() {}
        public static void ReportProgress(int i) { Console.WriteLine(i + "/" + Form1.maxProgress); }
    }
    public static class button1 { public static bool Enabled; }
    public static class button2 { public static bool Enabled; }
    public static class DialogResult { public static bool Cancel; }
    public static class progressBar1 {
        public static int Maximum;
        public static int Value;
    }
    public static class textBox1 {
        public static string Text;
        public static void setText(string set) { Text = set; }
    }
    public class VistaFolderBrowserDialog {
        public string Description;
        public string SelectedPath;
        public bool UseDescriptionForTitle;
        public bool ShowDialog() { return true; }
    }
    namespace Properties {
        namespace Settings {
            public static class Default {
                public static string totkPath;
                public static void Save() {}
            }
        }
    }
    namespace SystemSounds {
        public static class Exclamation {
            public static void Play() {}
        }
    }
}
