using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateThumbnail.DataAccess;

namespace CreateThumbnail.Service
{
    public class CategoryThumbnail : ICreateThumbnail
    {
        private readonly IPictureService _pictureService;
        public CategoryThumbnail()
        {
            _pictureService = new PictureService();
        }
        public void CreateThumbnails()
        {
            using (var ctx = new DataContext(Configs.Instance.Settings))
            {
                int quantity = 100;
                var mediaSettings = ctx.Settings.SingleOrDefault(x => x.Name == "mediasettings.defaultimagequality");
                if (mediaSettings != null)
                    Int32.TryParse(mediaSettings.Value, out quantity);
                int[] sizes = { 0, 100, 290 };

                var pIds = (from c in ctx.Categories.AsNoTracking()
                            where c.Published && !c.Deleted
                            orderby c.CreatedOnUtc descending
                            select c.PictureId).ToList();
                int page = 1;
                int size = 100;
                List<Picture> pictures = null;
                List<int> filterIds = null;

                while ((filterIds = pIds.Skip(((page++) - 1) * size).Take(size).ToList()) != null && filterIds.Count > 0)
                {
                    pictures = ctx.Pictures.Where(x => filterIds.Contains(x.Id)).ToList();
                    if (pictures != null)
                    {
                        foreach (var picture in pictures)
                        {
                            Console.WriteLine("Being Create Thumbnail for picture: " + picture.Id.ToString("0000000"));
                            _pictureService.CreateThumbnail(picture, sizes,quantity);
                            try
                            {
                                ctx.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
