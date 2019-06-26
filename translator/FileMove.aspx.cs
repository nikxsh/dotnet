using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Translator
{
    public partial class FileMove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string dest = @"E:\Songs\MyMusic";
                string src =  @"E:\Songs\Music";
                foreach (var file in Directory.GetFiles(src, "*.*",SearchOption.AllDirectories))
                {
                    string filename = Path.GetFileName(file);
                    if (!File.Exists(Path.Combine(dest, filename)))
                    File.Move(file, Path.Combine(dest, filename));
                }

                lblMessage.Text = "Sucess";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed";
            }

        }
    }
}