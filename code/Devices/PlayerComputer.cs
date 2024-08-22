namespace Vrtx.Devices
{
	public class PlayerComputer : Computer
	{
		private Player Player { get; set; }

		public PlayerComputer( Player player )
		{
			Player = player;
		}
	}
}

