using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ragnarok.DataContextsMenu
{
    public static class  ApplicationMenuModel
    {

        #region Data

        private const string HelpFooterTitle = "Press F1 for more help.";
        private static object _lockObject = new object();
        private static Dictionary<string, ControlData> _dataCollection = new Dictionary<string, ControlData>();

        // Store any data that doesnt inherit from ControlData
        private static Dictionary<string, object> _miscData = new Dictionary<string, object>();

        #endregion Data

        public static ControlData Save
        {
            get
            {
                lock (_lockObject)
                {
                    string Str = "Add a Digital _Signature";

                    if (!_dataCollection.ContainsKey(Str))
                    {
                        string TooTipTitle = "Ensure the integrity of the document by adding an invisible signature.";

                        MenuItemData menuItemData = new MenuItemData()
                        {
                            Label = Str,
                            SmallImage = new Uri("/Ragnarok;Resources/Images/GenericDocument.png", UriKind.Relative),
                            ToolTipTitle = TooTipTitle,
                            Command = System.Windows.Input.ApplicationCommands.Save,
                            KeyTip = "S",
                            
                        };
                        _dataCollection[Str] = menuItemData;
                    }

                    return _dataCollection[Str];
                }
            }
        }
    }
}
