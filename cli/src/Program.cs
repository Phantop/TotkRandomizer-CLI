namespace TotkRandomizer {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Please provide your TotK romfs path.");
                return;
            }
            textBox1.Text = args[0];
            Form1 theRando = new Form1();
            return;
        }
    }

    public partial class Form1 : Form {
        private void InitializeComponent() {
            button1_Click(null, null);
            backgroundWorker1_DoWork(null, null);
        }
        public static int getMax() { return maxProgress; }
    }
}
