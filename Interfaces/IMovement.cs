using FirstDesktopApp.Core;
using FirstDesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDesktopApp.Interfaces
{
    public interface IMovement
    {
       public void Move(GameObject obj, GameTime gameTime);
    }
}
