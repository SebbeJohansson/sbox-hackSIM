
using Vrtx.Devices;

namespace Vrtx
{
	public class Player : Component
	{
		public Connection? Connection { get; set; }
		public ulong SteamId { get; set; }

		protected override void OnStart()
		{
			GameNetworkController controller = GameNetworkController.Instance;
			if ( controller == null ) {
				Log.Error( "Game Controller component not found, this shouldnt happen. Preventing player to be added." );
				GameObject.Name = $"{GameObject.Name}(Error: No GameController)";
				return;
			}
			Log.Info( "Player started" );
			controller.AddPlayer( this, GameObject.Network.OwnerConnection );
			// var devices = Game.ActiveScene.Components.GetAll<BaseDevice>().ToList();
			// devices.ForEach(device => Log.Info($"Device: {device.Name}@{device.Ip}"));
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
