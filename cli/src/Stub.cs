namespace TotkRandomizer {
    public class Form {}
    public class Application {}
    public static class backgroundWorker1 {
        public static string Text;
        public static void RunWorkerAsync() {}
        public static void ReportProgress(int i) { Console.WriteLine(i + "/" + Form1.getMax()); }
    }
    public static class button1 { public static bool Enabled; }
    public static class button2 { public static bool Enabled; }
    public static class DialogResult { public static bool OK; }
    public static class progressBar1 {
        public static int Maximum;
        public static int Value;
    }
    public static class textBox1 {
        public static string Text;
    }
    public class FolderBrowserDialog {
        public bool AutoUpgradeEnabled;
        public bool ShowDialog() { return true; }
        public bool UseDescriptionForTitle;
        public string Description;
        public string SelectedPath;
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
    public class NativeLibraryManager {
        public static NativeLibraryManager
            RegisterAssembly(System.Reflection.Assembly a, out bool b)
            { b = false; return new NativeLibraryManager(); }
        public void Register(CsRestbl.RestblLibrary a, out bool b) { b = false; }
    }
}
