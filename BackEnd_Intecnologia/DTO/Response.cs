namespace BackEnd_Intecnologia.DTO
{
	public class Response
	{
		public bool Succeded => !Errors.Any();

		public long Identity { get; set; }
		public string StringCode { get; set; }
		public int Result { get; set; }

		public int Progress { get; set; }
		public Guid GuidReturn { get; set; }
		public List<string> Errors { get; set; } = new List<string>(0);
		public string? jwtToken { get; set; }
	}

	public class Response<T> : Response where T : class
	{
		public IEnumerable<T> DataList { get; set; }
		public T SingleData { get; set; }
	}
}
