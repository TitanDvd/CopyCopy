//************************************************************************************************
// Copyright © 2010 Steven M. Cohn. All Rights Reserved.
//
//************************************************************************************************

namespace CcCore.Usb
{
    using CcCore.Base;
    using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;


	/// <summary>
	/// Maintains a collection of USB disk objects.
	/// </summary>

	public class UsbDiskCollection : ObservableCollection<DiskInfo>
	{

		/// <summary>
		/// Determines if the named disk is contained in this collection.
		/// </summary>
		/// <param name="name">The Windows name, or drive letter, of the disk to remove.</param>
		/// <returns>
		/// <b>True</b> if the item is found; otherwise <b>false</b>.
		/// </returns>

		public bool Contains (string name)
		{
			return this.AsQueryable().Any(d => d.Name == name) == true;
		}


		/// <summary>
		/// Remove the named disk from the collection.
		/// </summary>
		/// <param name="name">The Windows name, or drive letter, of the disk to remove.</param>
		/// <returns>
		/// <b>True</b> if the item is removed; otherwise <b>false</b>.
		/// </returns>

		public bool Remove (string letter)
		{
			DiskInfo disk = 
				(this.AsQueryable()
				.Where(d => d.Letter == letter)
				.Select(d => d)).FirstOrDefault();

			if (disk != null)
			{
				return this.Remove(disk);
			}

			return false;
		}
	}
}
