namespace TotkRandomizer {
    public class Application {
        static void Main(string[] args) {
            if (args.Length > 0) { textBox1.Text = args[0]; Form1 f = new Form1(); }
        }
    }
    public partial class Form1 : Form {
        private void InitializeComponent() {
            button1_Click(null, null); backgroundWorker1_DoWork(null, null); }
        public static ComboBox enemiesBox = new ComboBox();
        public static ComboBox chestsBox = new ComboBox();
        public static ComboBox weaponsBox = new ComboBox();
        public static ComboBox natureBox = new ComboBox();
        public static ComboBox paragliderPatternBox = new ComboBox();
        public static int getMax() { return maxProgress; } }
    public class ComboBox {
        public bool InvokeRequired = false;
        public int SelectedIndex = 1;
        public void Invoke(object i){} }
    public delegate void MethodInvoker();
    public static class backgroundWorker1 {
        public static void RunWorkerAsync(){}
        public static void ReportProgress(int i) { Console.WriteLine(i + "/" + Form1.getMax()); } }
    public class FolderBrowserDialog {
        public bool AutoUpgradeEnabled, UseDescriptionForTitle;
        public bool ShowDialog() { return true; }
        public string Description, SelectedPath; }
    public class Form{}
    public static class DialogResult { public static bool OK; }
    public static class button1 { public static bool Enabled; }
    public static class button2 { public static bool Enabled; }
    public static class tabControl1 { public static bool Enabled; }
    public static class progressBar1 { public static int Maximum, Value; }
    public static class textBox1 { public static string Text; }
    namespace SystemSounds {
        public static class Exclamation { public static void Play(){} }}
    namespace Properties { namespace Settings {
        public static class Default {
            public static string totkPath;
            public static void Save(){} }}}
}
