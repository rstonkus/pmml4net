# pmml4net

Copyright (C) 2013  Damien Carol <damien.carol@gmail.com>

## About

Pmml Library provide class to read and consume decision trees stored in PMML files.

## Features

* Read/Load PMML file in 4.0/4.1 format.
* Easy evaluation of DecisionTree model
* (beta) Support for MiningModel with segmentation

### example 1

Load PMML file with TreeModel and execute it.

	// Load PMML file
	Pmml pmml = Pmml.loadModels(pFilePath);
			
	// Get the TreeModel by this name
	ModelElement model = pmml.getByName("golfing");
	
	// Load Input  ( parseParams="  var1=1, var2="foo" var3="bar" ...	
	Dictionary<string, object> lDict = parseParams(paramList);
	
	// Do scoring
	ScoreResult result = model.Score(lDict);

### example 2

Load PMML file, ask for variable and execute it.

	public static void Main(string[] args)
	{
		// Load PMML
		Console.Write("Loading PMML [" + args[0] + "]...");
		Pmml pmml = Pmml.loadModels(args[0]);
		Console.WriteLine("OK");
		
		// Get model
		ModelElement model = pmml.Models[0];
		
		// Get vars
		Dictionary<string, object> dict = new Dictionary<string, object>();
		Console.WriteLine("Enter value for data fields.");
		foreach (MiningField field in model.MiningSchema.MiningFields)
		{
			if (field.UsageType == FieldUsageType.Active)
			{
				string displayName = field.Name;
				/*if (displayName == null)
					displayName = data.Name;*/
				
				Console.Write("Enter value for [" + displayName + "] : ");
				string val = Console.ReadLine();
				
				dict.Add(field.Name, val);
			}
		}
		
		// Execute the model
		Console.Write("Processing model [" + model.ModelName + "] ...");
		ScoreResult result = model.Score(dict);
		Console.WriteLine("OK");
		
		
		Console.WriteLine("RESULT ******** [" + result.Value + "][" + result.Confidence + "] ...");
		
		
		
		Console.Write("Press any key to continue . . . ");
		Console.ReadKey(true);
		
	}

## Licence

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

