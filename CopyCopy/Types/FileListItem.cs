using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyCopy.Types
{
    public sealed class FileListItem
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public string DestinationDirectory { get; set; }
        public long   Size { get; set; }
        public string Extension { get; set; }


        public FileListItem(string source, string destination)
        {
            string filename      = source.Split('\\').Last();
            Destination          = $"{destination}\\{filename}";
            Extension            = filename.Contains(".") ? filename.Split('.').Last() : "";
            Source               = source;
            DestinationDirectory = destination;
            using (FileStream fs = new FileStream(source, FileMode.Open, FileAccess.Read))
                Size = fs.Length;
        }


        /// <summary>
        /// Explicit default constructor for an easy json serialize and deserialize manipulation
        /// </summary>
        public FileListItem() { }


        public static List<FileListItem> BuildFileListItems(string source, string destination)
        {
            List<FileListItem> items = new List<FileListItem>();
            if(Directory.Exists(source))
            {
                var subDirsAndFileREL = new List<string>();
                GetFilesRelativePathFromRoot(source, source, ref subDirsAndFileREL);

                foreach (var entry in subDirsAndFileREL)
                {
                    string file           = entry.Split('\\').Last();
                    string altFile        = entry != file ? "\\"+entry.Substring(0, entry.IndexOf(file) - 1) : "";
                    string path           = $"{source.Split('\\').Last()}{altFile}";
                    string sourceFilePtah = $"{source}\\{entry}";

                    items.Add(new FileListItem(sourceFilePtah, $"{destination}\\{path}"));
                }
            }
            else
                items.Add(new FileListItem(source, destination));

            return items;
        }
        


        public string GetDestinationDriveLetter()
        {
            if(!DestinationDirectory.StartsWith("\\"))
            {
                int firstAparitionOfSlash = DestinationDirectory.IndexOf('\\');
                return DestinationDirectory.Substring(0, firstAparitionOfSlash+1);
            }
            else
            {
                // \\10.32.3.1\h\dir\some.ext
                string[] slots = Destination.Split('\\');
                string addr    = slots[2];
                string drive   = slots[3];
                return $@"\\{addr}\{drive}";
            }
        }



        public string GetSourceDriveLetter()
        {
            if (!Source.StartsWith("\\"))
            {
                int firstAparitionOfSlash = Source.IndexOf('\\');
                return Source.Substring(0, firstAparitionOfSlash+1);
            }
            else
            {
                string[] slots = Source.Split('\\');
                string addr    = slots[2];
                string drive   = slots[3];
                return $@"\\{addr}\{drive}";
            }
        }



        public string GetDestinationDriveName()
        {
            string destLetter = GetDestinationDriveLetter();
            return Base.Win32_HddUtility.GetVolumeName(destLetter);
        }



        private static void GetFilesRelativePathFromRoot(string root, string entry, ref List<string> files)
        {
            if (Directory.Exists(entry))
            {
                string[] entries = Directory.GetFileSystemEntries(entry);
                foreach (var _entry in entries)
                    GetFilesRelativePathFromRoot(root, _entry, ref files);
            }
            else
                files.Add(entry.Replace(root + "\\", ""));
        }
    }
}
