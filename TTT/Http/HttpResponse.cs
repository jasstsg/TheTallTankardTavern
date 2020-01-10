using System.Text;

namespace TTT.Http
{
	public class HttpResponse
	{
		private readonly StringBuilder _Response = new StringBuilder();

		public string Response
		{
			get
			{
				return _Response.ToString();
			}
			set
			{
				_Response.Clear();
				_Response.Append(value);
			}
		}
	}
}
