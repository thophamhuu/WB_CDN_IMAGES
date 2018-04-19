using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreateThumbnail.DataAccess;

namespace CreateThumbnail.Service
{
    public class ProductThumbnail : ICreateThumbnail
    {
        private readonly IPictureService _pictureService;
        public ProductThumbnail()
        {
            _pictureService = new PictureService();
        }
        public void CreateThumbnails()
        {
            using (var ctx = new DataContext(Configs.Instance.Settings))
            {
                int quantity = 1000;
                var mediaSettings = ctx.Settings.SingleOrDefault(x => x.Name == "mediasettings.defaultimagequality");
                if (mediaSettings != null)
                    Int32.TryParse(mediaSettings.Value, out quantity);
                int[] sizes = { 0, 75, 100, 210, 415, 450, 550 };

                var pIds = (from p in ctx.Products.AsNoTracking()
                            join pim in ctx.Product_Picture_Mapping on p.Id equals pim.ProductId
                            where p.Published && !p.Deleted
                            orderby p.CreatedOnUtc descending
                            select pim.PictureId).ToList();
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
