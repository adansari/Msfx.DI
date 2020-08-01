using Msfx.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msfx.DI.Lab.Levels
{
    [Injectable]
    class Level5
    {
        public Level5(){ }

        [AutoInject]
        public Level2 _level2;

        private Level4 _level4ByCtor, _level4ByMethod;

        [AutoInject]
        public Level3 Level3 { get; set; }

        [AutoInject]
        public Level5(Level4 l4)
        {
            _level4ByCtor = l4;
        }

        [AutoInject]
        public void SetLevel4(Level4 l4)
        {
            _level4ByMethod = l4;
        }
    }

    [Injectable]
    class Level4
    {
        public Level3 Level3 { get; set; }
    }

    [Injectable]
    class Level3
    {

    }

    [Injectable]
    class Level2
    {

    }
}
