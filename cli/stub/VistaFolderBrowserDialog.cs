namespace TotkRandomizer
{
    public class VistaFolderBrowserDialog
    {
        public string Description;
        public string SelectedPath;
        public bool UseDescriptionForTitle;
        public bool ShowDialog() { return true; }
    }
    public static class DialogResult {
        public static bool Cancel;
    }
}
