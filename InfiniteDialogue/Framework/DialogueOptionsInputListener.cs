using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Quests;
using SFarmer = StardewValley.Farmer;

namespace InfiniteDialogue.Framework
{
    internal class DialogueOptionsInputListener : OptionsElement
    {
        /*********
        ** Fields
        *********/
        /// <summary>The action to perform when the button is toggled (or <c>null</c> to handle it manually).</summary>
        private readonly Action OnToggled;

        /// <summary>The translated 'press new key' label.</summary>
        private readonly string PressNewKeyLabel;

        /// <summary>The source rectangle for the 'set' button sprite.</summary>
        private readonly Rectangle SetButtonSprite = new Rectangle(294, 428, 21, 11);

        private readonly List<string> ButtonNames = new List<string>();
        private string ListenerMessage;
        private bool Listening;
        private Rectangle SetButtonBounds;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="label">The field label.</param>
        /// <param name="slotWidth">The field width.</param>
        /// <param name="onToggled">The action to perform when the button is toggled.</param>
        public DialogueOptionsInputListener(string label, int slotWidth, Action onToggled)
            : base(label, -1, -1, slotWidth + 1, 11 * Game1.pixelZoom) {
            this.SetButtonBounds = new Rectangle(slotWidth - 28 * Game1.pixelZoom, -1 + Game1.pixelZoom * 3,
                21 * Game1.pixelZoom, 11 * Game1.pixelZoom);
            this.OnToggled = onToggled;
        }

        public override void receiveLeftClick(int x, int y) {
            if (this.greyedOut || this.Listening || !this.SetButtonBounds.Contains(x, y))
                return;

            // callback handler
            if (this.OnToggled != null) {
                this.OnToggled();
                return;
            }
        }

        public override void receiveKeyPress(Keys key) {
            if (this.greyedOut || !this.Listening)
                return;
            this.ButtonNames[0] = key.ToString();
            Game1.soundBank.PlayCue("coin");
            this.Listening = false;
            GameMenu.forcePreventClose = false;
        }

        public override void draw(SpriteBatch spriteBatch, int slotX, int slotY) {
            string lvl = "";
            SFarmer plr = Game1.player;
            if (lvl != "")
                Utility.drawTextWithShadow(spriteBatch, $"{this.label}: {lvl}", Game1.dialogueFont, new Vector2(this.bounds.X + slotX, this.bounds.Y + slotY), this.greyedOut ? Game1.textColor * 0.33f : Game1.textColor, 1f, 0.15f);
            else if (this.ButtonNames.Count == 0)
                Utility.drawTextWithShadow(spriteBatch, this.label, Game1.dialogueFont, new Vector2(this.bounds.X + slotX, this.bounds.Y + slotY), this.greyedOut ? Game1.textColor * 0.33f : Game1.textColor, 1f, 0.15f);
            else
                Utility.drawTextWithShadow(spriteBatch, this.label + ": " + this.ButtonNames.Last() + (this.ButtonNames.Count > 1 ? ", " + this.ButtonNames.First() : ""), Game1.dialogueFont, new Vector2(this.bounds.X + slotX, this.bounds.Y + slotY), this.greyedOut ? Game1.textColor * 0.33f : Game1.textColor, 1f, 0.15f);
            Utility.drawWithShadow(spriteBatch, Game1.mouseCursors, new Vector2(this.SetButtonBounds.X + slotX, this.SetButtonBounds.Y + slotY), this.SetButtonSprite, Color.White, 0.0f, Vector2.Zero, Game1.pixelZoom, false, 0.15f);
            if (!this.Listening)
                return;
            spriteBatch.Draw(Game1.staminaRect, new Rectangle(0, 0, Game1.graphics.GraphicsDevice.Viewport.Width, Game1.graphics.GraphicsDevice.Viewport.Height), new Rectangle(0, 0, 1, 1), Color.Black * 0.75f, 0.0f, Vector2.Zero, SpriteEffects.None, 0.999f);
            spriteBatch.DrawString(Game1.dialogueFont, this.ListenerMessage, Utility.getTopLeftPositionForCenteringOnScreen(Game1.tileSize * 3, Game1.tileSize), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9999f);
        }
    }
}