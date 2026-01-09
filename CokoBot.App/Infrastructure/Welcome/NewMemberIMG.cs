using CokoBot.Core.Templates;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace CokoBot.App.Infrastructure.Welcome
{
    public class NewMemberIMG
    {
        public static readonly HttpClient httpClient = new HttpClient();
        private static async Task<Bitmap> GetUserAvatar(string avatarUrl)
        {
            Stream avatarStream = await httpClient.GetStreamAsync(avatarUrl);
            Bitmap avatarBitmap = new Bitmap(avatarStream);

            return avatarBitmap;
        }

        public static async Task CreateWelcomeMessage(DiscordChannel channel, string avatarUrl, string userName)
        {
            try
            {
                using Bitmap backgroundBitmap = Properties.Resources.BackGround;
                using Bitmap avatarBitmap = await GetUserAvatar(avatarUrl);

                int width = backgroundBitmap.Width;
                int height = backgroundBitmap.Height;

                using Bitmap finalImage = new(width, height);
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    g.Clear(Color.Transparent);
                    g.DrawImage(avatarBitmap, new Rectangle(395,25, 295, 295));
                    g.DrawImage(backgroundBitmap, new Rectangle(0, 0, width, height));
                }

                using MemoryStream ms = new();
                finalImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0; 

                var builder = new DiscordMessageBuilder()
                    .WithContent($"Welcome {userName}!")
                    .AddFile("card.png", ms);

                await channel.SendMessageAsync(builder);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando la imagen: {ex}");
            }
        }
    }
}
