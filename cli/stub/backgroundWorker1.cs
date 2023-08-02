namespace TotkRandomizer
{
    public static class backgroundWorker1
    {
        public static string Text;
        public static void RunWorkerAsync() { }
        public static void ReportProgress(int i) { Console.WriteLine(i + "/" + Form1.maxProgress); }
    }
}
