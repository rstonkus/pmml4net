﻿/*
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
using System.Collections.Specialized;
using System.Xml;
using System.Globalization;

namespace pmml4net
{
	/// <summary>
	/// Description of Node.
	/// </summary>
	public class RuleSet
	{
		private string id;
		private string score;
		private decimal recordCount;
		private string defaultChild;
		private string fdefaultscore;
		
		private IList<RuleSelectionMethod> ruleSelectionMethods;
		private Predicate predicate;
		private IList<ScoreDistribution> scoreDistributions = new List<ScoreDistribution>();
		private IList<Rule> rules = new List<Rule>();
		
		/// <summary>
		/// 
		/// </summary>
		public RuleSet(Predicate predicate)
		{
			//nodes = new List<Node>();
			this.predicate = predicate;
		}
		
		/// <summary>
		/// The value of id serves as a unique identifier for any given Node within the tree model.
		/// </summary>
		public string Id { get { return id; } set { id = value; } }
		
		/// <summary>
		/// score of this node
		/// </summary>
		public string Score { get { return score; } set { score = value; } }
		
		/// <summary>
		/// The value of recordCount in a Node serves as a base size for recordCount values in ScoreDistribution elements.
		/// These numbers do not necessarily determine the number of records which have been used to build/train the model.
		/// Nevertheless, they allow to determine the relative size of given values in a ScoreDistribution as well as the 
		/// relative size of a Node when compared to the parent Node.
		/// </summary>
		public decimal RecordCount { get { return recordCount; } set { recordCount = value; } }
		
		/// <summary>
		/// Only applicable when missingValueStrategy is set to defaultChild in the TreeModel element.
		/// Gives the id of the child node to use when no predicates can be evaluated due to missing values.
		/// Note that only Nodes which are immediate children of the respective Node can be referenced.
		/// </summary>
		public string DefaultChild { get { return defaultChild; } set { defaultChild = value; } }
		
		/// <summary>
		/// Rule selection method of this rule set
		/// </summary>
		public IList<RuleSelectionMethod> RuleSelectionMethods { get { return ruleSelectionMethods; } set { ruleSelectionMethods = value; } }
		
		/// <summary>
		/// Predicates of this node
		/// </summary>
		public Predicate Predicate { get { return predicate; } set { predicate = value; } }
		
		/// <summary>
		/// siblings of this node
		/// </summary>
		public IList<ScoreDistribution> ScoreDistributions { get { return scoreDistributions; } set { scoreDistributions = value; } }
		
		/// <summary>
		/// contains 0 or more rules which comprise the ruleset.
		/// </summary>
		public IList<Rule> Rules { get { return rules; } set { rules = value; } }
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static RuleSet loadFromXmlNode(XmlNode node)
		{
			RuleSet root = new RuleSet(new TruePredicate());
			
			if (node.Attributes["id"] != null)
				root.id = node.Attributes["id"].Value;
			
			if (node.Attributes["defaultScore"] != null)
				root.fdefaultscore = node.Attributes["defaultScore"].Value;
			
			if (node.Attributes["recordCount"] != null)
				root.recordCount = Convert.ToDecimal(node.Attributes["recordCount"].Value, CultureInfo.InvariantCulture);
			
			root.ruleSelectionMethods = new List<RuleSelectionMethod>();
			root.scoreDistributions = new List<ScoreDistribution>();
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					// TODO : implement extension
					//root.Nodes.Add(Node.loadFromXmlNode(item));
				}
				else if ("ruleselectionmethod".Equals(item.Name.ToLowerInvariant()))
				{
					root.RuleSelectionMethods.Add(RuleSelectionMethod.loadFromXmlNode(item));
				}
				else if ("scoredistribution".Equals(item.Name.ToLowerInvariant()))
				{
					root.ScoreDistributions.Add(ScoreDistribution.loadFromXmlNode(item));
				}
				else if ("simplerule".Equals(item.Name.ToLowerInvariant()))
				{
					root.Rules.Add(SimpleRule.loadFromXmlNode(item));
				}
				else
					throw new NotImplementedException();
			}
			
			return root;
		}
		
		/// <summary>
		/// Scoring with rule set model
		/// </summary>
		/// <param name="root">Parent node</param>
		/// <param name="criterionStr">Criterion</param>
		/// <param name="dict">Values</param>
		/// <param name="res" >Result to return</param>
		/// <returns></returns>
		public static ScoreResult Evaluate(RuleSet root, String criterionStr, 
		                            Dictionary<string, object> dict, ScoreResult res)
		{
			// HACK
			res.Value = root.fdefaultscore;
			res.Confidence = 1.0M;
			
			
			// Both the ordering of keys and values is significant
			OrderedDictionary/*<String, SimpleRule>*/ firedRules = new OrderedDictionary();//<String, SimpleRule>
			
			// Test childs
			foreach(Rule rule in root.Rules)
			{
				collectFiredRules(firedRules, rule, dict);
				
				if (firedRules.Count > 0)
					foreach (String key in firedRules.Keys) {
						res.Value = key;
						res.Confidence = 1.0M; //((SimpleRule)firedRules(key))..
						return res;
					}
			}
			
			// For now we return the first
			/*foreach (String key in firedRules.Keys) {
				res.Value = key;
				res.Confidence = 1.0M; //((SimpleRule)firedRules(key))..
				return res;
			}*/
			
			// Test childs
			/*foreach(Rule rule in root.Rules)
			{
				PredicateResult resRule = rule.Evaluate(dict);
				if (resRule == PredicateResult.True)
				{
					//res.Nodes.Add(child);
					res.Value = rule.Score;
					foreach(ScoreDistribution sco in rule.ScoreDistributions)
					{
						if (sco.Value.Equals(rule.Score))
						{
							if (sco.Confidence != null)
								res.Confidence = Decimal.Parse(sco.Confidence, NumberStyles.Any, CultureInfo.InvariantCulture);
						}
					}
					
					return Evaluate(child, missingvalueStr, noTrueChildStr, dict, res);
				}
				if (resRule == PredicateResult.Unknown)
					throw new NotImplementedException();*/
				
				/*else if (childPredicate == PredicateResult.Unknown)
				{
					// Unknow value lead to act with missingvalueStr
					switch(missingvalueStr)
					{
						case MissingValueStrategy.LastPrediction:
							return res;
							
						case MissingValueStrategy.NullPrediction:
							res.Value = null;
							return res;
							
						case MissingValueStrategy.WeightedConfidence:
							Dictionary<string, decimal> conf = CalculateConfidence(root, dict);
							string max_conf = null;
							foreach(string key in conf.Keys)
							{
								if (max_conf == null)
									max_conf = key;
								
								if (conf[key] > conf[max_conf])
									max_conf = key;
							}
							res.Value = max_conf;
							res.Confidence = conf[max_conf];
							return res;
							
						case MissingValueStrategy.AggregateNodes:
							return res;
							
						default:
							throw new NotImplementedException();
					}
				}
			}*/
			
			// All child nodes are false
			/*if (root.Nodes.Count > 0)
				if (noTrueChildStr == NoTrueChildStrategy.ReturnNullPrediction)
				{
					res.Value = null;
				}*/
			return res;
		}
		
		private static Dictionary<String, decimal> CalculateConfidence(Node node, Dictionary<string, object> dict)
		{
			Dictionary<String, decimal> ret = new Dictionary<string, decimal>();
			
			// Test childs
			foreach(Node child in node.Nodes)
			{
				PredicateResult childPredicate = child.Predicate.Evaluate(dict);
				if (childPredicate != PredicateResult.False)
				{
					foreach(ScoreDistribution sd in child.ScoreDistributions)
					{
						if (!ret.ContainsKey(sd.Value))
							ret.Add(sd.Value, 0);
						
						decimal new_val = (Convert.ToDecimal(sd.RecordCount)/child.RecordCount) * (child.RecordCount / node.RecordCount);
						ret[sd.Value] = ret[sd.Value] + new_val;
					}
				}
			}
			
			return ret;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("Node");
			
			writer.WriteAttributeString("id", this.Id);
			writer.WriteAttributeString("score", this.Score);
			writer.WriteAttributeString("recordCount", this.RecordCount.ToString());
			if (!string.IsNullOrEmpty(this.defaultChild))
				writer.WriteAttributeString("defaultChild", this.defaultChild);
			
			// Save predicate
			this.Predicate.save(writer);
			
			// FIXME : Add all elements in xml
			
			// Save score distribution
			foreach(ScoreDistribution scoreDistribution in this.ScoreDistributions)
				scoreDistribution.save(writer);
			
			// Save nodes
			/*foreach(Node node in this.Nodes)
				node.save(writer);
			
			writer.WriteEndElement();*/
			throw new NotImplementedException();
		}
		
		static
		private void collectFiredRules(OrderedDictionary/*<String, SimpleRule>*/ firedRules, Rule rule, Dictionary<string, object> dict/*, EvaluationContext context*/)
		{
			/*Predicate predicate = rule.getPredicate();
			if(predicate == null){
				throw new InvalidFeatureException(rule);
			}

                Boolean status = PredicateUtil.evaluate(predicate, context);
                if(status == null || !status.booleanValue()){
                        return;
                } // End if*/
			PredicateResult status = rule.Evaluate(dict);
			if (status == PredicateResult.Unknown || status == PredicateResult.False) {
				return;
			}
			
			if (rule is SimpleRule) {
				SimpleRule simpleRule = (SimpleRule)rule;
				
				firedRules.Add(simpleRule.Score, simpleRule);
			} else {
				if (rule is CompoundRule) 
				{
					CompoundRule compoundRule = (CompoundRule)rule;
					foreach(Rule childRule in compoundRule.Rules){
						collectFiredRules(firedRules, childRule, dict);
					}
				} else {
					throw new PmmlException("Type of Rule not supported");
				}
			}
        }
	}
}
