using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace SpeedewValley
{
    public class ModEntry : Mod
    {
		public override void Entry( IModHelper helper )
        {
			helper.Events.GameLoop.DayStarted += GameLoop_DayStarted;
			helper.Events.Input.ButtonPressed += Input_ButtonPressed;
			helper.Events.Input.ButtonReleased += Input_ButtonReleased;
			helper.Events.GameLoop.UpdateTicking += GameLoop_UpdateTicking;
			helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
			helper.Events.Player.Warped += Player_Warped;
        }

		private void GameLoop_DayStarted( object sender, DayStartedEventArgs e )
		{
			map = new bool[ Game1.player.currentLocation.map.Layers[ 0 ].LayerWidth, Game1.player.currentLocation.map.Layers[ 0 ].LayerHeight ];
			for( int x = 0; x < Game1.player.currentLocation.map.Layers[ 0 ].LayerWidth; x++ )
			{
				for( int y = 0; y < Game1.player.currentLocation.map.Layers[ 0 ].LayerHeight; y++ )
				{
					map[ x, y ] = Game1.player.currentLocation.isCollidingPosition( new Rectangle( x * Game1.tileSize + 1, y * Game1.tileSize + 1, Game1.tileSize - 2, Game1.tileSize - 2 ), Game1.viewport, false, 0, false, Game1.player, true, false, false );
				}
			}
			var name = Game1.player.currentLocation.Name;
		}

		private void moveLeft( bool move = true )
		{
			if( move )
			{
				Input.SendKeyDown( Input.KeyCode.KEY_A );
			}
			else
			{
				Input.SendKeyUp( Input.KeyCode.KEY_A );
			}
		}

		private void moveRight( bool move = true )
		{
			if( move )
			{
				Input.SendKeyDown( Input.KeyCode.KEY_D );
			}
			else
			{
				Input.SendKeyUp( Input.KeyCode.KEY_D );
			}
		}

		private void moveUp( bool move = true )
		{
			if( move )
			{
				Input.SendKeyDown( Input.KeyCode.KEY_W );
			}
			else
			{
				Input.SendKeyUp( Input.KeyCode.KEY_W );
			}
		}

		private void moveDown( bool move = true )
		{
			if( move )
			{
				Input.SendKeyDown( Input.KeyCode.KEY_S );
			}
			else
			{
				Input.SendKeyUp( Input.KeyCode.KEY_S );
			}
		}

		private void Input_ButtonPressed( object sender, ButtonPressedEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
				this.Monitor.Log( Game1.player.Position.X + " " + Game1.player.Position.Y, LogLevel.Info );
				//if( e.Button == SButton.LeftShift )
				//{
				//	moveLeft( true );
				//}
			}
		}

		private void Input_ButtonReleased( object sender, ButtonReleasedEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
				//if( e.Button == SButton.LeftShift )
				//{
				//	moveLeft( false );
				//}
			}
		}

		bool[,] map = null;
		uint gameTickDiff = 0;
		private void GameLoop_UpdateTicking( object sender, UpdateTickingEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
				gameTickDiff = e.Ticks;
				if( Game1.player.currentLocation.Name == "FarmHouse" )
				{
					if( Game1.player.Position.X > 200 )
					{
						moveLeft( true );
					}
					else
					{
						moveLeft( false );
					}
					if( Game1.player.Position.Y < 730 )
					{
						moveDown( true );
					}
					else
					{
						moveDown( false );
					}
				}
				//var map = new bool[ Game1.player.currentLocation.map.Layers[ 0 ].LayerWidth, Game1.player.currentLocation.map.Layers[ 0 ].LayerHeight ];
				//for( int x = 0; x < Game1.player.currentLocation.map.Layers[ 0 ].LayerWidth; x++ )
				//{
				//	for( int y = 0; y < Game1.player.currentLocation.map.Layers[ 0 ].LayerHeight; y++ )
				//	{
				//		map[ x, y ] = Game1.player.currentLocation.isCollidingPosition( new Rectangle( x * Game1.tileSize + 1, y * Game1.tileSize + 1, Game1.tileSize - 2, Game1.tileSize - 2 ), Game1.viewport, false, 0, false, Game1.player, true, false, false );
				//	}
				//}
				this.Monitor.Log( Game1.player.Position.X + " " + Game1.player.Position.Y, LogLevel.Info );
			}
		}

		private void GameLoop_UpdateTicked( object sender, UpdateTickedEventArgs e )
		{
			if( Context.IsWorldReady && Context.CanPlayerMove )
			{
				uint timeDiff = e.Ticks - gameTickDiff;
			}
		}

		private void Player_Warped( object sender, WarpedEventArgs e )
		{
			this.Monitor.Log( "Player Warped " + e.NewLocation, LogLevel.Info );
			map = new bool[ Game1.player.currentLocation.map.Layers[ 0 ].LayerWidth, Game1.player.currentLocation.map.Layers[ 0 ].LayerHeight ];
			for( int x = 0; x < Game1.player.currentLocation.map.Layers[ 0 ].LayerWidth; x++ )
			{
				for( int y = 0; y < Game1.player.currentLocation.map.Layers[ 0 ].LayerHeight; y++ )
				{
					map[ x, y ] = Game1.player.currentLocation.isCollidingPosition( new Rectangle( x * Game1.tileSize + 1, y * Game1.tileSize + 1, Game1.tileSize - 2, Game1.tileSize - 2 ), Game1.viewport, false, 0, false, Game1.player, true, false, false );
				}
			}
		}
	}
}
