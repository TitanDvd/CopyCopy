namespace CcCore.Usb
{
    using System;
    using System.Text;
    using CcCore.Base.Types;


    /// <summary>
    /// Represents the displayable information for a single USB disk.
    /// </summary>

    public class UsbDisk
	{
		private const int KB = 1024;
		private const int MB = KB * 1000;
		private const int GB = MB * 1000;

        /// <summary>
		/// Initialize a new instance with the given values.
		/// </summary>
		/// <param name="letter">The Windows drive letter assigned to this device.</param>
        internal UsbDisk(string letter) { Letter = letter; }


        /// <summary>
        /// Gets the available free space on the disk, specified in bytes.
        /// </summary>
        public ulong FreeSpace { get; set; }
        

        /// <summary>
        /// Gets the name of this disk.  This is the Windows identifier, drive letter.
        /// </summary>
        public string Letter { get; set; }


        /// <summary>
        /// Gets the total size of the disk, specified in bytes.
        /// </summary>
        public ulong Capacity { get; set; }

        
        /// <summary>
        /// Volume serial number. Inmutable
        /// </summary>
        public string VolumeSerial { get; set; }


        /// <summary>
        /// Get the volume name of this disk.  This is the friently name ("Stick").
        /// </summary>
        /// <remarks>
        /// When this class is used to identify a removed USB device, the Volume
        /// property is set to String.Empty.
        /// </remarks>
        public string Name { get; set; }


        /// <summary>
        /// Extended custom tags for the volume
        /// </summary>
        public string Tag { get; set; }
	}
}
