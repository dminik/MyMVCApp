namespace WebSite.BLL
{
	public class DebugInfo
	{
		public static bool IsDebug
		{
			get
			{
#if DEBUG
				return true;
#else
				return false;
#endif
			}	
		}
	}
}