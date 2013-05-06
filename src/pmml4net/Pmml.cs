/*
pmml4net - easy lib to read and consume tree model in PMML file
Copyright (C) 2013  Damien Carol <damien.carol@gmail.com>

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Library General Public
License as published by the Free Software Foundation; either
version 2 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Library General Public License for more details.

You should have received a copy of the GNU Library General Public
License along with this library; if not, write to the
Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace pmml4net
{
	/// <summary>
	/// Description of Pmml.
	/// </summary>
	public class Pmml
	{
		private IList<TreeModel> treeModels;
		private IList<MiningModel> miningModels;
		
		/// <summary>
		/// TreeModel in pmml file.
		/// </summary>
		public IList<TreeModel> TreeModels 
		{ 
			get { return treeModels; }
		}
		
		/// <summary>
		/// Mining models in pmml file.
		/// </summary>
		public IList<MiningModel> MiningModels 
		{ 
			get { return miningModels; }
		}
		
		/// <summary>
		/// Load pmml file
		/// </summary>
		/// <param name="path">Path of the PMML file</param>
		public static Pmml loadModels(string path)
		{
			FileInfo info = new FileInfo(path);
			return loadModels(info);
		}
		
		/// <summary>
		/// Load pmml file
		/// </summary>
		/// <param name="info">Informations about the PMML file to read></param>
		public static Pmml loadModels(FileInfo info)
		{
			XmlDocument xml = new XmlDocument();
			xml.Load(info.FullName);
			return loadModels(xml);
		}
		
		/// <summary>
		/// Load pmml file
		/// </summary>
		/// <param name="xml">Xml PMML file to read></param>
		public static Pmml loadModels(XmlDocument xml)
		{
			Pmml pmml = new Pmml();
			pmml.treeModels = new List<TreeModel>();
			pmml.miningModels = new List<MiningModel>();
			
			foreach (XmlNode root in xml.ChildNodes)
			{
				if (root is XmlElement)
				{
					foreach (XmlNode itemTreeModel in root.ChildNodes)
					{
						if (itemTreeModel.Name.Equals("TreeModel"))
						{
							pmml.treeModels.Add(TreeModel.loadFromXmlNode(itemTreeModel));
						}
						else if (itemTreeModel.Name.Equals("MiningModel"))
						{
							pmml.miningModels.Add(MiningModel.loadFromXmlNode(itemTreeModel));
						}
					}
				}
			}
			
			return pmml;
		}
		
		/// <summary>
		/// Get TreeModel by it's name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public TreeModel getByName(string name)
		{
			foreach (TreeModel item in treeModels)
				if (name.Equals(item.ModelName))
					return item;
			
			return null;
		}
	}
}
