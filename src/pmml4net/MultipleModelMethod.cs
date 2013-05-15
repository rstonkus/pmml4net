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

namespace pmml4net
{
	/// <summary>
	/// The model combination methods listed above are applicable as follows:
	/// selectFirst is applicable to any model type. Simply use the first model for which the predicate in the Segment evaluates to true.
	/// selectAll is applicable to any model type. All models for which the predicate in the Segment evaluates to true are evaluated. The Output element should be used to specify inclusion of a segment id in the evaluation results so as to match results with the associated model segment. The PMML standard does not specify a mechanism for returning more than one value per record scored. Different implementations may choose to implement returning multiple values for a single record differently.
	/// modelChain is applicable to any model type. During scoring, Segments whose Predicates evaluate to TRUE are executed in the order they appear in the PMML. The OutputFields from one model element can be passed as input to the MiningSchema of subsequent models. OutputFields from Segments whose Predicates evaluate to false are, by convention, to be treated by subsequent Segments as missing values. In this way, the MiningSchema's missing value handling features can be used to handle this missing input as appropriate. Since each Segment is executed in order, an OutputField must appear in a Segment before it can be used in the MiningSchema of a subsequent Segment. Loops are not possible. The results provided from a modelChain MiningModel are the results from the last Segment executed in the chain (i.e., the last Segment whose predicate evaluates to true).
	/// For clustering models only majorityVote, weightedMajorityVote, selectFirst, or selectAll can be used. In case of majorityVote the cluster ID that was selected by the largest number of models wins. For weightedMajorityVote the weights specified in Segment elements are used, and the cluster ID with highest total weight wins.
	/// For regression models only average, weightedAverage, median, sum, selectFirst, or selectAll are applicable. The first four methods are applied to the predicted values of all models for which the predicate evaluates to true.
	/// For classification models all the combination methods, except for sum, can be used. Note that average, weightedAverage, median, and max are applied to the predicted probabilities of target categories in each of the models used for the case, then the winning category is selected based on the highest combined probability, while majorityVote and weightedMajorityVote use the predicted categories from all applicable models and select the one based on the models' "votes".
	/// 
	///OutputFields contained at top level MiningModel element apply to the winning Segment selected by the multipleModelMethod attribute (selectFirst, selectAll, majorityVote, modelChain, etc.) and the RESULT-FEATURE entityId returns the id of the winning segment. OutputFields within Segments allow for results specific to that segment to be returned. Since the Segment id attribute is optional, if it is not specified, Segements are identified by an implicit 1-based index, indicating the position in which each segment appears in the model.
	/// Since identical OutputField elements can be duplicated across different segments, the OutputField that is used to return results is the OutputField that comes from the Segment selected by the multipleModelMethod attribute (selectFirst, selectAll, majorityVote, modelChain, etc.).
	/// 
	/// A MiningModel may contain Segments that also contain a MiningModel element. For example, the Model Composition approach allows Regression models to be selected using a DecisionTree. When the DecisionTree cannot or should not be implemented using Segment Predicates, the equivalent implementation using Segmentation would have a top-level MiningModel with two segments in a chain: The first Segment would implement the TreeModel and its result would be passed to the second Segment which contains a MiningModel which uses the TreeModel output to select one of it's Regression model Segments. This is the fifth example below, which shows how to pass the output of a Segment as an input to a Segment that contains a MiningModel.
	/// 
	/// It should be noted that a more efficient approach to implementing the Model Composition approach using Segmentation is shown in the sixth example, which does not require a MiningModel within a MiningModel.
	/// </summary>
	public enum MultipleModelMethod
	{
		/// <summary>
		/// For clustering models only majorityVote, weightedMajorityVote, selectFirst, or selectAll can be used. In case of majorityVote the cluster ID that was selected by the largest number of models wins. For weightedMajorityVote the weights specified in Segment elements are used, and the cluster ID with highest total weight wins.
		/// </summary>
		MajorityVote,
		
		/// <summary>
		/// For clustering models only majorityVote, weightedMajorityVote, selectFirst, or selectAll can be used. In case of majorityVote the cluster ID that was selected by the largest number of models wins. For weightedMajorityVote the weights specified in Segment elements are used, and the cluster ID with highest total weight wins.
		/// </summary>
		WeightedMajorityVote,
		
		/// <summary>
		/// For classification models all the combination methods, except for sum, can be used. Note that average, weightedAverage, median, and max are applied to the predicted probabilities of target categories in each of the models used for the case, then the winning category is selected based on the highest combined probability, while majorityVote and weightedMajorityVote use the predicted categories from all applicable models and select the one based on the models' "votes".
		/// </summary>
		Average,
		
		/// <summary>
		/// For classification models all the combination methods, except for sum, can be used. Note that average, weightedAverage, median, and max are applied to the predicted probabilities of target categories in each of the models used for the case, then the winning category is selected based on the highest combined probability, while majorityVote and weightedMajorityVote use the predicted categories from all applicable models and select the one based on the models' "votes".
		/// </summary>
		WeightedAverage,
		
		/// <summary>
		/// For classification models all the combination methods, except for sum, can be used. Note that average, weightedAverage, median, and max are applied to the predicted probabilities of target categories in each of the models used for the case, then the winning category is selected based on the highest combined probability, while majorityVote and weightedMajorityVote use the predicted categories from all applicable models and select the one based on the models' "votes".
		/// </summary>
		Median,
		
		/// <summary>
		/// For classification models all the combination methods, except for sum, can be used. Note that average, weightedAverage, median, and max are applied to the predicted probabilities of target categories in each of the models used for the case, then the winning category is selected based on the highest combined probability, while majorityVote and weightedMajorityVote use the predicted categories from all applicable models and select the one based on the models' "votes".
		/// </summary>
		Max,
		
		/// <summary>
		/// For classification models all the combination methods, except for sum, can be used. Note that average, weightedAverage, median, and max are applied to the predicted probabilities of target categories in each of the models used for the case, then the winning category is selected based on the highest combined probability, while majorityVote and weightedMajorityVote use the predicted categories from all applicable models and select the one based on the models' "votes".
		/// </summary>
		Sum,
		
		/// <summary>
		/// selectFirst is applicable to any model type. Simply use the first model for which the predicate in the Segment evaluates to true.
		/// </summary>
		SelectFirst,
		
		/// <summary>
		/// selectAll is applicable to any model type. All models for which the predicate in the Segment evaluates to true are evaluated.
		/// The Output element should be used to specify inclusion of a segment id in the evaluation results so as to match results with 
		/// the associated model segment. The PMML standard does not specify a mechanism for returning more than one value per record scored.
		/// Different implementations may choose to implement returning multiple values for a single record differently.
		/// </summary>
		SelectAll,
		
		/// <summary>
		/// modelChain is applicable to any model type. During scoring, Segments whose Predicates evaluate to TRUE are executed in the 
		/// order they appear in the PMML. The OutputFields from one model element can be passed as input to the MiningSchema of 
		/// subsequent models. OutputFields from Segments whose Predicates evaluate to false are, by convention, to be treated by 
		/// subsequent Segments as missing values. In this way, the MiningSchema's missing value handling features can be used to 
		/// handle this missing input as appropriate. Since each Segment is executed in order, an OutputField must appear in a Segment 
		/// before it can be used in the MiningSchema of a subsequent Segment. Loops are not possible. The results provided from a 
		/// modelChain MiningModel are the results from the last Segment executed in the chain (i.e., the last Segment whose predicate 
		/// evaluates to true).
		/// </summary>
		ModelChain
	}
}
