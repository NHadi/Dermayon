using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.SocialMedia.User.Framework.DTO
{
    public class AccountUserCreateRequest
    {

        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }        

        [StringLength(100)]
        public string Name { get; set; }
        
    }
}
