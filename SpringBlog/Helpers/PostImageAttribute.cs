using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace SpringBlog.Helpers
{
    public class PostImageAttribute:ValidationAttribute
    {
        public string AllowedExtensions { get; set; } = ".jpg, .jpeg, .png";
        public int MaxFileSizeMb { get; set; } = 3;
        public override bool IsValid(object value)
        {
            if (value == null || !(value is HttpPostedFileBase))
                return true;

            string[] allowedExtensions = AllowedExtensions.ToLower(CultureInfo.InvariantCulture).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var img = (HttpPostedFileBase)value;
            var ext = Path.GetExtension(img.FileName);

            if (!img.ContentType.StartsWith("image/") || !allowedExtensions.Contains(ext.ToLower(CultureInfo.InvariantCulture)))
            {
                ErrorMessage = "Accepted File Types: " + AllowedExtensions;
                return false;
            }
            else if (img.ContentLength > MaxFileSizeMb * 1024 * 1024)
            {
                ErrorMessage = $"Maximum Upload Size: {MaxFileSizeMb}MB";
                return false;
            }
            return true;
        }
    }
}