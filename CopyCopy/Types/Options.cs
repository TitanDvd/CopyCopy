namespace CopyCopy.Types
{
    public class Options
    {
        public bool   SoundWhenCopyFinish         { get; set; }
        public bool   AskWhenWindowIsForceClose   { get; set; }
        public bool   CloseWindowWhenCopyFinish   { get; set; }
        public bool   ShowProgressOnTitleBar      { get; set; }
        public bool   ShowUnitsRootInTitleBar     { get; set; }
        public bool   ShowIconPerCopyInTaskBar    { get; set; }
        public string SoundPath                   { get; set; }
    }
}
