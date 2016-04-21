using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevOAuth2App.Runtime
{
	/// <summary>
	/// Extensions assemblies with the ability to get the directories containing their corresponding .dll files
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class AssemblyDirectories
	{
		public static string GetDirectory(this Assembly assembly)
		{
			var codeBase = assembly.CodeBase;

			Uri codeBaseUri;

			if (Uri.TryCreate(codeBase, UriKind.Absolute, out codeBaseUri))
			{
				codeBase = codeBaseUri.AbsolutePath;
			}

			return Uri.UnescapeDataString(System.IO.Path.GetDirectoryName(codeBase) ?? String.Empty);
		}
	}
}
