using System.Threading.Tasks;
using Model.Options;
using UnityEngine;

namespace Providers.Previews
{
    public interface IPreviewProvider
    {
        Task<Texture2D> GetPreview(Option option);
        void DisposeResource(Texture2D texture2D);
    }
}