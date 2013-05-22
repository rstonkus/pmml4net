pmml4net
========

Pmml Library provide class to read and consume decision trees stored in PMML files.

Features
========

* Read/Load PMML file in 4.0/4.1 format.
* Easy evaluation of DecisionTree model
* (beta) Support for MiningModel with segmentation

example
-------

Load PMML file with TreeModel and execute it.

	// Load PMML file
	Pmml pmml = Pmml.loadModels(pFilePath);
			
	// Get the TreeModel by this name
	ModelElement model = pmml.getByName("golfing");
	
	// Load Input  ( parseParams="  var1=1, var2="foo" var3="bar" ...	
	Dictionary<string, object> lDict = parseParams(paramList);
	
	// Do scoring
	ScoreResult result = model.Score(lDict);


Licence
=======

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

