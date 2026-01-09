using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.DailySong.Domain.Entities
{
    public class CokoSong
    {
        public int id { get; set; }
        public string songName {  get; set; }
        public string songType { get; set; }
        public string songURL { get; set; }
        public string userName { get; set; }
        public string userURL { get; set; }
        public bool isRecommended { get; set; }
    }
}
