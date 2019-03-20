using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteDialogue.Framework;

namespace InfiniteDialogue
{
    public class Menu : IClickableMenu
    {
        private readonly string hoverText = "";

        public List<ClickableComponent> optionsSlots = new List<ClickableComponent>();
        private List<OptionsElement> options = new List<OptionsElement>();

        Shit shit = new Shit();

        public Menu()
            : base(0, 0, 0, 0, true) {
            Game1.playSound("bigSelect");
            this.width = 832;
            this.height = 576;
            if (LocalizedContentManager.CurrentLanguageCode == LocalizedContentManager.LanguageCode.ko || LocalizedContentManager.CurrentLanguageCode == LocalizedContentManager.LanguageCode.fr)
                this.height += 64;
            Vector2 centeringOnScreen = Utility.getTopLeftPositionForCenteringOnScreen(this.width, this.height, 0, 0);
            this.xPositionOnScreen = (int)centeringOnScreen.X;
            this.yPositionOnScreen = (int)centeringOnScreen.Y + 32;
            this.upperRightCloseButton = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width - 20, this.yPositionOnScreen - 8, 48, 48), Game1.mouseCursors, new Rectangle(337, 494, 12, 12), 4f, false);

            for (int index = 0; index < 7; ++index)
                this.optionsSlots.Add(new ClickableComponent(new Rectangle(this.xPositionOnScreen + 16, this.yPositionOnScreen + 80 + 4 + index * ((height - 128) / 7), width - 32, (height - 128) / 7 + 4), string.Concat((object)index)) {
                    myID = index,
                    downNeighborID = index < 6 ? index + 1 : -7777,
                    upNeighborID = index > 0 ? index - 1 : -7777,
                    fullyImmutable = true
                });
            this.options.Add((new DialogueOptionsInputListener("yeet", optionsSlots[0].bounds.Width, () => shit.Yeet(true))));
            this.options.Add((new DialogueOptionsInputListener("yoot", optionsSlots[0].bounds.Width, () => shit.Yeet(false))));

            //this.options.Add((OptionsElement)new OptionsInputListener("Reload last dialogue", 1,
            //    optionsSlots[0].bounds.Width, -1, -1));
            //this.options.Add((OptionsElement)new OptionsInputListener("Load entire dialogue", 2,
            //    optionsSlots[0].bounds.Width, -1, -1));
        }
        public override void update(GameTime time) {
            base.update(time);
        }

        public override void draw(SpriteBatch b) {
            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);
            SpriteText.drawStringWithScrollCenteredAt(b, "Infinite Dialogue", this.xPositionOnScreen + this.width / 2, this.yPositionOnScreen - 64, "", 1f, -1, 0, 0.88f, false);
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, Color.White, 4f, true);
            base.draw(b);
            Game1.mouseCursorTransparency = 1f;
            this.drawMouse(b);
            if (this.hoverText.Length <= 0)
                return;
            IClickableMenu.drawHoverText(b, this.hoverText, Game1.dialogueFont, 0, 0, -1, (string)null, -1, (string[])null, (Item)null, 0, -1, -1, -1, -1, 1f, (CraftingRecipe)null);
        }
    }
}
