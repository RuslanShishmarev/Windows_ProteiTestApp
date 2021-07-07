using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteiTestApp.Models
{
    public interface IStopwatcher
    {
        void Start(int speed = 1);
        void Stop();
        void Reset();
    }
}
