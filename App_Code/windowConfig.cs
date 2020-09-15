using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


// a file to change the state of the window based on input.

namespace TTools.Windowsconfiguration
{
    public class windowStyle
    {
        public void opacitySettings(string elementName, params Grid[] GridName)
        {
            // need to take in the object that is currently having their opacity changed.
            // potential that it reflects another control also, like the login and register options.
            string[] newname = elementName.Split("Toggle");
            foreach (Grid item in GridName)
            {
                // MessageBox.Show($"Button: {newname[0]} item: {item.Name}");
                if ((newname[0] + "Grid") == item.Name)
                {
                    item.Visibility = Visibility.Visible;
                }
                else
                {
                    item.Visibility = Visibility.Collapsed;
                    // Visibility GridNameVisibility = (item.Visibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
                    // item.Visibility = GridNameVisibility;
                    // item.Visibility = Visibility.Collapsed;
                }
            }
        }
    }

    // class used to change the movement on a specific window.
    public class windowMovement
    {
        public void move()
        {

        }
    }
}