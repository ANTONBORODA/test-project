using System;
using System.Linq;
using System.Threading.Tasks;
using Model.Options;
using UnityEngine;

namespace Providers.Previews
{
    public class LocalPreviewProvider : MonoBehaviour, IPreviewProvider
    {
        public Texture2D ErrorTexture;
        public TextureMap[] TextureMaps;


        public async Task<Texture2D> GetPreview(Option option)
        {
            var map = TextureMaps.FirstOrDefault(t => t.OptionCode == option.Code);
            if (map == null)
                return ErrorTexture;
            return map.Preview;
        }

        public void DisposeResource(Texture2D texture2D)
        {
        }
        
        [Serializable]
        public class TextureMap
        {
            public string OptionCode;
            public Texture2D Preview;
        }
    }
}