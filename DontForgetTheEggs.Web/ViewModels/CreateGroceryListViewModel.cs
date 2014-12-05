using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DontForgetTheEggs.Web.ViewModels
{
    public class CreateGroceryListViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}