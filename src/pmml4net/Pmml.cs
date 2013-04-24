/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 11:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace pmml4net
{
	/// <summary>
	/// Description of Pmml.
	/// </summary>
	public class Pmml
	{
		/// <summary>
		/// Load pmml xml file
		/// </summary>
		/// <param name="path"></param>
		public static Pmml loadModels(string path)
		{
			FileInfo info = new FileInfo(path);
			
			return new Pmml();
		}
	}
}
