namespace Common
{
	using System;

	public class ProgramException : Exception
	{
		public ProgramException(string msg) : base(msg) { }
	}
}