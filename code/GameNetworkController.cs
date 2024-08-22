

namespace Vrtx
{
	public class GameNetworkController : Component, Component.INetworkListener
	{
		private static GameNetworkController? _instance = null;

		[Property]
		public required GameObject PlayerPrefab { get; set; }
		public static GameNetworkController Instance => _instance!;

		[HostSync]
		public NetDictionary<Guid, Player> Players { get; set; } = new NetDictionary<Guid, Player>();

		public GameNetworkController()
		{
			if ( _instance != null )
			{ 
				Log.Warning( "Only one instance of GameController is allowed." );
			}
			_instance = this;
		}

		protected override void OnStart()
		{
		}

		/// <summary>
		/// Looks for existing players, and reuses if one exists taht is applicable.
		/// </summary>
		/// <param name="connection"></param>
		/// <returns>Hopefully, a player controller object</returns>
		private Player? GetOrCreatePlayer( Connection connection )
		{
			IEnumerable<Player> players = Scene.GetAllComponents<Player>();
			
			Player? foundPlayer = players.FirstOrDefault( p => p.SteamId == connection.SteamId );

			if ( foundPlayer.IsValid() )
			{
				Log.Warning( $"Found existing player state for {connection.SteamId} that we can re-use." );
				return foundPlayer;
			}

			// var gameobject = new GameObject();
			// gameobject.NetworkSpawn();
			// GameObject Prop = PropPrefab.Clone();
			// prop.BreakFromPrefab();
			// NetworkSpawn() 

			
			try {
				GameObject player = PlayerPrefab.Clone();
				player.BreakFromPrefab();
				player.NetworkSpawn();
				player.Name = $"Player ({connection.DisplayName})";
				player.Network.SetOrphanedMode( NetworkOrphaned.ClearOwner );

				if (!player.IsValid() || !player.Components.Get<Player>().IsValid())
				{
					throw new Exception( "Failed to create player object, spawning empty broken one. This should NEVER happen." );
				}

				return player.Components.Get<Player>();
			}
			catch ( Exception e )
			{
				Log.Error( e );
				return null;
			}
		}

		/// <summary>
		/// Called when a network connection becomes active
		/// </summary>
		/// <param name="connection"></param>
		public void OnActive( Connection connection )
		{
			Log.Info( $"Player '{connection.DisplayName}' is becoming active" );

			// var playerState = GetOrCreatePlayerState( channel );
			// if ( !playerState.IsValid() )
			// {
			// 	throw new Exception( $"Something went wrong when trying to create PlayerState for {channel.DisplayName}" );
			// }

			// OnPlayerJoined( playerState, channel );
		}


		public void AddPlayer( Connection connection )
		{
			Log.Info( $"Adding player: {connection.Id} {connection.DisplayName}" );
			try
			{

				Player? player = GetOrCreatePlayer( connection );
				if (!player.IsValid() || player == null)
				{
					Log.Error( $"Failed to create player object for player {connection.DisplayName}. Again... this should never happen." );
					return;
				}

				Players.Add( connection.Id, player! );
			}
			catch ( Exception e )
			{
				Log.Warning( e );
			}
		}
	}
}
