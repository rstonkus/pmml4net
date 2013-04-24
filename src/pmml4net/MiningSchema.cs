/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 16:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace pmml4net
{
	/// <summary>
	/// The MiningSchema is the Gate Keeper for its model element. All data entering a model must pass through the MiningSchema. 
	/// Each model element contains one MiningSchema which lists fields as used in that model. While the MiningSchema contains 
	/// information that is specific to a certain model, the DataDictionary contains data definitions which do not vary per 
	/// model. The main purpose of the MiningSchema is to list the fields that have to be provided in order to apply the model.
	/// 
	/// MiningFields also define the usage of each field (active, supplementary, predicted, ...) as well as policies for treating 
	/// missing, invalid or outlier values.
	/// </summary>
	public class MiningSchema
	{
	}
}
