
namespace Vrtx
{
	public class Player : Component
	{
		public Connection? Connection { get; set; }
		public ulong SteamId { get; set; }

		
		protected override void OnStart()
		{
			Log.Info( "Player started" );
		}

		public void SetConnection(Connection connection)
		{
			Connection = connection;
			SteamId = connection.SteamId;
		}

		// public Player(){

		// }
		// public Player(GameObject playerObject, Connection connection)
		// {
		// 	Connection = connection;
		// 	SteamId = connection.SteamId;
		// 	PlayerObject = playerObject;
		// }
	}
}
