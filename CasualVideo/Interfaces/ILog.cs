using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasualVideo.Interfaces
{
    interface ILog
    {
        string pathLog { get; set; }
        void addAction(string format);
    }
}
