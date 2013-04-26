/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 11:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
		
		/// <summary>
		/// TreeModel in pmml file.
		/// </summary>
		public IList<TreeModel> TreeModels 
		{ 
			get { return treeModels; }
		}
		
		/// <summary>
		/// Load pmml xml file
		/// </summary>
		/// <param name="path">Path of the PMML file</param>
		public static Pmml loadModels(string path)
		{
			FileInfo info = new FileInfo(path);
			if (!info.Exists)
				throw new PmmlException();
			
			return loadModels(info);
		}
		
		/// <summary>
		/// Load pmml xml file
		/// </summary>
		/// <param name="info">Informations about the PMML file to read></param>
		public static Pmml loadModels(FileInfo info)
		{
			XmlDocument xml = new XmlDocument();
			xml.Load(info.FullName);
			
			return loadModels(xml);
		}
		
		/// <summary>
		/// Load pmml xml file
		/// </summary>
		/// <param name="xml">Xml PMML file to read></param>
		public static Pmml loadModels(XmlDocument xml)
		{
			Pmml pmml = new Pmml();
			pmml.treeModels = new List<TreeModel>();
			
			foreach (XmlNode root in xml.ChildNodes)
			{
				if (root is XmlElement)
				{
					foreach (XmlNode itemTreeModel in root.ChildNodes)
					{
						if (itemTreeModel.Name.Equals("TreeModel"))
						{
							TreeModel tree = TreeModel.loadFromXmlNode(itemTreeModel);
							
							pmml.treeModels.Add(tree);
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
