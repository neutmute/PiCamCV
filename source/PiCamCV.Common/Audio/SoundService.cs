using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PiCamCV.Common.Audio
{
    public interface ISoundService
    {
        void PlaySync(string fileResource);
        void PlayAsync(string fileResource);
    }

    public class SoundService : ISoundService
    {

        public void PlaySync(string fileResource)
        {
            var player = new SoundPlayer();
            player.SoundLocation = GetFileResource(fileResource);
            player.PlaySync();
        }

        public void PlayAsync(string fileResource)
        {
            var player = new SoundPlayer();
            player.SoundLocation = GetFileResource(fileResource);
            player.Play();
        }

        private string GetFileResource(string filename)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var relativePath = $"Resources{Path.DirectorySeparatorChar}Audio{Path.DirectorySeparatorChar}{filename}";
            var audioFilepath = Path.Combine(baseDir, relativePath);
            return audioFilepath;
        }
        

    }
}
