namespace Talabat.core.Specifications
{
	public class ProudctSpecParams
	{
		private const int maxPageSize = 5;

		private int pageSize =5;

		public int  PageSize
		{
			get { return pageSize; }
			set { pageSize = value > maxPageSize ? maxPageSize : value; }
		}

		public int PageIndex { get; set; } = 1;

		public string? sort { get; set; }
		public int? brandId { get; set; }
		public int? typeId { get; set; }

		private string? Search;

		public string? search
		{
			get { return Search; }
			set { Search = value.ToLower(); }
		}






	}
}
