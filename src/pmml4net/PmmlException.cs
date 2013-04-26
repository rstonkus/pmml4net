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
using System.Runtime.Serialization;

namespace pmml4net
{
	/// <summary>
	/// 
	/// </summary>
	public class PmmlException : Exception, ISerializable
	{
		/// <summary>
		/// 
		/// </summary>
		public PmmlException()
		{
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
	 	public PmmlException(string message) : base(message)
		{
		}
		
	 	/// <summary>
	 	/// 
	 	/// </summary>
	 	/// <param name="message"></param>
	 	/// <param name="innerException"></param>
		public PmmlException(string message, Exception innerException) : base(message, innerException)
		{
		}
		
		/// <summary>
		/// This constructor is needed for serialization.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected PmmlException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
