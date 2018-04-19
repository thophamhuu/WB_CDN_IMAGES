using CreateThumbnail.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateThumbnail.Service
{
    public interface IPictureService
    {
        void CreateThumbnail(Picture picture, int[] sizes,int quantity);
    }
}
