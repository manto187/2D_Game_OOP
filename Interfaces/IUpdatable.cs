using FirstDesktopApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDesktopApp.Interfaces
{
    public interface IUpdatable
    { // Method to update the object
        void Update(GameTime gameTime);
    }
}
