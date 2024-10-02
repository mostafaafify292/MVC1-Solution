using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC1.viewModels
{
    public class userViewModel
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public userViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
