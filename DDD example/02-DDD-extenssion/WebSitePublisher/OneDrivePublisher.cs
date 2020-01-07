using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSitePublisher
{
    /// <summary>
    /// Clasa care implementeaza publicare de continut pe WEB
    /// </summary>
    public class OneDrivePublisher
    {
        public Uri PublishToOneDrive(string fisierContinut)
        {
            Console.WriteLine("Continutul a fost publicat: http://www.onedrive.com/continut.pdf");

            return new Uri("http://www.onedrive.com/continut.pdf");
        }
    }
}
