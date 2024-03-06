namespace HFilms.Models
{
	public class SaveSeason
	{
		public int Id { get; set; }

		public string PrimaryTitle { get; set; }

		public string UserId { get; set; }
		public AppUser User { get; set; }

		public string FilmsID { get; set; }

		public string Season { get; set; }

		public string Seriya { get; set; }
	}
}
