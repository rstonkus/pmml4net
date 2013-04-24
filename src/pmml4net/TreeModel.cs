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

namespace pmml4net
{
	/// <summary>
	/// Description of TreeModel.
	/// </summary>
	public class TreeModel
	{
		private String modelName;
		private MiningSchema miningSchema;
		
		/// <summary>
		/// Identifies the model with a unique name in the context of the PMML file.
		/// </summary>
		public String ModelName { get { return modelName; } set { modelName = value; } }
		
		/// <summary>
		/// Mining schema for this model.
		/// </summary>
		public MiningSchema MiningSchema { get { return miningSchema; } set { miningSchema = value; } }
		
		
		/// <summary>
		/// Scoring with Tree Model
		/// </summary>
		/// <param name="dict">Values</param>
		/// <returns></returns>
		public ScoreResult Score(Dictionary<string, object> dict)
		{
			return new ScoreResult("toto", "cool");
		}
	}
}
