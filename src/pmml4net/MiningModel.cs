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
using System.Xml;

namespace pmml4net
{
	/// <summary>
	/// Description of MiningModel.
	/// </summary>
	public class MiningModel : ModelElement
	{
		private Segmentation segmentation;
		
		/// <summary>
		/// A <see cref="Segmentation" /> element contains several <see cref="Segment" >Segments</see> and a model combination method.
		/// Each Segment includes a PREDICATE element specifying the conditions under which that segment is to be used.
		/// </summary>
		public Segmentation Segmentation { get { return segmentation; } set { segmentation = value; } }
		
		/// <summary>
		/// Load MiningModel node from XmlElement of PMML file
		/// </summary>
		/// <param name="node">Xml PMML file to read</param>
		/// <returns></returns>
		public static MiningModel loadFromXmlNode(XmlNode node)
		{
			MiningModel model = new MiningModel();
			
			/*tree.ModelName = node.Attributes["modelName"].Value;
			
			if (node.Attributes["missingValueStrategy"] != null)
				tree.MissingValueStrategy = MissingValueStrategyfromString(node.Attributes["missingValueStrategy"].Value);
			
			// By default noTrueChildStrategy = returnNullPrediction
			tree.noTrueChildStrategy = NoTrueChildStrategy.ReturnNullPrediction;
			if (node.Attributes["noTrueChildStrategy"] != null)
				tree.noTrueChildStrategy = NoTrueChildStrategyfromString(node.Attributes["noTrueChildStrategy"].Value);
			*/
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("Extension".Equals(item.Name))
				{
					// Not yet implemented
				}
				else if ("MiningSchema".Equals(item.Name))
				{
					//tree.Node = Node.loadFromXmlNode(item);
				}
				else if ("Segmentation".Equals(item.Name))
				{
					model.segmentation = Segmentation.loadFromXmlNode(item);
				}
				else
					throw new NotImplementedException();
			}
			
			return model;
		}
		
		private static MultipleModelMethod MissingValueStrategyfromString(string val)
		{
			switch (val.Trim().ToLowerInvariant())
			{
			case "selectall": 
				return MultipleModelMethod.SelectAll;
				
			default:
				throw new NotImplementedException();
			}
		}
		
		/// <summary>
		/// Scoring with Model
		/// </summary>
		/// <param name="dict">Values</param>
		/// <returns></returns>
		public override ScoreResult Score(Dictionary<string, object> dict)
		{
//			ScoreResult resStart = new ScoreResult("", null);
//			Node root = this.node;
//			resStart.Nodes.Add(root);
//			resStart.Value = root.Score;
			
			return this.Segmentation.Segments[0].Model.Score(dict);
		}
	}
}
