﻿using Luftschiff.Code.Game;
using Luftschiff.Code.Game.AreavRooms;
using Luftschiff.Code.States.Fights;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code
{
    /// <summary>
    ///     A standard globals class for things like enums or textures that need
    ///     to be accessible from everywhere.
    /// </summary>
    internal static class Globals
    {
        /// <summary>
        ///     Contains possible gamestates
        /// </summary>
        public enum EStates
        {
            menu,
            graphicstest,
            openworld,
            batfight,
            dragonfight,
            petuniefight,
            whalefight
        }

        /// <summary>
        ///     current standard font for use in dialogs
        /// </summary>
        public static Font DialogFont;

        /// <summary>
        ///     Standard notification font
        /// </summary>
        public static Font NotificationFont;

        /// <summary>
        ///     The standard height for every dialog button
        /// </summary>
        public static int TWO_BUTTON_DIALOG_BUTTON_HEIGHT = 40;

        /// <summary>
        ///     the standard color a dialog button uses when not hovered over
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_NORMAL = Color.White;

        /// <summary>
        ///     standard color for hover
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_HOVER = Color.Yellow;

        /// <summary>
        ///     when the button needs attention - originally for turn button
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_ATTENTIONSEEKER = Color.Red;

        /// <summary>
        ///     when the button is disabled
        /// </summary>
        public static Color DIALOG_BUTTON_COLOR_DISABLED = new Color(200, 200, 200);

        /// <summary>
        ///     standard color for the ship healthbar
        /// </summary>
        public static Color HEALTH_BAR_COLOR_SHIP = Color.Green;

        /// <summary>
        ///     standard color for the monster healthbar
        /// </summary>
        public static Color HEALTH_BAR_COLOR_MONSTER = Color.Blue;

        public static Color HEALTH_BAR_COLOR_ATTENTION = Color.Red;
        //globally used font sizes for text
        public static int FONT_SIZE_STANDARD = 20;
        public static int FONT_SIZE_HEADER = 40;
        public static int FONT_SIZE_SUBTEXT = 15;
        public static Time FRAME_TIME;

        /// <summary>
        ///     Texture for the background
        /// </summary>
        public static Texture BackgroundTexture;

        public static Texture DragonTexture;
        public static Texture SkywhaleTexture;
        public static Texture ShipTexture;
        public static Texture WhaleHornTexture;
        public static Texture BatTexture;
        public static Texture PetunienTexture;
        //special room stuff sprites
        public static Texture EngineTexture;
        public static Texture GunTexture;
        public static Texture HospitalTexture;
        //tile sprites
        public static Texture TileFloor;
        public static Texture TileWall;
        public static Texture TileDoor;
        public static Texture TileSpecial;
        public static Texture TileMetall;
        public static Texture TileElWall;
        //cursor sprites
        public static Texture CursorStandard;
        public static Texture CursorCrewMove;
        public static Texture CursorAim;
        //projectile textures
        public static Texture CannonBallTexture;
        public static Texture FireBallTexture;
        public static Texture ClawTexture;
        //projectile impact fx
        public static Texture RoomFireTexture;
        public static Texture Cannon_Explosion;
        //crew texture
        public static Texture CrewTexture;
        //global references
        public static FightState GameReference;
        public static TurnHandler TurnHandler;
        public static Area AreaReference;
        public static Collider ColliderReference;
        //sound fx
        public static SoundBuffer ClickSound;
        public static SoundBuffer CannonSound;
        public static SoundBuffer BoomSound;
        public static SoundBuffer FireSound;
        public static SoundBuffer FireCrackleSound;
    }
}